using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static MissionDataLoader;
using static UserTableSettingBase;

/// <summary>
/// 점수 컨트롤
/// </summary>
public class UserScoreText : MonoBehaviour
{

    //텍스트 정보
    public TextMeshPro _text;
    public int _score;

    //등록된 스코어 텍스트 정보
    public AddScoreText addScoreTextObject;
    //상위 점수 텍스트 오브젝트
    public GameObject parentScoreTextObject;

    //활성화시
    public void OnEnable()
    {
        isMissionStart = false;
        //_score = 0;
        //SetText(_score.ToString());
    }

    //스코어 데이터 리셋
    public void OnScoreDataReset() {
        ScoreUpdate(0);
    }

    //스코어 업데이트
    private void ScoreUpdate(int _score)
    {
        this._score = _score;
        SetText(_score.ToString());
    }

    //텍스트 변경
    private void SetText(string message) {
        _text.text = message;
    }

    //점수 획득에 대한 정의 구간 설정필요
    public void SendScoreData(int score, UserTableController userTableController) {
        if (!GameObjectControl.Instance.isGamePlay) { return; }
        //사용자 정보 
        //userTableController.

        if (GameObjectControl.Instance.stationState.Equals(UserTableSettingBase.StationState.experience))
        {
            _score = _score + score;
            Debug.Log("score : " + _score);
            SetText(_score.ToString());
        }
        //미션1 미션2 점수 획득 구간
        else if(
            GameObjectControl.Instance.stationState.Equals(UserTableSettingBase.StationState.mission1) ||
            GameObjectControl.Instance.stationState.Equals(UserTableSettingBase.StationState.mission2))
        {

            Debug.Log("미션1,미션2 일경우 점수 획득 정보 확인구간");
        }
    }

    /// <summary>
    /// 미션 시작 
    /// </summary>
    [ReadOnly]
    [SerializeField]
    public bool isMissionStart = false;

    //연습 게임 점수 데이터 정보
    [ReadOnly]
    public PracticeScoreSetting practiceScore;
    
    //점수 -> 내점수 항목에 저장
    [System.Serializable]
    public class PracticeScoreSetting
    {
        
        //*************작은별****************
        //작은별 회전 점수
        public int littleRotScore;
        //작은별 회전 결과 점수
        public int littleRotResultScore;
        

        //*************큰별****************
        //큰별 회전 결과 점수
        public int bigRotScore;
        //큰별 회전 결과 점수 
        public int bigRotResultScore;

    }


    //비디오 종료 이벤트 핸들러
    public void VideoEndEvent(UserTableController userTableController)
    {
        if (!GameObjectControl.Instance.isGamePlay) { return; }
        switch (GameObjectControl.Instance.stationState)
        {
            case UserTableSettingBase.StationState.experience:
                //Debug.Log("점수 증가");
                if (userTableController.starVideoType.Equals(StarVideoType.BigStar))
                {
                    Debug.Log("큰별 점수");
                    _score = _score + practiceScore.bigRotResultScore;
                    SetText(_score.ToString());
                }
                else if (userTableController.starVideoType.Equals(StarVideoType.LittleStar)) 
                {
                    Debug.Log("작은별 점수");
                    _score = _score + practiceScore.littleRotResultScore;
                    SetText(_score.ToString());
                }
                
                break;

            //미션1    
            case UserTableSettingBase.StationState.mission1:
                Debug.Log("-------------------미션1----------------------");
                if (GameObjectControl.Instance.isMissionPlay)
                {
                    //정답인지 오답인지 확인 필요
                    int userIndex = (int)userTableController.starIndex - 1;
                    int userDataIndex = 0;
                    bool isRightAnswer = GameObjectControl.Instance.userDataList[userDataIndex].isUserList[userIndex];
                    Debug.Log("userIndex(사용자 인덱스 정보) : " + userIndex);
                    //정답 유무  추론
                    MissionScoreSetting missionScoreSetting = GameObjectControl.Instance.missionDataLoader.jsonLoadData.mission1ScoreSetting;
                    int Mission1Score = MissionScoreData(isRightAnswer, missionScoreSetting);
                    Debug.Log("정답 여부] : " + Mission1Score);
                    //틀림 점수 부여 기능
                    bool applyWrongScore = GameObjectControl.Instance.missionDataLoader.jsonLoadData.mission1ScoreSetting.applyWrongScore;
                    if (applyWrongScore)
                    {
                        if (!isMissionStart)
                        {
                            if (isRightAnswer)//정답만 모션이 들어가도록 구성
                            {
                                MissionScoreScoreMotion(isRightAnswer, Mathf.Abs(Mission1Score));
                                isMissionStart = true;
                            }
                            else
                            {
                                MissionScoreScoreMotion(isRightAnswer, Mathf.Abs(Mission1Score));
                            }
                        }
                    }
                    else {
                        if (!isMissionStart)
                        {
                            if (isRightAnswer)//정답만 모션이 들어가도록 구성
                            {
                                MissionScoreScoreMotion(isRightAnswer, Mathf.Abs(Mission1Score));
                                isMissionStart = true;
                            }
                            
                        }
                    }
                }
                else {
                    Debug.Log("미션이 종료 되어서 점수를 획득할수없습니다.");
                }
                break;
            //미션2
            case UserTableSettingBase.StationState.mission2:
                Debug.Log("미션2");
                if (GameObjectControl.Instance.isMissionPlay)
                {
                    int userIndex = (int)userTableController.starIndex - 1;
                    int userDataIndex = 1;
                    bool isRightAnswer = GameObjectControl.Instance.userDataList[userDataIndex].isUserList[userIndex];
                    MissionScoreSetting missionScoreSetting = GameObjectControl.Instance.missionDataLoader.jsonLoadData.mission2ScoreSetting;
                    int Mission2Score = MissionScoreData(isRightAnswer, missionScoreSetting);
                    bool applyWrongScore = GameObjectControl.Instance.missionDataLoader.jsonLoadData.mission2ScoreSetting.applyWrongScore;
                    if (applyWrongScore)
                    {
                        if (!isMissionStart)
                        {
                            if (isRightAnswer)//정답만 모션이 들어가도록 구성
                            {
                                MissionScoreScoreMotion(isRightAnswer, Mathf.Abs(Mission2Score));
                                isMissionStart = true;
                            }
                            else
                            {
                                MissionScoreScoreMotion(isRightAnswer, Mathf.Abs(Mission2Score));
                            }
                        }
                    }
                    else
                    {
                        if (!isMissionStart)
                        {
                            if (isRightAnswer)//정답만 모션이 들어가도록 구성
                            {
                                MissionScoreScoreMotion(isRightAnswer, Mathf.Abs(Mission2Score));
                                isMissionStart = true;
                            }
                        }
                    }

                }
                else{
                    Debug.Log("미션이 종료 되어서 점수를 획득할수없습니다.");
                }

                break;
        }
    }

    //미션 점수 컨트롤
    private int MissionScoreData(bool isRightAnswer, MissionScoreSetting missionScoreSetting)
    {
        int result = 0;
        //정답 여부(정답 진행)
        if (isRightAnswer)
        {
            //정답일경우
           int rightAnswerScore = missionScoreSetting.rightAnswerScore;
           result = rightAnswerScore;
        }
        else//오답일경우(감점 진행)
        {
           int mustScore = missionScoreSetting.mustScore;
           result = -mustScore;
        }
        return result;
    }

    //정답&오답 확정 모션
    public void MissionScoreScoreMotion(bool isRightAnswer,int score) {
        //점수 모션 작동 구간
        AddScoreText addScoreText=GameObject.Instantiate<AddScoreText>(addScoreTextObject, parentScoreTextObject.transform);
        //시작 커맨드 작동 구간
        addScoreText.OnInit(isRightAnswer, score);
        if (isRightAnswer)
        {
            this._score += score;
        }
        else {
            this._score -= score;
        }
        ScoreUpdate(this._score);
    }



    //정답 스코어 정보(점수 등록구간)
    private void SetMissionRightAnswerScore() { 
        
    }





}
