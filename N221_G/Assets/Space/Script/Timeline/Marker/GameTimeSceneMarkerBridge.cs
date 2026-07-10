using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class GameTimeSceneMarkerBridge : MonoBehaviour, INotificationReceiver
{
    public PlayableDirector playableDirector;
    public PlayableDirector subPlayableDirector;
    public enum Type {Main,User}
    public Type type;


    //사용자 테이블 컨트롤
    public UserTableController userTableController;

    public void OnNotify(Playable origin, INotification notification, object context)
    {
        GameTimeSceneMarker marker = notification as GameTimeSceneMarker;
        //Debug.Log("[GameTimeSceneMarker]"+ marker.);
        switch (marker.sceneClipStats)
        {
            case GameSceneClip.SceneStats.OnPause:
                if (type.Equals(Type.Main))//메인 타임라인 컨트롤일경우
                {
                    if (!GameObjectControl.Instance.isTimelineChange)
                    {
                        double markerTime = marker.time;
                        playableDirector.time = markerTime;
                        playableDirector.Evaluate();
                        playableDirector.Pause();
                    }
                }
                else if(type.Equals(Type.User))//사용자 정보 타임라인 컨트롤일경우
                {
                    //playableDirector.Pause();
                    if (!userTableController.isTimelineChange)
                     {
                         playableDirector.Pause();
                         double markerTime = marker.time;
                         subPlayableDirector.time = markerTime;
                         subPlayableDirector.Evaluate();
                         subPlayableDirector.Pause();
                         /*subPlayableDirector.time = markerTime;
                         subPlayableDirector.Evaluate();
                         subPlayableDirector.Pause();*/
                    //Debug.Log(markerTime);

                    }
                }
                //timelineManager.playableDirector.Pause();
                break;
            case GameSceneClip.SceneStats.OnPlay:

                break;
        }
    }
}
