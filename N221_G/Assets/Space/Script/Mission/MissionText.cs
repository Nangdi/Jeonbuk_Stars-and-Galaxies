using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/// <summary>
/// 미션 텍스트
/// </summary>
public class MissionText : MonoBehaviour
{
    public TextMeshPro textmesh;
    //[SerializeField]
    [ReadOnly]//1.작은 별:공 1~4개 | 2.큰별:공 5개 이상
    private string defaultText= "1.작은 별:공 1~{Data1}개 | 2.큰별:공 {Data2}개 이상";

    public void SetText(int data1, int data2) {
        textmesh.text = TextChange(data1, data2);
    }

    //테스트 변경 컨트롤
    private string TextChange(int data1,int data2) {
        string text = defaultText.Replace("{Data1}",data1.ToString());
        text=text.Replace("{Data2}", data2.ToString());
        return text;
    }
}
