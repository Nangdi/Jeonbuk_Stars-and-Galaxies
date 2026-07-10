using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using JetBrains.Annotations;
using System;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(RectAreaDataView))]
public class RectAreaDataViewEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RectAreaDataView rectAreaDataView = target as RectAreaDataView;
        if (GUILayout.Button("위치에따른 오브젝트 검출"))
        {
            rectAreaDataView.SetEditorObject();
        }
    }
}
#endif
public class RectAreaDataView : MonoBehaviour
{
    public RectTransform rect;

    public Transform parentObject;

    public ArrangementObject[] customArrangementObjectList;

    public void OnEnable()
    {
        SetEditorObject();
    }


    //체크용
    public void SetEditorObject() {

        ArrangementObject[] arrangementObjectList= parentObject.GetComponentsInChildren<ArrangementObject>();
        customArrangementObjectList = 
            Array.FindAll(arrangementObjectList.ToArray(), item => isCheck(item));

        for (int i=0; i< customArrangementObjectList.Length; i++) {
            customArrangementObjectList[i].targetAnimator = customArrangementObjectList[i].GetComponent<Animator>();
        }
        Debug.Log(customArrangementObjectList.Length);
    }

    private bool isCheck(ArrangementObject arrangementObject)
    {
        bool result = false;
        if (rect.rect.Contains(rect.InverseTransformPoint(arrangementObject.transform.position), true))
        {
            result = true;
        }
        return result;
    }

}
