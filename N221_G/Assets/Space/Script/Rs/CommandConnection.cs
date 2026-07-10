using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandConnection : MonoBehaviour
{
    public UserTableController userTableController;

    //볼데이터 등록
    public void OnInsertBall() {
        userTableController.InsertBall();
    }

    //회전데이터
    public void OnRotatingDevice() {
        userTableController.RotatingDevice();

    }

    public void OnReset() {
        userTableController.OnReset();
    }

    //바람 생성 컨트롤 구간
    public void OnEndPointBallStart() {
        if (!userTableController.isReEndPointBall)
        {
            Debug.Log("배출 진행");
            userTableController.ReEndPointBallStartCall();
        }
        else
        {
            Debug.Log("배출 정지");
            userTableController.OnStopReEndPointBall();
        }
       
    }


}
