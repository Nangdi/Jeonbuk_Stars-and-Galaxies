using System.Collections;
using System.Collections.Generic;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(ScreenMeshToSpriteSetting))]
public class ScreenMeshToSpriteSettingEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ScreenMeshToSpriteSetting _target = target as ScreenMeshToSpriteSetting;
        


    }
}
#endif
public class ScreenMeshToSpriteSetting : MonoBehaviour
{

    //현재의 카메라 정보
    public Camera targetCamera;

    //메쉬 정보
    public SpriteRenderer spriteRenderer;


    public void GetData()
    {

        //크기 정보
        Vector3 _size= spriteRenderer.bounds.extents;
        
        //위치 정보
        Vector3 _center = spriteRenderer.bounds.center;
        

        Debug.Log(_size);
        Debug.Log(_center);
        Debug.Log("bounds extents : " + spriteRenderer.bounds.extents);

        /**
        Debug.Log(spriteRenderer.localBounds);
        Debug.Log(spriteRenderer.bounds);
        Debug.Log(targetCamera.aspect);
        Debug.Log(targetCamera.pixelRect);
        */
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
