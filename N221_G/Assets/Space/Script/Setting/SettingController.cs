using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using static UnityEngine.GraphicsBuffer;
#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(SettingController))]
public class SettingControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SettingController _target = target as SettingController;

        if (GUILayout.Button("카메라 위치 사이즈 정의"))
        {
            _target.mainTargetCamera.orthographicSize = 
                _target.SetCameraSize();
        }

        if (GUILayout.Button("메쉬 사이즈 확인")) {
            _target.SetMeshData();
        }

    }
}
#endif

/// <summary>
/// 메인 셋팅 화면 구성
/// </summary>
public class SettingController : MonoBehaviour
{

    //현재의 카메라 정보
    [Header("메인 Camera 정보")]
    public Camera mainTargetCamera;

    //메쉬 정보
    [Header("메인 Sprite 정보")]
    public SpriteRenderer mainSpriteRenderer;

    [Header("메인 Renderer 정보")]
    public Renderer mainRenderer;

    /// <summary>
    /// 카메라 셋팅
    /// </summary>
    /// <returns></returns>
    public float SetCameraSize()
    { 
        //크기 정보
        Vector3 _size = mainSpriteRenderer.bounds.extents;
        //위치 정보
        Vector3 _center = mainSpriteRenderer.bounds.center;
        _center.z = -50f;
        mainTargetCamera.transform.position = _center;
        return _size.x;
    }


    //메쉬 사이즈 변경
    public void SetMeshData() {
        
        //복제 오브젝트 
        Vector3 _size = mainSpriteRenderer.bounds.extents;
        Bounds bounds = mainRenderer.localBounds;
        float scX = 1f / bounds.extents.x;
        Transform tf = mainRenderer.transform;
        Vector3 scale=tf.localScale;
        scale.Set(scX * _size.x, scX * _size.x, scX * _size.x);
        tf.localScale = scale;

    }


}
