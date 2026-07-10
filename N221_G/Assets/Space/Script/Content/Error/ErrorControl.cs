using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static ErrorControl;
using static UserTableSettingBase;

//에러 사용자 지시 텍스트
public class ErrorControl : TextDataContent
{

    //타임 인덱스 정보
    private int timeIndex = 0;

    public override void OnEnable()
    {
        base.OnEnable();
        SetTextPhraseData();
        timeIndex = 0;
    }

    //텍스트 정보 등록 구간
    private void SetTextPhraseData()
    {
        if (textDataList[0].stationState.Equals(
            UserTableSettingBase.StationState.mission1))
        {
            textDataList[0].phraseTextObject.text = 
                GameObjectControl.Instance.missionDataLoader.jsonLoadData.missionDataList[0].wrongAnswer;
            
        }
    }

    //타임 텍스트 체인지 
    public override void TimeTextChange() {
        if (timeIndex > 2)
        {
            //최종문구 변경이 되도록 구성 진행 
            UserTableSettingBase.StationState stationState=GetCurrentMission();
            switch (stationState) {
                case UserTableSettingBase.StationState.mission1:
                    //textDataList[0].phraseTextObject.text =""
                    break;
                case UserTableSettingBase.StationState.mission2:

                    break;
            }
        }
        else
        {
            ++timeIndex;
        }
    }

    //현재의 텍스트 미션 상태 정보
    private UserTableSettingBase.StationState GetCurrentMission() {
        return textDataList[0].stationState;
    }



}
