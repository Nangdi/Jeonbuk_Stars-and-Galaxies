using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//오브젝트 이미지 교체 진행 가능한지 확인하는구간
public class ContentObject : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    private MaterialPropertyBlock materialPropertyBlock;
    
    //변경 텍스쳐 정보
    public Texture2D changeTexture;

    void Start()
    {
        materialPropertyBlock = new MaterialPropertyBlock();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) {
            Debug.Log("키 입력 테스트");
            Met();
        }        
    }

    private void Met() {

        //변경 텍스쳐 정보
        materialPropertyBlock.SetTexture("_MainTex", changeTexture);
        spriteRenderer.SetPropertyBlock(materialPropertyBlock);
        //spriteRenderer.material.setp
    }


}
