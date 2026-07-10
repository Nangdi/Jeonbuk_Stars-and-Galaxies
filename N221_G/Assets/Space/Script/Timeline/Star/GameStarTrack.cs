using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(1.0f, 0.0f, 0.0f)]
[TrackClipType(typeof(GameStarClip))]
public class GameStarTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        ScriptPlayable<GameStarMixerBehaviour> playable = ScriptPlayable<GameStarMixerBehaviour>.Create(graph, inputCount);
        GameStarMixerBehaviour gameSceneMixerBehaviour = playable.GetBehaviour();
        gameSceneMixerBehaviour.GetClips = this.GetClips().Cast<TimelineClip>().ToList();
       
        //
        //클립 이름 재정의 구간 
        for (int i = 0; i < gameSceneMixerBehaviour.GetClips.Count; i++)
        {
            GameStarClip sceneClip = gameSceneMixerBehaviour.GetClips[i].asset as GameStarClip;
            gameSceneMixerBehaviour.GetClips[i].displayName = sceneClip.clipName.ToString();
            sceneClip.StarObject = sceneClip.eStarObject.Resolve(graph.GetResolver());
        }
        return playable;
    }
}
