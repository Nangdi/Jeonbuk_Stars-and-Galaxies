using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
[DisplayName("SceneClip")]
public partial class GameSceneClip: PlayableAsset, ITimelineClipAsset, IPropertyPreview
{
    [Header("화면 표시용으로 개발")]
    public string clipName = "";

    [Header("데이터 기록")]
    [TextArea]
    public string note = "";

    [Header("현재 상태 정의 구간")] 
    public GameManager.GameState gameState;
    public UserTableSettingBase.StationState stationState;

    [Header("클립 상태")]
    public GameManager.ClipState clipState;

    //클립씬 상태 정보
    public SceneStats sceneStats;
    public enum SceneStats { OnPause, OnPlay }
    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        //SceneBehaviour
        ScriptPlayable<GameSceneBehaviour> playable = ScriptPlayable<GameSceneBehaviour>.Create(graph);
        GameSceneBehaviour sceneBehaviour = playable.GetBehaviour();

        //기본 셋팅 설정구간
        return playable;
    }

    public void GatherProperties(PlayableDirector director, IPropertyCollector driver)
    {
        //throw new NotImplementedException();
    }
}

