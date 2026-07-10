using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//점수 텍스트 등록 처리 구간
public class AddScoreText : MonoBehaviour
{
    public TextMeshPro text;
    //정답 유무 관련
    private bool isRightAnswer = false;

    //정답 컬러
    public Color rightAnswerColor;
    
    //오답 컬러
    public Color mustColor;
    //에니메이터 정보
    public Animator animator;

    /// <summary>
    /// 텍스트 스케일 정보
    /// </summary>
    private void TextScale() {
        Vector3 scale = text.transform.localScale;
        scale = Vector3.zero;
        text.transform.localScale = scale;
    }


    //시작 커맨드 작동 구간
    public void OnInit(bool isRightAnswer, int score) { 
        Debug.Log("OnInit");
        this.isRightAnswer = isRightAnswer;
        string addText = "";
        if (isRightAnswer) {
            addText = "+";
        }else{
            addText = "-";
        }
        SetText(addText+score);
        SetTextColor();

        animator.SetBool("IsView",true);

    }

    private void SetTextColor() {
        if (isRightAnswer)
        {
            text.color = rightAnswerColor;
        }
        else {
            text.color = mustColor;
        }
    }


    //파괴 오브젝트(해당 품목 제거)
    public void OnDestroyObject() {
        //오브젝트 삭제
        GameObject.Destroy(this.gameObject);
    }


    //텍스트 등록
    private void SetText(string textData) {
        text.text = textData;

        
    }

}
