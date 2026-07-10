using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MainTableManager;
using static StarObject;



//충돌영역 카메라 좌표 연계 
public class ElementHitCirclePoint : MonoBehaviour
{
    //메인 카메라 설정
    public Camera mainSystemCamera;
    public StarObject starObject;
    private Coroutine onLoop = null;
    
    [Header("히트 영역 layerMask")]
    public string hitLayerMask;

    //사운드 클래스
    public SoundClass soundClass;

    [System.Serializable]
    public class SoundClass
    {
        //빛 충돌 생성 음향
        public AudioClip audioClip1;
        public AudioClip audioClip2;
        public AudioSource audioSource;
    }

    //콜라이더 정보
    public Collider2D collider2D;

    private void OnEnable()
    {
        collider2D.enabled = false;
        soundClass.audioSource.clip =
                       soundClass.audioClip1;
        SoundPlay();
        RemoveOnLoop();
        onLoop =StartCoroutine(OnLoop());
        StartCoroutine(OnInitStart());
    }

    IEnumerator OnInitStart() {
        yield return new WaitForSeconds(0.2f);
        collider2D.enabled = true;
        yield return new WaitForSeconds(1.5f);
        collider2D.enabled = false;

    }

    //시작 커맨드 
    public void OnInit(StarObject starObject) {
        CreateStarObject(starObject);
    }

    //별오브젝트 생성 구간 
    private void CreateStarObject(StarObject starObject) {

        GameObject addStarObject = GameObject.Instantiate(
          starObject,
          Vector3.one * 500f, Quaternion.identity).gameObject;
        this.starObject = addStarObject.GetComponent<StarObject>();
        this.starObject.OnInit(mainSystemCamera);
    }



    //루프 정지
    private void RemoveOnLoop() {
        if (onLoop != null) {
            StopCoroutine(onLoop);
            onLoop = null;
        }
    }

    //루프 작동 (코루틴)
    IEnumerator OnLoop() {
        yield return new WaitUntil(()=> starObject);
        while (true) {
            yield return null;
            StarPosition();
            //HitObject();
        }
    }

    private Coroutine onTriggerDisable = null;

    //충돌 트리거 삭제 진행
    IEnumerator OnTriggerDisable() {
        yield return null;
        collider2D.enabled = false;
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        //StartCoroutine(OnTriggerDisable());
        HitObject(collision);
    }
    private void HitObject(Collider2D collision)
    {

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer(hitLayerMask)))
        {
            int stepValue = (int)starObject.insertStep;
            if (starObject.tweenerRadiusInner != null &&
                starObject.tweenerRadiusOuter != null)
            {
                //int stepValue = (int)starObject.insertStep;
                ++stepValue;
                if (stepValue < 4)
                {
                    collision.enabled = false;
                    starObject.insertStep = (StarStepData.Step)stepValue;
                    starObject.StepStarChange();
                }
            }
            else
            {
                ++stepValue;
                if (stepValue < 4)
                {
                    collision.enabled = false;
                    //타임라인 생성도되기 전에 충돌했을경우
                    starObject.insertStep = (StarStepData.Step)stepValue;
                    starObject.StepStarChange();
                }
            }
            if (stepValue == 0)
            {
                //soundClass.audioSource.clip =
                //       soundClass.audioClip1;
                //SoundPlay();
            }
            else {
                soundClass.audioSource.clip =
                       soundClass.audioClip2;
                SoundPlay();
            }
            ElementHitCollider oppElementHitCollider = collision.gameObject.GetComponent<ElementHitCollider>();
            Vector3 centerVer = oppElementHitCollider.GetCenterVer(this.transform,collision);
            StartCoroutine(oppElementHitCollider.OnHitImpact(centerVer));
        }
    }

    public void SoundPlay()
    {
        soundClass.audioSource.Play();
    }


    //private StarStepData.Step currentStep = StarStepData.Step.Step1;
    /// <summary>
    /// 별 생성시 원소 기호 충돌처리 관련 
    /// 별 충돌 누적 처리 하는공간 
    /// </summary>
    private void HitObject() {
        //외부 원소기호 충돌 처리 
        RaycastHit2D[] hits = Physics2D.RaycastAll(
           this.transform.position,
           this.transform.right*2f);

        if (hits.Length > 0)
        {

            int index = Array.FindIndex(hits, item =>
                !item.collider.gameObject.Equals(this.transform.gameObject) &&
                item.collider.gameObject.layer.Equals(LayerMask.NameToLayer(hitLayerMask)));
            if (index > -1) {

                RaycastHit2D his = hits[index];
                int stepValue = (int)starObject.insertStep;
                if (starObject.tweenerRadiusInner != null &&
                    starObject.tweenerRadiusOuter != null)
                {
                    //int stepValue = (int)starObject.insertStep;
                    ++stepValue;
                    if (stepValue < 4)
                    {
                        his.collider.enabled = false;
                        starObject.insertStep = (StarStepData.Step)stepValue;
                        starObject.StepStarChange();
                    }
                }
                else
                {
                    ++stepValue; 
                    if (stepValue < 4)
                    {
                        his.collider.enabled = false;
                        //타임라인 생성도되기 전에 충돌했을경우
                        starObject.insertStep = (StarStepData.Step)stepValue;
                        starObject.StepStarChange();
                    }
                }
            }
        }
    }


    /// <summary>
    /// 별생성 위치 선정 구간 
    /// </summary>
    public void StarPosition() {
        Vector3 pos = mainSystemCamera.transform.position + this.transform.position;
        pos.z = -7f;
        Vector3 starPosition = starObject.transform.position;
        starPosition = pos;
        starObject.transform.position = starPosition;
    }


    public void OnDrawGizmos()
    {
        if (mainSystemCamera)
        {
            Gizmos.color = Color.green;
            Vector3 pos = mainSystemCamera.transform.position + this.transform.position;
            pos.z = -7f;
            Gizmos.DrawLine(mainSystemCamera.transform.position, pos);
            Gizmos.DrawWireSphere(pos, 0.5f);
            if (starObject)
            {
                Vector3 starPosition = starObject.transform.position;
                starPosition = pos;
                starObject.transform.position = starPosition;
            }
        }
    }







}
