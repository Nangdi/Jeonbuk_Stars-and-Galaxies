/// <summary>
/// 프로젝터 설정창(캘리브레이션 GUI)이 열려 있는 동안
/// 게임 측 키보드 디버그/테스트 입력을 잠그기 위한 전역 플래그.
/// ProjectionWarpSystem.UpdateProjectionWarpGUI()에서 설정창 표시 여부에 맞춰 갱신되고,
/// 각 디버그 키 핸들러가 Update/콜백 최상단에서 이 값을 확인해 잠금 상태면 입력을 무시한다.
/// </summary>
public static class DebugInputLock
{
    /// <summary>true면 게임 측 키보드 디버그 입력을 무시(잠금)한다.</summary>
    public static bool Locked = false;
}
