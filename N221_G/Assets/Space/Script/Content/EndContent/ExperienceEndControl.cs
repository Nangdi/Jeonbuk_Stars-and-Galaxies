using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using static UserTableSettingBase;

//미션 비디오 재생 여부 판단
public class ExperienceEndControl : MonoBehaviour
{
    //활성화 작동

    private Coroutine onInit = null;
    public MediaPlayer endVideoPlayer;
    //GUI
    public GameObject endVideoPlayerGUI;
    public GameObjectControl gameObjectControl;
   

    public void OnEnable()
    {

        if (endVideoPlayer.Control != null)
        {
            endVideoPlayer.Control.SeekFast(0);
        }
        OnInitStart();
    }

    public void OnDisable()
    {
        endVideoPlayer.Control.SeekFast(0);
        OnRemoveInit();
        if (endVideoPlayerGUI)
        {
            endVideoPlayerGUI.SetActive(false);
        }
    }

    private void OnInitStart() {
        OnRemoveInit();
        onInit = StartCoroutine(OnInit());
    }

    private void OnRemoveInit() {
        if (onInit != null) {
            StopCoroutine(onInit);
            onInit = null;
        }
    }
    //시작 커맨드 설정
    IEnumerator OnInit()
    {
        Debug.Log("OnInit] : ");
        isVideoFinished = false;
        endVideoPlayer.Control.SeekFast(0);
        yield return null;
        //점수 정보 토탈 
        GameObjectControl.Instance.ScoreRankingControl();
        //존재하는 엘리먼트 삭제
        GameObjectControl.Instance.RemoveElementObject();
        //시작 컨트롤(하드웨어장비작동X)
        GameObjectControl.Instance.IsMissionPlayAllUserList(false);
        //랭킹데이터 정보 화면에 표시
        GameObjectControl.Instance.SettingRankDatas();
        //생성된 엘리먼트 정보 삭제 (초기화 하는구간)
        GameObjectControl.Instance.RemoveElementObject();
        MainTableManager.instance.RemoveStar();

        //'미션 종료' 문구 오브젝트처럼 영상 GUI가 연결되지 않은 인스턴스는 여기까지만 처리
        if (endVideoPlayerGUI == null) yield break;

        MissionDataLoader.TimeSetting timeSetting = GetTimeSetting();
        float textTime = timeSetting != null ? timeSetting.experienceEndTextTime : -1f;
        float videoTime = timeSetting != null ? timeSetting.experienceEndVideoTime : 0f;

        //타임라인이 영상 GUI를 켤 때까지 대기 (기본 약 5초).
        //textTime(초)이 지나도 안 켜졌으면 메인 타임라인을 영상 시작 지점으로 넘겨 대기 시간을 줄인다.
        //프레임 시간이 밀려도 어긋나지 않게 실제 시간(realtime) 기준으로 잰다
        float waitStart = Time.realtimeSinceStartup;
        float waited = 0f;
        bool skipped = false;
        while (!endVideoPlayerGUI.activeSelf)
        {
            waited = Time.realtimeSinceStartup - waitStart;
            if (!skipped && textTime >= 0f && waited >= textTime)
            {
                skipped = true;
                SkipTimelineToEndVideo();
            }
            yield return null;
        }
        waited = Time.realtimeSinceStartup - waitStart;
        Debug.Log("[ExperienceEnd] 전환 영상 시작 (문구 표시 " + waited.ToString("F1") + "초" + (skipped ? ", 타임라인 단축" : "") + ")");
        //영상 재생 시작 (설정한 시간 안에 영상 전체가 끝나도록 배속 적용)
        float rate = GetVideoPlaybackRate(videoTime);
        endVideoPlayer.PlaybackRate = rate;
        endVideoPlayer.Control.SeekFast(0);
        endVideoPlayer.Control.Play();

        RemoveElementObject();
        //존재하는 엘리먼트 삭제
        GameObjectControl.Instance.RemoveElementObject();
        MainTableManager.instance.RemoveStar();

        //안전장치: 배속을 걸었는데 영상이 멈추거나 이벤트가 안 와서 끝나지 않으면
        //실제 시간 (설정 시간 + 5초)이 지난 뒤 영상이 끝난 것처럼 다음 단계(미션1)로 넘어간다
        if (videoTime > 0f)
        {
            float videoStart = Time.realtimeSinceStartup;
            while (!isVideoFinished && gameObject.activeInHierarchy &&
                   Time.realtimeSinceStartup - videoStart < videoTime + 5f)
            {
                yield return null;
            }
            if (!isVideoFinished && gameObject.activeInHierarchy)
            {
                Debug.LogWarning("[ExperienceEnd] 전환 영상이 " + (videoTime + 5f) + "초 안에 끝나지 않아 다음 단계로 이동");
                endVideoPlayer.Control.Pause();
                OnVideoFinished();
            }
        }
    }

    /// <summary>
    /// 전환 영상 전체가 targetSeconds 안에 끝나도록 배속을 구한다.
    /// 예) 영상 30초, 설정 15초 → 2배속. 0 이하이거나 영상이 설정보다 짧으면 1배속(원래 속도)
    /// </summary>
    private float GetVideoPlaybackRate(float targetSeconds)
    {
        if (targetSeconds <= 0f || endVideoPlayer.Info == null) return 1f;
        double duration = endVideoPlayer.Info.GetDuration();
        if (duration <= 0 || duration <= targetSeconds)
        {
            Debug.Log("[ExperienceEnd] 전환 영상 1배속 (영상 " + duration.ToString("F1") + "초, 설정 " + targetSeconds + "초)");
            return 1f;
        }
        float rate = (float)(duration / targetSeconds);
        Debug.Log("[ExperienceEnd] 전환 영상 " + rate.ToString("F2") + "배속 (영상 " + duration.ToString("F1") + "초 → " + targetSeconds + "초, 설정 experienceEndVideoTime)");
        return rate;
    }

    //영상 완료 여부 (안전장치와 FinishedPlaying 이벤트가 겹쳐 두 번 넘어가지 않게).
    //영상 재생은 '미션비디오' 인스턴스가, FinishedPlaying 이벤트는 '미션종료' 인스턴스가 받으므로 static으로 공유한다.
    private static bool isVideoFinished = false;

    private MissionDataLoader.TimeSetting GetTimeSetting()
    {
        GameObjectControl goc = GameObjectControl.Instance;
        if (goc == null || goc.missionDataLoader == null || goc.missionDataLoader.jsonLoadData == null) return null;
        return goc.missionDataLoader.jsonLoadData.timeSetting;
    }

    /// <summary>
    /// 메인 타임라인을 전환 영상 GUI가 켜지는 시점으로 이동한다.
    /// 영상 GUI를 켜는 Activation 클립 위치는 하위 타임라인(연습게임)에서 찾고,
    /// 하위/메인 타임라인의 시간 차이를 더해 메인 타임라인 시간으로 바꾼다.
    /// </summary>
    private void SkipTimelineToEndVideo()
    {
        PlayableDirector mainDirector = gameObjectControl != null ? gameObjectControl.playableDirector : null;
        PlayableDirector nestedDirector = null;
        for (Transform t = endVideoPlayerGUI.transform.parent; t != null; t = t.parent)
        {
            nestedDirector = t.GetComponent<PlayableDirector>();
            if (nestedDirector != null) break;
        }
        TimelineAsset nestedTimeline = nestedDirector != null ? nestedDirector.playableAsset as TimelineAsset : null;
        if (mainDirector == null || nestedTimeline == null)
        {
            Debug.LogWarning("[ExperienceEnd] 타임라인을 찾지 못해 문구 표시 시간을 줄이지 못했습니다.");
            return;
        }

        double activateTime = -1;
        foreach (TrackAsset track in nestedTimeline.GetOutputTracks())
        {
            if (!(track is ActivationTrack)) continue;
            if (nestedDirector.GetGenericBinding(track) as GameObject != endVideoPlayerGUI) continue;
            foreach (TimelineClip clip in track.GetClips())
            {
                if (clip.start >= nestedDirector.time && (activateTime < 0 || clip.start < activateTime))
                    activateTime = clip.start;
            }
        }
        if (activateTime < 0)
        {
            Debug.LogWarning("[ExperienceEnd] 전환 영상 Activation 클립을 찾지 못했습니다.");
            return;
        }

        double target = activateTime - (nestedDirector.time - mainDirector.time) + 0.02;
        if (target <= mainDirector.time) return;
        Debug.Log("[ExperienceEnd] 메인 타임라인 " + mainDirector.time.ToString("F2") + " → " + target.ToString("F2") + " 이동 (전환 영상 시작 앞당김)");
        gameObjectControl.PlayableDirectorPlay(target);
    }


    public void PlayerEvent(MediaPlayer mp, MediaPlayerEvent.EventType eventType, ErrorCode errorCode)
    {
        Debug.Log(eventType);
        switch (eventType)
        {
            case MediaPlayerEvent.EventType.ReadyToPlay:
                
                Debug.Log("ReadyToPlay]");
                break;
            case MediaPlayerEvent.EventType.FirstFrameReady:

                break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                Debug.Log("영상 완료 이벤트");
                OnVideoFinished();
                break;
        }
    }

    //전환 영상 종료 처리 (영상 끝 또는 최대 재생 시간 도달)
    private void OnVideoFinished()
    {
        if (isVideoFinished) return;
        isVideoFinished = true;
        //UserTableSettingBase.StationState _stationState,
        //GameManager.ClipState _clipState
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(OnEndNextScene());
        }
        GameObjectControl.Instance.RemoveElementObject();
        MainTableManager.instance.RemoveStar();
    }

    //미션 종료 씬 이동
    IEnumerator OnEndNextScene() {
        yield return null;
        Debug.Log("공배출 [하드웨어 초기화]");
        //미션 1시작 지점
        gameObjectControl.GotoSceneClip(UserTableSettingBase.StationState.experience, GameManager.ClipState.Scene2);
        gameObjectControl.stationState = UserTableSettingBase.StationState.mission1;
        for (int i=0;i < gameObjectControl.userTableSettingList.Count; i++) {
            UserTableController userTableController = gameObjectControl.userTableSettingList[i];
            if (userTableController.ballSetting.insertBallCount > 0)
            {
                GameObjectControl.Instance.rsControl.multiRS.SendConsole(i, "1");
                Debug.Log("리셋 진행 사용자" + i);
            }
        }
        OnResetData("1");
        yield return new WaitForSeconds(0.01f);

        for (int i = 0; i < gameObjectControl.userTableSettingList.Count; i++)
        {
            //GameObjectControl.Instance.rsControl.multiRS.SendConsole(i, "0");
            gameObjectControl.userTableSettingList[i].ballSetting.insertBallCount = 0;
            gameObjectControl.userTableSettingList[i].ballSetting.littleSaveRotatingDeviceCount = 0;
            gameObjectControl.userTableSettingList[i].ballSetting.bigSaveRotatingDeviceCount = 0;
            //gameObjectControl.userTableSettingList[i].ballSetting.m2_insertBallText.OnTextHide(false);
            //gameObjectControl.userTableSettingList[i].ballSetting.m1_insertBallText.OnTextHide(false);
            gameObjectControl.userTableSettingList[i].ballSetting.rotatingDeviceCount = 0;
            //GameObjectControl.Instance.rsControl.multiRS.SendConsole(i, "0");
            GameObjectControl.Instance.userTableSettingList[i].rotatingDeviceEvent.Invoke();
        }

        yield return new WaitForSeconds(1f);
       
        OnResetData("0");
        

        GameObjectControl.Instance.RemoveElementObject();
        MainTableManager.instance.RemoveStar();
        
        //시작 컨트롤(하드웨어장비작동O)
        GameObjectControl.Instance.IsMissionPlayAllUserList(true);
    }

    

    //데이터 리셋
    private void OnResetData(string data) {
        for (int i = 0; i < gameObjectControl.userTableSettingList.Count; i++)
        {
            for (int j = 0; j < 40; j++)
            {
                UserTableController userTableController = gameObjectControl.userTableSettingList[i];
                if (userTableController.ballSetting.insertBallCount > 0)
                {
                    GameObjectControl.Instance.rsControl.multiRS.SendConsole(i, data);
                    Debug.Log("리셋 진행 사용자" + i);
                }
            }
        }
    }

    private void RemoveElementObject() {
        ElementObject[] _elementObject= GameObjectControl.Instance.elementParentObject.GetComponentsInChildren<ElementObject>();
        for (int i= _elementObject.Length-1; i>=0; i--) {
            GameObject.Destroy(_elementObject[i].gameObject);
        }
    }

}
