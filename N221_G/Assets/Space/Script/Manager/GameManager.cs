using Mono.Cecil.Cil;
using Newtonsoft.Json;
using Runtime2DTransformInteractor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using static ComponentVO;
using static GameManager;

/// <summary>
/// 게임설정위한 데이터 화면 구성
/// </summary>
public class GameManager : MonoBehaviour
{
    public bool isEditor = false;
    
    [Header("셋팅 파일 경로")]
    public string settingFile = "";

    [Header("관리가 되어야 하는 오브젝트")]
    public List<UIComponent> managementTransformList;

    [ReadOnly]
    public ComponentVO componentVO;

    private Coroutine onJsonFileLoadCoroutine = null;

    public ManagerData managerData;

    [System.Serializable]
    public class ManagerData {

        [Header("키보드 설정 매니저")]
        public KeyboardManager keyboardManager;

        [Header("타임라인 매니저")]
        public TimelineManager timelineManager;

    }

    /// <summary>
    /// 에디터 설정
    /// </summary>
    [Header("런타임 에디터 설정")]
    public EditorControl editorControl;
    [System.Serializable]
    public class EditorControl {

        public TextMeshPro EditorText;
        private bool isEditor = false;

        public bool IsEditor { get => isEditor;
            set {
                isEditor = value;
                EditorText.gameObject.SetActive(isEditor);
                for (int i = 0; i < GameManager.instance.managementTransformList.Count; i++) {
                    UIComponent _UIComponent = GameManager.instance.managementTransformList[i];
                    if (isEditor)
                    {

                        _UIComponent.RendererSprite(0.5f);
                        //_UIComponent.transformInteractor.enabled = isEditor;
                    }
                    else {

                        _UIComponent.RendererSprite(1f);
                        //_UIComponent.transformInteractor.enabled = isEditor;
                    }
                }
                //GameManager.instance.transformInteractorController.enabled = isEditor;
            }
        }
    }

    //위치 조정 컨트롤러
    public TransformInteractorController transformInteractorController;

    /// <summary>
    /// 인스턴스
    /// </summary>
    public static GameManager instance
    {
        get
        {
            return GameObject.FindObjectOfType<GameManager>();
        }
    }

    //게임 타입 정보
    public enum GameState { 초기화면,사용자대기카운터진행,공투입,회전판별생성, 게임설명};
    public GameState gameType = GameState.초기화면;

    //public enum StationState { waiting, experience, mission1, mission2};

    //public StationState stationState = StationState.experience;

    public enum ClipState { Scene0, Scene1 , Scene2 , Scene3 , Scene4 , Scene5 , Scene6 , Scene7 , Scene8 , Scene9 , Scene10}    

    public void Start()
    {
        if (!isEditor)
        {
            //기본 셋팅 
            OnInit();
        }
        Cursor.visible = false;
    }



    //파일 경로 설정 구간
    private string GetPath()
    {
        return Application.streamingAssetsPath;
        //return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }

    //시작 커맨드
    private void OnInit() {
        IsSettingJsonFile();
        //OnInitInteractorController();
    }


    //컨트롤러 초기화
    private void OnInitInteractorController() {
        transformInteractorController.enabled = false;
    }

    /// <summary>
    /// 데이터 리셋(초기화)
    /// </summary>
    public void DataComponentVOReset() {
        componentVO.componentList.Clear();
        for (int i = 0; i < managementTransformList.Count; i++)
        {
            ComponentList componentList = new ComponentList();
            componentList.X =0;
            componentList.Y =0;
            componentVO.componentList.Add(componentList);
            managementTransformList[i].LocalPosition = componentList.Vector2;
        }
    }


    //외부 Json 저장요소가 존재 하는지 확인)
    private void IsSettingJsonFile() {
        string filePath = GetJsonPath();

        if (!File.Exists(filePath))
        {
            Debug.Log(filePath);
            //기본 데이터가 존재 하지않을경우
            //componentVO = new ComponentVO();
            componentVO.componentList = new List<ComponentVO.ComponentList>();
            for (int i=0;i< managementTransformList.Count; i++) {
                ComponentList componentList = new ComponentList();
                componentList.X = managementTransformList[i].LocalPosition.x;
                componentList.Y = managementTransformList[i].LocalPosition.y;
                componentVO.componentList.Add(componentList);
            }

            string dateText=JsonConvert.SerializeObject(componentVO, Formatting.Indented);
            Debug.Log(dateText);
            //기본 데이터 생성
            File.WriteAllText(filePath, dateText);
        }
        else {

            //파일 존재함
            Debug.Log(filePath);
            RemoveOnFileLoadCoroutine();
            onJsonFileLoadCoroutine =StartCoroutine(OnJsonFileLoadCoroutine(filePath));
        }
    }

    //초기화
    private void RemoveOnFileLoadCoroutine() {
        if (onJsonFileLoadCoroutine != null) {
            StopCoroutine(onJsonFileLoadCoroutine);
            onJsonFileLoadCoroutine = null;
        }
    }

    //외부 파일로드
    IEnumerator OnJsonFileLoadCoroutine(string path) {
        yield return null;
        UnityWebRequest uwr = UnityWebRequest.Get(path);
        yield return uwr.SendWebRequest();
        if (uwr.result.Equals(UnityWebRequest.Result.Success))
        {
            Debug.Log("데이터 로드 성공");
            string _text = uwr.downloadHandler.text;
            componentVO = JsonConvert.DeserializeObject<ComponentVO>(_text);
            Debug.Log("-----다음진행 UI데이터 위치 조정 시작-----");
            SetUIComponentData();
        }
        else {
            SetEditorText("외부 파일 로드오류");
        }


    }

    public void SetUIComponentData() {
        for (int i = 0; i < componentVO.componentList.Count; i++) {
            ComponentVO.ComponentList componentList = componentVO.componentList[i];
            managementTransformList[i].LocalPosition = componentList.Vector2;
        }
    }


    /// <summary>
    /// 에디터 텍스트 글자 표현
    /// </summary>
    /// <param name="text"></param>
    public void SetEditorText(string text) {
        editorControl.IsEditor = true;
        editorControl.EditorText.text= text;
    }

    //Json Path
    private string GetJsonPath() {
        string mainPath = GetPath();
        string filePath = mainPath + Path.DirectorySeparatorChar + settingFile;
        return filePath;
    }

    /// <summary>
    /// 프로그램 종료 
    /// </summary>
    public void OnApplicationQuit()
    {
        if (isEditor)
        {
            SettingDataSave();
        }
    }

    /// <summary>
    /// 셋팅 데이터 저장
    /// </summary>
    public void SettingDataSave() {
        string filePath = GetJsonPath();
        string dateText = JsonConvert.SerializeObject(componentVO, Formatting.Indented);
        
        //기본 데이터 생성
        File.WriteAllText(filePath, dateText);
    }







}
