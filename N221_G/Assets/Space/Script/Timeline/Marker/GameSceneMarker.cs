using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

//[CustomStyle("AnnotationStyle")]
//[DisplayName("Annotation")]
public partial class GameSceneMarker : Marker, INotification, INotificationOptionProvider
{
    public Color color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    public bool showLineOverlay = true;
  

    [Header("클립 재생정보")]
    public GameSceneClip.SceneStats sceneClipStats;

    public enum SignalType { None, BallOut }
    public SignalType signalType = SignalType.BallOut;



    public PropertyName id { get{return new PropertyName();}}

    bool emitOnce=false;
    bool emitInEditor=true;

    NotificationFlags INotificationOptionProvider.flags =>
       (emitOnce ? NotificationFlags.TriggerOnce : default) |
       (emitInEditor ? NotificationFlags.TriggerInEditMode : default);
}

