using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using static ErrorControl;
using static GameObjectControl;
using static MissionDataLoader;
using static UserTableController;
using static UserTableSettingBase;
using static UserTotalControl;

public partial class UserTableSettingBase : MonoBehaviour
{
    //사용자 테이블 인덱스 선언 구간
    public enum StarIndex { None, User1, User2, User3, User4, User5 }
    [Header("사용자 테이블 인덱스 정보")]
    public StarIndex starIndex;

    private Coroutine onInit = null;

    [Header("스테이션 상태 정보")]
    public StationState stationState;
    public enum StationState { waiting, experience, mission1, mission2,MissionComplete }

    [Header("타임라인 설정")]
    public PlayableDirector pd;

    //볼 컨트롤
    public BallSetting ballSetting;

    [System.Serializable]
    public class BallSetting {
        [Header("볼카운터 데이터 ")]
        public BallCountData ballCountData;
        public MissionText mission1Text;
        public MissionText mission2Text;
        public int bigInsertCountBall;
        public int littleInsertCountBall;

        [Header("--------------------------")]
        [ReadOnly]
        public int insertBallCount = 0;
        [ReadOnly]
        public float rotatingDeviceCount = 0;

        [ReadOnly]
        [Header("작은별 회전 저장 카운터")]
        public float littleSaveRotatingDeviceCount;
        //작은별 회전 Max 데이터 값
        public float littleRotatingNumberMaxRot;

        [ReadOnly]
        [Header("큰별 회전 저장 카운터")]
        public float bigSaveRotatingDeviceCount;
        //큰별 회전 Max 데이터 값
        public float bigRotatingNumberMaxRot;

        /**[Header("공투입 정보(미션1)")]
        public InsertBallText m1_insertBallText;

        [Header("공투입 정보(미션2)")]
        public InsertBallText m2_insertBallText;**/

    }

    [Header("큰별")]
    public StarControl bigStar;

    [Header("작은별")]
    public StarControl littleStar;

    //별 컨트롤
    [System.Serializable]
    public class StarControl
    {

        public Texture texture = null;
        
        //비디오 재생 여부
        public bool isStarVideoPlay = false;

        //비디오 재생위치 조정
        public float videoValue = 0;

        //루프 컨트롤
        private Coroutine onLoop = null;

        public bool isPlay = false;

        public double videoDuration;
    }

    /// <summary>
    /// 비디오 재생위치
    /// </summary>
    public float bigVideoPosition = 0;

    /// <summary>
    /// 별 비디오 타임 정의 구간
    /// </summary>
    public enum StarVideoType {ConnectionUser, BigStar, LittleStar }
    public StarVideoType starVideoType;


    public Transform createPrdc;
    public GameObject createPrdcPrefab;

    public Renderer renderer;
    public MaterialPropertyBlock materialPropertyBlock;
    
    [Header("실행 여부")]
    public bool isPlaying = false;

    public bool isMissionPlay = true;

    public UserTableAddElementParentObject userTableAddElementParentObject;

    //비디오 완료 이벤트 핸들러
    public VideoCompleteEvent videoCompleteEvent;
    [System.Serializable]
    public class VideoCompleteEvent : UnityEvent<UserTableController> { }

    //먼지 정보
    public List<DustControl> makingDustDataList;


    //사운드 데이터
    public SoundData soundData;
    [System.Serializable]
    public class SoundData
    {
        public AudioSource audioSource;
        public AudioClip insertAudioClip;
        public AudioClip rotionAudioClip;
    }

    [Header("스코어 정보")]
    public ScoreControl scoreControl;
    
    [Header("점수 정보")]
    public UserScoreText userScoreText;

    public void OnEnable()
    {
        OnInitStart();
    }

    public void InsertSoundPlay() {
        soundData.audioSource.clip = soundData.insertAudioClip;
        soundData.audioSource.Play();
    }

    public void RotionSoundPlay()
    {
        soundData.audioSource.clip = soundData.rotionAudioClip;
        soundData.audioSource.Play();
    }


    public virtual void OnDisable()
    {
        OnRemoveInit();
    }

    private void OnInitStart() {
        OnRemoveInit();
        onInit =StartCoroutine(OnInitCoroutine());
    }

    private void OnRemoveInit() {
        if (onInit != null) {
            StopCoroutine(onInit);
            onInit = null;
        }
    }

    //시작 코루틴 설정 구간
    IEnumerator OnInitCoroutine() {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(()=> !starIndex.Equals(StarIndex.None));
        OnInit();
    }


    public virtual void OnInit() {
        SetUserScene();
    }


    public virtual void UserTableGotoClip(UserTableSettingBase.StationState _stationState, GameManager.ClipState _clipState)
    {
    }

    public virtual GameSceneClip GetClipState() {
        return null;
    }

        //문제 진행
    public void SetUserScene()
    {
        switch (stationState)
        {
            case StationState.waiting: //기다림
                UserTableGotoClip(UserTableSettingBase.StationState.waiting, GameManager.ClipState.Scene0);
                break;
            case StationState.experience://체험
                UserTableGotoClip(UserTableSettingBase.StationState.experience, GameManager.ClipState.Scene0);
                break;
            case StationState.mission1://미션1
                UserTableGotoClip(UserTableSettingBase.StationState.mission1, GameManager.ClipState.Scene0);
                break;
            case StationState.mission2://미션 2
                UserTableGotoClip(UserTableSettingBase.StationState.mission2, GameManager.ClipState.Scene0);
                break;
        }
    }

    public void SettingMission1(bool isConnection)
    {

        //연결되지 않았을경우 (대기 화면으로변경)
        if (!isConnection)
        {
            isDisable = false;
            //대기 화면으로 이동
            UserTableGotoClip(UserTableSettingBase.StationState.mission1, GameManager.ClipState.Scene0);

        }
        else
        {
            Debug.Log("사용자 연결되는구간");
            UserTableGotoClip(UserTableSettingBase.StationState.mission1, GameManager.ClipState.Scene1);
            //isDisable = false;
        }
    }

    //미션 2 셋팅
    public void SettingMission2(bool isConnection) {

        //연결되지 않았을경우 (대기 화면으로변경)
        if (!isConnection)
        {
            isDisable = true;
            //대기 화면으로 이동
            UserTableGotoClip(UserTableSettingBase.StationState.mission2, GameManager.ClipState.Scene1);

        }
        else {
            isDisable = false;
        }
    }

    public void SettingMakingDust(bool isConnection)
    {
        //연결되지 않았을경우 (대기 화면으로변경)
        if (!isConnection)
        {
            isDisable = true;
            //대기 화면으로 이동
            //초기 연습게임일경우 태기 화면 
            UserTableGotoClip(UserTableSettingBase.StationState.experience, GameManager.ClipState.Scene6);
            
        }
        else
        {
            isDisable = false;
            UserTableGotoClip(UserTableSettingBase.StationState.experience, GameManager.ClipState.Scene0);
            
            for (int i=0;i< makingDustDataList.Count; i++) {
                makingDustDataList[i].OnStop();
                //makingDustDataList[i].SetActive(false);
            }
        }
    }


    //랭킹 데이터 정보 등록
    public void SettingRankData(bool isConnection) {
        if (!isConnection)
        {
            
            
        }
        else{
            //UserTableController[] userTableControllerList = Array.FindAll(GameObjectControl.Instance.userTableSettingList.ToArray(),
            //  item => (int)item.userTableSetting.userState > 0);
            //Debug.Log(userTableControllerList.Length);
            //int connectionUserTableCount = userTableControllerList.Length;

            //UserTableGotoClip(UserTableSettingBase.StationState.experience, GameManager.ClipState.Scene8);
            bool[] connectionList = Array.FindAll(GameObjectControl.Instance.connectionUser, item => item);
            if (connectionList.Length > 1)
            {

                Debug.Log("-단독인원일경우 작동되는구간 ");
                //인원수에 따른 화면표현이 다르게 구현 일단 1
                UserTableGotoClip(UserTableSettingBase.StationState.experience, GameManager.ClipState.Scene7);
            }else{
                Debug.Log("-다수 인원 접근 진행");
                UserTableGotoClip(UserTableSettingBase.StationState.experience, GameManager.ClipState.Scene8);
            }
            //기존의 영상 정보 초기화 
            GameObjectControl.Instance.OnResetBall();
        }
    }


    [Header("----데이터 입력----")]
    //공투입 이벤트 핸들러
    public InsertBallEvent insertBallEvent;
    [System.Serializable]
    public class InsertBallEvent : UnityEvent<int> { }

    //회전 데이터 정보
    public RotatingDeviceEvent rotatingDeviceEvent;
    [System.Serializable]
    public class RotatingDeviceEvent : UnityEvent { }


    //볼 입력 컨트롤(볼입력시 먼지 생성)
    public void InsertBall() {
        ++ballSetting.insertBallCount;
        InsertSoundPlay();
        insertBallEvent.Invoke(ballSetting.insertBallCount);

        if (ballSetting.insertBallCount > 12) {
            ReEndPointBallStart();
        }

        if (!GameObjectControl.Instance.isGamePlay || isDisable) { return; }
        //ballSetting.m1_insertBallText.SetText(ballSetting.insertBallCount);
        //ballSetting.m2_insertBallText.SetText(ballSetting.insertBallCount);
        CreateDustObject();
    }


    [Header("----회전 먼지 컨트롤 정보----")]
    //공투입 이벤트 핸들러
    public InsertBallCreateDust insertBallCreateDust;
    [System.Serializable]
    public class InsertBallCreateDust : UnityEvent<int> { }

    //회전 데이터 정보
    public RotatingDeviceDust rotatingDeviceDust;
    [System.Serializable]
    public class RotatingDeviceDust:UnityEvent<int> { }




    //먼지 생성 컨트롤
    public void CreateDustObject() {
        //체험 화면에서만 작동 
        if (stationState.Equals(UserTableSettingBase.StationState.experience))
        {
            //먼지 생성
            if (ballSetting.insertBallCount > 0)
            {
                UserTableGotoClip(UserTableSettingBase.StationState.experience, GameManager.ClipState.Scene1);
            }

            for (int i = 0; i < makingDustDataList.Count; i++)
            {
                if (ballSetting.insertBallCount > i)
                {
                    makingDustDataList[i].isPlay = true;
                    makingDustDataList[i].gameObject.SetActive(true);
                    makingDustDataList[i].enabled = true;
                    makingDustDataList[i].OnPlay();
                }
                else
                {
                    makingDustDataList[i].OnStop();
                }
            }
            insertBallCreateDust.Invoke(ballSetting.insertBallCount);
        }
    }

    /// <summary>
    /// 먼지 오브젝트 숨기기(초기화)
    /// </summary>
    public void OnResetDustObject() {
        for (int i = 0; i < makingDustDataList.Count; i++)
        {
            makingDustDataList[i].OnStop();
        }
        if (stationState.Equals(UserTableSettingBase.StationState.experience) && isMissionPlay)
        {
            UserTableGotoClip(UserTableSettingBase.StationState.experience, GameManager.ClipState.Scene0);
        }
    }


    private Coroutine onReEndPointBallStart = null;

    private void ReEndPointBallStart() {
        OnResetReEndPointBallStart();
        onReEndPointBallStart = StartCoroutine(OnReEndPointBallStart());
    }

    private void OnResetReEndPointBallStart() {
        if (onReEndPointBallStart != null) {
            StopCoroutine(onReEndPointBallStart);
            onReEndPointBallStart = null;
        }
    }

    IEnumerator OnReEndPointBallStart() {
        yield return null;
        OnResetBallOut();
        OnBallOutStart();
    }



    //볼 정보 리셋
    public void ResetBall() {
        ballSetting.insertBallCount = 0;
        ballSetting.littleSaveRotatingDeviceCount = 0;
        ballSetting.bigSaveRotatingDeviceCount = 0;
    }

    public bool isDisable = false;

    [Header("스코어 이벤트")]
    public ScoreDataEvent scoreDataEvent;
    [System.Serializable]
    public class ScoreDataEvent : UnityEvent<int, UserTableController> { }

    //비디오 재생 컨트롤
    [SerializeField]
    [ReadOnly]
    private bool noVideoPlay = true;

    //회전 데이터
    public void RotatingDevice()
    {
        isDisable = false;
        Debug.Log("회전데이터] : " + ballSetting.insertBallCount);
        if (!GameObjectControl.Instance.isGamePlay ) {
            VideoViewTween(0); 
        }

        //&& !isDisable
        if ((int)starVideoType == 0 )
        {
            Debug.Log("사용자 연결 시작");
            GameObjectControl.Instance.UserConnection(this);

            if (ballSetting.insertBallCount > 0)
            {
                ResetBall();
                OnResetBallOut();
                OnBallOutStart();
            }
            return;
        }

        //대기 화면일경우 작동 하지않음
        if (stationState.Equals(StationState.waiting) || !isMissionPlay  )
        {
            if (ballSetting.insertBallCount > 0) {
                ResetBall();
                OnResetBallOut();
                OnBallOutStart();
            }
            return;
        }

        if (GameObjectControl.Instance.stationState.Equals(UserTableSettingBase.StationState.mission1) ||
            GameObjectControl.Instance.stationState.Equals(UserTableSettingBase.StationState.mission2)) {
            if (!GameObjectControl.Instance.isMissionPlay && 
                GameObjectControl.Instance.isGamePlay && 
                GameObjectControl.Instance.isGameEnd)
            {
                UserTableGotoClip(UserTableSettingBase.StationState.mission2, GameManager.ClipState.Scene8);
                if (ballSetting.insertBallCount > 0)
                {
                    ResetBall();
                    OnResetBallOut();
                    OnBallOutStart();
                }
                return;
            }else if (!GameObjectControl.Instance.isMissionPlay)
            {
                if (ballSetting.insertBallCount > 0)
                {
                    ResetBall();
                    OnResetBallOut();
                    OnBallOutStart();
                }
                return;
            }
        }

        //공발사를 위한 회전 카운터 정보
        if (ballSetting.insertBallCount > 0)
        {
            resetValue = 1;
            
            if (ballSetting.insertBallCount>0 &&
               ballSetting.insertBallCount <= ballSetting.littleInsertCountBall)
            {
                if (noVideoPlay)
                {
                    Debug.Log("작은 별 생성");

                    /**ballSetting.rotatingDeviceCount += 0.3333333f;
                    isPlaying = true;
                    VideoViewTween(1);
                    if (ballSetting.rotatingDeviceCount >= 0.3f * 3)
                    {
                        noVideoPlay = false;
                        ballSetting.rotatingDeviceCount = 1;
                        Debug.Log("작은 별 생성완료");
                        OnVideoResetStart();
                    }
                    scoreDataEvent.Invoke(10,this as UserTableController);
                    LittleVideoChangeDetectionValue(ballSetting.rotatingDeviceCount);
                    starVideoType = StarVideoType.LittleStar;
                    RotionSoundPlay();*/
                    //값 변경시 적용 가능함
                    ballSetting.littleSaveRotatingDeviceCount += 1f/ ballSetting.littleRotatingNumberMaxRot;
                    
                    isPlaying = true;
                    VideoViewTween(1);

                    //1과 같거나 클경우 종료되는구간
                    if (ballSetting.littleSaveRotatingDeviceCount >= 1f)
                    {
                        noVideoPlay = false;
                        ballSetting.littleSaveRotatingDeviceCount = 1;
                        Debug.Log("작은 별 생성완료");
                        OnVideoResetStart();
                    }
                    scoreDataEvent.Invoke(userScoreText.practiceScore.littleRotScore, this as UserTableController);
                    LittleVideoChangeDetectionValue(ballSetting.littleSaveRotatingDeviceCount);
                    starVideoType = StarVideoType.LittleStar;
                    RotionSoundPlay();
                }
                else { 
                    
                }
            }
            else if(ballSetting.insertBallCount > ballSetting.littleInsertCountBall)
            {
                if (noVideoPlay)
                {
                    Debug.Log("큰 별 생성");
                    //ballSetting.littleSaveRotatingDeviceCount += 1f / ballSetting.littleRotatingNumberMaxRot;
                    ballSetting.bigSaveRotatingDeviceCount += 1f/ballSetting. bigRotatingNumberMaxRot;

                    isPlaying = true;
                    VideoViewTween(1);
                    if (ballSetting.bigSaveRotatingDeviceCount >= 1f)
                    {
                        noVideoPlay = false;
                        ballSetting.bigSaveRotatingDeviceCount = 1;
                        Debug.Log("큰 별 생성완료");
                        OnVideoResetStart();
                    }
                    scoreDataEvent.Invoke(userScoreText.practiceScore.bigRotScore, this as UserTableController);
                    BigVideoChangeDetectionValue(ballSetting.bigSaveRotatingDeviceCount);
                    starVideoType = StarVideoType.BigStar;
                    RotionSoundPlay();
                }
            }
        }
        else {
            Debug.Log("작동되지 않음 [공이 투입되지 않았음]");
        }
    }


    private Coroutine onVideoReset = null;


    private void OnVideoResetStart() {
        OnRemoveVideoReset();
        onVideoReset=StartCoroutine(OnVideoReset());
    }

    private void OnRemoveVideoReset() {
        if (onVideoReset != null) {
            StopCoroutine(onVideoReset);
            onVideoReset = null;
        }
    }

    public float resetValue = 0;

    /// <summary>
    /// 비디오 상태 리셋
    /// </summary>
    /// <returns></returns>
    IEnumerator OnVideoReset() {
        //영상 종료 화면 전송 이벤트 핸들러 위치 
        //정답 여부 확인
        AnswerResult(ballSetting.insertBallCount);
        videoCompleteEvent.Invoke((UserTableController)this);
        
        SendLoopData("1");

        yield return new WaitForSeconds(1.5f);
        GameObjectControl.Instance.rsControl.multiRS.SendConsole((int)starIndex - 1, "0");
        
        //먼지오브젝트 숨기기
        OnResetDustObject();
        ballSetting.insertBallCount = 0;
        //ballSetting.m2_insertBallText.OnTextHide(false);
        //ballSetting.m1_insertBallText.OnTextHide(false);
        ballSetting.rotatingDeviceCount = 0;
        ballSetting.littleSaveRotatingDeviceCount = 0;
        ballSetting.bigSaveRotatingDeviceCount = 0;

        yield return null;
        GameObjectControl.Instance.rsControl.multiRS.SendConsole((int)starIndex - 1, "0");
        VideoViewTween(0);
        littleStar.videoValue = 0;
        bigVideoPosition = 0;
        VideoReset();
        noVideoPlay = true;
        rotatingDeviceDust.Invoke(0);
        rotatingDeviceEvent.Invoke(); 
        yield return null;
        GameObjectControl.Instance.rsControl.multiRS.SendConsole((int)starIndex - 1, "0");
    }

    private void SendLoopData(string Data) {
        for (int i=0;i<10 ;i++) {
            GameObjectControl.Instance.rsControl.multiRS.SendConsole((int)starIndex - 1, Data);
        }
    }

    private Coroutine onBallOut = null;

    private void OnResetBallOut() {
        if (onBallOut != null) {
            StopCoroutine(onBallOut);
            onBallOut = null;
        }
    }
    //볼 내보내기 아웃 이벤트 리스너
    private void OnBallOutStart() {
        onBallOut = StartCoroutine(OnBallOut());
        VideoReset();
    }

    //볼 내보내기
    IEnumerator OnBallOut() {
       
        rotatingDeviceEvent.Invoke();
        yield return null;
        SendLoopData("1"); 
        yield return new WaitForSeconds(0.01f);
        ballSetting.insertBallCount = 0;
        Debug.Log("[볼 출력 하는구간]");
        //rotatingDeviceEvent.Invoke();
        insertBallEvent.Invoke(ballSetting.insertBallCount);
        yield return new WaitForSeconds(1.5f);
        SendLoopData("0");
        yield return null;
        GameObjectControl.Instance.rsControl.multiRS.SendConsole((int)starIndex - 1, "0");
    }


    //정답결과
    public void AnswerResult(int insertBallCount) {
        //미션 정답 공유
        UserTotalControl.MissionData missionData = GameObjectControl.Instance.GetMissionData(stationState);
        int index = GameObjectControl.Instance.GetUserData(GameObjectControl.Instance.stationState);
        if (index > -1)
        {
            if (GameObjectControl.Instance.userDataList[index].isUserList[(int)starIndex - 1])
            {
                return;
            }

            if (missionData.minRightAnswer <= insertBallCount &&
                       missionData.maxRightAnswer >= insertBallCount)
                //if (insertBallCount.Equals(missionData.rightAnswer))
            {
               
                GameObjectControl.Instance.userDataList[index].isUserList[(int)starIndex - 1] = true;
                switch (stationState)
                {
                    case UserTableSettingBase.StationState.mission1:
                        RightAnswerScene(UserTableSettingBase.StationState.mission1);
                        break;
                    case UserTableSettingBase.StationState.mission2:
                        RightAnswerScene(UserTableSettingBase.StationState.mission2);
                        break;
                }

            }
            else
            {
                switch (stationState)
                {
                    case UserTableSettingBase.StationState.mission1:
                        MustScene(UserTableSettingBase.StationState.mission1);
                        break;
                    case UserTableSettingBase.StationState.mission2:
                        MustScene(UserTableSettingBase.StationState.mission2);
                        break;
                }
            }
        }
    }


    //비디오 리셋
    public virtual void VideoReset() { }

    /// <summary>
    /// 사용자 테이블 비디오 화면 노출 여부 Tween
    /// </summary>
    /// <param name="value"></param>
    private void VideoViewTween(float value) {
        if (!GameObjectControl.Instance.isGamePlay || isDisable)
        {
            Color _baseColor = Color.white;
            _baseColor.r = 1;
            _baseColor.g = 1;
            _baseColor.b = 1;
            _baseColor.a = 0;
            materialPropertyBlock.SetColor("_BaseColor", _baseColor);
            renderer.SetPropertyBlock(materialPropertyBlock);
            /**DOTween.To(() =>
               resetValue,
               value => resetValue = value,
               0, 0.5f).OnUpdate(OnResetValueUpdate);*/
        }
        else
        {
            DOTween.To(() =>
                resetValue,
                value => resetValue = value,
                value, 0.5f).OnUpdate(OnResetValueUpdate);
        }
    }

    private void OnResetValueUpdate() {
        Color _baseColor=materialPropertyBlock.GetColor("_BaseColor");
        _baseColor.r = 1;
        _baseColor.g = 1;
        _baseColor.b = 1;
        _baseColor.a = resetValue;
        materialPropertyBlock.SetColor("_BaseColor", _baseColor);
        renderer.SetPropertyBlock(materialPropertyBlock);
    }

    public virtual void BigVideoChangeDetectionValue(float value) { }

    public virtual void LittleVideoChangeDetectionValue(float value) { }

    //리셋 (화면 초기화)
    public void OnReset() {
        Debug.Log("사용자 초기화 설정 구간");
        VideoReset();
        isDisable = false;
        noVideoPlay = true;
        //대기로 이동
        stationState = StationState.waiting;
        starVideoType = StarVideoType.ConnectionUser;
        VideoViewTween(0); 
        UserTableGotoClip(UserTableSettingBase.StationState.waiting, GameManager.ClipState.Scene0);

        //먼지오브젝트 숨기기
        OnResetDustObject();
        userTableAddElementParentObject.OnReset();


       // Debug.Log("모든 공 배출 로직 추가");
        /**ballSetting.rotatingDeviceCount = 0;
        ballSetting.insertBallCount = 0;
        isRotating = false;
        littleStar.videoValue = 0;
        bigVideoPosition = 0;*/

    }

    //비디오 화면 비활성화
    public void VideoViewDisable() {
        VideoViewTween(0);
    }

    //볼 리셋
    public void OnResetBall() {
       
        ballSetting.insertBallCount = 0;
        //ballSetting.m2_insertBallText.OnTextHide(false);
        //ballSetting.m1_insertBallText.OnTextHide(false);
        ballSetting.rotatingDeviceCount = 0;
        ballSetting.littleSaveRotatingDeviceCount = 0;
        ballSetting.bigSaveRotatingDeviceCount = 0;

        LittleVideoChangeDetectionValue(0);
        BigVideoChangeDetectionValue(0);
    }

    //게임종료시작동
    public void TotalGameEnd()
    {
        isDisable = false;


    }

    public void RightAnswerScene(UserTableSettingBase.StationState stationState) {
        switch (stationState) {
            case UserTableSettingBase.StationState.mission1:
                UserTableGotoClip(UserTableSettingBase.StationState.mission1, GameManager.ClipState.Scene2);
                break;

            case UserTableSettingBase.StationState.mission2:
                UserTableGotoClip(UserTableSettingBase.StationState.mission2, GameManager.ClipState.Scene2);
                break;
            default:
                Debug.Log("정답이 설정되어있지 않음");
                break;
        }
        Debug.Log("정답 작동");
    }

    //틀림
    public void MustScene(UserTableSettingBase.StationState stationState) {
        switch (stationState)
        {
            case UserTableSettingBase.StationState.mission1:
                UserTableGotoClip(UserTableSettingBase.StationState.mission1, GameManager.ClipState.Scene4);
                break;

            case UserTableSettingBase.StationState.mission2:
                UserTableGotoClip(UserTableSettingBase.StationState.mission2, GameManager.ClipState.Scene4);
                break;
            default:
                Debug.Log("틀림이 설정되어있지 않음");
                break;
        }
        Debug.Log("틀림");
    }

    public void EndMustScene(UserTableSettingBase.StationState stationState)
    {
        switch (stationState)
        {
            case UserTableSettingBase.StationState.mission1:
                UserTableGotoClip(UserTableSettingBase.StationState.mission1, GameManager.ClipState.Scene3);
                break;

            case UserTableSettingBase.StationState.mission2:
                UserTableGotoClip(UserTableSettingBase.StationState.mission2, GameManager.ClipState.Scene3);
                break;
            default:
                Debug.Log("틀림이 설정되어있지 않음");
                break;
        }
        Debug.Log("틀림");
    }

    //오브젝트 생성(원소 주기율표 정보 날라가기)
    public void CreateElementObject(UserTableAddElementParentObject.PlayState playState, 
        UserTableSettingBase.StationState mission) {

        userTableAddElementParentObject._stationState = mission;
        userTableAddElementParentObject.playState = playState;
        /*
        switch (playState) {
            case UserTableAddElementParentObject.PlayState.TimeOut:
                //타임 아웃
                userTableAddElementParentObject._stationState = UserTableSettingBase.StationState.mission1;
                userTableAddElementParentObject.playState = UserTableAddElementParentObject.PlayState.TimeOut;
                break;

            case UserTableAddElementParentObject.PlayState.RightAnswer:
                //정답 수행
                userTableAddElementParentObject._stationState = UserTableSettingBase.StationState.mission2;
                userTableAddElementParentObject.playState = UserTableAddElementParentObject.PlayState.RightAnswer;
                break;
        }*/
    }

    //스코어 텍스트 
    public void UserScoreTextMove(float move)
    {
        Vector3 localPos = userScoreText.transform.localPosition;
        /*DOTween.To(() => userScoreText.transform.localPosition,
            pos => userScoreText.transform.localPosition = pos,
            new Vector3(move, localPos.y, localPos.z), 0.5f);*/
    }

    //외부 json 파일 로드 완료 이벤트 핸들러 
    public void JsonLoadCompleteEvent(JsonLoadData jsonLoadData)
    {

        //**********************************************************************************************
        //연습 접수 부여
        //작은별 점수
        /**jsonLoadData.practiceScoreSetting.littleRotScore = practiceScore.littleRotScore;
        jsonLoadData.practiceScoreSetting.littleRotResultScore = practiceScore.littleRotResultScore;

        //큰별 점수 
        jsonLoadData.practiceScoreSetting.bigRotScore = practiceScore.bigRotScore;
        jsonLoadData.practiceScoreSetting.bigRotResultScore = practiceScore.bigRotResultScore;*/
        UserScoreText.PracticeScoreSetting _practiceScore = userScoreText.practiceScore;
        _practiceScore.littleRotScore = jsonLoadData.practiceScoreSetting.littleRotScore;
        _practiceScore.littleRotResultScore = jsonLoadData.practiceScoreSetting.littleRotResultScore;
        ballSetting.littleRotatingNumberMaxRot = jsonLoadData.practiceScoreSetting.littleRotatingNumberMaxRot;

        _practiceScore.bigRotScore = jsonLoadData.practiceScoreSetting.bigRotScore;
        _practiceScore.bigRotResultScore = jsonLoadData.practiceScoreSetting.bigRotResultScore;
        ballSetting.bigRotatingNumberMaxRot= jsonLoadData.practiceScoreSetting.bigRotatingNumberMaxRot;
        //작은별
        /*practiceScore.littleRotScore = jsonLoadData.practiceScoreSetting.littleRotScore;
        practiceScore.littleRotResultScore = jsonLoadData.practiceScoreSetting.littleRotResultScore;

        //큰별
        practiceScore.bigRotScore = jsonLoadData.practiceScoreSetting.bigRotScore;
        practiceScore.bigRotResultScore = jsonLoadData.practiceScoreSetting.bigRotResultScore;*/

        //**********************************************************************************************




    }


}
