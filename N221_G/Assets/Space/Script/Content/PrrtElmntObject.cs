using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(PrrtElmntObject))]
public class PrrtElmntObjectEditor:Editor { 

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PrrtElmntObject prrtElmntObject= (PrrtElmntObject)target;
        if (GUILayout.Button("이미지 교체 진행&스크립트 등록")) {
            prrtElmntObject.SetObject();
        }
    }

}
#endif

public class PrrtElmntObject : MonoBehaviour
{
    public List<Sprite> textureList;
    public List<GameObject> objectList;

    public List<TextureData> textureDataList;
    public List<SpriteRenderer> spriteRendererList;
    [System.Serializable]
    public class TextureData {
        public Texture2D texture;

    }



    private void OnEnable()
    {
        ObjectAlphaView();
    }

  
    //오브젝트 하면 알파 
    public void ObjectAlphaView() {
        for (int i=0;i< spriteRendererList.Count; i++) {
            spriteRendererList[i].color = Color.clear;
        }
    }

    //오브젝트 등록 설정
    public void SetObject() {
        spriteRendererList.Clear();
        for (int i=0;i< objectList.Count; i++) {
            PrrtElementObject userTableAddElementObject =objectList[i].GetComponent<PrrtElementObject>();
            SpriteRenderer _spriteRenderer = objectList[i].GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = textureList[i];
            userTableAddElementObject.index = i;
            userTableAddElementObject.spriteRenderer = _spriteRenderer;
            spriteRendererList.Add(_spriteRenderer);
        }
    }
}
