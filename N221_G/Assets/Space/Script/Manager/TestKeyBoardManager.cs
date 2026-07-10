using com.humanc.rsconn;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;
using UnityRawInput;
using static UnityEngine.InputSystem.InputAction;

public class TestKeyBoardManager : MonoBehaviour
{
    //총인원 5명 컨트롤 제어는 추후 다음주에 컨트롤러 제작


    public ElementObject elementObject;

    public RsConnection rsConnection;

    public DebugData debugData;
    [System.Serializable]
    public class DebugData {
        public TextMeshProUGUI debugText;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnHookSetting();
    }

    public bool WorkInBackground;
    public bool InterceptMessages;

    private void OnHookSetting() {
        RawInput.WorkInBackground = WorkInBackground;
        RawInput.InterceptMessages = InterceptMessages;

        RawInput.OnKeyUp += LogKeyUp;
        RawInput.OnKeyDown += LogKeyDown;
        RawInput.OnKeyDown += DisableIntercept;

        RawInput.Start();
    }


    private void OnDisable()
    {
        RawInput.Stop();

        RawInput.OnKeyUp -= LogKeyUp;
        RawInput.OnKeyDown -= LogKeyDown;
        RawInput.OnKeyDown -= DisableIntercept;
    }

    private void DisableIntercept(RawKey key)
    {
        if (RawInput.InterceptMessages && key == RawKey.Escape)
            RawInput.InterceptMessages = InterceptMessages = false;
    }

    private void LogKeyUp(RawKey key)
    {
        Debug.Log("Key Up: " + key);
        switch (key) {

            case RawKey.L:
                Debug.Log("RS232 디버그창 생성");
                TestDebujgTextView();
                break;
            case RawKey.T:
                rsConnection.SendData("1");
                break;
            case RawKey.G:
                rsConnection.SendData("0");
                break;

        }



    }

    private void LogKeyDown(RawKey key)
    {
        Debug.Log("Key Down: " + key);
    }


    // Update is called once per frame
    void Update()
    {
       
    }

    void callback(string keyString) {
        Debug.Log("keyString] : " + keyString);
    }

    public void OnUserInsert() {
        Debug.Log("[UserInsert]");
    }

    public void OnUserInsert(CallbackContext context) {

        Debug.Log("[UserInsert]"+ context);

    }

    //유저 사용자 접속 
    public void OnUserAccess(InputValue value) {
        Debug.Log("사용자 접속 시작 하는구간");
        Debug.Log("[OnUserAccess] : " + value.isPressed);
        //영상 진행 하는구간 
        UserAccess();
    }

    /// <summary>
    /// 사용자 접근 시작 컨트롤
    /// </summary>
    void UserAccess() {
        UserControlManager.instance.userControlList[0].Stage2_OnPlay();

        //UserControlManager.instance
    }


    public void OnBandingWheel(InputValue value) {
        //초기호면일경우 다음화면으로 이동 컨트롤( 타임라인컨트롤)
        if (GameManager.instance.gameType.Equals(GameManager.GameState.초기화면)) {
            Debug.Log("회전판 움직임작동 임시 버튼 테스트");
            //재생 시작하는구간
            UserControlManager.instance.userControlList[0].Stage1_OnPlay();
        }   
    }

    public RawImage sRawImage;
    public RectTransform edigRectTransform;

    public SettingData settingData;
    [System.Serializable]
    public class SettingData {
        public int sX = 0;
        //
        public Vector2 edigRectSize = Vector2.zero;
    }

    private void SettingJson() {
        string date= JsonConvert.SerializeObject(settingData);
        string savePath = Application.streamingAssetsPath + Path.DirectorySeparatorChar + "data.json";
        File.WriteAllText(savePath, date);
    }

    private void LoadJson() {

    }
 

    //원소 오브젝트 움직임 처리 키보드 이벤트 핸들러 작동 설정 구간(키보드 부분)
    public void OnElementMove(InputValue value)
    {

        if (Keyboard.current.eKey.isPressed)
        {
            Debug.Log("[OnElementMove] E");
            RectTransform rect = sRawImage.rectTransform;
            Vector3 pos = rect.anchoredPosition;
            pos.x = pos.x + 0.2f;
            rect.anchoredPosition = pos;
        }
        else if (Keyboard.current.rKey.isPressed)
        {
            Debug.Log("[OnElementMove] R");
        }
        else if (Keyboard.current.iKey.isPressed)
        {
            /*Debug.Log("[OnElementMove] I");
            SettingJson();
            settingData.edigRectSize = edigRectTransform.sizeDelta;*/
            rsConnection.SendData("2");
            //elementObject.OnInit();
        }
        else if (Keyboard.current.tKey.isPressed)
        {
            /*Debug.Log("사이즈 증가");
            Vector2 _size = edigRectTransform.sizeDelta;
            _size.y += 1f;
            edigRectTransform.sizeDelta = _size;*/

            rsConnection.SendData("1");
        } else if (Keyboard.current.gKey.isPressed) {
            /*Debug.Log("사이즈 하강");
            Vector2 _size = edigRectTransform.sizeDelta;
            _size.y -= 1f;
            edigRectTransform.sizeDelta = _size;*/
            rsConnection.SendData("0");
        }
    }
    /* 1 -> 바람
    0 -> 바람 해제
    2 -> 입구막기*/
    public void OnStarCreate()
    {
        Debug.Log("[OnStarCreate]");
        MainTableManager.instance.CreateStar(Vector3.zero);
        //elementObject.OnInit();
    }

    public void OnDebug() {
        Debug.Log("[OnDebug]");
        if (Keyboard.current.lKey.isPressed) {
           
        }
    }

    private void TestDebujgTextView() {
        debugData.debugText.gameObject.SetActive(!debugData.debugText.gameObject.activeSelf);
    }

}
