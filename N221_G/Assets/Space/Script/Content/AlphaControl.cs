using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class AlphaControl : UIGroup
{

    //공투입시 안내 문구 삭제
    public void OnInsertBallEvent() {
        DOTween.To(() => alphaValue,value=> alphaValue=value,1,0.5f); 
    }

    //회전시 안내 문구 삭제
    public void OnRotatingDataEvent() {
        DOTween.To(() => alphaValue, value => alphaValue = value, 0, 0.5f);
    }


}
