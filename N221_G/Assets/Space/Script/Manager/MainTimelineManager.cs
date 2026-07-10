using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class MainTimelineManager : MonoBehaviour
{
    //메인 타임라인 설정 
    public PlayableDirector playableDirector;
    
    //신호 연동
    public RsControl rsControl;

    //타임라인 체인지
    public bool isTimelineChange = false;

    public void GotoSceneClip(UserTableSettingBase.StationState _stationState, 
        GameManager.ClipState _clipState) {
        List<PlayableBinding> playableBindings = playableDirector.playableAsset.outputs.Cast<PlayableBinding>().ToList();
        for (int i = 0; i < playableBindings.Count; i++)
        {
            PlayableBinding _playableBindings = playableBindings[i];
            if (_playableBindings.sourceObject != null && _playableBindings.sourceObject.GetType().Equals(typeof(GameSceneTrack)))
            {
                GameSceneTrack sceneTrack = _playableBindings.sourceObject as GameSceneTrack;
                List<TimelineClip> timelineClips = sceneTrack.GetClips().Cast<TimelineClip>().ToList();
                List<GameSceneClip> sceneClips = Array.ConvertAll(timelineClips.ToArray(), item => item.asset as GameSceneClip).ToList();

                int index = Array.FindIndex(sceneClips.ToArray(), item =>
                    item.stationState.Equals(_stationState) && item.clipState.Equals(_clipState));
              
                if (index > -1)
                {
                    Debug.Log(timelineClips[index].start);
                    PlayableDirectorPlay(timelineClips[index].start);
                    break;
                }
            }
        }
    }

    //시간 변경 코루틴 
    public void PlayableDirectorPlay(double time)
    {
        isTimelineChange = true;
        playableDirector.Pause();
        RemovePlayableDirectorPlayCoroutine();
        onPlayableDirectorPlayCoroutine = StartCoroutine(OnPlayableDirectorPlayCoroutine(time));
    }

    //플레이어 코루틴
    private Coroutine onPlayableDirectorPlayCoroutine = null;
    private void RemovePlayableDirectorPlayCoroutine()
    {
        if (onPlayableDirectorPlayCoroutine != null)
        {
            StopCoroutine(onPlayableDirectorPlayCoroutine);
            onPlayableDirectorPlayCoroutine = null;
        }
    }


    IEnumerator OnPlayableDirectorPlayCoroutine(double time)
    {
      
        yield return null; 
        Debug.Log("playableDirector Play1");
        SetChangeTime(time);
        playableDirector.time = time;
        playableDirector.Evaluate();
        yield return null;
        Debug.Log("playableDirector Play2");
        playableDirector.Play();
        yield return null;
        isTimelineChange = false;
    }
    //타임변경 이벤트 핸들러
    public void SetChangeTime(double time)
    {
        playableDirector.time = time;
        playableDirector.Evaluate();
    }
}
