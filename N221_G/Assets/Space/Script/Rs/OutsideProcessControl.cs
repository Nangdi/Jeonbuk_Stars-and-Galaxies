using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 컨트롤러 RS232 통신 베이스.
///
/// [구성] 단일 컨트롤러(포트 1개)가 5개 테이블을 모두 담당한다.
/// - 포트 설정: StreamingAssets/port.json (controllerId 0, COM 1개)
/// - 송신: SendConsole(index, msg) → 단일 포트로 "E{index}{cmd}E"(예: E31E) 전송.
///         프레임에 테이블 번호(index, 0~4)를 포함하므로 컨트롤러가 어느 테이블인지 구분 가능.
/// - 수신: 컨트롤러가 JSON {Index,Data}로 어느 테이블 이벤트인지 담아 보냄.
///         SerialPortManager.OnDataReceived → RsReceived 이벤트 → RsControl이 JSON 파싱.
///         (어느 포트로 오든 JSON Index로 테이블을 결정하므로 단일 포트로 동작)
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

    // 단일 컨트롤러(포트) 방식: 모든 송신을 이 controllerId의 포트로 보낸다. (port.json의 controllerId와 일치)
    private const int SingleControllerId = 0;

    /// <summary>
    /// 테이블(index)로 명령 송신. 단일 컨트롤러 방식이므로 프레임에 테이블 번호를 포함한다: E{index}{cmd}E (예: E31E).
    /// </summary>
    public void SendConsole(int index, string message)
    {
        SerialPortManager mgr = SerialPortManager.Instance;
        if (mgr == null || !mgr.IsControllerOpen(SingleControllerId))
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
        // 프레임에 테이블 번호(index) 포함 → 단일 컨트롤러가 어느 테이블인지 구분. 예: "E" + "3" + "1" + "E" = "E31E"
        mgr.SendData(SingleControllerId, s + index.ToString() + message + e);
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
