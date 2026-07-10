using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using static MissionDataLoader;
using static MissionDataLoader.MissionData;
using Formatting = Newtonsoft.Json.Formatting;

/// <summary>
///  데이터 문구 변경 항목 
/// </summary>
public class MissionDataLoader : MonoBehaviour
{
    private Coroutine onLoader = null;
    
    //사용자 전체 컨트롤
    public UserTotalControl userTotalControl;

    //json 파일정보
    public JsonLoadData jsonLoadData;

    [System.Serializable]
    public class JsonLoadData {
        //미션 데이터 정보(외부 로드 설정 구간)
        [ReadOnly]
        public List<MissionData> missionDataList;

        //타이머 셋팅 설정구간
        [ReadOnly]
        public TimeSetting timeSetting;

        //윈도우 셋팅
        [ReadOnly]
        public WindowsSetting windowsSetting;

        //연습 모드 점수 부여 컨트롤
        [ReadOnly]
        public PracticeScoreSetting practiceScoreSetting;

        //미션1 점수
        [ReadOnly]
        public MissionScoreSetting mission1ScoreSetting;

        //미션2 점수
        [ReadOnly]
        public MissionScoreSetting mission2ScoreSetting;

    }

    [System.Serializable]
    public class MissionData {
        public enum MissionType { mission1, mission2 }
        //미션 타입 정보
        public MissionType missionType;

        public string problemData = "문제 정보";
        //정답 범위
        public string rightAnswerRange;

        //정답
        public string rightAnswer = "정답입니다!";
        public string right_messageData1;
        public string right_messageData2;

        //오답
        public string wrongAnswer = "오답입니다.";
        public string wrong_messageData1;
        public string wrong_messageData2;
    }

    
    [System.Serializable]
    public class TimeSetting {

        [Header("사용자 접근 카운터")]
        public int connectionTime;

        [Header("연습타임 정보")]
        public int practiceTime;
        
        [Header("미션1타임 정보")]
        public int mission1Time;

        [Header("미션1타임 다음턴으로 넘어가는 타임 정보")]
        public int mission1EndTimeDelay;

        [Header("미션2타임 정보")]
        public int mission2Time;

    }

    [System.Serializable]
    public class WindowsSetting {
        //마우스 커서 컨트롤
        public bool isMouseCursor = false;
        //시작 데이터
        public string startData= "";
        //종료 데이터
        public string endData = "";
    }

    //연습게임 점수 셋팅 설정 구간
    [System.Serializable]
    public class PracticeScoreSetting {
        [Header("작은별 회전 점수")]
        public int littleRotScore;

        [Header("작은별 결과 점수")]
        public int littleRotResultScore;

        [Header("작은별 회전 카운터")]
        public int littleRotatingNumberMaxRot;

        [Header("큰별 회전 점수")]
        public int bigRotScore;
        
        [Header("큰별 결과 점수")]
        public int bigRotResultScore;
        
        [Header("큰별 회전 카운터")]
        public int bigRotatingNumberMaxRot;
    }

    //점수 데이터 등록 구간
    [System.Serializable]
    public class MissionScoreSetting {

        public int rightAnswerScore = 0;
        public int mustScore = 0;
        //틀림 점수 적용
        public bool applyWrongScore = false;

        /*//정답 반복
        public bool isRightAnswerRepeat = false;
        //틀림 반복
        public bool isMustScoreRepeat = false;*/
    }



    //정답/오답 표기에 따른 데이터 정보 표기 
    public List<AnswerControlData> answerControlDataList;
    
    [System.Serializable]
    public class AnswerControlData {
        
        //정답 컨트롤(미션1)
        public RightAnswerControl rightAnswerControl_Mission1;

        //오답 컨트롤(미션1)
        public ErrorControl errorControl_Mission1;

        //정답 컨트롤(미션2)
        public RightAnswerControl rightAnswerControl_Mission2;

        //오답 컨트롤(미션2)
        public ErrorControl errorControl_Mission2;

    }

    [Header("외부 텍스트 파일 로드 완료 이벤트 핸들러")]
    public LoadCompleteEvent loadCompleteEvent;

    [System.Serializable]
    public class LoadCompleteEvent : UnityEvent<JsonLoadData> { 
    }

    private void OnEnable()
    {
        MissionTextDataLoader();
    }

    private void DisableLoader() {
        if (onLoader != null) {
            StopCoroutine(onLoader);
            onLoader = null;
        }
    }

    private void MissionTextDataLoader() {
        string missionText=Application.streamingAssetsPath + Path.DirectorySeparatorChar + "MissionText.json";
        DisableLoader();
        onLoader = StartCoroutine(OnLoader(missionText));
    }

    //외부 파일 로드 
    IEnumerator OnLoader(string path) {
        if (!File.Exists(path))
        {
            Debug.Log("파일생성");
            jsonLoadData = new JsonLoadData();
            jsonLoadData.missionDataList = new List<MissionData>();
            MissionData missionData = new MissionData();
            missionData.missionType = MissionType.mission1;
            jsonLoadData.missionDataList.Add(missionData);
            missionData.missionType = MissionType.mission2;
            jsonLoadData.missionDataList.Add(missionData);
            jsonLoadData.timeSetting = new TimeSetting();
            string jsonData = JsonConvert.SerializeObject(jsonLoadData,Formatting.Indented);
            File.WriteAllText(path, jsonData);
        }
        else
        {
            yield return null;
            UnityWebRequest request = UnityWebRequest.Get(path);
            yield return request.SendWebRequest();
            if (string.IsNullOrEmpty(request.error)) {
                //리스트 정보 리턴 구간
                jsonLoadData = JsonConvert.DeserializeObject<JsonLoadData>(request.downloadHandler.text);
                SetDataValue();
            }
        }
    }

    //데이터 값 연동 구간 
    private void SetDataValue()
    {
        Debug.Log("[SetDataValue] 데이터 연결성");
        for(int i=0;i< answerControlDataList.Count; i++){

            //미션1
            RightAnswerControl rightAnswerControl= answerControlDataList[i].rightAnswerControl_Mission1;
            rightAnswerControl.textDataList[0].problemTextObject.text = jsonLoadData.missionDataList[0].problemData;
            rightAnswerControl.textDataList[0].phraseTextObject.text = jsonLoadData.missionDataList[0].rightAnswer;
            rightAnswerControl.textDataList[0].textData1 = jsonLoadData.missionDataList[0].right_messageData1;
            rightAnswerControl.textDataList[0].textData2 = jsonLoadData.missionDataList[0].right_messageData2;
            
            ErrorControl errorControl= answerControlDataList[i].errorControl_Mission1;
            errorControl.textDataList[0].phraseTextObject.text = jsonLoadData.missionDataList[0].wrongAnswer;
            errorControl.textDataList[0].textData1 = jsonLoadData.missionDataList[0].wrong_messageData1;
            errorControl.textDataList[0].textData2 = jsonLoadData.missionDataList[0].wrong_messageData2;

            //미션2
            RightAnswerControl rightAnswerControl2 = answerControlDataList[i].rightAnswerControl_Mission2;
            rightAnswerControl2.textDataList[0].problemTextObject.text = jsonLoadData.missionDataList[1].problemData;
            rightAnswerControl2.textDataList[0].phraseTextObject.text = jsonLoadData.missionDataList[1].rightAnswer;
            rightAnswerControl2.textDataList[0].textData1 = jsonLoadData.missionDataList[1].right_messageData1;
            rightAnswerControl2.textDataList[0].textData2 = jsonLoadData.missionDataList[1].right_messageData2;

            ErrorControl errorControl2 = answerControlDataList[i].errorControl_Mission2;
            errorControl2.textDataList[0].phraseTextObject.text = jsonLoadData.missionDataList[1].wrongAnswer;
            errorControl2.textDataList[0].textData1 = jsonLoadData.missionDataList[1].wrong_messageData1;
            errorControl2.textDataList[0].textData2 = jsonLoadData.missionDataList[1].wrong_messageData2;
        }

        //문제 풀이 범위
        for (int i=0;i < userTotalControl.missionDataList.Count; i++) {
            Debug.Log(userTotalControl.missionDataList[i].stationState.ToString());
            MissionData missionData= Array.Find(jsonLoadData.missionDataList.ToArray(), item => item.missionType.ToString().Equals(userTotalControl.missionDataList[i].stationState.ToString()));
            string Range = missionData.rightAnswerRange;
            string[] rangeData = Range.Split(",");
            UserTotalControl.MissionData _missionData = userTotalControl.missionDataList[i];
            if (rangeData.Length > 1) {
                _missionData.minRightAnswer = int.Parse(rangeData[0]);
                _missionData.maxRightAnswer = int.Parse(rangeData[1]);
            }
            else {
                _missionData.minRightAnswer = int.Parse(missionData.rightAnswerRange);
                _missionData.maxRightAnswer = int.Parse(missionData.rightAnswerRange) * 1000;
            }
        }

        //사용자 연결 카운트 다운
        GameObjectControl.Instance.userWaitingCountDown.maxCount = jsonLoadData.timeSetting.connectionTime;
        GameObjectControl.Instance.practiceCountDown.maxCount = jsonLoadData.timeSetting.practiceTime;
        GameObjectControl.Instance.missionCount.maxCount = jsonLoadData.timeSetting.mission1Time;
        GameObjectControl.Instance.missionCount2.maxCount = jsonLoadData.timeSetting.mission2Time;
     
        UnityEngine.Cursor.visible = jsonLoadData.windowsSetting.isMouseCursor;
        loadCompleteEvent.Invoke(jsonLoadData);
    }

}
