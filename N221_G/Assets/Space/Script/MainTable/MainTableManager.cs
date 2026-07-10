using SpaceGraphicsToolkit.Ring;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.UI;

public class MainTableManager : MonoBehaviour
{
    //메인 시스템 카메라
    public Camera mainSystemCamera;

    //충돌 VFX 오브젝트
    public List<GameObject> hitVfxObject;

    public StarControl starControl;
    [System.Serializable]
    public class StarControl {

        [Header("별오브젝트 [2D 기준]")]
        public StarObject starObject;
        public ElementHitCirclePoint elementHitCirclePoint;
        public Transform parentTransform;

        [Header("절치 초기 데이터")]
        public StarStepData initStarStepData;

        [Header("별 단계적 블랙홀 생성 데이터")]
        public List<StarStepData> starStepDataList;

        [Header("별 화면 생성 ")]
        public List<StarStepData> starViewStep;
    }

    //단계 데이터
    [System.Serializable]
    public class StarStepData {
        public enum Step {Init, Step1, Step2, Step3, Step4 };
        public Step step;

        //크기
        public float radius = 0;

        //배경 사이즈 정의
        public float back_InnerSize = 0;
        public float back_OuterSize = 0;
    }

    //리스트 정보 
    [ReadOnly]
    public List<ElementHitCollider> elementHitColliderList;


    public UserInsertTimeCompleteEvent userInsertTimeCompleteEvent;
    [System.Serializable]
    public class UserInsertTimeCompleteEvent:UnityEvent { }


    /// <summary>
    /// 인스턴스
    /// </summary>
    public static MainTableManager instance
    {
        get
        {
            return GameObject.FindObjectOfType<MainTableManager>();
        }
    }

    /// <summary>
    /// ElementHitCollider 존재 여부 파악
    /// </summary>
    /// <param name="elementHitCollider"></param>
    /// <returns></returns>
    public bool IsElementHitColliderDataExist(ElementHitCollider elementHitCollider, ElementHitCollider oppElementHitCollider) {
        int dis = (int)Vector3.Distance(elementHitCollider.transform.position, oppElementHitCollider.transform.position);
        if (dis < 5) {
            int index = Array.FindIndex(elementHitColliderList.ToArray(), item =>
                item.Equals(elementHitCollider) || item.Equals(oppElementHitCollider));
            if (index > -1)
            {
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }  
    }



    /// <summary>
    /// 히트 임펟트 진행되는구간
    /// </summary>
    /// <param name="CreatePosition"></param>
    public GameObject[] HitImpact(Vector3 CreatePosition) {
        GameObject[] ImpactObjects=new GameObject[2];
        for (int i=0; i< hitVfxObject.Count; i++) {
            GameObject impactObject= GameObject.Instantiate(hitVfxObject[i], CreatePosition,Quaternion.identity);
            ImpactObjects[i] = impactObject;
        }
        return ImpactObjects;
    }

    /// <summary>
    /// 별생성 (충돌되는 부분에 따라  2D 기준 오브젝트 생성)
    /// </summary>
    public void CreateStar(Vector3 createPosition) {
        Debug.Log("충돌 위치 : "+createPosition);
        GameObject addStarObject=GameObject.Instantiate(
            starControl.elementHitCirclePoint.gameObject, 
            createPosition, Quaternion.identity, 
            starControl.parentTransform).gameObject;
        ElementHitCirclePoint elementHitCirclePoint=addStarObject.GetComponent<ElementHitCirclePoint>();
        elementHitCirclePoint.mainSystemCamera = mainSystemCamera;
        elementHitCirclePoint.OnInit(starControl.starObject);
        //addStarObject.transform.position = createPosition;

    }

    //생성된 별삭제
    public void RemoveStar() {
        ElementHitCirclePoint[] elementHitCirclePointList = GameObject.FindObjectsOfType<ElementHitCirclePoint>();
        for (int i= elementHitCirclePointList.Length-1;i>=0 ;i--) {
            GameObject.Destroy(elementHitCirclePointList[i].gameObject);
        }

        StarObject[] StarObjects = GameObject.FindObjectsOfType<StarObject>();
        for (int i= StarObjects.Length-1;i>=0 ;i--) {
            GameObject.Destroy(StarObjects[i].gameObject);
        }
    }

    


    public UserTable userTable;

    [System.Serializable]
    public class UserTable {
        //현재 카운터 정보
        public int currentCount = 0;
        public int maxCount = 0;
        public Coroutine onUserInsertCountDown=null;

        public List<TextMeshProUGUI> textMeshProTextList;
    }

    //다른 체험자 진행 컨트롤
    public void InsertUser() {
        Debug.Log("사용자 카운터 진행");
        RemoveOnUserInsertCountDown();
        userTable.onUserInsertCountDown=StartCoroutine(UserInsertCountDown());
        
    }

    private void RemoveOnUserInsertCountDown() {
        if (userTable.onUserInsertCountDown != null) {
            StopCoroutine(userTable.onUserInsertCountDown);
            userTable.onUserInsertCountDown = null;
        }
    }


    //사용자 저장 카운터
    IEnumerator UserInsertCountDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            float currentCount = userTable.maxCount - userTable.currentCount;

            if (currentCount == 0)
            {
                Debug.Log("영상 타임라인 시작하는구간");
                RemoveOnUserInsertCountDown();
                userInsertTimeCompleteEvent.Invoke();
            }
            else {
                for (int i = 0; i < userTable.textMeshProTextList.Count; i++)
                {
                    userTable.textMeshProTextList[i].text = currentCount + "";

                }
                ++userTable.currentCount;
            }
        }
    }



}
