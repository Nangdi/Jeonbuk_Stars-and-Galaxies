using Runtime2DTransformInteractor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

/// <summary>
/// 별과은하의 탄생 /지구 달의 탄생 UI 현장 위치 조정용으로 사용 
/// 각 테이블 위치 마우스로 조정(런타임으로 조정 가능하도록 구사)
/// </summary>
public class UILocationComponent : MonoBehaviour
{
    //인덱스 정보
    [Header("사용자 인덱스")]
    public int index = 0;

    [SerializeField]
    private Transform targetTF;

    private Vector2 localPosition;
    
    public Vector2 LocalPosition
    {
        get {
            return targetTF.localPosition;
        } 
        set
        {
            localPosition = value;
            SetPos(localPosition);
        }
    }

    public TransformInteractor transformInteractor;
    
    //이미지 렌더러
    public SpriteRenderer spriteRenderer;

    private Coroutine onSelectObject = null;

    //포지션 정의 구간
    private void SetPos(Vector2 _pos)
    {
        if (targetTF)
        {
            targetTF.localPosition = _pos;
        }
        else
        {
            Debug.LogError("타겟이 지정되어있지않음! 확인 필요");
        }
    }
   
    //활성화
    public virtual void OnEnable()
    {
        RemoveOnSelectObject();
        onSelectObject =StartCoroutine(OnSelectObject());
    }

    private void RemoveOnSelectObject() {
        if (onSelectObject != null) {
            StopCoroutine(onSelectObject);
            onSelectObject = null;
        }
    }

    //데이터 업데이트
    IEnumerator OnSelectObject() {
        while (true) {
            yield return null;
            yield return new WaitUntil(() => transformInteractor.selected);
            ComponentVO.ComponentList componentList = GetComponentList();
            componentList.X = this.transform.localPosition.x;
            componentList.Y = this.transform.localPosition.y;
        }
    }

    private ComponentVO.ComponentList GetComponentList() {
        return GameManager.instance.componentVO.componentList[index];
    }

    /// <summary>
    /// 렌더러 데이터
    /// </summary>
    public void RendererSprite(float value) {
        Color _color = spriteRenderer.color;
        _color.a = value;
        spriteRenderer.color = _color;
    }



}
