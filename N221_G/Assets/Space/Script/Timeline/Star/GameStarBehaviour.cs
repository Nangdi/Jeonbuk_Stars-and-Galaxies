using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameStarBehaviour : PlayableBehaviour
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
