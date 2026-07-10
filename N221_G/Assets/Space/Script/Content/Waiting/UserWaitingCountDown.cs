using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UserWaitingCountDown : NextSceneClip
{
    public Coroutine onLoop = null;
    public int currentCount = 0;
    public int maxCount = 20;
    public bool isReset = false;
    public TextMeshPro countText;
    public bool isEnd = false;

    [Header("타이머 종료")]
    public EndTimeCountEvent endTimeCountEvent;
    [System.Serializable]
    public class EndTimeCountEvent : UnityEvent { }

    [Header("타이머 작동")]
    public TimeChangeEvent timeChangeEvent;
    [System.Serializable]
    public class TimeChangeEvent:UnityEvent<int> { }


    public  void OnEnable()
    {
        ResetTimer();
        OnLoopStart();
        //StartTimer();
        MissionStart();
        timeChangeEvent.Invoke(currentCount);
    }

    /// <summary>
    /// 미션 스타트
    /// </summary>
    private void MissionStart()
    {
        Debug.Log("MissionStart");
        GameObjectControl.Instance.OnResetBall(this);
        GameObjectControl.Instance.IsMissionPlayAllUserList(true);
    }

    private void OnDisable()
    {
        currentCount = maxCount;
        OnRemoveLoop();
    }

    //카운터 루프 진행
    IEnumerator OnLoop()
    {
        while (true)
        {
            yield return new WaitUntil(() => !isEnd);
            yield return new WaitForSeconds(1f);
            if (isReset)
            {
                currentCount = maxCount;
                isReset = false;
            }
            else
            {
                
                if (currentCount == 0)
                {
                    //endTimeCountEvent.Invoke();
                    Debug.Log("타이머 종료");
                    GotoSceneClip();
                    GameObjectControl.Instance.stationState = UserTableSettingBase.StationState.experience;
                    isEnd = true;
                    GameObjectControl.Instance.isGamePlay = false;
                }
                else
                {
                    --currentCount;
                }
                timeChangeEvent.Invoke(currentCount);
            }
            SetText();
        }
    }

    //시간 초기화
    public void ResetTimer()
    {
        isEnd = true;
        currentCount = maxCount;
        isReset = false;
        SetText();
    }

    //타이머 작동
    public void StartTimer()
    {
        isEnd = false;
        GameObjectControl.Instance.isGamePlay = true;
    }

    //일시정지
    public void PauseTimer()
    {
        isEnd = true;
    }

    private void SetText()
    {
        if (countText)
        {
            countText.text = currentCount.ToString();
        }
    }

    private void OnLoopStart()
    {
        OnRemoveLoop();
        onLoop = StartCoroutine(OnLoop());
    }


    private void OnRemoveLoop()
    {
        if (onLoop != null)
        {
            StopCoroutine(onLoop);
            onLoop = null;
        }
    }

}
