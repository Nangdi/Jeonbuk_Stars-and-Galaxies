using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ElementHitCollider : MonoBehaviour
{
    public ElementObject elementObject;

    //충돌 처리 
    //거리로 했을경우 문제점 존재
    public void OnTriggerEnter2D(Collider2D collision)
    {
        HitObject(collision);
    }

    /// <summary>
    /// 원소 끼리 충돌시 작동 되는 구간 -1 일경우 무조건 충돌 진행
    /// </summary>
    private void HitObject(Collider2D collision) {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer(elementObject.hitLayerMask)))
        {
            ElementHitCollider oppElementHitCollider= collision.gameObject.GetComponent<ElementHitCollider>();
            if (!oppElementHitCollider.elementObject.crashIndex.Equals(elementObject.crashIndex) || elementObject.crashIndex==-1) {

                elementObject.isMove = false;
                oppElementHitCollider.elementObject.isMove = false;

                elementObject.collider2D.enabled = false;
                oppElementHitCollider.elementObject.collider2D.enabled = false;
                Vector3 centerVer = GetCenterVer(this.transform, collision);

                if (!MainTableManager.instance.IsElementHitColliderDataExist(this, oppElementHitCollider))
                {
                    MainTableManager.instance.elementHitColliderList.Add(oppElementHitCollider);
                    //별생성
                    StartCoroutine(OnHitObject(centerVer));

                }
                else
                {
                    //별키우기
                    StartCoroutine(OnHitImpact(centerVer));

                }
            }   
        }
    }


    public Vector3 GetCenterVer(Transform transform, Collider2D collision) {
        Vector3 centerVer = Vector3.zero;
        float _x = (transform.position.x + collision.transform.position.x) / 2f;
        float _y = (transform.position.y + collision.transform.position.y) / 2f;
        centerVer.Set(_x, _y, this.transform.position.z);
        return centerVer;
    }


    //히트 오브젝트
    IEnumerator OnHitObject(Vector3 centerVer)
    {
        yield return null;
        elementObject.isMove = false;
        DOTween.To(() =>
                this.transform.localScale,
                localScale => this.transform.localScale = localScale,
                Vector3.zero, 0.5f);
      
        MainTableManager.instance.CreateStar(centerVer);
        yield return StartCoroutine(OnHitImpact(centerVer));
    }

    //히트 임펙트 
    public IEnumerator OnHitImpact(Vector3 centerVer) {
        yield return null;
        elementObject.isMove = false;
        DOTween.To(() =>
                this.transform.localScale,
                localScale => this.transform.localScale = localScale,
                Vector3.zero, 0.5f);

        GameObject[] impactObjects = MainTableManager.instance.HitImpact(centerVer);
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < impactObjects.Length; i++)
        {
            GameObject.Destroy(impactObjects[i]);
        }
        yield return null;
        GameObject.Destroy(elementObject.gameObject);
    }

}
