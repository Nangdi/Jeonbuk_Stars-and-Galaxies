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

        [Header("별만들기 종료 후 '미션 종료' 문구만 보이는 시간(초, 음수면 타임라인 기본 약 5초)")]
        public float experienceEndTextTime = 2f;

        [Header("별만들기 종료 후 전환 영상 재생 시간(초). 영상 전체가 이 시간에 끝나도록 배속. 0이면 원래 속도(약 20초)")]
        public float experienceEndVideoTime = 15f;

    }

    [System.Serializable]
    public class WindowsSetting {
        //마우스 커서 컨트롤
        public bool isMouseCursor = false;
        //시작 데이터
        public string startData= "";
        //종료 데이터
        public string endData = "";
        //배경음 켜기 (SoundControl의 오디오)
        public bool isBgmOn = true;
        //효과음 켜기 (배경음 외 모든 AudioSource)
        public bool isSfxOn = true;
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
        //persistent 경로 (없으면 StreamingAssets 기본값을 복사해 옴)
        string missionText = MissionTextPath;
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
            //File.ReadAllText 는 메모장 저장 시 붙는 BOM 을 자동으로 떼어 준다
            string text = null;
            try
            {
                text = File.ReadAllText(path);
            }
            catch (Exception e)
            {
                Debug.LogError("[MissionDataLoader] 설정 파일 읽기 실패: " + path + " (" + e.Message + ")");
            }

            JsonLoadData loaded = null;
            if (text != null)
            {
                try
                {
                    loaded = JsonConvert.DeserializeObject<JsonLoadData>(text);
                }
                catch (Exception e)
                {
                    //깨진 파일은 덮어쓰지 않고 그대로 둔다 (운영자가 고칠 수 있게)
                    Debug.LogError("[MissionDataLoader] 설정 파일 형식 오류: " + path + " (" + e.Message + ")");
                }
            }

            if (loaded != null)
            {
                jsonLoadData = loaded;
                //새 버전에서 추가된 항목을 기본값으로 채워 파일에 다시 저장 (기존 값은 그대로 유지)
                if (FillMissingDefaults(jsonLoadData, text))
                {
                    WriteFile(path);
                    Debug.Log("[MissionDataLoader] 새 설정 항목을 기본값으로 추가해 저장: " + path);
                }
                SetDataValue();
            }
        }
    }

    /// <summary>
    /// 이전 버전 파일에 없던 항목을 기본값으로 채운다. 파일에 다시 써야 하면 true.
    /// (Newtonsoft 는 없는 필드를 클래스 기본값으로 두지만, 없는 객체는 null 이 되므로 채워 준다)
    /// </summary>
    private static bool FillMissingDefaults(JsonLoadData d, string originalText)
    {
        bool changed = false;
        if (d.missionDataList == null) { d.missionDataList = new List<MissionData>(); changed = true; }
        if (d.timeSetting == null) { d.timeSetting = new TimeSetting(); changed = true; }
        if (d.windowsSetting == null) { d.windowsSetting = new WindowsSetting(); changed = true; }
        if (d.practiceScoreSetting == null) { d.practiceScoreSetting = new PracticeScoreSetting(); changed = true; }
        if (d.mission1ScoreSetting == null) { d.mission1ScoreSetting = new MissionScoreSetting(); changed = true; }
        if (d.mission2ScoreSetting == null) { d.mission2ScoreSetting = new MissionScoreSetting(); changed = true; }

        //다시 직렬화한 결과에 원본에 없던 키가 있으면 새 항목이 생긴 것
        if (!changed)
        {
            try
            {
                var before = Newtonsoft.Json.Linq.JObject.Parse(originalText);
                var after = Newtonsoft.Json.Linq.JObject.FromObject(d);
                changed = HasMissingKey(before, after);
            }
            catch (Exception) { }
        }
        return changed;
    }

    //after 에 있는 키가 before 에 없으면 true (하위 객체까지 확인)
    private static bool HasMissingKey(Newtonsoft.Json.Linq.JToken before, Newtonsoft.Json.Linq.JToken after)
    {
        var a = after as Newtonsoft.Json.Linq.JObject;
        if (a == null) return false;
        var b = before as Newtonsoft.Json.Linq.JObject;
        if (b == null) return true;
        foreach (var prop in a.Properties())
        {
            Newtonsoft.Json.Linq.JToken child;
            if (!b.TryGetValue(prop.Name, out child)) return true;
            if (HasMissingKey(child, prop.Value)) return true;
        }
        return false;
    }

    private void WriteFile(string path)
    {
        string dir = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
        File.WriteAllText(path, JsonConvert.SerializeObject(jsonLoadData, Formatting.Indented));
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
        ApplySound(jsonLoadData.windowsSetting.isBgmOn, jsonLoadData.windowsSetting.isSfxOn);
        loadCompleteEvent.Invoke(jsonLoadData);
    }

    //배경음/효과음 음소거 적용 (비활성 오브젝트 포함)
    private void ApplySound(bool bgmOn, bool sfxOn) {
        var bgmSources = new HashSet<AudioSource>();
        foreach (var sc in FindObjectsOfType<SoundControl>(true)) {
            if (sc.audioSource != null) bgmSources.Add(sc.audioSource);
        }
        foreach (var src in FindObjectsOfType<AudioSource>(true)) {
            src.mute = bgmSources.Contains(src) ? !bgmOn : !sfxOn;
        }
    }

    //MissionText.json 경로 (persistentDataPath. 없으면 StreamingAssets 기본값을 복사해 옴)
    public string MissionTextPath {
        get { return ConfigPath.Get("MissionText.json"); }
    }

    /// <summary>
    /// 현재 jsonLoadData를 MissionText.json에 저장하고 게임에 실시간 재적용
    /// (설정 패널에서 호출)
    /// </summary>
    public void SaveAndApply() {
        WriteFile(MissionTextPath);
        SetDataValue();
    }

}
