

using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Collections;
using UnityEngine.SceneManagement;

//타임라인 매니저
public class TimelineManager:MonoBehaviour
{
    [Header("테이블 사용자 번호 정보")]
    public int tableUserIndex = 0;

    public PlayableDirector playableDirector;
    #region  클립 컨트롤 이벤트 핸들러

    public enum ClipType { None, Start, End }

    public static string currentSceneName_Start = "";
    public static string currentSceneName_End = "";

    public delegate void ClipEventDelegate(ClipType clipType, string sceneName, string ClipName);

    public static event ClipEventDelegate ClipEvent;

    //클립상황에 따른이벤트 핸들러
    public static void ClipEvented(ClipType clipType, string sceneName, string ClipName)
    {
        TimelineManager timelineManager = GameManager.instance.managerData.timelineManager;

        //시작 부분 이벤트 처리 구간
        GameManager.GameState _sceneName = timelineManager.GetSceneClip();

        switch (clipType)
        {
            case TimelineManager.ClipType.Start:
                Debug.Log("[Start]");
                /*if (sceneName.Equals(_sceneName.ToString()) &&
                    !currentSceneName_Start.Equals(sceneName))
                {
                    currentSceneName_Start = sceneName;
                    ClipEvent?.Invoke(clipType, sceneName, ClipName);

                }*/
                break;

            case TimelineManager.ClipType.End:
                Debug.Log("[End]");

                /*if (sceneName.Equals(_sceneName.ToString()) &&
                    !currentSceneName_End.Equals(sceneName))
                {
                    currentSceneName_End = sceneName;
                    ClipEvent?.Invoke(clipType, sceneName, ClipName);

                }*/
                break;

        }
    }
    #endregion

    public void Start()
    {
        OnInit();
    }


    public virtual void OnInit() {
        OnEvent();
        GoToSceneClip(GameManager.GameState.초기화면);
    }

    //현재의 클립위치 정보
    public GameManager.GameState GetSceneClip()
    {
        GameManager.GameState gameState = GameManager.GameState.초기화면;
        List<PlayableBinding> playableBindings = playableDirector.playableAsset.outputs.Cast<PlayableBinding>().ToList();
        for (int i = 0; i < playableBindings.Count; i++)
        {
            PlayableBinding _playableBindings = playableBindings[i];
            if (_playableBindings.sourceObject != null && _playableBindings.sourceObject.GetType().Equals(typeof(GameSceneTrack)))
            {
                GameSceneTrack sceneTrack = _playableBindings.sourceObject as GameSceneTrack;
                List<TimelineClip> timelineClips = sceneTrack.GetClips().Cast<TimelineClip>().ToList();
                int index = Array.FindIndex(timelineClips.ToArray(), item => item.start < playableDirector.time && item.end > playableDirector.time);
                if (index > -1)
                {
                    List<GameSceneClip> sceneClips = Array.ConvertAll(timelineClips.ToArray(), item => item.asset as GameSceneClip).ToList();
                    gameState = sceneClips[index].gameState;
                    break;
                }
            }
        }
        return gameState;
    }



    //게임상태에 해당하는 타임 클립으로 이동
    public void GoToSceneClip(GameManager.GameState gameState)
    {
        List<PlayableBinding> playableBindings = playableDirector.playableAsset.outputs.Cast<PlayableBinding>().ToList();
        for (int i = 0; i < playableBindings.Count; i++)
        {
            PlayableBinding _playableBindings = playableBindings[i];
            if (_playableBindings.sourceObject != null && _playableBindings.sourceObject.GetType().Equals(typeof(GameSceneTrack)))
            {
                GameSceneTrack sceneTrack = _playableBindings.sourceObject as GameSceneTrack;
                List<TimelineClip> timelineClips = sceneTrack.GetClips().Cast<TimelineClip>().ToList();
                List<GameSceneClip> sceneClips = Array.ConvertAll(timelineClips.ToArray(), item => item.asset as GameSceneClip).ToList();
                
                int index = Array.FindIndex(sceneClips.ToArray(), item => item.gameState.Equals(gameState));
                if (index > -1)
                {

                    PlayableDirectorPlay(timelineClips[index].start);
                    break;
                }
            }
        }
    }


    private Coroutine onPlayableDirectorPlayCoroutine = null;
    private void RemovePlayableDirectorPlayCoroutine()
    {
        if (onPlayableDirectorPlayCoroutine != null)
        {
            StopCoroutine(onPlayableDirectorPlayCoroutine);
            onPlayableDirectorPlayCoroutine = null;
        }
    }

    //시간 변경 코루틴 
    public void PlayableDirectorPlay(double time)
    {
        playableDirector.Pause();
        RemovePlayableDirectorPlayCoroutine();
        onPlayableDirectorPlayCoroutine = StartCoroutine(OnPlayableDirectorPlayCoroutine(time));
    }

    IEnumerator OnPlayableDirectorPlayCoroutine(double time) {
        yield return null;
        SetChangeTime(time);
        yield return null;
        playableDirector.Play();
    }


    //타임변경 이벤트 핸들러
    public void SetChangeTime(double time)
    {
        playableDirector.time = time;
        playableDirector.Evaluate();
    }

    /// <summary>
    /// 이벤트
    /// </summary>
    private void OnEvent()
    {

    }

}

