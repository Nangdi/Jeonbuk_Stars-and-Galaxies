using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        /**Debug.Log("에디터 작동 확인:1");
        SerializedProperty componentVOSerializedProperty = serializedObject.FindProperty("componentVO");
        Debug.Log(componentVOSerializedProperty);
        Debug.Log("에디터 작동 확인:2");*/
        //componentVOSerializedProperty.en
    }

}
