using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/*
[CustomEditor(typeof(CommonSetting)), CanEditMultipleObjects]
public class CommonSettingEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CommonSetting commonSetting = target as CommonSetting;
        if (GUILayout.Button("이미지 메쉬 정보 변경")) {
            commonSetting.SetMeshData();
        }

    }

}*/


public class CommonSetting : MonoBehaviour
{
    //메쉬 정보
    [Header("메인 Sprite 정보")]
    public SpriteRenderer mainSpriteRenderer;

    [Header("메인 Renderer 정보")]
    public Renderer mainRenderer;

    //메쉬 사이즈 변경
    public void SetMeshData()
    {
        Vector3 parentScale = this.transform.parent.lossyScale;
        //복제 오브젝트 
        Vector3 _size = mainSpriteRenderer.bounds.extents;
        Bounds bounds = mainRenderer.localBounds;
        float scX = 1f / bounds.extents.x;
        Transform tf = mainRenderer.transform;
        Vector3 scale = tf.localScale;
        scale.Set((scX * _size.x) / parentScale.x, scX * _size.x / parentScale.y, scX * _size.x / parentScale.z);
        tf.localScale = scale;

        

    }

}
