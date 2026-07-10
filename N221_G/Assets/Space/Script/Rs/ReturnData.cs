using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReturnData : MonoBehaviour
{

    //디버그 텍스트 
    public TextMeshProUGUI debugText;

    //데이터 리턴 정보 확인(디버그창)
    public void Rs_returnData(string data) {
        Debug.Log("Rs232 Data : "+data);
        string[] texts = debugText.text.Split('\n');
        if (texts.Length > 12)
        {
            debugText.text = "";
        }
        debugText.text+= data+"\n";
    }

    

}
