using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UserTableSettingBase;

public class BallCountData : MonoBehaviour
{

    //텍스트 정보
    public TMP_Text text;
    [ReadOnly]
    [TextArea]
    private string defaultText = "공을 투입구에 넣어\r\n우주먼지를 만들어 보세요.\r\n<size=60%>1.작은 별:공 1~4개 | 2.큰별:공 5개 이상</size>";

    //활성화시 작동
    /**public void OnEnable()
    {
        GameObjectControl.instance.
        string data1 = "1";
        string data2 = ballSetting.bigInsertCountBall;
        SetTextData(data1, data2);
    }*/

    //텍스트 변경
    public void SetText(int data1, int data2)
    {
        text.text = TextChange(data1, data2);
    }

    //텍스트 변경 구간
    private string TextChange(int data1, int data2) {
        string text= defaultText.Replace("{Data1}", data1.ToString());
        text = text.Replace("{Data2}", data2.ToString());
        return text;
    }

}
