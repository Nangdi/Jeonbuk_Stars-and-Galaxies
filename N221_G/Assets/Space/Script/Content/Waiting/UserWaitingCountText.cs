using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserWaitingCountText : MonoBehaviour
{

    public TextMeshPro _text;

    //텍스트 데이터 등록
    public void SetText(int index) {
        _text.text = index.ToString();
    }
}
