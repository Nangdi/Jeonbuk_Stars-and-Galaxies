using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneClip : MonoBehaviour
{
    //[Header("다음씬으로 이동하는구간")]
    //public UserTableSettingBase.StationState nextStationState;
    //public GameManager.ClipState nextClipState;
    public GameObjectControl gameObjectControl;
    public UserTableSettingBase.StationState stationState;
    public GameManager.ClipState clipState;

    //public void NextClip() { 
    //    
    //}

    public void GotoSceneClip()
    {
        gameObjectControl.GotoSceneClip(
           stationState,
            clipState);
    }

}
