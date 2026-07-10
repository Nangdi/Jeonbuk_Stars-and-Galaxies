using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class GameSceneMixerBehaviour: PlayableBehaviour
{
    private List<TimelineClip> getClips;
    public List<TimelineClip> GetClips { get => getClips; set => getClips = value; }
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        Debug.Log("OnBehaviourPause]");
    }


    public override void OnPlayableDestroy(Playable playable)
    {

        Debug.Log("OnPlayableDestroy]");
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        
        int inputCount = playable.GetInputCount();
       // Debug.Log("재생중"+ inputCount);
        for (int i = 0; i < inputCount; i++)
        {
            TimelineClip timelineClip = getClips[i];
            
            //게임메인씬 트랙정보
            GameSceneTrack gameSceneTrack= (GameSceneTrack)timelineClip.GetParentTrack();
            GameSceneClip sceneClip = getClips[i].asset as GameSceneClip;
            //타임 정보 확인 구간
            double time = playable.GetTime();
            int index = Array.FindIndex(getClips.ToArray(), item => ((TimelineClip)(item)).start < time && time < ((TimelineClip)(item)).end);
            if (index > -1)
            {
                timelineClip = getClips[index];
                sceneClip = getClips[index].asset as GameSceneClip;
                Playable input = playable.GetInput(index);
                float normalizedInputTime = (float)(input.GetTime() / input.GetDuration());
                //퍼센트 구성으로만 사용 
                //이벤트 접근을 확인 
                if (playerData!=null)
                {
                    TimelineManager timelineManager = playerData as TimelineManager;
                    Debug.Log(timelineManager);

                }
                /**switch (sceneClip.sceneStats)
                {
                    case GameSceneClip.SceneStats.OnPlay:
                       // Debug.Log("재생중");
                        break;

                    case GameSceneClip.SceneStats.OnPause:

                        //Debug.Log("[OnPause] : "+ normalizedInputTime);
                        break;
                }*/

            }
        }
    }
}

