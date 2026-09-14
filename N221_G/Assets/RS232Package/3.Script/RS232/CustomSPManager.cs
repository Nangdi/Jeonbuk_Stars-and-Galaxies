/// <summary>
/// 이 프로젝트(별/은하 테이블)용 RS232 매니저.
///
/// 포트 설정은 StreamingAssets/port.json(JsonManager)에서 읽어 컨트롤러(테이블)별로 포트를 연다.
/// 수신 데이터는 부모 SerialPortManager가 OnDataReceived 이벤트로 알려주며,
/// 실제 게임 처리(회전/공투입 등)는 OutsideProcessControl이 그 이벤트를 구독해 담당한다.
///
/// [정리] 이전 버전에 있던 다른 프로젝트용 로직(화력/수력/풍력 명명, TCP 서버 전송,
/// 80.E.C 프레임 파싱, Q/W 테스트키)은 이 프로젝트와 무관하여 모두 제거했다.
/// 부모 SerialPortManager의 기본 동작(포트 오픈 + 이벤트 전달)만 사용한다.
/// </summary>
public class CustomSPManager : SerialPortManager
{
    // 별도 커스텀 동작 없음. 프로젝트별 수신 처리는 OutsideProcessControl(OnDataReceived 구독)에서 수행.
}
