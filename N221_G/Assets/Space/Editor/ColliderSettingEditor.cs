using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(ColliderSetting))]
public class ColliderSettingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ColliderSetting colliderSetting = target as ColliderSetting;
        if (GUILayout.Button("버튼 클릭")) {
            colliderSetting.SetPoint();
        }
    }

}
