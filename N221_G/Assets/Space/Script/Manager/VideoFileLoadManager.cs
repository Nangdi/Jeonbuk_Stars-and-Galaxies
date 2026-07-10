using RenderHeads.Media.AVProVideo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UserTableTimelineManager;
using static VideoFileLoadManager;


/// <summary>
/// 비디오 파일 로드 매니저 
/// </summary>
public class VideoFileLoadManager : MonoBehaviour
{
    //미디어 플레이어
    public List<VideoReference> mediaPlayerList;

    [System.Serializable]
    public class VideoReference {
        
        [Header("별의 상태")]
        public UserTableTimelineManager.StarState starState;
        
        [Header("별 단계 분리")]
        public UserTableTimelineManager.VideoStep videoStep;

        //미디어 레퍼런스 정보
        public MediaReference mediaReference;
    }

    public static VideoFileLoadManager instance
    {
        get
        {
            return GameObject.FindObjectOfType<VideoFileLoadManager>();
        }
    }


    public void Start()
    {
        SettingVideoData();
       
    }

    //비디오 셋팅정보
    private void SettingVideoData() {
        for (int i=0;i< mediaPlayerList.Count; i++) {
            //mediaPlayerList[i];
        }
    }

    /// <summary>
    /// 해당 씬에 대한 영상 재생
    /// </summary>
    /// <param name="starState"></param>
    /// <returns></returns>
    public VideoReference GetVideoReference(StarState starState) {
        int index = Array.FindIndex(mediaPlayerList.ToArray(), item => item.starState.Equals(starState));
        if (index > -1)
        {
            return mediaPlayerList[index];
        }
        else
        {
            return null;
        }
    }

    private void VideoLoad() {
        
    }

    private void VideoClone() { 
    
    }

}
