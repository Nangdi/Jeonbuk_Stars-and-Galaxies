using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoSceneClipControl : MonoBehaviour
{
    public GameObjectControl gameObjectControl;
    public UserTableSettingBase.StationState stationState;
    public GameManager.ClipState clipState;

    public virtual void OnEnable()
    {
        //클립 초기화
        gameObjectControl.GotoSceneClip(stationState,
           clipState);
        //GameObjectControl.Instance.OnResetSceneClip();
    }
}
