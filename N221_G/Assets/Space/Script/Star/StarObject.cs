using SpaceGraphicsToolkit.Flare;
using SpaceGraphicsToolkit.Ring;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using static MainTableManager.StarStepData;
using static MainTableManager;
using DG.Tweening;
using DG.Tweening.Core;
using static StarObject;

//별 오브젝트 컨트롤
public class StarObject : MonoBehaviour
{

    public PlayableDirector playableDirector;
    
    [Header("메인 시스템 카메라")]
    public Camera mainSystemCamera;

    private Coroutine onLoop = null;

    //컨트롤 오브젝트 설정
    public ControlObject controlObject;

    [System.Serializable]
    public class ControlObject
    {
        [Header("별 배경")]
        public SgtRing starBackgrand;

        [Header("별 오브젝트")]
        public List<StarSetting> starObjectList;

        public Transform starAddParent;

    }

    [System.Serializable]
    public class StarSetting {
        public GameObject starObject;
        //라이트 메쉬 정보
        public SgtFlareMesh sgtFlareMesh_Main;
        public SgtFlareMesh sgtFlareMesh_Sub;
    }

    [ReadOnly]
    public int selectStarIndex = -1;
    
    [ReadOnly]
    public StarSetting starSetting;

    public TimelineEvent timelineEvent;
    [System.Serializable]
    public class TimelineEvent: UnityEvent<GameStarClip.ClipType, float> { }

    public void OnEnable()
    {
        playableDirector.Stop();
        //OnInit(MainTableManager.instance.mainSystemCamera);
    }

    public void OnInit(Camera mainSystemCamera) {
        Debug.Log("[OnInit]");
        BaseSettingData(mainSystemCamera);
        StartLoop();
        playableDirector.Play();
    }

    //기본셋팅 설정구간
    private void BaseSettingData(Camera mainSystemCamera) {
        this.mainSystemCamera = mainSystemCamera;
        //별 렌덤 설정
        selectStarIndex = UnityEngine.Random.Range(0, controlObject.starObjectList.Count-1);
        starSetting = controlObject.starObjectList[selectStarIndex];
        starSetting.starObject.SetActive(true);

    }


    private void StartLoop() {
        RemoveOnLoop();
        onLoop=StartCoroutine(OnLoop());
    }


    private void RemoveOnLoop() {
        if (onLoop != null) {
            StopCoroutine(onLoop);
            onLoop = null;
        }
    }


    /// <summary>
    /// 루프 컨트롤설정구간
    /// 1. 카메라 방향에 따른 별의 위치 조정 시작 하는구간 
    /// 2. 빨려들어가는 블랙홀 배경 바라보는 방향 동적으로 변경
    /// </summary>
    /// <returns></returns>
    IEnumerator OnLoop() {
        yield return null;
        while (true) {
            yield return null;
            StarPosition();
            StarBackgrandLookCamera();
            //mainSystemCamera.
        }
    }

    public float createStarDistance = 0;

    //기즈모 작동
    public void OnDrawGizmos()
    {
        if (mainSystemCamera) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(mainSystemCamera.transform.position,0.5f);
            Gizmos.DrawLine(mainSystemCamera.transform.position, controlObject.starAddParent.transform.position);

            Gizmos.color = Color.blue;
            Vector3 heading = (controlObject.starAddParent.transform.position-mainSystemCamera.transform.position);
            //거리 정보 필요함
            var direction = heading / createStarDistance;
            Vector3 customVector = mainSystemCamera.transform.position + direction;
            Gizmos.DrawWireSphere(customVector, 1f);

            StarPosition();
            StarBackgrandLookCamera();
        }
    }

    //별의 위치 변경 하는구간
    private void StarPosition() {
        Vector3 heading = (controlObject.starAddParent.transform.position - mainSystemCamera.transform.position);
        //거리 정보 필요함
        var direction = heading / createStarDistance;
        Vector3 resultPosition = mainSystemCamera.transform.position + direction;
        for (int i=0;i< controlObject.starObjectList.Count; i++) {
            controlObject.starObjectList[i].starObject.transform.position = resultPosition;
        }
    }

    //별 배경 바라보는 위치 조정 
    private void StarBackgrandLookCamera() {
        Vector3 vec = mainSystemCamera.transform.position-controlObject.starBackgrand.transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        Quaternion q1 = Quaternion.Euler(new Vector3(90, 90, 90));
        controlObject.starBackgrand.transform.rotation = q * q1;

    }

    private GameStarClip.ClipType currentClipType = GameStarClip.ClipType.Init;
    
    //기본 스텝 1
    public StarStepData.Step insertStep= StarStepData.Step.Step1;

    public Tweener tweenerRadiusInner = null;
    public Tweener tweenerRadiusOuter = null;
    public Tweener tweenerStarRadius_main = null;
    public Tweener tweenerStarRadius_sub = null;
    private bool isTweenerComplete = true;

    //타임라인 연동 이벤트 핸들러
    public void SetTimelineEvent(GameStarClip.ClipType clipType, float duration) {

        if (!clipType.Equals(currentClipType)) {
            StarStepData starStepData = GetStarStepData(clipType);
            isTweenerComplete = false;
            tweenerRadiusInner = DOTween.To(() =>
                controlObject.starBackgrand.RadiusInner, value =>
                controlObject.starBackgrand.RadiusInner = value, starStepData.back_InnerSize, duration)
                    .OnComplete(AnimationComplete);

            tweenerRadiusOuter = DOTween.To(() =>
               controlObject.starBackgrand.RadiusOuter, value =>
               controlObject.starBackgrand.RadiusOuter = value, starStepData.back_OuterSize, duration);

            tweenerStarRadius_main = DOTween.To(() =>
               starSetting.sgtFlareMesh_Main.Radius, value =>
               starSetting.sgtFlareMesh_Main.Radius = value, starStepData.radius, duration);

            tweenerStarRadius_sub = DOTween.To(() =>
              starSetting.sgtFlareMesh_Sub.Radius, value =>
              starSetting.sgtFlareMesh_Sub.Radius = value, starStepData.radius, duration);

        }
        this.currentClipType = clipType;
    }

    //트윈 종료 여부 확인 
    void AnimationComplete() {
        isTweenerComplete = true;
    }


    //연쇄 반응에 따른 별 화면 표현 변환 
    public void StepStarChange() {
        StarStepData starStepData = GetStarStepData(currentClipType);

        if (!isTweenerComplete) {

            tweenerRadiusInner.ChangeEndValue(starStepData.back_InnerSize);
            tweenerRadiusOuter.ChangeEndValue(starStepData.back_OuterSize);
            tweenerStarRadius_main.ChangeEndValue(starStepData.radius);
            tweenerStarRadius_sub.ChangeEndValue(starStepData.radius);

        }
        else{

            tweenerRadiusInner = DOTween.To(() =>
                controlObject.starBackgrand.RadiusInner, value =>
                controlObject.starBackgrand.RadiusInner = value, starStepData.back_InnerSize, 0.5f)
                    .OnComplete(AnimationComplete);

            tweenerRadiusOuter = DOTween.To(() =>
               controlObject.starBackgrand.RadiusOuter, value =>
               controlObject.starBackgrand.RadiusOuter = value, starStepData.back_OuterSize, 0.5f);

            tweenerStarRadius_main = DOTween.To(() =>
              starSetting.sgtFlareMesh_Main.Radius, value =>
              starSetting.sgtFlareMesh_Main.Radius = value, starStepData.radius, 0.5f);

            tweenerStarRadius_sub = DOTween.To(() =>
              starSetting.sgtFlareMesh_Sub.Radius, value =>
              starSetting.sgtFlareMesh_Sub.Radius = value, starStepData.radius, 0.5f);
        }
    }

    private StarStepData GetStarStepData(GameStarClip.ClipType clipType)
    {
        StarStepData starStepData = null;
        int index = -1;
        switch (clipType) {
            case GameStarClip.ClipType.Init:
                //초기 
                starStepData = MainTableManager.instance.starControl.initStarStepData;
                break;
            case GameStarClip.ClipType.blackHoleCreate_start:
                //블랙홀 생성
                List<StarStepData> starStepDataList = MainTableManager.instance.starControl.starStepDataList;
                index = Array.FindIndex(starStepDataList.ToArray(), item => item.step.Equals(insertStep));
                starStepData = starStepDataList[index];
                break;
            case GameStarClip.ClipType.StarCreate_Start:
                //블랙홀 생성
                List<StarStepData> starViewStep = MainTableManager.instance.starControl.starViewStep;
                index = Array.FindIndex(starViewStep.ToArray(), item => item.step.Equals(insertStep));
                starStepData = starViewStep[index];
                break;
        }
        return starStepData;
    }

}
