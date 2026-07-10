using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using static UserTableSettingBase;

public class UserTotalControl : MainTimelineManager
{ 
    //사운드 컨트롤
    public SoundControl soundControl;

    [Header("사용자 작동 결과")]
    public List<UserData> userDataList;

    [System.Serializable]
    public class UserData {
        public UserTableSettingBase.StationState stationState;
        [ReadOnly]
        public bool[] isUserList = new bool[] { false, false, false, false, false };
    }

    [Header("게임 데이터 ")]
    public List<MissionData> missionDataList;
    [System.Serializable]
    public class MissionData
    {
        public UserTableSettingBase.StationState stationState;
        //정답 갯수
        //public int rightAnswer = 0;
        //최소 
        public int minRightAnswer = 0;
        //최대
        public int maxRightAnswer = 0;
    }

    //[ReadOnly]
    public bool[] connectionUser = new bool[] { false, false, false, false, false };


    [Header("사용자 대기 카운트 다운")]
    public UserWaitingCountDown userWaitingCountDown;
    [Header("연습 게임 카운트 다운")]
    public MissionCount practiceCountDown;
    [Header("미션 카운터")]
    public MissionCount missionCount;
    [Header("미션 카운터2")]
    public MissionCount missionCount2;

    private Coroutine onUserLoop = null;
    public UserTableSettingBase.StationState stationState;
    public List<UserTableController> userTableSettingList;
    
    //미션 완료 컨트롤
    public bool isMissionComplete = false;
    private Coroutine onMissionComplete = null;

    [Header("태양계 오브젝트")]
    public GameObject solarSystemObject;

    [Header("주기율표 오브젝트")]
    public GameObject periodicTableObject;

    [Header("미션 종료 오브젝트")]
    public GameObject missionCompleteObject;
    
    [Header("체험 생성 오브젝트")]
    public ElementObject elementObject;
    public Transform elementParentObject;

    [Header("터임 카운터 이미지")]
    public TimeCountSliderImage timeCountSliderImage;
    [System.Serializable]
    public class TimeCountSliderImage {
        //미션 1 딜레이 슬라이더 이미지
        public Image mission1DelaySliderImage;
    }
    

    public virtual void OnEnable()
    {
        OnUserLoopStart();
    }

    public virtual void OnDisable()
    {
        OnRemoveUserLoop();
    }

    private void OnUserLoopStart() {
        OnRemoveUserLoop();
        onUserLoop =StartCoroutine(OnUserLoop());
    }

    private void OnRemoveUserLoop() {
        if (onUserLoop != null) {
            StopCoroutine(onUserLoop);
            onUserLoop = null;
        }
    }

    //루프(결과 화면 동시에 작동여부 파악 수정 필요한구간)
    IEnumerator OnUserLoop() {
        while (true)
        {
            yield return null;

            if (missionCount.currentCount > 0)
            {
                switch (GameObjectControl.Instance.stationState)
                {
                    case UserTableSettingBase.StationState.mission2:
                    case UserTableSettingBase.StationState.mission1:
                        bool[] connectionList=
                            Array.FindAll(connectionUser, item => item.Equals(true));

                        int index = GetUserData(GameObjectControl.Instance.stationState);
                        if (index > -1 && connectionList.Length > 0)
                        {
                            UserData userData = userDataList[index];
                            
                            bool isRightAnswer = false;
                            int rightAnswerCount = 0;
                            for (int i = 0; i < connectionUser.Length; i++)
                            {
                                if (!userData.isUserList[i])
                                {
                                    //isRightAnswer = false;
                                    //break;
                                }
                                else
                                {
                                    //isRightAnswer = true;
                                    ++rightAnswerCount;
                                }
                            }

                            if (rightAnswerCount.Equals(connectionList.Length)) {
                                isRightAnswer = true;
                            }

                            if (isRightAnswer && !isMissionComplete)
                            {
                                if (GameObjectControl.Instance.stationState.
                                    Equals(UserTableSettingBase.StationState.mission1)) {
                                    missionCount.PauseTimer();
                                }
                                else if(GameObjectControl.Instance.stationState.
                                    Equals(UserTableSettingBase.StationState.mission2)){
                                    missionCount2.PauseTimer();
                                }


                                Debug.Log("인력에 따른 정답인식 완료 - 게임 종료");
                                OnMissionCompleteStart(GameObjectControl.Instance.stationState);
                            }
                        }
                        break;
                    //case UserTableSettingBase.StationState.mission2:
                        //미션2 완료 루프 작동
                       // break;
                }
            }
        }
    }

    //미션완료 코루틴
    private void OnMissionCompleteStart(UserTableSettingBase.StationState stationState) {
        isMissionComplete = true;
        OnRemoveMissionComplete();
        onMissionComplete =StartCoroutine(OnMissionComplete(stationState));
    }

    private void OnRemoveMissionComplete() {
        if (onMissionComplete != null) {
            StopCoroutine(onMissionComplete);
            onMissionComplete = null;
        }
    }

    //미션 종료 간주
    IEnumerator OnMissionComplete(UserTableSettingBase.StationState stationState) {
        if (stationState.Equals(UserTableSettingBase.StationState.mission1))
        {
            yield return new WaitForSeconds(5f);
        }
        else
        {
            yield return new WaitForSeconds(5f);
        }
        Debug.Log("[OnMissionComplete]");
        //타임 종료로 간주
        TimeEnd();
        if (stationState.Equals(UserTableSettingBase.StationState.mission2))
        {
           
            yield return StartCoroutine(OnGotoSceneClip());
        }
    }

    


    //사용자 등록
    public void UserConnection(UserTableSettingBase userTableSettingBase) {
        if (GameObjectControl.Instance.isGameEnd) {
            userTableSettingBase.UserTableGotoClip(UserTableSettingBase.StationState.mission2,
                       GameManager.ClipState.Scene8);
            return;
        }

        int connectionUserIndex =
            (int)userTableSettingBase.starIndex - 1;
        connectionUser[connectionUserIndex] = true;
        Debug.Log("사용자 연결지점으로 고정 [Scene1]");

        //사용자 연결은 진행이 되지만 타임변경은 되지않음
        if (!GameObjectControl.Instance.isGamePlay)
        {
            GameSceneClip gameSceneClip = userTableSettingBase.GetClipState();
            if (!gameSceneClip.clipState.Equals(GameManager.ClipState.Scene9))
            {
                
                if (stationState.Equals(UserTableSettingBase.StationState.mission2) && GameObjectControl.Instance.isGameEnd)
                {
                    userTableSettingBase.UserTableGotoClip(UserTableSettingBase.StationState.mission2,
                       GameManager.ClipState.Scene8);
                }
                else {
                    userTableSettingBase.UserTableGotoClip(
                            stationState,
                            GameManager.ClipState.Scene9);
                }
                /*if (!GameObjectControl.Instance.isMissionPlay &&
                    GameObjectControl.Instance.isGamePlay &&
                    GameObjectControl.Instance.isGameEnd)
                {
                    userTableSettingBase.UserTableGotoClip(UserTableSettingBase.StationState.mission2,
                        GameManager.ClipState.Scene8);
                }
                else
                {
                    //공통 대기 화면으로 진행(영상 종료 대기 문구)
                    userTableSettingBase.UserTableGotoClip(
                            stationState,
                            GameManager.ClipState.Scene9);
                }*/
            }
        }
        else
        {

            if (stationState.Equals(UserTableSettingBase.StationState.mission1))
            {
                userTableSettingBase.UserTableGotoClip(
                   stationState,
                    GameManager.ClipState.Scene1);
            }else if (stationState.Equals(UserTableSettingBase.StationState.mission2))
            {
                userTableSettingBase.UserTableGotoClip(
                   stationState,
                    GameManager.ClipState.Scene0);
            }
            else if (stationState.Equals(UserTableSettingBase.StationState.experience))
            {
                userTableSettingBase.UserTableGotoClip(
                  stationState,
                   GameManager.ClipState.Scene0);
            }
            else
            {
                userTableSettingBase.UserTableGotoClip(
                    stationState,
                     GameManager.ClipState.Scene1);
            }
            //userTableSettingBase.starVideoType = StarVideoType.BigStar;
        }
        userTableSettingBase.starVideoType = StarVideoType.BigStar;
        if (stationState.Equals(UserTableSettingBase.StationState.waiting)) {
            //대기에서 사용자 접근일경우 메인 타임라인 변경
            //카운트 다운으로 이동
            GotoSceneClip(UserTableSettingBase.StationState.waiting,
                GameManager.ClipState.Scene1);
        }
    }

    //리셋
    public void OnReset() {

        //대기 화면으로 강제 이동
        stationState = UserTableSettingBase.StationState.waiting;
        solarSystemObject.SetActive(false);
        periodicTableObject.SetActive(false);
        missionCompleteObject.SetActive(false);
        missionCount.gameObject.SetActive(false);
        missionCount2.gameObject.SetActive(false);

        OnRemoveMissionComplete();
        //결과 완료 이동 
        isMissionComplete = false;
        //사용자 접근 초기화
        for (int i=0; i< connectionUser.Length; i++) {
            connectionUser[i] = false;
        }

        //사용자 작동 결과 초기화
        for (int i=0;i<userDataList.Count ;i++) {
            for (int j = 0; j < userDataList[i].isUserList.Length; j++) {
                userDataList[i].isUserList[j] = false;
            }
        }

        for (int i = 0; i < userTableSettingList.Count; i++) {
            userTableSettingList[i].OnReset();
        }
        //게임 진행 가능
        GameObjectControl.Instance.isGamePlay = true;
        StartCoroutine(OnResetDivce());
    }

    IEnumerator OnResetDivce()
    {
        Debug.Log("공배출 [하드웨어 초기화]");
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

        for (int i = 0; i < GameObjectControl.Instance.userTableSettingList.Count; i++)
        {
            //GameObjectControl.Instance.rsControl.multiRS.SendConsole(i, "0");
            GameObjectControl.Instance.userTableSettingList[i].ballSetting.insertBallCount = 0;
            GameObjectControl.Instance.userTableSettingList[i].ballSetting.littleSaveRotatingDeviceCount = 0;
            GameObjectControl.Instance.userTableSettingList[i].ballSetting.bigSaveRotatingDeviceCount = 0;
            //GameObjectControl.Instance.userTableSettingList[i].ballSetting.m2_insertBallText.OnTextHide(false);
            //GameObjectControl.Instance.userTableSettingList[i].ballSetting.m1_insertBallText.OnTextHide(false);
            GameObjectControl.Instance.userTableSettingList[i].ballSetting.rotatingDeviceCount = 0;
            //GameObjectControl.Instance.rsControl.multiRS.SendConsole(i, "0");
            GameObjectControl.Instance.userTableSettingList[i].rotatingDeviceEvent.Invoke();
        }

        yield return new WaitForSeconds(2f);
        
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
                Debug.Log("리셋 진행 사용자" + i);
            }
        }
    }

    public MissionData GetMissionData(UserTableSettingBase.StationState stationState)
    {
        MissionData missionData = null;
        int index = Array.FindIndex(missionDataList.ToArray(), item => item.stationState.Equals(stationState));
        if (index > -1)
        {
            missionData = missionDataList[index];
        }
        return missionData;
    }

    public int GetUserData(UserTableSettingBase.StationState stationState) {
        
        return Array.FindIndex(userDataList.ToArray(), item => item.stationState.Equals(stationState));
    }

    //사용자 컨트롤(영상 종료 이벤트)
    public void OnSetUserTotal(UserTableController userTableController) {
        //영상 종료 이벤트 핸들러
        if (!GameObjectControl.Instance.isGamePlay) {
            return;
        }
        switch (GameObjectControl.Instance.stationState) {
            //
            case UserTableSettingBase.StationState.experience:

                if (!userTableController.isDisable)
                {
                    ExperienceEndVideo(userTableController);
                }
                
                break;
            case UserTableSettingBase.StationState.mission1:
            case UserTableSettingBase.StationState.mission2:
                //Debug.Log(starIndex);
                int index=GetUserData(GameObjectControl.Instance.stationState);
                if (index > -1) {

                    MissionData missionData=GetMissionData(userTableController.stationState);
                    Debug.Log(userTableController.ballSetting.insertBallCount+":"+ missionData.minRightAnswer + ":" + missionData.maxRightAnswer);
                    if(missionData.minRightAnswer <= userTableController.ballSetting.insertBallCount &&
                        missionData.maxRightAnswer>= userTableController.ballSetting.insertBallCount)
                    //if ( userTableController.ballSetting.insertBallCount.Equals(missionData.rightAnswer))
                    {
                        //사용자 문제풀이 정답
                        userDataList[index].isUserList[(int)userTableController.starIndex - 1] = true;
                        MissionCompleteEvent(userTableController,GameObjectControl.Instance.stationState);
                        Debug.Log("문제 맞음["+ GameObjectControl.Instance.stationState+"]");


                    }
                    else
                    {
                        Debug.Log("문제 틀림");
                    }
                    UserScoreTextMove(userTableController,0.09f);
                }
                    
                break;
            default:

                break;
        }
    }

    //private Coroutine 
    private void ExperienceEndVideo(UserTableController userTableController) {
        StartCoroutine(OnExperienceEndVideo(userTableController));
    }

    //비디오 실행 결과  과정 
    IEnumerator OnExperienceEndVideo(UserTableController userTableController) {
        
        yield return new WaitForSeconds(1f);

        Debug.Log("영상 종료 원자 글자 움직임");
        int createValue=UnityEngine.Random.Range(2, 4);

        for (int i=0;i< createValue; i++) {
            //영상 종료 작동 구간 
            ElementObject _elementObject =
                GameObject.Instantiate<ElementObject>(elementObject, elementParentObject);
            _elementObject.transform.position = userTableController.transform.position;
            _elementObject.isEditorMove = false;
            _elementObject.gameObject.SetActive(true);

            //충돌 인덱스 선정
            _elementObject.crashIndex = (int)userTableController.starIndex;
            _elementObject.starVideoType = userTableController.starVideoType;
            _elementObject.OnInit();
            
        }
    }

    //엘리먼트 오브젝트 삭제
    public void RemoveElementObject() {

        ElementObject[] elementObjectList = GameObject.FindObjectsOfType<ElementObject>();
        for (int i= elementObjectList.Length-1;i>=0 ;i--) {
            GameObject.Destroy(elementObjectList[i].gameObject);
        }

        
    }


    /// <summary>
    /// 미션 완료 이벤트 화면 설정 구간
    /// </summary>
    private void MissionCompleteEvent(UserTableController userTableController,
        UserTableSettingBase.StationState _stationState) {
        userTableController.CreateElementObject(
                                UserTableAddElementParentObject.PlayState.RightAnswer, _stationState);
        switch (_stationState) {
            case UserTableSettingBase.StationState.mission1:
                //원소 생성
                
                
                Debug.Log("문제 정답 작동mission1");
                break;
            case UserTableSettingBase.StationState.mission2:
                //20230424 수정 사항 
                // 1. 원소 생성 
                // 2. 중앙 지구 항목으로변경
                // 3. 사람, 나무, 건물 , 자동자 등 다양한 우리 주변 사물이 생겨나는 내용 으로 변경 
                //윈소 생성 
                //userTableController.CreateElementObject(
                                //UserTableAddElementParentObject.PlayState.RightAnswer);
                //태양계 생성
                solarSystemObject.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// 미션진행여부
    /// </summary>
    public bool isMissionPlay = false;

    //타임 종료
    public void TimeEnd()
    {
        //시간 종료 이벤트 핸들러
        bool isConnectionUser = false;
        switch (stationState)
        {
            case StationState.experience:
                Debug.Log("[experience 시간 종료 이벤트 핸들러]");
                GameObjectControl.Instance.isGamePlay = false;
                //다음씬으로 이동
                ExperienceEnd();

                break;
            case StationState.mission1:
                Debug.Log("[mission1 시간 종료 이벤트 핸들러]");
                isMissionPlay = false;
                //int index = GetUserData(GameObjectControl.instance.stationState);
                //bool[] isUserList = userDataList[index].isUserList;
                for (int i=0;i< connectionUser.Length; i++) {
                    if (connectionUser[i]) {

                        int index = GetUserData(GameObjectControl.Instance.stationState);
                        if (index > -1)
                        {
                            if (userDataList[index].isUserList[i])
                            {
                                Debug.Log("정답 작동");
                                userTableSettingList[i].RightAnswerScene(UserTableSettingBase.StationState.mission1);
                                userTableSettingList[i].CreateElementObject(
                                    UserTableAddElementParentObject.PlayState.TimeOut, stationState);
                                UserScoreTextMove(userTableSettingList[i], 0.09f);
                            }
                            else
                            {
                                Debug.Log("틀림 작동");
                                userTableSettingList[i].EndMustScene(UserTableSettingBase.StationState.mission1);
                                UserScoreTextMove(userTableSettingList[i], 0.09f);
                            }
                        }
                        else {
                            Debug.Log("틀림 작동");
                            userTableSettingList[i].EndMustScene(UserTableSettingBase.StationState.mission1);
                            UserScoreTextMove(userTableSettingList[i], 0.09f);
                        }
                        userTableSettingList[i].VideoViewDisable();
                        isConnectionUser = true;
                    }
                }
                if (!isConnectionUser) {

                    Debug.Log("사용자 테이블 사용자 아무도 없음 초기화면으로 이동");
                }

                //다음씬 진행
                NextSceneStart();
                break;

            case StationState.mission2:
                Debug.Log("[mission2 시간 종료 이벤트 핸들러 작동]");
                isMissionPlay = false;
                //정답 화면으로 이동
                for (int i = 0; i < connectionUser.Length; i++)
                {
                    if (connectionUser[i])
                    {
                        int index = GetUserData(GameObjectControl.Instance.stationState);
                        if (index > -1)
                        {
                            if (userDataList[index].isUserList[i])
                            {
                                Debug.Log("정답 작동");
                                userTableSettingList[i].RightAnswerScene(UserTableSettingBase.StationState.mission2);
                                /*userTableSettingList[i].CreateElementObject(
                                    UserTableAddElementParentObject.PlayState.TimeOut);*/
                                UserScoreTextMove(userTableSettingList[i],0.09f);
                            }
                            else
                            {
                                Debug.Log("틀림 작동");
                                userTableSettingList[i].EndMustScene(UserTableSettingBase.StationState.mission2);
                                UserScoreTextMove(userTableSettingList[i], 0.09f);
                            }
                        }
                        else {
                            Debug.Log("틀림 작동");
                            userTableSettingList[i].EndMustScene(UserTableSettingBase.StationState.mission2);
                            UserScoreTextMove(userTableSettingList[i], 0.09f);

                        }
                        userTableSettingList[i].VideoViewDisable();
                        isConnectionUser = true;
                    }
                }

                if (!isConnectionUser)
                {

                    Debug.Log("사용자 테이블 사용자 아무도 없음 초기화면으로 이동");
                }

                //다음 씬으로이동(게임종료)
                //메인 타임라인 설정 구간
                EndScene();
                break;
        }
    }

    //사용자 스코어 움직임
    public void UserScoreTextMove(UserTableController userTableController,float move) {
        Vector3 localPos = userTableController.userScoreText.transform.localPosition;
        /*DOTween.To(() => userTableController.userScoreText.transform.localPosition,
            pos => userTableController.userScoreText.transform.localPosition = pos, 
            new Vector3(move, localPos.y, localPos.z), 0.5f);*/
    }

    public virtual void NextSceneStart() { }
    
    //종료 신
    public virtual void EndScene() {

        GameObjectControl.Instance.isGameEnd = true;
        soundControl.SoundStop();
        Debug.Log("게임종료");
        StartCoroutine(OnGotoSceneClip());
        GameObjectControl.Instance.IsMissionPlayAllUserList(false);
        StartCoroutine(OnResetDivce());
        //OnMissionCompleteStart(GameObjectControl.instance.stationState);
    }

    IEnumerator OnGotoSceneClip()
    {
        yield return new WaitForSeconds(1f);
        GotoSceneClip(UserTableSettingBase.StationState.mission2,
              GameManager.ClipState.Scene1);

        bool[] resultList= Array.FindAll(GameObjectControl.Instance.userDataList[1].isUserList, item => item);

        if (resultList.Length > 0)
        {
            yield return new WaitForSeconds(10f);
           
        }
        else {
            yield return new WaitForSeconds(1f);
        }

        OnResetSceneClip();

    }

    public void ExperienceEnd() {
        GotoSceneClip(UserTableSettingBase.StationState.experience,
               GameManager.ClipState.Scene1);
    }


    //씬 초기화(메인)
    public void OnResetSceneClip() {
        Debug.Log("미션 완료 작동 구간");
        GotoSceneClip(UserTableSettingBase.StationState.MissionComplete,
                GameManager.ClipState.Scene0);
    }

    //사용자 점수 리셋 
    public void UserScoreReset() {

        //사용자 스코어 리셋 텍스트 정보
        for (int i=0;i<userTableSettingList.Count ;i++) {            
            userTableSettingList[i].userScoreText.OnScoreDataReset();
        }
    }

}
