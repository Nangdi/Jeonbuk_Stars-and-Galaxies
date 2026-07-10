using UnityEngine.Playables;

public class GameSceneBehaviour: PlayableBehaviour
{
    /// <summary>
    /// 타임 컨트롤 객체 배열 정보 
    /// </summary>
    public TimeData timeData = new TimeData();

    [System.Serializable]
    public class TimeData
    {
        public double start;
        public double end;
    }


}
