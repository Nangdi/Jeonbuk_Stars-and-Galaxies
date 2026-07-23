using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Formatting = Newtonsoft.Json.Formatting;

/// <summary>
/// GameSettingsPanel 전용 커스텀 인스펙터.
/// 실행 중 OnGUI 패널을 열지 않고도, 에디터 Inspector에서
/// MissionText.json 값을 직접 수정하고 저장할 수 있다.
///  - 에디트 모드: StreamingAssets/MissionText.json 파일을 직접 읽고/쓴다.
///  - 플레이 모드: 로더의 jsonLoadData를 편집하고 SaveAndApply()로 실시간 반영.
/// </summary>
[CustomEditor(typeof(GameSettingsPanel))]
public class GameSettingsPanelEditor : Editor
{
    private MissionDataLoader.JsonLoadData data;
    private string statusMessage = "";
    private MessageType statusType = MessageType.Info;
    private bool loaded = false;

    private string JsonPath
    {
        get { return Path.Combine(Application.streamingAssetsPath, "MissionText.json"); }
    }

    public override void OnInspectorGUI()
    {
        var panel = (GameSettingsPanel)target;

        // 컴포넌트 원본 필드(로더 참조, 토글 키)는 그대로 노출
        DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("게임 설정 직접 편집", EditorStyles.boldLabel);

        if (Application.isPlaying)
        {
            DrawPlayMode(panel);
        }
        else
        {
            DrawEditMode();
        }

        if (!string.IsNullOrEmpty(statusMessage))
        {
            EditorGUILayout.Space();
            EditorGUILayout.HelpBox(statusMessage, statusType);
        }
    }

    // ─────────────────────────── 에디트 모드 ───────────────────────────
    private void DrawEditMode()
    {
        EditorGUILayout.HelpBox("StreamingAssets/MissionText.json 파일을 직접 편집합니다.", MessageType.None);

        if (!loaded)
        {
            if (GUILayout.Button("설정 불러오기", GUILayout.Height(28)))
                LoadFromFile();
            return;
        }

        DrawFields();

        EditorGUILayout.Space();
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("저장 & 적용", GUILayout.Height(30)))
                SaveToFile();
            if (GUILayout.Button("다시 불러오기", GUILayout.Height(30)))
                LoadFromFile();
        }
    }

    private void LoadFromFile()
    {
        try
        {
            if (!File.Exists(JsonPath))
            {
                data = null;
                loaded = false;
                SetStatus("파일이 없습니다: " + JsonPath, MessageType.Warning);
                return;
            }

            string json = File.ReadAllText(JsonPath);
            data = JsonConvert.DeserializeObject<MissionDataLoader.JsonLoadData>(json);
            EnsureData();
            loaded = true;
            SetStatus("불러옴: " + JsonPath, MessageType.Info);
        }
        catch (System.Exception e)
        {
            loaded = false;
            SetStatus("불러오기 오류: " + e.Message, MessageType.Error);
        }
    }

    private void SaveToFile()
    {
        try
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(JsonPath, json);
            AssetDatabase.Refresh();
            SetStatus("저장 완료: " + JsonPath, MessageType.Info);
        }
        catch (System.Exception e)
        {
            SetStatus("저장 오류: " + e.Message, MessageType.Error);
        }
    }

    // ─────────────────────────── 플레이 모드 ───────────────────────────
    private void DrawPlayMode(GameSettingsPanel panel)
    {
        var loader = panel.missionDataLoader;
        if (loader == null)
            loader = Object.FindObjectOfType<MissionDataLoader>();

        if (loader == null || loader.jsonLoadData == null)
        {
            EditorGUILayout.HelpBox("jsonLoadData가 아직 로드되지 않았습니다.", MessageType.Warning);
            return;
        }

        data = loader.jsonLoadData;
        EnsureData();
        DrawFields();

        EditorGUILayout.Space();
        if (GUILayout.Button("저장 & 실시간 적용", GUILayout.Height(30)))
        {
            try
            {
                loader.SaveAndApply();
                SetStatus("저장 & 적용 완료: " + loader.MissionTextPath, MessageType.Info);
            }
            catch (System.Exception e)
            {
                SetStatus("저장 오류: " + e.Message, MessageType.Error);
            }
        }
    }

    // ─────────────────────────── 필드 그리기 ───────────────────────────
    private void DrawFields()
    {
        if (data == null) return;

        var t = data.timeSetting;
        EditorGUILayout.LabelField("── 타이머 (초) ──", EditorStyles.boldLabel);
        t.connectionTime = EditorGUILayout.IntField("접속 대기시간", t.connectionTime);
        t.practiceTime = EditorGUILayout.IntField("연습 시간", t.practiceTime);
        t.mission1Time = EditorGUILayout.IntField("미션1 시간", t.mission1Time);
        t.mission1EndTimeDelay = EditorGUILayout.IntField("미션1 종료 딜레이", t.mission1EndTimeDelay);
        t.mission2Time = EditorGUILayout.IntField("미션2 시간", t.mission2Time);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("── 정답 범위 (예: 5  또는  1,4) ──", EditorStyles.boldLabel);
        if (data.missionDataList != null && data.missionDataList.Count > 0)
            data.missionDataList[0].rightAnswerRange = EditorGUILayout.TextField("미션1 정답범위", data.missionDataList[0].rightAnswerRange);
        if (data.missionDataList != null && data.missionDataList.Count > 1)
            data.missionDataList[1].rightAnswerRange = EditorGUILayout.TextField("미션2 정답범위", data.missionDataList[1].rightAnswerRange);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("── 점수 · 미션1 ──", EditorStyles.boldLabel);
        var m1 = data.mission1ScoreSetting;
        m1.rightAnswerScore = EditorGUILayout.IntField("정답 점수", m1.rightAnswerScore);
        m1.mustScore = EditorGUILayout.IntField("기본 점수", m1.mustScore);
        m1.applyWrongScore = EditorGUILayout.Toggle("오답 감점 적용", m1.applyWrongScore);

        EditorGUILayout.LabelField("── 점수 · 미션2 ──", EditorStyles.boldLabel);
        var m2 = data.mission2ScoreSetting;
        m2.rightAnswerScore = EditorGUILayout.IntField("정답 점수", m2.rightAnswerScore);
        m2.mustScore = EditorGUILayout.IntField("기본 점수", m2.mustScore);
        m2.applyWrongScore = EditorGUILayout.Toggle("오답 감점 적용", m2.applyWrongScore);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("── 연습모드 회전 점수 ──", EditorStyles.boldLabel);
        var p = data.practiceScoreSetting;
        p.littleRotScore = EditorGUILayout.IntField("작은별 회전점수", p.littleRotScore);
        p.littleRotResultScore = EditorGUILayout.IntField("작은별 결과점수", p.littleRotResultScore);
        p.littleRotatingNumberMaxRot = EditorGUILayout.IntField("작은별 회전카운트", p.littleRotatingNumberMaxRot);
        p.bigRotScore = EditorGUILayout.IntField("큰별 회전점수", p.bigRotScore);
        p.bigRotResultScore = EditorGUILayout.IntField("큰별 결과점수", p.bigRotResultScore);
        p.bigRotatingNumberMaxRot = EditorGUILayout.IntField("큰별 회전카운트", p.bigRotatingNumberMaxRot);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("── 기타 ──", EditorStyles.boldLabel);
        data.windowsSetting.isMouseCursor = EditorGUILayout.Toggle("마우스 커서 표시", data.windowsSetting.isMouseCursor);
    }

    // 역직렬화 결과에 누락된 하위 객체가 있으면 안전하게 채운다.
    private void EnsureData()
    {
        if (data == null) return;
        if (data.timeSetting == null) data.timeSetting = new MissionDataLoader.TimeSetting();
        if (data.windowsSetting == null) data.windowsSetting = new MissionDataLoader.WindowsSetting();
        if (data.practiceScoreSetting == null) data.practiceScoreSetting = new MissionDataLoader.PracticeScoreSetting();
        if (data.mission1ScoreSetting == null) data.mission1ScoreSetting = new MissionDataLoader.MissionScoreSetting();
        if (data.mission2ScoreSetting == null) data.mission2ScoreSetting = new MissionDataLoader.MissionScoreSetting();
    }

    private void SetStatus(string msg, MessageType type)
    {
        statusMessage = msg;
        statusType = type;
    }
}
