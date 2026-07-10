using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDataContent : MonoBehaviour
{
    public List<TextData> textDataList;

    [System.Serializable]
    public class TextData
    {

        public UserTableSettingBase.StationState stationState;

        //문제 텍스트 
        public TextMeshPro problemTextObject;

        //내용 텍스트
        public TextMeshPro contentTextObject;
        
        //문구 텍스트 
        public TextMeshPro phraseTextObject;

        [TextArea]
        public string textData1;

        [TextArea]
        public string textData2;

    }

    private Coroutine onTimeTextViewStart;
    private int index = 0;

    //활성화시 메세지 정보 초기화
    public virtual void OnEnable()
    {
        TextData errorTextData = OnInitSetTextView();
        OnTimeTextViewStart(errorTextData);
    }



    //텍스트 뷰
    private TextData OnInitSetTextView()
    {
        //현재 상황에 따른 메세지 정보
        TextData errorTextData = GetErrorTextData();
        SetText(errorTextData, errorTextData.textData1);
        return errorTextData;
    }




    //텍스트
    private void SetText(TextData errorTextData, string textData)
    {
        errorTextData.contentTextObject.text = textData;
    }



    //상황에 따른에러 텍스트 데이터 리턴
    private TextData GetErrorTextData()
    {
        TextData errorTextData = null;
        int index = Array.FindIndex(textDataList.ToArray(), item => item.stationState.Equals(GameObjectControl.Instance.stationState));
        if (index > -1)
        {
            errorTextData = textDataList[index];
        }
        return errorTextData;

    }


    private void OnTimeTextViewStart(TextData errorTextData)
    {
        onTimeTextViewStart = StartCoroutine(OnTimeTextView(errorTextData));
    }

   
    //타임 텍스트 뷰
    public IEnumerator OnTimeTextView(TextData errorTextData)
    {
        yield return null;
        while (true)
        {
            yield return new WaitForSeconds(5f);
            if (index == 0)
            {

                DOTween.Sequence()

                    .Append(DOTween.To(() =>
                        errorTextData.contentTextObject.color,
                        _color => errorTextData.contentTextObject.color = _color,
                        new Color(1, 1, 1, 0), 0.5f).OnComplete(() =>
                        { SetText(errorTextData, errorTextData.textData1); }))

                    .Append(DOTween.To(() =>
                        errorTextData.contentTextObject.color,
                        _color => errorTextData.contentTextObject.color = _color,
                        Color.white, 0.5f)).SetAutoKill(true);


                index = 1;
            }
            else if (index == 1)
            {
                DOTween.Sequence()

                    .Append(DOTween.To(() =>
                        errorTextData.contentTextObject.color,
                        _color => errorTextData.contentTextObject.color = _color,
                        new Color(1, 1, 1, 0), 0.5f).OnComplete(() =>
                        { SetText(errorTextData, errorTextData.textData2); }))

                    .Append(DOTween.To(() =>
                        errorTextData.contentTextObject.color,
                        _color => errorTextData.contentTextObject.color = _color,
                        Color.white, 0.5f)).SetAutoKill(true);


                index = 0;
            }
            TimeTextChange();
        }
    }


    public virtual void TimeTextChange() { }

}
