using AllIn1VfxToolkit;
using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//비디오 컴포넌트 컨트롤러
public class UIVideoComponent : UILocationComponent
{
    // Start is called before the first frame update
   
    //비디오 파일 경로
    public string path="";

    //미디어 플레이어
    public MediaPlayer mediaPlayer;

    [Header("이미지 변경 렌더러")]
    public SpriteRenderer spriteRenderer;
    
    //메트리얼 조작 
    public MaterialPropertyBlock materialPropertyBlock;

    private Coroutine onLoop = null;
    private bool isPlay = false;


    public override void OnEnable()
    {
        base.OnEnable();
        SettingData();
        VideoLoadSetting();
    }

    //기본 셋팅 
    private void SettingData(){
        materialPropertyBlock=new MaterialPropertyBlock();
    }


    //비디오 파일 로드 준비
    public void VideoLoadSetting(){
        string VideoFilePath = Application.streamingAssetsPath + Path.DirectorySeparatorChar + path;
        MediaPath mediaPath = new MediaPath(VideoFilePath, MediaPathType.RelativeToStreamingAssetsFolder);
        mediaPlayer.OpenMedia(mediaPath, false);
        RemoveOnLoop();
        onLoop=StartCoroutine(OnLoop());
    }

    //루프 초기화 
    private void RemoveOnLoop(){
        if(onLoop!=null){
            StopCoroutine(onLoop);
            onLoop=null;
        }
    }
    
    public Texture2D VideoTexture;
    public Texture texture;

    IEnumerator OnLoop() {
        yield return null;
        while (true) {
            yield return new WaitUntil(() => isPlay);

            texture = mediaPlayer.TextureProducer.GetTexture();
            if(texture){
                VideoTexture= Helper.GetReadableTexture(texture, mediaPlayer.TextureProducer.RequiresVerticalFlip(), 
                    Helper.GetOrientation(mediaPlayer.Info.GetTextureTransform()));
                
                MaterialChange();
            }
            yield return null;
        }
    }

    //이벤트 사용방법 테스트
    public void VideoEvents(MediaPlayer mp, MediaPlayerEvent.EventType eventType,ErrorCode errorCode) {
        
        Debug.Log("eventType] : "+eventType);
        switch (eventType) {
            case MediaPlayerEvent.EventType.ReadyToPlay://준비및 재생 시작 일경우 작동되는구간
                Debug.Log(isPlay);
                isPlay = true;
                break ;
            case MediaPlayerEvent.EventType.FirstFrameReady:
                //재생 준비가 완료되었을경우 작동 
                VideoPlayer();
                break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                VideoPlayer();
                break;
        }
    }

    //재질 변경 시작
    public void MaterialChange(){
        //spriteRenderer.sprite.te
        materialPropertyBlock.SetTexture("_MainTex",VideoTexture);
        spriteRenderer.SetPropertyBlock(materialPropertyBlock);
    }

    public void VideoPlayer() {
        mediaPlayer.Control.SeekFast(0);
        mediaPlayer.Play();
    }



}
