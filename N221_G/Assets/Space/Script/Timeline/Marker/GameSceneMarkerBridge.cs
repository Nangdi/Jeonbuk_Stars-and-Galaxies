using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

//타임라인 제어 마커
public class GameSceneMarkerBridge : MonoBehaviour, INotificationReceiver
{
    public PlayableDirector playableDirector;
    
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (!playableDirector) {
            Debug.LogWarning("타임라인이 설정되어있지 않음");
        }

        Debug.Log("Object] : "+context);

        GameSceneMarker marker = notification as GameSceneMarker;
        UserTableTimelineManager _timelineManager = null;
        if (context!=null)
        {
            _timelineManager = context as UserTableTimelineManager;
            Debug.Log("tableUserIndex] : " + _timelineManager.tableUserIndex);
        }

        if (_timelineManager)
        {
            //마커 설정
            switch (marker.signalType)
            {
                case GameSceneMarker.SignalType.BallOut:
                    _timelineManager.BallOut();
                    break;
            }
        }

        switch (marker.sceneClipStats)
        {
            case GameSceneClip.SceneStats.OnPause:

                playableDirector.Pause();
                //timelineManager.playableDirector.Pause();
                break;
            case GameSceneClip.SceneStats.OnPlay:

                break;
        }

        Debug.Log("[OnNotify] : ");
        /**GameSceneMarker marker = notification as GameSceneMarker;
        Debug.Log(marker);
        switch (marker.sceneClipStats)
        {
            case GameSceneClip.SceneStats.OnPause:

                playableDirector.Pause();
                //timelineManager.playableDirector.Pause();
                break;
            case GameSceneClip.SceneStats.OnPlay:

                break;
        }**/

    }



}

