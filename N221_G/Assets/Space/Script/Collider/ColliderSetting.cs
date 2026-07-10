using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class ColliderSetting : MonoBehaviour
{
    //public int segments = 10;
    public float angle = 20f;
    //public float radius = 1f;
    public EdgeCollider2D edgeCollider2D;
    [SerializeField][Range(0f, 10f)] private float speed = 1;
    [SerializeField][Range(0f, 40f)] private float radius = 1;

    //포인트 선정 구간(원형 충돌 구성)
    public void SetPoint() {
        Array.Clear(edgeCollider2D.points,0, edgeCollider2D.points.Length);
        float _x;
        float _y;
        float z = 0f;

        int segments = 50;
        //edgeCollider2D.points = new Vector2[segments+1];
        List<Vector2> pointList = new List<Vector2>();
        for (int i=0; i< segments+1; i++) {
            _x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            _y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            angle += (360f / segments);
          
            //Vector2 pos = edgeCollider2D.points[i];
            //pos.Set(_x, _y);
            Vector2 pos = new Vector2(_x, _y); ;
            pointList.Add(pos);
            /*runningTime += Time.deltaTime * speed;
            float x = radius * Mathf.Cos(runningTime);
            float y = radius * Mathf.Sin(runningTime);
            newPos = new Vector2(x, y);*/
        }
        edgeCollider2D.SetPoints(pointList);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
