using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSystemControl : MonoBehaviour
{

    public CanvasControl canvasControl;

    [System.Serializable]
    public class CanvasControl
    {

        public Camera mainCamera;

        public Canvas topCanvas;
        public Canvas bottomCanvas;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        OnInit();
    }

    //시작 설정 되는구간
    private void OnInit()
    {
        OnSetting();
    }

    //기본 셋팅 
    private void OnSetting()
    {
        //스크린 카메라 
        canvasControl.topCanvas.renderMode = UnityEngine.RenderMode.ScreenSpaceCamera;
        canvasControl.bottomCanvas.renderMode = UnityEngine.RenderMode.ScreenSpaceCamera;
        canvasControl.topCanvas.worldCamera = canvasControl.mainCamera;
    }
}
