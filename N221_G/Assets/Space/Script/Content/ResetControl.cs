using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//리셋 컨트롤
public class ResetControl : GotoSceneClipControl
{

    //리셋 초기화
    public override void OnEnable()
    {
        base.OnEnable();

        gameObjectControl.UserScoreReset();
        gameObjectControl.OnReset();
    }


    public void OnDisable()
    {
        
    }
}
