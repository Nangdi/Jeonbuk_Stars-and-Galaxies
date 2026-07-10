using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

//사용자 문제 
public class UserTableQuestion : UserTableSettingControl
{
    //타임라인 변경 
    public bool isTimelineChange = false;
    
    public override void OnInit()
    {
        base.OnInit();
        QuestionSettig();

    }

    //문제 풀이 설정 컨트롤 구간
    public void QuestionSettig() {
        //공 정보 화면 표시 컨트롤
        ballSetting.mission1Text.SetText(ballSetting.littleInsertCountBall, ballSetting.bigInsertCountBall);
        ballSetting.mission2Text.SetText(ballSetting.littleInsertCountBall, ballSetting.bigInsertCountBall);
        ballSetting.ballCountData.SetText(1, ballSetting.bigInsertCountBall);

    }

    //현재 진행중인 클립정보
    public override GameSceneClip GetClipState() {
        List<PlayableBinding> playableBindings = pd.playableAsset.outputs.Cast<PlayableBinding>().ToList();

        int index = Array.FindIndex(playableBindings.ToArray(), item =>
            item.sourceObject != null && item.sourceObject.GetType().Equals(typeof(GameSceneTrack)));

        GameSceneClip gameSceneClip = null;

        if (index > -1)
        {
            PlayableBinding _playableBindings = playableBindings[index];
            GameSceneTrack sceneTrack = _playableBindings.sourceObject as GameSceneTrack;
            List<TimelineClip> timelineClips = sceneTrack.GetClips().Cast<TimelineClip>().ToList();
            List<GameSceneClip> sceneClips = Array.ConvertAll(timelineClips.ToArray(), item => item.asset as GameSceneClip).ToList();

            int resultIndex= Array.FindIndex(timelineClips.ToArray(), item => (item.start <= pd.time) && (item.end >= pd.time));
            if (resultIndex > -1) {
                gameSceneClip = sceneClips[resultIndex];
            }
        }
        return gameSceneClip;
    }

    //해당 클립으로이동
    public override void UserTableGotoClip(UserTableSettingBase.StationState _stationState, GameManager.ClipState _clipState) {
        List<PlayableBinding> playableBindings = pd.playableAsset.outputs.Cast<PlayableBinding>().ToList();
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
                    Debug.Log(_clipState.ToString());
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
        pd.Pause();
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
        //SetChangeTime(time);
        pd.time = time;
        pd.Evaluate();
        yield return null;
        pd.Play(); 
        yield return null;
        isTimelineChange = false;
    }


    //타임변경 이벤트 핸들러
    public void SetChangeTime(double time)
    {
        pd.time = time;
        pd.Evaluate();
    }
}
