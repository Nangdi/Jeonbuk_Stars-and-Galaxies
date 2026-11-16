using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSettingData
{
    public bool useUnityOnTop;
}

public class GameDynamicData
{
}

[Serializable]
public class PortConfig
{
    public int controllerId;
    public string com;
    public int baudLate;
    // RS232 사용 여부. false면 해당 컨트롤러는 포트를 열지 않는다.
    public bool enabled = true;
}

[Serializable]
public class PortJson
{
    // 컨트롤러 1~5 각각 전용 포트
    public List<PortConfig> ports = new List<PortConfig>
    {
        new PortConfig { controllerId = 1, com = "COM4", baudLate = 19200, enabled = true },
        new PortConfig { controllerId = 2, com = "COM5", baudLate = 19200, enabled = true },
        new PortConfig { controllerId = 3, com = "COM6", baudLate = 19200, enabled = true },
        new PortConfig { controllerId = 4, com = "COM7", baudLate = 19200, enabled = true },
        new PortConfig { controllerId = 5, com = "COM8", baudLate = 19200, enabled = true },
    };
}

[Serializable]
public class TcpJson
{
    // 메인 서버(EcoLolong_ServerEarthSystem 등) 접속 주소와 포트
    public string host = "127.0.0.1";
    public int port = 5000;
    // 끊겼을 때 재시도 간격(초). 0 이하이면 재연결 시도하지 않음.
    public float reconnectIntervalSeconds = 3f;
    // 송신 메시지 끝에 줄바꿈을 자동으로 붙일지 여부
    public bool appendOutgoingLineEnding = true;
}

public class JsonManager : MonoBehaviour
{
    public static JsonManager instance;
    public GameSettingData gameSettingData = new GameSettingData();
    public PortJson portJson = new PortJson();
    public TcpJson tcpJson = new TcpJson();
    public GameDynamicData gameDynamicData = new GameDynamicData();

    private string gameDataPath;
    private string gameDynamicDataPath;
    private string portPath;
    private string tcpPath;

    public string GameDataPath => gameDataPath;

    // 싱글톤을 초기화하고 JSON 파일에서 런타임 설정 데이터를 불러옵니다.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 운영 설정은 persistentDataPath 에서 읽고 쓴다. 없으면 StreamingAssets 기본값을 복사해 온다 (ConfigPath).
        portPath = ConfigPath.Get("port.json");
        tcpPath = ConfigPath.Get("tcp.json");
        gameDynamicDataPath = ConfigPath.Get("Setting.json");
        gameDataPath = Path.Combine(Application.persistentDataPath, "gameSettingData.json");

        gameSettingData ??= new GameSettingData();
        gameDynamicData ??= new GameDynamicData();
        portJson ??= new PortJson();
        tcpJson ??= new TcpJson();

        gameSettingData = LoadData(gameDataPath, gameSettingData);
        gameDynamicData = LoadData(gameDynamicDataPath, gameDynamicData);
        portJson = LoadData(portPath, portJson);
        tcpJson = LoadData(tcpPath, tcpJson);
    }

    // 현재 게임 설정 데이터를 gameSettingData.json 파일에 저장합니다.
    public void SaveGameSettingData()
    {
        SaveData(gameSettingData, gameDataPath);
    }

    // 지정한 경로에 JSON 파일을 생성하거나 덮어씁니다.
    public static void SaveData<T>(T jsonObject, string path) where T : new()
    {
        if (jsonObject == null)
            jsonObject = new T();

        string directoryPath = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        string json = JsonUtility.ToJson(jsonObject, true);
        File.WriteAllText(path, json);
        Debug.Log($"Saved JSON: {path}");
    }

    // 비교용: 공백·줄바꿈 차이는 무시한다 (값이 같으면 다시 저장하지 않음)
    private static string NormalizeJson(string json)
    {
        if (string.IsNullOrEmpty(json)) return "";
        var sb = new System.Text.StringBuilder(json.Length);
        bool inString = false;
        for (int i = 0; i < json.Length; i++)
        {
            char c = json[i];
            if (c == '"' && (i == 0 || json[i - 1] != '\\')) inString = !inString;
            if (!inString && char.IsWhiteSpace(c)) continue;
            sb.Append(c);
        }
        return sb.ToString();
    }

    // JSON 파일을 읽고, 파일이 없으면 기본값으로 새 파일을 만듭니다.
    // FromJsonOverwrite를 사용해 json에 없는 필드는 인스턴스 기본값을 유지한다.
    // (예: 기존 port.json에 enabled 필드가 없어도 PortConfig.enabled = true 가 보존됨)
    public static T LoadData<T>(string path, T data) where T : new()
    {
        if (data == null) data = new T();

        if (!File.Exists(path))
        {
            Debug.LogWarning($"JSON file does not exist. Creating a new file: {path}");
            SaveData(data, path);
            return data;
        }

        Debug.Log($"Loaded JSON: {path}");
        string json;
        try
        {
            json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, data);
        }
        catch (Exception e)
        {
            // 깨진 파일은 덮어쓰지 않고 기본값으로 실행한다 (운영자가 고칠 수 있게)
            Debug.LogError($"JSON 읽기 실패, 기본값 사용: {path} ({e.Message})");
            return data;
        }

        // 새 버전에서 추가된 항목이 파일에 없으면 기본값으로 채워 다시 저장한다 (기존 값은 유지)
        string merged = JsonUtility.ToJson(data, true);
        if (NormalizeJson(merged) != NormalizeJson(json))
        {
            try
            {
                File.WriteAllText(path, merged);
                Debug.Log($"JSON 새 항목 반영 저장: {path}");
            }
            catch (Exception e)
            {
                Debug.LogError($"JSON 저장 실패: {path} ({e.Message})");
            }
        }
        return data;
    }
}
