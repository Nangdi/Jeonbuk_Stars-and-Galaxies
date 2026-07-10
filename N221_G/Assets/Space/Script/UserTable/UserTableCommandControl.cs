using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UserTableSettingBase : MonoBehaviour
{
    private Coroutine onReEndPointBallStartCall = null;
    public bool isReEndPointBall = false;
    public void ReEndPointBallStartCall()
    {
        isReEndPointBall = true;
        OnReEndPointBallStartRemove();
        onReEndPointBallStartCall = StartCoroutine(OnReEndPointBallStartCall());
    }

    public void OnStopReEndPointBall()
    {
        OnReEndPointBallStartRemove();
        SendLoopData("0"); 
        isReEndPointBall = false;
    }


    private void OnReEndPointBallStartRemove() {
        if (onReEndPointBallStartCall != null) {
            StopCoroutine(onReEndPointBallStartCall);
            onReEndPointBallStartCall = null;
        }
    }

    IEnumerator OnReEndPointBallStartCall()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            yield return StartCoroutine(OnBallOutCall());
        }
        //VideoReset();
    }


    IEnumerator OnBallOutCall()
    {
        //rotatingDeviceEvent.Invoke();
        yield return null;
        SendLoopData("1");
        //yield return new WaitForSeconds(2f);
        //ballSetting.insertBallCount = 0;
        Debug.Log("[볼 출력 하는구간]");
        //rotatingDeviceEvent.Invoke();
        //insertBallEvent.Invoke(ballSetting.insertBallCount);
    }

   



}
