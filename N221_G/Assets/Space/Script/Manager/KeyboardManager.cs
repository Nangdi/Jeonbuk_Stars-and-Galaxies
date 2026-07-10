using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//키보드 매니저 
//키보드 컨트롤러 제작
public class KeyboardManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    //시작 커맨드 
    private void OnInit() {
        OnReset();
    }

    //return EditorControl
    private GameManager.EditorControl EditorControl() {
        return GameManager.instance.editorControl;
    }


    //초기화
    private void OnReset() {
        EditorControl().IsEditor = false;
    }

    // Update is called once per frame
    void Update()
    {

        //UI 배치 시작및 정지(데이터 저장)
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            //키보드 1등록
            Debug.Log("KeyDown_1");
            if (EditorControl().IsEditor)
            {
                EditorControl().IsEditor = false;
                GameManager.instance.SetUIComponentData();
                GameManager.instance.SettingDataSave();

            }
            else {
                EditorControl().IsEditor = true;
                GameManager.instance.SetUIComponentData();
                GameManager.instance.SettingDataSave();
            }
            //화면셋팅 준비
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            //데이터 리셋
            GameManager.instance.DataComponentVOReset();
            GameManager.instance.SettingDataSave();

        }    
    }
}
