using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 스코어 정보 체크
/// </summary>
public class CheckScoreLevel : MonoBehaviour
{
    //사용자 테이블 컨트롤러
    //public UserTableController userTableController;

    //활성화시 점수 데이터 체크 
    public void OnEnable()
    {
        //userTableController.UserTableGotoClip(
            //UserTableSettingBase.StationState.mission2, 
            //GameManager.ClipState.Scene5);
        UserControl();
    }

    private void UserControl() {
        bool[] connectionUser = GameObjectControl.Instance.connectionUser;
        for (int i=0;i< connectionUser.Length; i++) {
            if (connectionUser[i]) {
                //GameObjectControl.Instance.userTableSettingList[i].scoreControl.gameObject.SetActive(true);
                GameObjectControl.Instance.userTableSettingList[i].UserTableGotoClip(
                    UserTableSettingBase.StationState.mission2, 
                    GameManager.ClipState.Scene5);
            }
        }
        bool[] customConnectionList = Array.FindAll(connectionUser, item => item);
        //1명이상일경우
        if (customConnectionList.Length > 1)
        {
            ManyUserRenk("최종 점수");
        }
        else {
            //1명 단독으로 구성되어있을경우
            int index = Array.FindIndex(connectionUser, item => item);
            UserTableController userTableController = GameObjectControl.Instance.userTableSettingList[index];
            ScoreControl scoreControl = userTableController.scoreControl;
            UserScoreText userScoreText = userTableController.userScoreText;
            scoreControl.SetRank(-1);
            scoreControl.SetScore(userScoreText._score);
            scoreControl.SetText_NoUser("최종 점수");

        }
    }


    //단독일경우 랭킹 데이터 정보
    public void ManyUserRenk(string text) {
        GameObjectControl.Instance.GetRenk(text);
    }


}
