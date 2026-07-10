using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using JetBrains.Annotations;
using System;
using static ArrangementObject;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(ObjectDeployment))]
public class ObjectDeploymentEditor : Editor {

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        ObjectDeployment objectDeployment = target as ObjectDeployment;

        //나무/빌딩(땅)
        if (GUILayout.Button("포인트 생성(나무/빌딩)")) {
            objectDeployment.AddChildPoint();
        }
        if(GUILayout.Button("자식들 위치 조정을 위한 배열 정보(나무/빌딩)")) {
            objectDeployment.CreateFloorObject();
        }

        //구름 / 비행기(하늘) 위치 선정 구간
        if (GUILayout.Button("포인트 생성(구름/비행기)"))
        {
            objectDeployment.AddCloudChildPoint();
        }
        if (GUILayout.Button("자식들 위치 조정을 위한 배열 정보(구름/비행기)"))
        {
            objectDeployment.CreateSkyObject();
        }

        if (GUILayout.Button("포인트 및 각도 생성(자동차/자전거)"))
        {
            List<Vector3> posList = objectDeployment.posList2;
            List<float> angList = objectDeployment._angleList2;
            int objectCount = objectDeployment.pos2Count;
            int radius = objectDeployment.radius2Radius;
            objectDeployment.AddChildPointCustom(posList, angList, objectCount, radius);
        }

        if (GUILayout.Button("자식들 위치 조정을 위한 배열 정보(자동차/자전거)"))
        {
            objectDeployment.CreateFloor2Object();
        }

        //보트/요트 바다 위치 선정 구간

    }
}
#endif

public class ObjectDeployment : MonoBehaviour
{
    //오브젝트 정보 
    public GameObject _object;

    //부모 오브젝트 정보
    [Header("부모 오브젝트 정보")]
    public GameObject parentObject;
    public GameObject parentCloudObject;
    public GameObject parentObject_2;

    public int createIndex = 0;

    public List<Vector3> posList;
    public List<float> _angleList;
    public float radius;

    //구름 리스트 
    public List<Vector3> cloudListPos;

    //구름 배치 위치 각도 
    public List<float> angleCloudList;
    public int createCloudIndex = 0;
    public int createCloudRadius = 0;

    //땅2(자동차 자전거 등 컨트롤용
    [Header("땅2")]
    public List<Vector3> posList_2;
    public List<float> po_angleList;


    public List<Vector3> posList2;
    public List<float> _angleList2;
    public int pos2Count;
    public int radius2Radius;
    
    //보트 에어리어
    public SetBoatAreaControl setBoatAreaControl;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i=0;i< posList.Count; i++)
        {
            Gizmos.DrawSphere(posList[i], 0.1f);
        }

        Gizmos.color = Color.green;

        for (int i = 0; i < cloudListPos.Count; i++)
        {
            Gizmos.DrawSphere(cloudListPos[i], 0.1f);
        }
        Gizmos.color = Color.yellow;

        for (int i = 0; i < posList2.Count; i++)
        {
            Gizmos.DrawSphere(posList2[i], 0.1f);
        }
    }

    //자식 배치를 위치를 위한 포인트 정보
    public void AddChildPoint() {
        posList.Clear();
        _angleList.Clear();
        for (int i = 0; i < createIndex; i++)
        {
            float angle = i * (Mathf.PI * 2.0f) / createIndex;
            Vector3 pos=(new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0)) * radius;
            posList.Add(pos);
            _angleList.Add(angle);
        }
    }

    //땅/하늘/바다 연동
    public void CreateFloorObject() {
        CreateObject(ArrangementObject.ObjectType.땅, parentObject, posList);
    }

    public void CreateSkyObject() {
        CreateObject(ArrangementObject.ObjectType.하늘, parentCloudObject, cloudListPos);
    }

    public void CreateFloor2Object()
    {
        CreateObject(ArrangementObject.ObjectType.바다, parentObject_2, posList2);
        setBoatAreaControl.SetBoatAreaObject();
    }


    public void AngleChange() {
        AngleChange(parentObject);
        AngleChange(parentCloudObject);
        AngleChange(parentObject_2);
    }

    //각도 변경
    public void AngleChange(GameObject parentObject)
    {
        ArrangementObject[] chList = parentObject.GetComponentsInChildren<ArrangementObject>();
        for (int i = 0; i < chList.Length; i++)
        {
            GameObject gameObject = chList[i].gameObject;
            Vector2 dis = new Vector2(
                gameObject.transform.localPosition.x - parentObject.transform.localPosition.x,
                gameObject.transform.localPosition.y - parentObject.transform.localPosition.y
                );

            float angle = Mathf.Atan2(dis.y, dis.x) * Mathf.Rad2Deg;
            RectTransform _thisRect = gameObject.GetComponent<RectTransform>();
            Vector3 _angles = _thisRect.localEulerAngles;
            _angles.z = angle - 90;
            _thisRect.localEulerAngles = _angles;

        }
    }

    //오브젝트 생성
    public void CreateObject(ArrangementObject.ObjectType objectType,
        GameObject parentObject,List<Vector3> posList) {

        ArrangementObject[] chList = parentObject.GetComponentsInChildren<ArrangementObject>();
        for (int i= chList.Length-1;i>=0; i--) {
#if UNITY_EDITOR
            GameObject.DestroyImmediate(chList[i].gameObject);
#else
            GameObject.Destroy(chList[i].gameObject);
#endif
        }

        if (objectType.Equals(ArrangementObject.ObjectType.바다))
        {
            setBoatAreaControl.arrangementObjectList.Clear();
        }

        int index = 0;
        for (int i=0;i< posList.Count; i++) {

            //자식 오브젝트 생성 처리 구간
            GameObject gameObject = GameObject.Instantiate(_object, parentObject.transform);
            gameObject.transform.position = posList[i];

            Vector2 dis = new Vector2(
                gameObject.transform.localPosition.x - parentObject.transform.localPosition.x,
                gameObject.transform.localPosition.y - parentObject.transform.localPosition.y
                );

            float angle = Mathf.Atan2(dis.y, dis.x) * Mathf.Rad2Deg;

            RectTransform _thisRect = gameObject.GetComponent<RectTransform>();
            Vector3 _angles = _thisRect.localEulerAngles;
            _angles.z = angle - 90;
            _thisRect.localEulerAngles = _angles;

            ArrangementObject arrangementObject = gameObject.GetComponent<ArrangementObject>();
           
            if (objectType.Equals(ArrangementObject.ObjectType.바다))
            {
                arrangementObject.SetImage(objectType, i);
                if (i < 2)
                {
                    
                    setBoatAreaControl.arrangementObjectList.Add(arrangementObject);
                }
            }
            else
            {
                arrangementObject.SetImage(objectType);
            }

        }
    }

    //구름 자식 포인트 등록 구간
    public void AddCloudChildPoint() {
        cloudListPos.Clear();
        angleCloudList.Clear();
        for (int i = 0; i < createCloudIndex; i++)
        {
            float angle = i * (Mathf.PI * 2.0f) / createCloudIndex;
            Vector3 pos = (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0)) * createCloudRadius;
            cloudListPos.Add(pos);
            angleCloudList.Add(angle);
        }
    }


    //구름 자식 포인트 등록 구간
    public void AddChildPointCustom(List<Vector3> posList, List<float> angList,int objectCount,int radius)
    {
        posList.Clear();
        angList.Clear();
        for (int i = 0; i < objectCount; i++)
        {
            float angle = i * (Mathf.PI * 2.0f) / objectCount;
            Vector3 pos = (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0)) * radius;
            posList.Add(pos);
            angList.Add(angle);
        }
    }



}
