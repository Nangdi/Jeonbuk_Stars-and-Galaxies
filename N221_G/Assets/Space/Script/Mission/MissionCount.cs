using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UserTableTimelineManager;

public class MissionCount : NextSceneClip
{
    public Coroutine onLoop = null;
    public int currentCount = 0;
    public int maxCount = 20;
    public bool isReset = false;
    public TMP_Text countText;
    public TMP_Text countText1;
    public bool isEnd = false;


    [Header("타이머 종료")]
    public EndTimeCountEvent endTimeCountEvent;
    [System.Serializable]
    public class EndTimeCountEvent : UnityEvent { }

    //그라데이션 색상 
    public Gradient gradient;

    //슬라이더 이미지
    public Image sliderImage;
    
    private void OnEnable()
    {
        ResetTimer();
        OnLoopStart();
        //StartTimer();
        MissionStart();
    }

    /// <summary>
    /// 미션 스타트
    /// </summary>
    private void MissionStart() {
        Debug.Log("MissionStart");
        GameObjectControl.Instance.OnResetBall(this);
        GameObjectControl.Instance.IsMissionPlayAllUserList(true);
    }

    private void OnDisable()
    {
        currentCount = maxCount;
        SliderColorChange();
        OnRemoveLoop();
    }

    //카운터 루프 진행
    IEnumerator OnLoop() {
        while (true)
        {
            yield return new WaitUntil(()=>!isEnd);
            yield return new WaitForSeconds(1f);
            if (isReset)
            {
                currentCount = maxCount;
                isReset = false;
            }
            else {
                if (currentCount == 0)
                {
                    SliderColorChange();
                    endTimeCountEvent.Invoke();
                    isEnd = true; 
                    //GameObjectControl.Instance.isGameEnd = false;
                    GameObjectControl.Instance.isGamePlay = false;
                }
                else {
                    SliderColorChange();
                    --currentCount;
                }
            }
            SetText();
        }
    }



    private void SliderColorChange() {
        if (sliderImage)
        {
            float value = 1 - (float)currentCount / (float)maxCount;
          
            sliderImage.color = gradient.Evaluate(value);
            sliderImage.fillAmount = 1 - value;
        }
    }


    //시간 초기화
    public void ResetTimer() {
        isEnd = true;
        currentCount = maxCount;
        isReset = false;
        SetText();
    }

    //타이머 작동
    public void StartTimer() {
        isEnd = false;
        GameObjectControl.Instance.isGamePlay = true;
    }

    //일시정지
    public void PauseTimer() {
        isEnd = true;
    }

    private void SetText() {
        countText.text= currentCount.ToString();
        countText1.text = currentCount.ToString();
    }

    private void OnLoopStart() {
        OnRemoveLoop();
        onLoop = StartCoroutine(OnLoop());
    }


    private void OnRemoveLoop() {
        if (onLoop != null) {
            StopCoroutine(onLoop);
            onLoop = null;
        }
    }
}
