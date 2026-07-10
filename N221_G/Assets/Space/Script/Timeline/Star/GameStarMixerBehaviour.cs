using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using static MainTableManager;
using static StarObject;

public class GameStarMixerBehaviour : PlayableBehaviour
{

    private List<TimelineClip> getClips;
    public List<TimelineClip> GetClips { get => getClips; set => getClips = value; }
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        int inputCount = playable.GetInputCount();

        for (int i = 0; i < inputCount; i++)
        {
            TimelineClip timelineClip = getClips[i];
            GameStarClip sceneClip = getClips[i].asset as GameStarClip;
            //타임 정보 확인 구간
            double time = playable.GetTime();
            int index = Array.FindIndex(getClips.ToArray(), item => ((TimelineClip)(item)).start < time && time < ((TimelineClip)(item)).end);
            if (index > -1)
            {
                timelineClip = getClips[index];
                sceneClip = getClips[index].asset as GameStarClip;
                Playable input = playable.GetInput(index);
                float normalizedInputTime = (float)(input.GetTime() / input.GetDuration());

                List<StarStepData> starStepDataList = MainTableManager.instance.starControl.starStepDataList;
                if (starStepDataList.Count > 0) {
                    StarObject starObject = sceneClip.StarObject;
                    if (starObject)
                    {

                        float duration = (float)input.GetDuration();
                        //Debug.Log("Duration  :"+ Duration);
                        starObject.timelineEvent.Invoke(sceneClip.clipType, duration);
                        
                    }
                }
            }
        }
    }


    private void StarSizeChange(StarObject starObject, StarStepData.Step step,float value) {
        List<StarSetting> starSettings = starObject.controlObject.starObjectList;
        for (int i=0;i < starSettings.Count; i++) {
            StarStepData starStepData = GetStarStepData(step);
            if (starStepData != null)
            {
                starObject.controlObject.starBackgrand.RadiusInner = starStepData.back_InnerSize * value;
                starObject.controlObject.starBackgrand.RadiusOuter = starStepData.back_OuterSize * value;
            }

        }

    }

    //절차 정보 
    private StarStepData GetStarStepData(StarStepData.Step step) {
        List<StarStepData> starStepDataList = MainTableManager.instance.starControl.starStepDataList;
        int index=Array.FindIndex(starStepDataList.ToArray(), item => item.step.Equals(step));
        
        if (index > -1)
        {
            return starStepDataList[index];
        }
        else {
            return null;
        }
    }


}
