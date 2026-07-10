using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class GameTimeSceneMarker : Marker, INotification, INotificationOptionProvider
{
    public Color color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    public bool showLineOverlay = true;


    [Header("클립 재생정보")]
    public GameSceneClip.SceneStats sceneClipStats;


    public PropertyName id { get { return new PropertyName(); } }

    bool emitOnce = false;
    bool emitInEditor = true;

    NotificationFlags INotificationOptionProvider.flags =>
       (emitOnce ? NotificationFlags.TriggerOnce : default) |
       (emitInEditor ? NotificationFlags.TriggerInEditMode : default);
}
