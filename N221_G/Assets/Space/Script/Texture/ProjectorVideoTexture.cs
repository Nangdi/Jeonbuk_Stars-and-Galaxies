using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ProjectorVideoTexture;
using OpenCVRect = OpenCVForUnity.CoreModule.Rect;


public class ProjectorVideoTexture : MonoBehaviour
{
    //렌더 텍스쳐
    public RenderTexture renderTexture;
    public Texture2D renderTexture2D = null;
    private Coroutine onLoop = null;
    Mat renderMat = null;

    private void OnEnable()
    {
        OnInit();
    }

    public void OnInit() {
        RemoveOnLoop();
        onLoop = StartCoroutine(OnLoop());
    }

    private void RenderTextureToMat() {
        if (renderMat == null)
        {
            renderMat = new Mat(renderTexture.height, renderTexture.width, CvType.CV_8UC4);
            renderTexture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        }
        Utils.textureToTexture2D(renderTexture, renderTexture2D);

    }

    private void RemoveOnLoop() {
        if (onLoop != null) {
            StopCoroutine(onLoop);
            onLoop = null;
        }
    }

    IEnumerator OnLoop() {
        while (true)
        {
            yield return null;
            RenderTextureToMat();
            SettingTexture();
        }
    }


    Mat videoMat1 = null;
    Mat videoMat2 = null;


    public CropData cropData_1;
    public CropData cropData_2;
    [System.Serializable]
    public class CropData {
        public OpenCVRect rectCrop = null;
        public Mat rectCropMat = null;
        public RawImage rawImage;
        public Texture2D cropTexture2D = null;
    }
   



    private void SettingTexture() {
        if (videoMat1 == null)
        {
            //videoMat = new Mat(videoTexture.height, videoTexture.width, CvType.CV_8UC4);
            videoMat1 = new Mat(renderTexture2D.height, renderTexture2D.width, CvType.CV_8UC4);
            videoMat2 = new Mat(renderTexture2D.height, renderTexture2D.width, CvType.CV_8UC4);
        }
        Utils.texture2DToMat(renderTexture2D, videoMat1, true, 0);
        Utils.texture2DToMat(renderTexture2D, videoMat2, true, 0);
        float p = 1920f / renderTexture2D.width;
        if (cropData_1.cropTexture2D == null)
        {
            cropData_1.rectCrop.x = (int)(cropData_1.rectCrop.x / p);
            cropData_1.rectCrop.y = (int)(cropData_1.rectCrop.y / p);

            cropData_1.rectCrop.width = (int)(cropData_1.rectCrop.width / p);
            cropData_1.rectCrop.height = (int)(cropData_1.rectCrop.height / p);
            //renderTexture2D.width
            cropData_1.rectCropMat= new Mat(videoMat1, cropData_1.rectCrop);
            cropData_1.cropTexture2D = new Texture2D(cropData_1.rectCrop.width, cropData_1.rectCrop.height, 
                TextureFormat.ARGB32, false);

        }
        Utils.matToTexture2D(cropData_1.rectCropMat, cropData_1.cropTexture2D);
        cropData_1.rawImage.texture = cropData_1.cropTexture2D;

        
        if (cropData_2.cropTexture2D == null)
        {
            cropData_2.rectCrop.x = (int)(cropData_2.rectCrop.x / p); 
            cropData_2.rectCrop.y = (int)(cropData_2.rectCrop.y / p);
            cropData_2.rectCrop.width = (int)(cropData_2.rectCrop.width / p);
            cropData_2.rectCrop.height = (int)(cropData_2.rectCrop.height / p);
            cropData_2.rectCropMat = new Mat(videoMat2, cropData_2.rectCrop);
            cropData_2.cropTexture2D = new Texture2D(cropData_2.rectCrop.width, cropData_2.rectCrop.height,
                TextureFormat.ARGB32, false);

        }

        Utils.matToTexture2D(cropData_2.rectCropMat, cropData_2.cropTexture2D);
        cropData_2.rawImage.texture = cropData_2.cropTexture2D;
    }

}
