using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI 스크린 설정 사이즈및 정렬 조건 
public class UIScreen : MonoBehaviour
{
    //Sprite 렌더러 설정 
    public SpriteRenderer spriteRenderer;
    //캔버스
    public Canvas canvas;
    public RectTransform canvasRectTransform;

    public void OnEnable()
    {
        OnInit();
    }


    private void OnInit() {

        SettingArea();
        
    }

    //위치 조정
    private void SettingArea() {
        //위치 
        canvasRectTransform.anchoredPosition = spriteRenderer.transform.position;
        //사이즈정의 
        canvasRectTransform.sizeDelta = spriteRenderer.bounds.size;
        //스케일 정의
        canvasRectTransform.localScale = Vector3.one;
    }





}

