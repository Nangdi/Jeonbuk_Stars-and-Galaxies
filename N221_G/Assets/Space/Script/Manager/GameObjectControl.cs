using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UserTableSettingBase;
using static UserTotalControl;

public class GameObjectControl : UserTotalControl
{
    private static GameObjectControl instance;

    public static GameObjectControl Instance
    {
        get
        {
            if (instance == null) {
                instance = GameObject.FindObjectOfType<GameObjectControl>();
            }
            return instance;
        }
    }

    public UserTableSettingBase.StationState saveStationState = UserTableSettingBase.StationState.waiting;

    private Coroutine onLoop;

    [Header("게임 진행 여부 확인")]
    public bool isGamePlay = false;
    public bool isGameEnd = false;
    //미션 데이터 로드 설정 
    public MissionDataLoader missionDataLoader;
    public ObjectCreationManager objectCreationManager;


    public override void OnEnable()
    {
        base.OnEnable();
        OnLoopStart();
        OnSetting();
    }

    private void OnSetting() {
        if (stationState.Equals(UserTableSettingBase.StationState.waiting))
        {
            for (int i = 0; i < userTableSettingList.Count; i++)
            {
                if (userTableSettingList[i])
                {
                    userTableSettingList[i].stationState = stationState;
                    userTableSettingList[i].OnInit();
                }
            }
        }
    }

    private void OnLoopStart() {
        OnRemoveLoop();
        onLoop =StartCoroutine(OnLoop());
    }

    private void OnRemoveLoop() {
        if (onLoop != null) {
            StopCoroutine(onLoop);
            onLoop = null;
        }
    }


    IEnumerator OnLoop() {
       
        while (true) {
            yield return null;
            yield return new WaitUntil(()=> !stationState.Equals(saveStationState));

            for (int i = 0; i < userTableSettingList.Count; i++)
            {
                if (userTableSettingList[i])
                {
                    userTableSettingList[i].stationState = stationState;
                    userTableSettingList[i].SetUserScene();
                }
            }

            saveStationState = stationState;

            switch (stationState) {
                case UserTableSettingBase.StationState.waiting:
                    GameObjectControl.Instance.isGameEnd = false;
                    objectCreationManager.ResetPlayIndex();
                    Debug.Log("대기화면 진행");
                    isMissionComplete = false;
                    solarSystemObject.SetActive(false);
                    periodicTableObject.SetActive(false);
                    missionCompleteObject.SetActive(false);
                    //missionCount.gameObject.SetActive(false);
                    //missionCount2.gameObject.SetActive(false);
                    soundControl.SoundStop();
                    
                    break;
                case UserTableSettingBase.StationState.experience:
                    Debug.Log("수소먼지 만들기준비");
                    SettingMakingDust();
                    isMissionComplete = false;
                    solarSystemObject.SetActive(false);
                    periodicTableObject.SetActive(false);
                    missionCompleteObject.SetActive(false);
                    //missionCount.gameObject.SetActive(false);
                    //missionCount2.gameObject.SetActive(false);
                    soundControl.BackgraundSoundPlay(SoundControl.SoundType.Sound1);
                    GameObjectControl.Instance.isGamePlay = true;
                    break;
                case UserTableSettingBase.StationState.mission1:
                    isMissionPlay = true;
                    Debug.Log("미션1 진행");
                    isMissionComplete = false;
                    SettingMission1();
                    solarSystemObject.SetActive(false);
                    periodicTableObject.SetActive(true);
                    missionCompleteObject.SetActive(false);
                    //missionCount.gameObject.SetActive(true);
                    //missionCount2.gameObject.SetActive(false);
                    soundControl.BackgraundSoundPlay(SoundControl.SoundType.Sound2);
                    GameObjectControl.Instance.isGamePlay = true;
                    break;

                case UserTableSettingBase.StationState.mission2:
                    Debug.Log("미션2 진행");
                    isMissionPlay = true;
                    isMissionComplete = false;
                    SettingMission2();
                    solarSystemObject.SetActive(false);
                    periodicTableObject.SetActive(false);
                    missionCompleteObject.SetActive(false);
                    //missionCount2.gameObject.SetActive(true);
                    //missionCount.gameObject.SetActive(false);
                    soundControl.BackgraundSoundPlay(SoundControl.SoundType.Sound3);
                    objectCreationManager.BaseObjectLoad();
                    GameObjectControl.Instance.isGamePlay = true;
                    break;

                default:
                    objectCreationManager.ResetPlayIndex();
                    isMissionComplete = false;
                    solarSystemObject.SetActive(false);
                    periodicTableObject.SetActive(false);
                    missionCompleteObject.SetActive(false);
                    //missionCount.gameObject.SetActive(false);
                    //missionCount2.gameObject.SetActive(false);
                    
                    break;
            }
        }
    }


    //수소먼지만들기 씬 셋팅
    private void SettingMakingDust() {
        Debug.Log("수소먼지만들기 씬");
        for (int i = 0; i < connectionUser.Length; i++) {
            userTableSettingList[i].SettingMakingDust(connectionUser[i]);
        }
        StartCoroutine(OnResetDevice());
    }

    //랭킹 컨트롤
    public void SettingRankDatas() {
        Debug.Log("랭킹 데이터 전달");
        for (int i = 0; i < connectionUser.Length; i++)
        {
            userTableSettingList[i].SettingRankData(connectionUser[i]);
        }
    }


    //미션 2 셋팅
    private void SettingMission2() {

        for (int i = 0; i < userTableSettingList.Count; i++)
        {
            userTableSettingList[i].SettingMission2(connectionUser[i]);
            userTableSettingList[i].userScoreText.isMissionStart = false;
        }

    }


    private void SettingMission1()
    {

        for (int i=0; i< connectionUser.Length; i++) {
            if (!connectionUser[i]) {
                userTableSettingList[i].starVideoType = StarVideoType.ConnectionUser;
            }
        }

        for (int i = 0; i < userTableSettingList.Count; i++)
        {
            userTableSettingList[i].SettingMission1(connectionUser[i]);
            userTableSettingList[i].userScoreText.isMissionStart = false;
        }

    }

    private Coroutine onResetBallCoroutine = null;

    //모든 공배출
    public void OnResetBall(MonoBehaviour monoBehaviour=null) {
        
        Debug.Log("원활한 게임진행을 위한 [모든 공 배출 초기화]");
       
        OnRemoveResetBall();
        onResetBallCoroutine = StartCoroutine(OnResetBallCoroutine(monoBehaviour));
    }



    private void OnRemoveResetBall() {
        if (onResetBallCoroutine != null) {
            StopCoroutine(onResetBallCoroutine);
            onResetBallCoroutine = null;
        }
    }

    IEnumerator OnResetBallCoroutine(MonoBehaviour monoBehaviour=null) {
        Debug.Log("공배출");

        OnResetData("1"); 
        yield return new WaitForSeconds(0.01f);
        //userTableSettingList
        for (int i = 0; i < userTableSettingList.Count; i++)
        {
            userTableSettingList[i].VideoViewDisable();
            userTableSettingList[i].insertBallEvent.Invoke(0);
        }

        yield return new WaitForSeconds(1f);

        OnResetData("0");

        Debug.Log("펌프 정지");
        
        try
        {
            monoBehaviour.SendMessage("StartTimer", null, SendMessageOptions.DontRequireReceiver);
        }
        catch (Exception e) { }
    }




    private Coroutine onNextScene = null;

    public override void NextSceneStart() {
        if (onNextScene != null) {
            StopCoroutine(onNextScene);
            onNextScene = null;
        }
        onNextScene=StartCoroutine(OnNextScene());
    }


    //10초뒤에 넘어감 (다음씬으로)
    IEnumerator OnNextScene() {

        //대기 씬으로 이동 
        GotoSceneClip(UserTableSettingBase.StationState.mission1,
                   GameManager.ClipState.Scene1);

        //마지막 종료시점에따른 카운터진행 
        int mission1EndTimeDelay =
            GameObjectControl.instance.missionDataLoader.jsonLoadData.timeSetting.mission1EndTimeDelay;
        timeCountSliderImage.mission1DelaySliderImage.fillAmount = 0;
        Debug.Log("미션1 게임 종료 딜레이 타임 : "+ mission1EndTimeDelay);
        int currentTimeCount = 0;
        if (mission1EndTimeDelay > 0) {
            while (true) {
                yield return new WaitForSeconds(1f);
                if (mission1EndTimeDelay > currentTimeCount)
                {
                    ++currentTimeCount; 
                    Debug.Log("미션1종료 대기 시간"+ currentTimeCount);
                    timeCountSliderImage.mission1DelaySliderImage.fillAmount = (float)currentTimeCount/ (float)mission1EndTimeDelay;
                }
                else
                {
                    timeCountSliderImage.mission1DelaySliderImage.fillAmount = 1;
                    Debug.Log("대기 시간 종료");
                    break;
                }
            }
        }
      
        Debug.Log("공배출 [하드웨어 초기화]");
        /*for (int i = 0; i < GameObjectControl.Instance.userTableSettingList.Count; i++)
        {
            GameObjectControl.Instance.rsControl.multiRS.SendConsole(i, "1");
        }*/

        yield return new WaitForSeconds(0.01f);
        OnResetData("1");
        yield return new WaitForSeconds(0.01f);
        OnResetData("1");
        yield return new WaitForSeconds(0.01f);
        OnResetData("1");
        yield return new WaitForSeconds(0.01f);
        OnResetData("1");
        yield return new WaitForSeconds(0.01f);
        OnResetData("1");


        yield return new WaitForSeconds(1f);
        for (int i = 0; i < GameObjectControl.Instance.userTableSettingList.Count; i++)
        {
            GameObjectControl.Instance.rsControl.multiRS.SendConsole(i, "0");
            GameObjectControl.Instance.userTableSettingList[i].ballSetting.insertBallCount = 0;
            GameObjectControl.Instance.userTableSettingList[i].ballSetting.littleSaveRotatingDeviceCount = 0;
            GameObjectControl.Instance.userTableSettingList[i].ballSetting.bigSaveRotatingDeviceCount = 0;
            //GameObjectControl.Instance.userTableSettingList[i].ballSetting.m2_insertBallText.OnTextHide(false);
            //GameObjectControl.Instance.userTableSettingList[i].ballSetting.m1_insertBallText.OnTextHide(false);
            GameObjectControl.Instance.userTableSettingList[i].ballSetting.rotatingDeviceCount = 0;
            GameObjectControl.Instance.userTableSettingList[i].rotatingDeviceEvent.Invoke();
            GameObjectControl.Instance.rsControl.multiRS.SendConsole(i, "0");
            GameObjectControl.Instance.userTableSettingList[i].UserScoreTextMove(0);
        }

        yield return null;
        switch (stationState) {
            case StationState.mission1:
                stationState=StationState.mission2;
                GotoSceneClip(UserTableSettingBase.StationState.mission2,
                    GameManager.ClipState.Scene0);
                break;
        }

        yield return new WaitForSeconds(0.01f);
        OnResetData("0");
        yield return new WaitForSeconds(0.01f);
        OnResetData("0");
        yield return new WaitForSeconds(0.01f);
        OnResetData("0");
        yield return new WaitForSeconds(0.01f);
        OnResetData("0");
        yield return new WaitForSeconds(0.01f);
        OnResetData("0");
        yield return new WaitForSeconds(0.01f);
        OnResetData("0");
        yield return new WaitForSeconds(0.01f);
        OnResetData("0");
        
    }

    private void OnResetData(string data)
    {
        for (int i = 0; i < GameObjectControl.Instance.userTableSettingList.Count; i++)
        {
            UserTableController userTableController = GameObjectControl.Instance.userTableSettingList[i];
            if (userTableController.ballSetting.insertBallCount > 0)
            {
                GameObjectControl.Instance.rsControl.multiRS.SendConsole(i, data);
                Debug.Log("리셋 진행 사용자"+i);
            }
        }
    }


    IEnumerator OnResetDevice()
    {

        yield return null;
        Debug.Log("공배출 [하드웨어 초기화]");
        /*for (int i = 0; i < GameObjectControl.Instance.userTableSettingList.Count; i++)
        {
            GameObjectControl.Instance.rsControl.multiRS.SendConsole(i, "1");
        }*/
        OnResetData("1");
        yield return new WaitForSeconds(0.01f);
        OnResetData("1");
       

        yield return new WaitForSeconds(1f);
        for (int i = 0; i < GameObjectControl.Instance.userTableSettingList.Count; i++)
        {
            GameObjectControl.Instance.rsControl.multiRS.SendConsole(i, "0");
            GameObjectControl.Instance.userTableSettingList[i].ballSetting.insertBallCount = 0;
            //GameObjectControl.Instance.userTableSettingList[i].ballSetting.m2_insertBallText.OnTextHide(false);
            //GameObjectControl.Instance.userTableSettingList[i].ballSetting.m1_insertBallText.OnTextHide(false);
            GameObjectControl.Instance.userTableSettingList[i].ballSetting.rotatingDeviceCount = 0;
            GameObjectControl.Instance.userTableSettingList[i].ballSetting.littleSaveRotatingDeviceCount = 0;
            GameObjectControl.Instance.userTableSettingList[i].ballSetting.bigSaveRotatingDeviceCount = 0;
            GameObjectControl.Instance.rsControl.multiRS.SendConsole(i, "0");
            GameObjectControl.Instance.userTableSettingList[i].rotatingDeviceEvent.Invoke();
            //GameObjectControl.Instance.userTableSettingList[i].OnResetDustObject();
        }
        yield return new WaitForSeconds(0.01f);
        OnResetData("0");


    }


    //미션 재생 여부 파악 (유저 리스트)
    public void IsMissionPlayAllUserList(bool isMission) {
        for(int i=0;i<userTableSettingList.Count ;i++){
            userTableSettingList[i].isMissionPlay = isMission;
        }
    }


    /// <summary>
    /// 점수 스코어 정의 구간
    /// </summary>
    public void ScoreRankingControl() {

        UserTableController[] userTableControllerList = Array.FindAll(userTableSettingList.ToArray(), item => (int)item.userTableSetting.userState>0);
        Debug.Log(userTableControllerList.Length);
        int connectionUserTableCount = userTableControllerList.Length;
        
        bool[] connectionList=Array.FindAll(connectionUser, item => item);
        if (connectionList.Length > 1)
        {
            GetRenk("중간 점수");
        }
        else {

            int index=Array.FindIndex(connectionUser, item => item);
            UserTableController userTableController = userTableSettingList[index];
            ScoreControl scoreControl = userTableController.scoreControl;
            UserScoreText userScoreText = userTableController.userScoreText;
            scoreControl.SetRank(-1);
            scoreControl.SetScore(userScoreText._score);
            scoreControl.SetText_NoUser("중간 점수");
            Debug.Log("단독일경우 작동되는구간");

        }
    }


    //랭킹정보
    public class RankData {
        //사용자 인덱스 정보
        public int index;
        //랭킹 데이터
        public int rankData;
        //점수 데이터 
        public int scoreData;

        public bool isRank = false;
    }


    //현재의 랭킹 정보 화면에 노출
    public void GetRenk(string text) {

        bool[] connectionUser = GameObjectControl.instance.connectionUser;
        List<RankData> rank = new List<RankData>();
        for (int i = 0; i < userTableSettingList.Count; i++)
        {
            UserTableController userTableController = userTableSettingList[i];
            UserScoreText userScoreText = userTableController.userScoreText;
            if (connectionUser[i])
            {
                RankData rankData = new RankData();
                rankData.index = i;
                rankData.rankData = 1;
                rankData.scoreData = userScoreText._score;
                rank.Add(rankData);

            }
        }

        for (int i = 0; i < rank.Count; i++)
        {
            List<int> customList = rank.Select(obj => obj.scoreData).Distinct().ToList();
            for (int j = 0; j < customList.Count; j++)
            {
                if (rank[i].scoreData < customList[j])
                {
                    rank[i].rankData++;
                }
            }

        }

        //랭킹 정보 
        for (int i = 0; i < rank.Count; i++)
        {
            Debug.Log("랭킹 정보] : "+rank[i].index + ":" + rank[i].scoreData + ":" + rank[i].rankData);
            int userIndex = rank[i].index;
            //사용자 스코어 텍스트 정보
            UserScoreText userScoreText = userTableSettingList[userIndex].userScoreText;
            ScoreControl scoreControl = userTableSettingList[userIndex].scoreControl;
            //랭킹데이터 정보등록
            scoreControl.SetRank(rank[i].rankData);
            scoreControl.SetScore(rank[i].scoreData);

            //중간점수
            scoreControl.SetText(text);
        }
    }

    private int GetMaxRankData(List<RankData> rank) {
        int max = 0;
        for (int i=0; i< rank.Count; i++) {
            if (max < rank[i].rankData) {
                max = rank[i].rankData;
            }
            
        }

        return max;
    }


}
