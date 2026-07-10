using RenderHeads.Media.AVProVideo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;
using static UserTableController;


//사용자 테이블 컨트롤러
public class UserTableController : UserTableQuestion
{

    //큰별 비디오 플레이어
    public MediaPlayer bigStarVideoPlayer;
    public MediaReference bigMediaReference;

    //작은별 비디오 플레이어
    public MediaPlayer littleStarVideoPlayer;
    public MediaReference littleMediaReference;

    private Coroutine onLoop = null;

    private bool isLittleStarVideoPlay = false;


    //기본 텍스쳐
    public Texture2D defaultTexture;
    
    private IEnumerator Start()
    {
        materialPropertyBlock = new MaterialPropertyBlock();
        //littleStar.materialPropertyBlock = new MaterialPropertyBlock();

        yield return null;
        SetDefaultTexture();
        Debug.Log("파일로드 시작 컨트롤");
        VideoFileLoad();
        OnLoopStart();
    }

    //기본 텍스쳐 변경
    private void SetDefaultTexture() {
        materialPropertyBlock.SetTexture("_BaseMap", defaultTexture);
        renderer.SetPropertyBlock(materialPropertyBlock);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        OnRemoveLoop();
    }


    private void OnLoopStart() {
        OnRemoveLoop();
        onLoop = StartCoroutine(OnLoop());
    }

    private void OnRemoveLoop() {
        if (onLoop != null) {
            StopCoroutine(onLoop);
            onLoop = null;
        }
    }


    //public float smoothTime = 0.3F;
    //private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3F;
    private float yVelocity = 0.0F;


    //private bool isPlay = true;

    /// <summary>
    /// 루프 
    /// </summary>
    /// <returns></returns>
    IEnumerator OnLoop() {
        yield return null;
        while (true)
        {
            yield return new WaitUntil(() => bigStar.isStarVideoPlay && littleStar.isStarVideoPlay);
            yield return null;
            switch (starVideoType) {
                case StarVideoType.BigStar://큰 별
                    //현재 비디오 시간 타임정의 구간 
                    if (bigStar.isStarVideoPlay)
                    {
                        if (bigStar.isPlay) {
                            float currentVideoBigPosition = (float)bigStarVideoPlayer.Control.GetCurrentTime();
                            bigVideoPosition = Mathf.SmoothDamp(
                            bigVideoPosition,
                            (float)bigStar.videoValue,
                            ref yVelocity, smoothTime);
                            //비디오 포지션 
                            float currentVideoBigSmoothPosition = bigVideoPosition * (float)bigStar.videoDuration;

                            bigStarVideoPlayer.Control.Seek(currentVideoBigSmoothPosition);
                            bigStar.texture = bigStarVideoPlayer.TextureProducer.GetTexture();
                            materialPropertyBlock.SetTexture("_BaseMap", bigStar.texture);
                            renderer.SetPropertyBlock(materialPropertyBlock);
                           
                            float difference = bigStar.videoValue - bigVideoPosition;
                            float bigTruncateData = ((float)Math.Truncate(difference * 1000f)/1000f);
                            //Debug.Log("[difference ] : " + bigTruncateData);
                            if (bigTruncateData == 0)
                            {
                               
                                bigStar.isPlay = false;
                                isPlaying = false;
                            }
                        }
                    }
                    break;
                case StarVideoType.LittleStar://작은 별
                    if (littleStar.isStarVideoPlay)
                    {
                        if (littleStar.isPlay)
                        {
                            float currentVideoBigPosition = (float)littleStarVideoPlayer.Control.GetCurrentTime();
                            bigVideoPosition = Mathf.SmoothDamp(
                            bigVideoPosition,
                            (float)littleStar.videoValue,
                            ref yVelocity, smoothTime);
                            //비디오 포지션 
                            float currentVideoBigSmoothPosition = bigVideoPosition * (float)littleStar.videoDuration;

                            littleStarVideoPlayer.Control.Seek(currentVideoBigSmoothPosition);
                            littleStar.texture = littleStarVideoPlayer.TextureProducer.GetTexture();
                            materialPropertyBlock.SetTexture("_BaseMap", littleStar.texture);
                            renderer.SetPropertyBlock(materialPropertyBlock);

                            float difference = littleStar.videoValue - bigVideoPosition;
                            float bigTruncateData = ((float)Math.Truncate(difference * 1000f) / 1000f);
                            //Debug.Log("[difference ] : " + bigTruncateData);
                            if (bigTruncateData == 0)
                            {
                                //videoCompleteEvent.Invoke(this);
                                littleStar.isPlay = false;
                                isPlaying = false;
                            }
                        }
                    }
                    break;
            }
           
        }
    }

    //비디오 포지션 리셋
    public override void VideoReset()
    {
        bigVideoPosition = 0;
        littleStar.videoValue = 0;
        bigStar.videoValue = 0;
        bigStarVideoPlayer.Control.Seek(0);
        littleStarVideoPlayer.Control.Seek(0);
    }

    private float maxValue;
   
    
    public void  VideoValueChange(float value) {
        //비디오 타입에 따른 영상위치 변경 
        switch (starVideoType) {
            case StarVideoType.BigStar:
                BigVideoChangeDetectionValue(value);
                break;
            case StarVideoType.LittleStar:
                LittleVideoChangeDetectionValue(value);
                break;
        }
        
    }

    /// <summary>
    /// 비디오 변화 감지
    /// </summary>
    public override void BigVideoChangeDetectionValue(float value) {
        this.bigStar.videoValue = value;
        this.bigStar.isPlay = true;
        Debug.Log("영상 변화 감지 되는구간");
    }

    public override void LittleVideoChangeDetectionValue(float value)
    {
        //비디오 포지션 변경 위치
        this.littleStar.videoValue = value;
        this.littleStar.isPlay = true;
        Debug.Log("영상 변화 감지 되는구간");
    }

    /// <summary>
    /// 파일로드
    /// </summary>
    private void VideoFileLoad() {
        //큰별 오픈
        bigStarVideoPlayer.OpenMedia(bigMediaReference, false);
        //작은별 오픈
        littleStarVideoPlayer.OpenMedia(littleMediaReference, false);
    }

    private void VideoPlayerStart() { 
        
    }


    //큰별 영상 재생 이벤트 
    public void BigStarPlayerEvent(MediaPlayer mp, MediaPlayerEvent.EventType eventType, ErrorCode errorCode) {
        Debug.Log(eventType);
        switch (eventType) {
            case MediaPlayerEvent.EventType.ReadyToPlay:
                bigStar.isStarVideoPlay = true;
                bigVideoPosition = 0;
                bigStar.videoDuration = mp.Info.GetDuration();
                bigStar.videoValue = 0;
                Debug.Log("BigStarPlayerStart]");
                break;
            case MediaPlayerEvent.EventType.FirstFrameReady:
               
                break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                Debug.Log("영상 완료 이벤트");
                
                break;
        }
    }


    //작은별 영상 재생 이벤트
    public void LittleStarPlayerEvent(MediaPlayer mp, MediaPlayerEvent.EventType eventType, ErrorCode errorCode) {
        //Debug.Log(eventType);
        switch (eventType)
        {
            case MediaPlayerEvent.EventType.ReadyToPlay:
                littleStar.isStarVideoPlay = true;
                bigVideoPosition = 0;
                littleStar.videoDuration = mp.Info.GetDuration();
                littleStar.videoValue = 0;
                Debug.Log("BigStarPlayerStart]");
                break;
            case MediaPlayerEvent.EventType.FirstFrameReady:
                break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                Debug.Log("영상 완료 이벤트");
                //videoCompleteEvent.Invoke(this);
                break;
        }
    }


    


}
