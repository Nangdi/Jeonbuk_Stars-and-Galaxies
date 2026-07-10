using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserTableAddElementObject : MonoBehaviour
{
    //public List<Sprite> textureList;
    public Renderer renderer;
    public MaterialPropertyBlock materialPropertyBlock;

    //움직임 타겟 오브젝트
    public Transform moveTargetObject;
    
    private Coroutine onInit = null;

    //분류 확인 인덱스 정보
    public int index = -1;

    [Header("모션 타입 선언")]
    public MotionType motionType;
    //모션 타입 구분 지정
    //모션1 : 미션1 모션 - 주기율표에 원자 안착 
    //모션2 : 미션2 모션 - 지구에 원자 안착 (모션까지 구성 필요)
    public enum MotionType { mission1Motion, mission2Motion }

    [Header("충돌 파티클 시스템")]
    public ParticleSystemImpect hitImpectParticleSystem;
    

    public void OnInitStart()
    {
        OnRemoveInit();
        onInit = StartCoroutine(OnInit());
    }

    private void OnRemoveInit()
    {
        if (onInit != null)
        {
            StopCoroutine(onInit);
            onInit = null;
        }
    }

    IEnumerator OnInit()
    {
        //렌덤 움직임 작동구간
        yield return new WaitForSeconds(UnityEngine.Random.Range(0, 0.8f));
        var moveRandomTime=UnityEngine.Random.Range(1.5f, 3f);
        DOTween.To(() => transform.position,
            pos => transform.position = pos, moveTargetObject.transform.position, moveRandomTime).OnComplete(OnCompleteEvent);

        DOTween.To(() => transform.localScale,
            pos => transform.localScale = pos, new Vector3(0.05f, 0.05f, 0.05f), moveRandomTime);

    }

    private void OnCompleteEvent()
    {
        Debug.Log("[OnCompleteEvent]");
    }
    //할성화 작동시 

    public void OnEnable()
    {
    }

    private void OnDisable()
    {
        OnRemoveInit();
    }

    private void OnDestroy()
    {

        //무브 오브젝트 삭제 진행 
        if (motionType.Equals(MotionType.mission2Motion))
        {
            GameObject.Destroy(moveTargetObject.gameObject);
        }
        OnRemoveInit();
    }

    private void CreateMaterialPropertyBlock() {
        if (materialPropertyBlock == null)
        {
            materialPropertyBlock = new MaterialPropertyBlock();
        }
    }


    //재질 변경
    public void SeMaterial(Texture2D texture) {
        CreateMaterialPropertyBlock();
        materialPropertyBlock.SetTexture("_BaseMap", texture);
        renderer.SetPropertyBlock(materialPropertyBlock);
    }

    //컬러 변경 시작 구간
    private void ColorChange() {
        renderer.material.DOColor(Color.clear, "_BaseColor", 0.3f);
    }

    public void Update()
    {
        if (moveTargetObject) {
            float distance=
                Vector3.Distance(moveTargetObject.position, this.transform.position);
            if (motionType.Equals(MotionType.mission1Motion))
            {
                if (distance < 0.01f)
                {
                    GameObject.Destroy(this.gameObject);
                  
                }
            } else if (motionType.Equals(MotionType.mission2Motion)) {
                //Debug.Log(distance);
                /**else if (motionType.Equals(MotionType.mission2Motion))
                  {
                      //모션 완료 이벤트 핸들러(파티클 생성)
                      hitImpectParticleSystem.gameObject.SetActive(true);
                }*/
                if (distance < 0.01f)
                {
                    ColorChange();
                    hitImpectParticleSystem.gameObject.SetActive(true);
                    hitImpectParticleSystem.destroyObject = this.gameObject;

                }
            }
        }
    }

}
