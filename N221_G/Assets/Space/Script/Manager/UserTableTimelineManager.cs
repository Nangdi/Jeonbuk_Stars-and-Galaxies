using RenderHeads.Media.AVProVideo;
using RenderHeads.Media.AVProVideo.Demos;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using static UserTableTimelineManager;
using static VideoFileLoadManager;

/// <summary>
/// 사용자 테이블 타임라인 매니저
/// </summary>
public class UserTableTimelineManager : TimelineManager
{

    public enum Mission { mission1 ,mission2, mission3 }
    public Mission mission;

    //별 상태 
    public enum StarState {None, StarBirth, StarEvolution, StarDeath }
    
    //비디오 단계
    public enum VideoStep { Step_1/*강하게*/, Step_2/*약하게*/}

    [Header("현재 별의 상태 정보")]
    public StarState currentStarState;

    #region 미디어 플레이어 교차 진행을 위한 비디오 플레이어 2 별의 진화 과정 영상 따로 구사

    //생성
    public MediaPlayer mediaPlayerBirth;
    //진화
    public MediaPlayer mediaPlayerEvolution;
    //죽음
    public MediaPlayer mediaPlayerDeath;

    #endregion

    public int currentBall = 0;
    public int currentHandleData = 0;

    //먼지 컨트롤
    public DustControl dustControl;

    [System.Serializable]
    public class DustControl {

        [Header("먼지 오브젝트")]
        public GameObject dustObject;
        public Transform parentDustObject;

        public List<Transform> pointList;
       
        //포인트 인덱스 배열정보
        public List<int> selectPointIndex;
        public System.Random random;
        public int[] randomIndexArray;

    }

    public DisplayUGUI starBirthDU;
    public DisplayUGUI starEvolutionDU;
    public DisplayUGUI starDeathDU;

    private System.Diagnostics.Stopwatch stopwatch = null;

    public GameManager.GameState initTimelineGameState;
    public bool isReady = false;


    //비디오 오브젝트 로드 완료 테스트
    public override void OnInit()
    {
        base.OnInit();
        Debug.Log("[UserTableTimelineManager]");
        //VideoLoadComplete();
        dustControl.random = new System.Random();
        OnResetData();
       
        stopwatch = new System.Diagnostics.Stopwatch();
        StartLoop();
        OnInitTimeline();
    }

    private void OnInitTimeline() {
        GoToSceneClip(initTimelineGameState);
    }

    //비디오 재생 컴플렛트 이벤트 핸들러 
    private void VideoLoadComplete() {
        VideoReference currentVideoReference = VideoFileLoadManager.instance.GetVideoReference(StarState.StarBirth);
        mediaPlayerBirth.OpenMedia(currentVideoReference.mediaReference,false);
    }

    /// <summary>
    /// 미디어 플레이어 이벤트 핸들러
    /// </summary>
    /// <param name="_mediaPlayer"></param>
    /// <param name="_eventType"></param>
    /// <param name="errorCode"></param>
    public void MediaPlayerEventHandler(MediaPlayer _mediaPlayer, MediaPlayerEvent.EventType _eventType, ErrorCode errorCode) {
        Debug.Log(_eventType.ToString());
        switch (_eventType) {
            case MediaPlayerEvent.EventType.FirstFrameReady://재생준비완료
                Debug.Log("[FirstFrameReady]  : ");
                isReady = true;
                break;

           /** case MediaPlayerEvent.EventType.PlaylistFinished:
                Debug.Log("[PlaylistFinished]  :  영상 종료 작동 구간 ");

                //준비 단계 초기화
                isReady = false;
                videoSeekData = 0;
                break;*/

            //FinishedPlaying
            case MediaPlayerEvent.EventType.FinishedPlaying:
                Debug.Log("[PlaylistFinished]  :  영상 종료 작동 구간 ");
                isReady = false;
                videoSeekData = 0;
                break;
        }
    }

    //1. 사용자 입력
    private void InsertUser() {

        Debug.Log("[InsertUser]");
        GameManager.GameState gameState=GetSceneClip();
        if (!gameState.Equals(GameManager.GameState.사용자대기카운터진행))
        {
            //사용자 대기 카운터 진행 
            GoToSceneClip(GameManager.GameState.사용자대기카운터진행);
        }
        //사용자 카운터 진행
        MainTableManager.instance.InsertUser();
    }

    //전단계 회전데이터 입력신호 정보
    private float elapsedMilliseconds = 0;
    
    /// <summary>
    /// 회전 핸들 데이터
    /// </summary>
    public void InsertRotationHandle() {
        GameManager.GameState gameState = GetSceneClip();
        switch (gameState) {
            case GameManager.GameState.초기화면:
                InsertUser();
                break;
            case GameManager.GameState.회전판별생성:
                //회전시 영상 재생시작하는구간
                //Debug.Log("회전 데이터 확인 구간");
               
                break;
        }

        stopwatch.Stop();
        
        //기존 디폴트 속도 
        if (elapsedMilliseconds == 0) {
            currentStarState = StarState.StarBirth;
        }

        switch (currentStarState) {
            case StarState.StarBirth:

                if (!mediaPlayerBirth.MediaOpened)
                {
                    VideoPlayerBirth();
                }

                break;
            case StarState.StarEvolution:

                //if (!mediaPlayerEvolution.MediaOpened)
                //{
                //    VideoPlayerEvolution();
                //}

                break;

            case StarState.StarDeath:

                //if (!mediaPlayerDeath.MediaOpened)
               // {
                //    VideoPlayerStarDeath();
                //}

                break;
        }

        Debug.Log("회전 속도 정보 :"+stopwatch.ElapsedMilliseconds+" ms");
        elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        videoSeekData += 1f;
        TurnHandleScore();
        stopwatch.Restart();

    }







    private Coroutine onLoop = null;
    public float videoSeekData = 0;

    [Header("비디오 셋팅")]
    public VideoSpeed videoSpeed;

    [System.Serializable]
    public class VideoSpeed {
        public float smoothTime = 0.3f;
        public float yVelocity = 0.0f;
    }

    private void StartLoop() {
        RemoveOnLoop();
        onLoop = StartCoroutine(OnLoop());
    }

    private void RemoveOnLoop() {
        if (onLoop != null) {
            StopCoroutine(onLoop);
            onLoop = null;
        }
    }

    /// <summary>
    /// 영상 속도 제어를 위한 루프 구간
    /// </summary>
    /// <returns></returns>
    IEnumerator OnLoop() {
        while (true) {
            yield return null;
            if (isReady)
            {
                switch (currentStarState)
                {
                    case StarState.StarBirth:
                        if (mediaPlayerBirth.MediaOpened)
                        {
                            starBirthDU.gameObject.SetActive(true);
                            starEvolutionDU.gameObject.SetActive(false);
                            starDeathDU.gameObject.SetActive(false);
                            double GetCurrentTimeFramesTime = mediaPlayerBirth.Control.GetCurrentTime();
                            float value = Mathf.SmoothDamp((float)GetCurrentTimeFramesTime, videoSeekData, ref videoSpeed.yVelocity, videoSpeed.smoothTime);
                            
                            mediaPlayerBirth.Control.Seek(value);
                            int frame = mediaPlayerBirth.Control.GetCurrentTimeFrames();
                            int duration = mediaPlayerBirth.Info.GetDurationFrames();
                            float currentFramePercent = (float)frame / (float)duration;

                            if (currentFramePercent * 100 > 99f)
                            {
                                currentStarState = StarState.StarEvolution;
                                VideoPlayerEvolution();
                                isReady = false;
                                videoSeekData = 0;
                              
                            }
                        }
                        else {
                            Debug.Log("미디어 맏힘");
                        }
                        break;
                    case StarState.StarEvolution:
                        if (mediaPlayerEvolution.MediaOpened)
                        {
                            starEvolutionDU.gameObject.SetActive(true);
                            starBirthDU.gameObject.SetActive(false);
                            starDeathDU.gameObject.SetActive(false);
                            double GetCurrentTimeFramesTime = mediaPlayerEvolution.Control.GetCurrentTime();
                            float value = Mathf.SmoothDamp((float)GetCurrentTimeFramesTime, videoSeekData, ref videoSpeed.yVelocity, videoSpeed.smoothTime);
                           
                            mediaPlayerEvolution.Control.Seek(value);

                            int frame = mediaPlayerEvolution.Control.GetCurrentTimeFrames();
                            int duration = mediaPlayerEvolution.Info.GetDurationFrames();
                            float currentFramePercent = (float)frame / (float)duration;

                            if (currentFramePercent * 100 > 99f)
                            {
                                currentStarState = StarState.StarDeath;
                                VideoPlayerStarDeath();
                                isReady = false;
                                videoSeekData = 0;
                            }
                        }
                        break;

                    case StarState.StarDeath:
                        if (mediaPlayerDeath.MediaOpened)
                        {
                            if (!isStarDeath) {
                                isStarDeath = true;
                                starBirthDU.gameObject.SetActive(false);
                                starEvolutionDU.gameObject.SetActive(false);
                                starDeathDU.gameObject.SetActive(true);
                                //Debug.Log("[StarDeath]");
                                EndExplosionScore();
                                mediaPlayerDeath.Control.Seek(0);
                                mediaPlayerDeath.Control.Play();
                            }
                          
                            /**
                            starDeathDU.gameObject.SetActive(true);
                            double GetCurrentTimeFramesTime = mediaPlayerDeath.Control.GetCurrentTime();
                            float value = Mathf.SmoothDamp((float)GetCurrentTimeFramesTime, videoSeekData, ref videoSpeed.yVelocity, videoSpeed.smoothTime);
                           
                            mediaPlayerDeath.Control.Seek(value);

                            int frame = mediaPlayerDeath.Control.GetCurrentTimeFrames();
                            int duration = mediaPlayerDeath.Info.GetDurationFrames();
                            float currentFramePercent = (float)frame / (float)duration;

                            if (currentFramePercent * 100 > 99f)
                            {
                                Debug.Log("최종 영상 종료 시점");
                                //currentStarState = StarState.StarDeath;
                                isReady = false;
                                videoSeekData = 0;
                            }*/
                        }
                        break;
                }
            }
        }
    }


    private bool isStarDeath = false;

    //별의 탄생 영상 재생 시작 
    private void VideoPlayerBirth() {
        if (!mediaPlayerBirth.MediaOpened) {
            VideoReference currentVideoReference = VideoFileLoadManager.instance.GetVideoReference(StarState.StarBirth);
            mediaPlayerBirth.OpenMedia(currentVideoReference.mediaReference, false);
            Debug.Log("VideoPlayerBirth] : ");
        }
    }

    private void VideoPlayerEvolution()
    {
        if (!mediaPlayerEvolution.MediaOpened)
        {
            VideoReference currentVideoReference = VideoFileLoadManager.instance.GetVideoReference(StarState.StarEvolution);
            mediaPlayerEvolution.OpenMedia(currentVideoReference.mediaReference, false);
            Debug.Log("VideoPlayerEvolution] : ");
        }
    }

    private void VideoPlayerStarDeath()
    {
        if (!mediaPlayerDeath.MediaOpened)
        {
            VideoReference currentVideoReference = VideoFileLoadManager.instance.GetVideoReference(StarState.StarDeath);
            mediaPlayerDeath.OpenMedia(currentVideoReference.mediaReference, false);
            Debug.Log("VideoPlayerEvolution] : ");
        }
    }

    /// <summary>
    /// 공투입  :  타임라인과 별도로 구현 시작
    /// </summary>
    public void InsertBall()
    {

        if (currentBall < 5)
        {
            GameManager.GameState gameState = GetSceneClip();
            switch (gameState)
            {
                case GameManager.GameState.초기화면:
                    Debug.Log("초기화면에서 공투입함");
                    break;

                case GameManager.GameState.공투입:
                    Debug.Log("공투입 진행 시작하는구간 입력 확인 필요");
                    OnInsertDustObject(currentBall);
                    break;
            }
            ++currentBall;
            Debug.Log("현재 입력 공 카운터 : "+currentBall);
        }
        else {
            Debug.Log("공이 최대로 입력되었음");
        }
       
    }


    //먼지구름 등록
    private void OnInsertDustObject(int currentBall) {
        GameObject targetGameObject=GameObject.Instantiate(
            dustControl.dustObject,
            dustControl.parentDustObject);
        targetGameObject.transform.position = dustControl.pointList[currentBall].position;
        
        //배열 섞기 진행
        //Debug.Log(dustControl.randomIndexArray.Length);
        //Debug.Log("공투입 정보" + currentBall);
        //Debug.Log("카운터 정보 : "+dustControl.pointList.Count);

    }




    //볼 배출
    public void BallOut() {
        Debug.Log("공 배출 메세지 전송");
        OnResetBall();

    }


    /// <summary>
    /// 데이터 초기화
    /// </summary>
    public void OnResetData() {
        OnResetBall();
        OnRandomPointIndex();
    }

    private void OnResetBall() {
        currentBall = 0;
    }

    private void OnRandomPointIndex() {
        dustControl.randomIndexArray = dustControl.selectPointIndex.OrderBy(x => dustControl.random.Next()).ToArray();
    }


    //타임 완료 이벤트 
    public void UserInsertTimeCompleteEvent() {
        Debug.Log("[UserInsertTimeCompleteEvent]");
        //다음씬으로 이동
        GoToSceneClip(GameManager.GameState.공투입);

    }



    #region 상속 영역
    /// <summary>
    /// 사용자 점수 
    /// </summary>
    public virtual void UserScore() { }
    //회전핸들 점수
    public virtual void TurnHandleScore() { }
    //종료 폭발점수 
    public virtual void EndExplosionScore() { }
    #endregion

}
