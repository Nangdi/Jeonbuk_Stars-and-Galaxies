
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Rendering;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnityExample;
using System.Collections;

public class ScreenManager:MonoBehaviour
{
    //스크린 렌더러
    public SpriteRenderer spriteRenderer;
    public RenderTexture targetRenderTexture;

    private Coroutine onScreenControl = null;
    public Texture2D customTexture = null;

    /// <summary>
    /// 코루틴 리셋
    /// </summary>
    private void RemoveOnScreenControl() {
        if (onScreenControl != null) {
            StopCoroutine(onScreenControl);
            onScreenControl = null;
        }
    }
    private void OnEnable()
    {
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        OnInit();
    }

    private void OnDisable()
    {
        RemoveOnScreenControl();
    }

    public void OnInit() {
        RemoveOnScreenControl();
        onScreenControl=StartCoroutine(OnScreenControl());
    }

    IEnumerator OnScreenControl()
    {
        yield return null;
       
        while (true) {
            yield return null;
            RenderToTexture2D();
            //Utils.textureToTexture2D(texture, customTexture);
            //targetRenderTexture.
        }
    }


    private void RenderToTexture2D() {
        if (customTexture == null) {
            customTexture = new Texture2D(targetRenderTexture.width, targetRenderTexture.height, TextureFormat.RGBA32, false);
        }
        OpenCVForUnity.UnityUtils.Utils.textureToTexture2D(targetRenderTexture, customTexture);
        spriteRenderer.material.SetTexture("_MainTex", customTexture);
    }




}

