using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//사용자 테이블 셋팅 현장 설정용
[CustomEditor(typeof(UserTableSetting))]
public partial class UserTableSettingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UserTableSetting userTableSetting=target as UserTableSetting;
        if (GUILayout.Button("하위 글자 변경")) {
            userTableSetting.TextSizeChange();
         
        }

        if (GUILayout.Button("테이블 셋팅"))
        {
            userTableSetting.SetMeshData();
            //TableSetting();
            //userTableSetting.TextSizeChange();
        }
    }
}
