using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;


/// <summary>
/// 사용자 테이블 셋팅 설정
/// </summary>
public class UserTableSetting : CommonSetting
{
    /// <summary>
    /// 텍스트 사이즈 변경 설정구간
    /// </summary>
    public List<TextMeshProUGUI> textMeshProUGUIList;
    public float fontMaxSize = 10;

    [Header("원소 데이터 정보")]
    public List<ElementData> elementDataList;

    [Header("원소 오브젝트")]
    public GameObject elementObject;

    [System.Serializable]
    public class ElementData {

        //원소 타입
        public enum ElementType { littleStar, bigStar };

        [Header("원소 타입")]
        public ElementType elementType;

        public enum ElementName { H,He,C,N,O,Na,Mg,Si,Fe,Ni,Cu,Ag,Au,Pb,U}
        [Header("원소 이름")]
        public ElementName elementName;

        public Sprite elementImage;

    }


    //public 


    public void Start()
    {
           
    }

    /// <summary>
    /// 텍스트 사이즈 변경
    /// </summary>
    public void TextSizeChange() {

        Debug.Log("하위 글자 변경");
        for (int i=0;i< textMeshProUGUIList.Count; i++) {
            textMeshProUGUIList[i].fontSizeMax = fontMaxSize;
        }
    }

}
