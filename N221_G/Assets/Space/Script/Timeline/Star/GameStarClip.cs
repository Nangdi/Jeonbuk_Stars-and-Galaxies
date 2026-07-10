using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using static MainTableManager;


//별 게임 클립추가 컨트롤
public class GameStarClip : PlayableAsset, ITimelineClipAsset, IPropertyPreview
{
    [Header("화면 표시용으로 개발")]
    public string clipName = "";

    public enum ClipType { Init,blackHoleCreate_start, StarCreate_Start }
    [Header("클립 타입")]
    public ClipType clipType;

    //[Header("스탭 정보")]
    //public StarStepData.Step step;

    public ExposedReference<StarObject> eStarObject;
    private StarObject starObject;

    public StarObject StarObject { get => starObject; set => starObject = value; }

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        //저장 변수 만들기 ??
        //SceneBehaviour
        ScriptPlayable<GameStarBehaviour> playable = ScriptPlayable<GameStarBehaviour>.Create(graph);
        GameStarBehaviour sceneBehaviour = playable.GetBehaviour();
        //기본 셋팅 설정구간
        return playable;
    }

    public void GatherProperties(PlayableDirector director, IPropertyCollector driver)
    {
        //throw new NotImplementedException();
    }
}
