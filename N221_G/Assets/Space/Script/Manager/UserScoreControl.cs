using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UserScoreControl;

/// <summary>
///  사용자 점수 컨트롤 정의 구간
/// </summary>
public class UserScoreControl : UserTableTimelineManager
{
    
    public UserScoreData userScoreData;

    [System.Serializable]
    public class UserScoreData {

        //사용자 점수 컨트롤 텍스트 선정 구간
        [Header("사용자 입력 텍스트")]
        public TextMeshProUGUI userScoreText;
        public int currentScore = 0;

        //타임 별 스코어 점수 정보
        public int timeStarScore = 0;

        //핸들 점수
        public int turnHandleScore = 10;
        public int littleStarMaxScore = 150;
        public int bigStarScore = 70;

        //초신성 폭발 점수
        public int superNovaExplosionScore = 150;
        
        //행성상 성운 폭발 점수
        public int planetaryExplosionScore = 150;

        /*
         * 점수
        핸들 회전시 10점 공통
        최대 입력 범주 
         - 작은별 150점 까지             
         - 큰별 70점 까지

        초신성 폭발시  150점
        행성상 성운시  30점 
         */
    }

    //시작 커맨드
    /*public override void OnInit()
    {
        //base.OnInit();
        OnUserScoreInit();
    }*/


    //사용자 점수 컨트롤 시작 부분
    private void OnUserScoreInit(){
        
    }


    //회전 핸들 점수 
    public override void TurnHandleScore() {
        
        int _turnHandleScore = 0;

        if (this.currentBall >= 3)
        {
            Debug.Log("큰별");
            if (userScoreData.timeStarScore <= userScoreData.bigStarScore)
            {
                _turnHandleScore = userScoreData.turnHandleScore;
            }
        }
        else {
            Debug.Log("작은 별");
            if (userScoreData.timeStarScore <= userScoreData.littleStarMaxScore)
            {
                _turnHandleScore = userScoreData.turnHandleScore;
            }
        }

        userScoreData.currentScore += _turnHandleScore;
        userScoreData.timeStarScore += _turnHandleScore;
        userScoreData.userScoreText.text = ""+ userScoreData.currentScore;
        Debug.Log(userScoreData.currentScore);
    }


    public override void EndExplosionScore() {
        Debug.Log("[EndExplosionScore]");
    }

    public override void UserScore() {


        //this.currentBall
    }




}
