using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UserTableSettingBase;

//미션 비디오 재생 여부 판단
public class ExperienceEndControl : MonoBehaviour
{
    //활성화 작동

    private Coroutine onInit = null;
    public MediaPlayer endVideoPlayer;
    //GUI
    public GameObject endVideoPlayerGUI;
    public GameObjectControl gameObjectControl;
   

    public void OnEnable()
    {

        if (endVideoPlayer.Control != null)
        {
            endVideoPlayer.Control.SeekFast(0);
        }
        OnInitStart();
    }

    public void OnDisable()
    {
        endVideoPlayer.Control.SeekFast(0);
        OnRemoveInit();
        if (endVideoPlayerGUI)
        {
            endVideoPlayerGUI.SetActive(false);
        }
    }

    private void OnInitStart() {
        OnRemoveInit();
        onInit = StartCoroutine(OnInit());
    }

    private void OnRemoveInit() {
        if (onInit != null) {
            StopCoroutine(onInit);
            onInit = null;
        }
    }
    //시작 커맨드 설정
    IEnumerator OnInit()
    {
        Debug.Log("OnInit] : ");
        endVideoPlayer.Control.SeekFast(0);
        yield return null;
        //점수 정보 토탈 
        GameObjectControl.Instance.ScoreRankingControl();
        //존재하는 엘리먼트 삭제
        GameObjectControl.Instance.RemoveElementObject();
        //시작 컨트롤(하드웨어장비작동X)
        GameObjectControl.Instance.IsMissionPlayAllUserList(false);
        //랭킹데이터 정보 화면에 표시
        GameObjectControl.Instance.SettingRankDatas();
        //생성된 엘리먼트 정보 삭제 (초기화 하는구간)
        GameObjectControl.Instance.RemoveElementObject();
        MainTableManager.instance.RemoveStar();
        //10초 뒤에 진행
        yield return new WaitUntil(()=> endVideoPlayerGUI.gameObject.activeSelf);
        Debug.Log("실행작동 구간 ");
        //영상 재생 시작
        endVideoPlayer.Control.SeekFast(0);
        endVideoPlayer.Control.Play();
       
        RemoveElementObject();
        //존재하는 엘리먼트 삭제
        GameObjectControl.Instance.RemoveElementObject();
        MainTableManager.instance.RemoveStar();
    }


    public void PlayerEvent(MediaPlayer mp, MediaPlayerEvent.EventType eventType, ErrorCode errorCode)
    {
        Debug.Log(eventType);
        switch (eventType)
        {
            case MediaPlayerEvent.EventType.ReadyToPlay:
                
                Debug.Log("ReadyToPlay]");
                break;
            case MediaPlayerEvent.EventType.FirstFrameReady:

                break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                Debug.Log("영상 완료 이벤트");
                //UserTableSettingBase.StationState _stationState, 
                //GameManager.ClipState _clipState
                if (this.gameObject.activeSelf)
                {
                    StartCoroutine(OnEndNextScene());
                }
                GameObjectControl.Instance.RemoveElementObject();
                MainTableManager.instance.RemoveStar();
                break;
        }
    }

    //미션 종료 씬 이동
    IEnumerator OnEndNextScene() {
        yield return null;
        Debug.Log("공배출 [하드웨어 초기화]");
        //미션 1시작 지점
        gameObjectControl.GotoSceneClip(UserTableSettingBase.StationState.experience, GameManager.ClipState.Scene2);
        gameObjectControl.stationState = UserTableSettingBase.StationState.mission1;
        for (int i=0;i < gameObjectControl.userTableSettingList.Count; i++) {
            UserTableController userTableController = gameObjectControl.userTableSettingList[i];
            if (userTableController.ballSetting.insertBallCount > 0)
            {
                GameObjectControl.Instance.rsControl.multiRS.SendConsole(i, "1");
                Debug.Log("리셋 진행 사용자" + i);
            }
        }
        OnResetData("1");
        yield return new WaitForSeconds(0.01f);

        for (int i = 0; i < gameObjectControl.userTableSettingList.Count; i++)
        {
            //GameObjectControl.Instance.rsControl.multiRS.SendConsole(i, "0");
            gameObjectControl.userTableSettingList[i].ballSetting.insertBallCount = 0;
            gameObjectControl.userTableSettingList[i].ballSetting.littleSaveRotatingDeviceCount = 0;
            gameObjectControl.userTableSettingList[i].ballSetting.bigSaveRotatingDeviceCount = 0;
            //gameObjectControl.userTableSettingList[i].ballSetting.m2_insertBallText.OnTextHide(false);
            //gameObjectControl.userTableSettingList[i].ballSetting.m1_insertBallText.OnTextHide(false);
            gameObjectControl.userTableSettingList[i].ballSetting.rotatingDeviceCount = 0;
            //GameObjectControl.Instance.rsControl.multiRS.SendConsole(i, "0");
            GameObjectControl.Instance.userTableSettingList[i].rotatingDeviceEvent.Invoke();
        }

        yield return new WaitForSeconds(1f);
       
        OnResetData("0");
        

        GameObjectControl.Instance.RemoveElementObject();
        MainTableManager.instance.RemoveStar();
        
        //시작 컨트롤(하드웨어장비작동O)
        GameObjectControl.Instance.IsMissionPlayAllUserList(true);
    }

    

    //데이터 리셋
    private void OnResetData(string data) {
        for (int i = 0; i < gameObjectControl.userTableSettingList.Count; i++)
        {
            for (int j = 0; j < 40; j++)
            {
                UserTableController userTableController = gameObjectControl.userTableSettingList[i];
                if (userTableController.ballSetting.insertBallCount > 0)
                {
                    GameObjectControl.Instance.rsControl.multiRS.SendConsole(i, data);
                    Debug.Log("리셋 진행 사용자" + i);
                }
            }
        }
    }

    private void RemoveElementObject() {
        ElementObject[] _elementObject= GameObjectControl.Instance.elementParentObject.GetComponentsInChildren<ElementObject>();
        for (int i= _elementObject.Length-1; i>=0; i--) {
            GameObject.Destroy(_elementObject[i].gameObject);
        }
    }

}
