
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SettingControl : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public KeyCode keyCode;
    public void Update()
    {
        if (DebugInputLock.Locked) return; // 프로젝터 설정창 열림 중 디버그 입력 잠금
        if (Input.GetKeyUp(keyCode)) {
            //파일로 저장
            string data = "{'size':'"+ spriteRenderer.transform.localScale+ "'}";
            data += "pos:" + spriteRenderer.transform.position+ "";
            System.IO.File.WriteAllText(Application.streamingAssetsPath+Path.DirectorySeparatorChar+"dataTestViw.json", data);

        }
    }

}
