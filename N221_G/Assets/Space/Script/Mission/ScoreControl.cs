using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

//스코어 컨트롤
public class ScoreControl : MonoBehaviour
{
    //사용자 테이블 컨트롤러
    public UserTableController userTableController;
    
    [Header("현재의 랭킹 정보")]
    public int currentRank;

    [Header("현재까지의 스코어 정보")]
    public int currentScore;
    
    //결과 텍스트 표현
    public string resultText;
    
    //텍스트 메쉬
    public TMP_Text _text;

    //결과 텍스트
    public TMP_Text _resultText;
    //{Data}등 | <size=50%>123점</size>
    [ReadOnly]
    private string defaultText = "{Data1}등 | <size=50%>{Data2}점</size>";
    [ReadOnly]
    private string defaultText_NoUser = "{Data1}점";

    //임펙트 오브젝트 컨트롤
    public GameObject viewEffObject;

    //ui 그룹 정보
    public UIGroup uiGroup;

    //스코어 데이터
    public ScoreData scoreData;

    [System.Serializable]
    public class ScoreData {
        //1등
        public GameObject firstPlace;
        //2등
        public GameObject secondPlace;
        //3등
        public GameObject thirdPlace;
    }

    private Coroutine onViewRank = null;

    //활성화
    private void OnEnable()
    {
        if (currentRank > -1)
        {
            viewEffObject.SetActive(true);
        }
        //OnResetData();
        onViewRank = StartCoroutine(OnViewRank());
       
    }

    //코루틴 초기화
    private void OnRemoveViewRank() {
        if (onViewRank != null) {
            StopCoroutine(onViewRank);
            onViewRank = null;
        }
    }

    IEnumerator OnViewRank() {
        yield return new WaitForSeconds(0.2f);
        DOTween.To(() => uiGroup.alphaValue, _value => uiGroup.alphaValue = _value, 1f, 0.3f);
    }


    private void OnDisable()
    {
        OnRemoveViewRank();
        currentRank = -1;
        currentScore = -1;
        uiGroup.alphaValue = 0;
        //OnResetData();
    }

    //랭킹 정보 등록
    public void SetRank(int rank) {
        currentRank = rank;
    }

    //현재의 스코어 정보 등록
    public void SetScore(int score) {
        currentScore = score;
    }

    private string TextChange_Rank(int data1, int data2)
    {
        string text = "";
        if (currentScore == 0)
        {
            text = TextChange_NoUser(data2);
        }
        else
        {
            text = defaultText.Replace("{Data1}", data1.ToString());
            text = text.Replace("{Data2}", data2.ToString());
        }
        return text;
    }
    private string TextChange_NoUser(int data1)
    {
        string text = defaultText_NoUser.Replace("{Data1}", data1.ToString());
        return text;
    }

    //텍스트 변경 
    public void SetText(string setText) {
        _text.text = TextChange_Rank(currentRank, currentScore);
        _resultText.text = setText;
        ScoreUI();
        
    }

    //다른 사용자가 존재 하지않는 단독일경우
    public void SetText_NoUser(string setText)
    {
        _text.text = TextChange_NoUser(currentScore);
        _resultText.text = setText;
        OnResetData();
        
    }


    //스코어 UI
    private void ScoreUI() {
        OnResetData();
        //스코어 값이 0일경우 작동되지 않음
        if (currentScore == 0) {
            return;
        }
        switch (currentRank) {
            case 1:
                scoreData.firstPlace.gameObject.SetActive(true);
                break;
            case 2:
                scoreData.secondPlace.gameObject.SetActive(true);
                break;
            case 3:
                scoreData.thirdPlace.gameObject.SetActive(true);
                break;
            case 4:
                break;
            case 5:
                break;
        }
    }

    //UI Reset
    public void OnResetData() {
        scoreData.firstPlace.gameObject.SetActive(false);
        scoreData.secondPlace.gameObject.SetActive(false);
        scoreData.thirdPlace.gameObject.SetActive(false);
      
    }




}
