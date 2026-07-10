using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InsertBallText : MonoBehaviour
{

    public TMP_Text _text;
    //투입된 공:{Data}개
    [ReadOnly]
    private string defaultText = "투입된 공 : {Data}개";
    private int currentText = -1;

    public void OnEnable()
    {
        _text.text = "";
        if (currentText > -1)
        {
            SetText(currentText);
        }
    }

    private void OnDisable()
    {
        currentText = -1;
    }


    //텍스트 등록
    public void SetText(int data1) {
        if (data1 == 0)
        {
            OnTextHide(false);
        }
        else
        {
            OnTextHide(true);
            _text.text = TextChange(data1);
            currentText = data1;
        }
    }

    public void OnReset() {
        _text.text = "";
    }

    //테스트 변경 컨트롤
    private string TextChange(int data1)
    {
        string text = defaultText.Replace("{Data}", data1.ToString());
        return text;
    }

    public void OnTextHide(bool isView) {
        _text.gameObject.SetActive(isView);
    }
}
