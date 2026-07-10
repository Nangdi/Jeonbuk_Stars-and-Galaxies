using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Timeline;
using UnityEngine;
using UnityEngine.Playables;

[TrackColor(1.0f, 0.0f, 0.0f)]
//타입 매니저 정보 등록 함
[TrackBindingType(typeof(TimelineManager))]
[TrackClipType(typeof(GameSceneClip))]
public class GameSceneTrack: TrackAsset
{

    //트랙정보 표현 
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        
        ScriptPlayable<GameSceneMixerBehaviour> playable = ScriptPlayable<GameSceneMixerBehaviour>.Create(graph, inputCount);
        GameSceneMixerBehaviour gameSceneMixerBehaviour = playable.GetBehaviour();
        gameSceneMixerBehaviour.GetClips = this.GetClips().Cast<TimelineClip>().ToList();

        //클립 이름 재정의 구간 
        for (int i = 0; i < gameSceneMixerBehaviour.GetClips.Count; i++)
        {
            GameSceneClip sceneClip = gameSceneMixerBehaviour.GetClips[i].asset as GameSceneClip;
            if (!string.IsNullOrEmpty(sceneClip.clipName))
            {
                gameSceneMixerBehaviour.GetClips[i].displayName = sceneClip.gameState.ToString() + "_" + sceneClip.clipName;
            }
            else
            {
                gameSceneMixerBehaviour.GetClips[i].displayName = sceneClip.gameState.ToString();
            }
        }
        return playable;
    }

}

