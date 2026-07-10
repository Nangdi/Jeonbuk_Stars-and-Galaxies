using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 원소 오브젝트 움직임 처리 
/// </summary>
public class ElementObject : MonoBehaviour
{

    [Header("렌덤 나아가는 각도")]
    [ReadOnly]
    public float rendom_advancing_angle;

    //루프 코루틴
    private Coroutine onLoop = null;

    // 시야 영역의 반지름과 시야 각도
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    //나아가는 방향 포인트 정보
    private Vector3 forwardPosition = Vector3.zero;
    public _2D_Reflection _2d_Reflection;
    public Transform moveTarget;
    
    //콜라이더 설정 
    public Collider2D collider2D;

    public bool isEditorMove = true;
    [ReadOnly]
    public bool isMove = false;
    public int elementIndex = 0;

    //생성된 비디오 타입 정보
    public UserTableSettingBase.StarVideoType starVideoType;

    public List<Sprite> littleStarSprite;
    public List<Sprite> bigStarSprite;
    public SpriteRenderer spriteRenderer;

    //충돌 인덱스 파생된 입자의 위치 정보
    public int crashIndex = 0;
  

    // Start is called before the first frame update
    void Start()
    {
        collider2D.enabled = false;
        //isMove = true;
        //onLoop = StartCoroutine(OnLoop());
    }

#if UNITY_EDITOR
    /// <summary>
    /// 기즈모 움직임 처리
    /// </summary>
    public void OnDrawGizmos()
    {
        Handles.color = Color.red;
        
        Vector3 viewAngleA = DirFromAngle(-viewAngle / 2, false);
        Vector3 viewAngleB = DirFromAngle(viewAngle / 2, false);
       


        Handles.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
        Handles.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);

        Handles.color = Color.blue;
        Vector3 viewAngleC = DirFromAngle(0, false);
        Handles.DrawLine(moveTarget.transform.position, moveTarget.transform.position + viewAngleC * viewRadius);

        //벡터 정보가 0,0,0 이 아닐경우 작동되는구간
        if (!forwardPosition.Equals(Vector3.zero))
        {
            Handles.color = Color.green;
            //float randomForwardAngle = UnityEngine.Random.Range(-viewAngle, viewAngle);
            //forwardPosition = DirFromAngle(randomForwardAngle / 2, false);
            Handles.DrawLine(transform.position, transform.position + forwardPosition * viewRadius);
        }
    }
#endif
    float GetAngle(Vector2 start, Vector2 end)
    {
        Vector2 v2 = end - start;
        return ((Mathf.Atan2(v2.y, v2.x)) * Mathf.Rad2Deg) + 90f;
    }


    /// <summary>
    /// 나아가는 방향렌덤 기즈모 설정 구간
    /// </summary>
    public void SetRandomForwardAngle() {

        float randomForwardAngle = UnityEngine.Random.Range(-viewAngle, viewAngle);
        forwardPosition = DirFromAngle(randomForwardAngle / 2, false);

    }

    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleDegrees += transform.eulerAngles.z*-1;
        }

        return new Vector3(Mathf.Cos((-angleDegrees + 90) * Mathf.Deg2Rad), Mathf.Sin((-angleDegrees + 90) * Mathf.Deg2Rad), 0);
    }

    /// <summary>
    /// 시작 커맨드
    /// </summary>
    public void OnInit() {
        collider2D.enabled = false;
        LookAtWorldCenter();
        SelectionAngle();
        OnRemoveLoop();
        onLoop=StartCoroutine(OnLoop());
        isMove = true;
        SetSptite();
        speed = UnityEngine.Random.Range(0.05f,0.3f);
        StartCoroutine(OnCollider());
    }


    /// <summary>
    /// 충돌체 생성
    /// </summary>
    /// <returns></returns>
    IEnumerator OnCollider() {
        yield return new WaitForSeconds(1f);
        collider2D.enabled = true;
    }


    private void SetSptite() {
        switch (starVideoType) {
            case UserTableSettingBase.StarVideoType.LittleStar:
                spriteRenderer.sprite = littleStarSprite[UnityEngine.Random.Range(0, littleStarSprite.Count)];
                break;
            case UserTableSettingBase.StarVideoType.BigStar:
                spriteRenderer.sprite = bigStarSprite[UnityEngine.Random.Range(0, bigStarSprite.Count)];
                break;

        }
       
    }




    public void OnDisable()
    {
        OnRemoveLoop();
    }


    void LookAtWorldCenter() {
        float _angle = GetAngle(this.transform.parent.position, this.transform.position);
        Vector3 eng = this.transform.localEulerAngles;
        eng.z = _angle;
        this.transform.localEulerAngles = eng;
    }


    /// <summary>
    /// 각도 선정
    /// </summary>
    private void SelectionAngle() {
        SetRandomForwardAngle();

        float _angle = GetAngle(forwardPosition*20f, this.transform.position);
        Vector3 eng = this.transform.localEulerAngles;
        eng.z = _angle;
        this.transform.localEulerAngles = eng;

        Debug.Log(_angle);
        //this.transform.LookAt(this.transform.parent);
    }


    private void OnRemoveLoop() {
        if (onLoop != null)
        {
            StopCoroutine(onLoop);
            onLoop = null;
        }
    }

    public int currentPoint = 0;
    public float speed = 5f;
    public float reachDist = 1f;
    
    /// <summary>
    /// 루프 작동 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator OnLoop()
    {
        yield return new WaitUntil(() => _2d_Reflection.line_rend);
        yield return new WaitUntil(() => _2d_Reflection.isUpdate);
        yield return new WaitUntil(()=> _2d_Reflection.line_rend.positionCount>0);
        int pointCount = _2d_Reflection.line_rend.positionCount;
        yield return null;
        Vector3[] points = new Vector3[pointCount];
        _2d_Reflection.line_rend.GetPositions(points);
        while (true) {
            yield return null;
            if (!isEditorMove )
            {
                if (isMove)
                {
                    float dist = Vector3.Distance(points[currentPoint], moveTarget.transform. localPosition);
                    moveTarget.transform.localPosition = Vector3.MoveTowards(moveTarget.transform.localPosition, 
                        points[currentPoint], Time.deltaTime * speed);
                    //|| (Mathf.Round(points[currentPoint].x)==0 && Mathf.Round(points[currentPoint].y) == 0)
                    if (dist <= reachDist)
                    {
                        currentPoint++;
                        if (currentPoint>2) {
                            crashIndex = -1;
                        }
                    }

                    if (currentPoint >= points.Length)
                    {
                        currentPoint = 0;
                    }
                }
            }
        }
    }

    //히트 영역 layerMask
    public string hitLayerMask;

    //충돌 처리(
    /// <summary>
    /// 원소 끼리 충돌시 작동 되는 구간
    /// </summary>
    public void HitObject() {

        //Ray2D ray = new Ray2D(wp, Vector2.zero);
        Vector3 viewAngleC = DirFromAngle(0, false);
        RaycastHit2D[] hits = Physics2D.RaycastAll(
            moveTarget.position,
            moveTarget.transform.position + viewAngleC * viewRadius);
        if (hits.Length > 0)
        {
           
            int index=Array.FindIndex(hits, item =>
                !item.collider.gameObject.Equals(moveTarget.gameObject) &&
                item.collider.gameObject.layer.Equals(LayerMask.NameToLayer(hitLayerMask)));
            if (index > -1) {
                RaycastHit2D his = hits[index];
                Debug.Log("[hit]");
                //1. 충돌시 정지 
                isMove = false;

                collider2D.enabled = false;
                his.collider.enabled = false;
                ElementObject elementObject=his.collider.transform.parent.parent.GetComponent<ElementObject>();
                elementObject.isMove = false;
                StartCoroutine(OnHitObject(his));

            }
        }
    }

    //오브젝트 히트 임펙트 생성 구역 (일단 위치 부터 확인)
    IEnumerator OnHitObject(RaycastHit2D his) { 
        yield return null;
        Vector3 centerVer = Vector3.zero;
        float _x = (this.transform.position.x + his.collider.transform.position.x) / 2f;
        float _y = (this.transform.position.y + his.collider.transform.position.y) / 2f;
        centerVer.Set(_x, _y, this.transform.position.z);
        MainTableManager.instance.CreateStar(centerVer);
    }



}
