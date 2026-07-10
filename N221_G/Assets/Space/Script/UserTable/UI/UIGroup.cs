using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class UIGroup : MonoBehaviour
{
    public float alphaValue;

    public List<RendererData> renderers;

    public List<TextMeshPro> textMeshProList;

    [System.Serializable]
    public class RendererData {
        public Renderer renderer;
        public MaterialPropertyBlock materialPropertyBlock;
    }

    public void Update()
    {
        UIGroupAlpha();
    }

    private void UIGroupAlpha() {
        for (int i=0;i< renderers.Count; i++) {
            RendererData rendererData = renderers[i];
            if (rendererData.materialPropertyBlock == null) {
                rendererData.materialPropertyBlock = new MaterialPropertyBlock();
            }

            Color baseColor = rendererData.renderer.sharedMaterial.GetColor("_BaseColor");
            baseColor.a= alphaValue;
            rendererData.materialPropertyBlock.SetColor("_BaseColor", baseColor);
            rendererData.renderer.SetPropertyBlock(rendererData.materialPropertyBlock);
        }

        for (int i=0;i< textMeshProList.Count; i++) {
            textMeshProList[i].alpha= alphaValue;
        }
    }

}
