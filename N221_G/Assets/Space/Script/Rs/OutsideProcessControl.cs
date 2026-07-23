using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 컨트롤러(테이블) RS232 통신 베이스.
///
/// [변경] 기존 외부 exe(Rs232_Console_Connection.exe) 브리지를 제거하고,
/// RS232Package의 네이티브 시리얼 매니저(SerialPortManager)를 통해 송수신한다.
/// - 포트 설정: StreamingAssets/port.json (JsonManager가 로드) — 컨트롤러(테이블)별 COM/baud
/// - 송신: SendConsole(index, msg) → SerialPortManager.SendData(index, "E"+msg+"E")
/// - 수신: SerialPortManager.OnDataReceived → RsReceived 이벤트 → RsControl이 JSON 파싱
///   (프로토콜/호출부 무변경 → 컨트롤러 펌웨어 변경 불필요)
///
/// controllerId(=SendData/OnDataReceived의 index)는 테이블 번호(0부터)와 1:1 대응하도록
/// port.json의 controllerId를 0~4로 맞춘다.
/// </summary>
public class OutsideProcessControl : MonoBehaviour
{
    //외부 컨트롤 데이터 정보
    public MissionDataLoader missionDataLoader;

    [Header("(미사용) 이전 exe 브리지 경로 - 호환용")]
    [SerializeField]
    private string processFile;

    [Header("(미사용) 이전 포트 설정 - 현재 포트 설정은 StreamingAssets/port.json 사용")]
    public List<ProcessData> processDataList;

    [Header("이벤트 핸들러")]
    public RsEventHandler rsEventHandler = new RsEventHandler();

    [System.Serializable]
    public class RsEventHandler : UnityEvent<string> { }

    public delegate void RsReceivedDelegate(string data);
    public static event RsReceivedDelegate RsReceived;

    public static void RsReceivedData(string data) {
        if (RsReceived != null) {
            RsReceived.Invoke(data);
        }
    }

    [System.Serializable]
    public class ProcessData {
        [Header("컨트롤러(테이블) 인덱스")]
        public int processIndex;

        [Header("rs232 comName")]
        public string comName;

        [Header("rs232 baud")]
        public int bit;

        public string data = "";
    }

    private bool subscribed = false;

    //프로그램 실행 컨트롤
    IEnumerator Start() {
        yield return null;
        TrySubscribe();
    }

    //SerialPortManager 수신 이벤트 구독
    private void TrySubscribe() {
        if (subscribed) return;
        if (SerialPortManager.Instance == null) {
            Debug.LogWarning("[Rs232] SerialPortManager를 찾지 못했습니다. 씬에 RS232 매니저 오브젝트가 있는지 확인하세요.");
            return;
        }
        SerialPortManager.Instance.OnDataReceived += OnSerialReceived;
        subscribed = true;
    }

    /// <summary>
    /// 수신 콜백. SerialPortChannel이 메인 스레드로 복귀시켜 호출한다.
    /// 기존 수신 파이프라인(RsReceived 이벤트 → RsControl JSON 파싱) 그대로 사용.
    /// </summary>
    private void OnSerialReceived(int controllerId, string data) {
        RsReceivedData(data);
    }

    /// <summary>
    /// 특정 컨트롤러(테이블)로 명령 송신. 기존 프레임(startData+message+endData, 예: "E1E") 유지.
    /// </summary>
    public void SendConsole(int index, string message)
    {
        SerialPortManager mgr = SerialPortManager.Instance;
        if (mgr == null || !mgr.IsControllerOpen(index))
        {
            Debug.Log("연결되지 않음 index=" + index);
            return;
        }

        if (message.Equals("1"))
        {
            Debug.Log("볼 출력 진행: 인덱스" + index);
        }
        else
        {
            Debug.Log("볼 바람 종료 진행: 인덱스" + index);
        }

        string s = missionDataLoader.jsonLoadData.windowsSetting.startData;
        string e = missionDataLoader.jsonLoadData.windowsSetting.endData;
        mgr.SendData(index, s + message + e);
    }

    public virtual void ReceiverData(string data) { }

    public virtual void OnInit() { }

    public string data;

    private void OnDestroy()
    {
        if (subscribed && SerialPortManager.Instance != null)
        {
            SerialPortManager.Instance.OnDataReceived -= OnSerialReceived;
            subscribed = false;
        }
    }
}
