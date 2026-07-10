using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(ArrangementObject))]
public class ArrangementObjectEditor : Editor
{

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        ArrangementObject arrangementObject = target as ArrangementObject;

    }
}
#endif


public class ArrangementObject : MonoBehaviour
{
    public enum ObjectType { 땅, 하늘, 바다}
    [Header("오브젝트 타입 정보")]
    public ObjectType objectType;

    public enum AnimationType { 비행기,구름,자동차,배}
    public AnimationType animationType;

    //public List<SpriteData> spriteList;

    //땅에 생겨나는 이미지 리스트
    public List<SpriteData> landSpriteList;
    
    //하늘 이미지 리스트
    public List<SpriteData> skySpriteList;
    
    //바다 이미지 리스트
    public List<SpriteData> seaSpriteList;

    [Header("에니메이터 작동")]
    public Animator targetAnimator;

    [System.Serializable]
    public class SpriteData {
        [Header("오브젝트 타입 정보")]
        public ObjectType objectType;
        public Sprite targetSprite;
        //에니메이션 타입 정보
        public AnimationType animationType;
    }

    //타겟 이미지
    public Image targetImage;

    //이미지 교체 진행 
    public void SetImage(ObjectType objectType,int _index=0,bool isRandom=false) {

        List<SpriteData> targetSpriteDataList = null;
        switch (objectType) {
            case ObjectType.땅:
                targetSpriteDataList = landSpriteList;
                break;
            case ObjectType.하늘:
                targetSpriteDataList = skySpriteList;
                break;
            case ObjectType.바다:
                targetSpriteDataList = seaSpriteList;
                break;
        }
        
        if (objectType.Equals(ArrangementObject.ObjectType.바다))
        {
            if (_index < 2)
            {

                targetSpriteDataList = Array.FindAll(targetSpriteDataList.ToArray(),
                    item => item.animationType.Equals(ArrangementObject.AnimationType.배)).ToList();
                
            }
            else
            {
                targetSpriteDataList = Array.FindAll(targetSpriteDataList.ToArray(),
                    item => !item.animationType.Equals(ArrangementObject.AnimationType.배)).ToList();
                
            }
        }
        _index = UnityEngine.Random.Range(0, targetSpriteDataList.Count);
        targetImage.sprite = targetSpriteDataList[_index].targetSprite;
        animationType = targetSpriteDataList[_index].animationType;
        this.name = animationType.ToString();
        targetImage.SetNativeSize();
    }


}
