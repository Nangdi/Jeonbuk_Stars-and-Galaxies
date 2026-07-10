#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Threading;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

// include HDRP settings
#if UNITY_2018 || UNITY_2019_0 || UNITY_2019_1 || UNITY_2019_2
using UnityEngine.Experimental.Rendering.HDPipeline;
#else // NO LONGER EXPERIMENTAL IN 2019.3
//using UnityEngine.Rendering.Universal;
#endif

public struct CookieData
{
    public float shift_v, shift_h, keystone_h, keystone_v, ratio, aspect;
    public CookieData(float shiftV, float shiftH, float keystoneH, float keystoneV, float throwRatio, float imageAspect)
    {
        shift_v = shiftV;
        shift_h = shiftH;
        keystone_h = keystoneH;
        keystone_v = keystoneV;
        ratio = throwRatio;
        aspect = imageAspect;
    }
}

public struct VignetteData
{
    public bool _enabled;
    public float _radius;
    public float _fadeSize;
    public Vector2 _offset;
    public bool _forceCircle;
    public bool _circular;

    public VignetteData(bool enabled, float radius, float fadeSize, Vector2 offset, bool forceCircular, bool circular)
    {
        _enabled = enabled;
        _radius = radius;
        _fadeSize = fadeSize;
        _offset = offset;
        _forceCircle = forceCircular;
        _circular = circular;
    }
}

public class Cookie
{
    public enum ImageType { Colour, Grey };
    Texture sourceImage = null;
    CookieData data;
    float imageWidth, imageHeight;
    float maxImageEdgeDistance;
    RenderTexture cookie;

    Vector2 cookieSpaceScale, cookieSpaceOffset;

    // used for calculating angle
    const float distance = 10.0f; // throw distance
    int textureSize = 1024; // texture width and height

    Light projectorLight;

    VignetteData _vignetteData;
    RenderTexture vignetteCookie;

    static Material singlePassShader;
    static Material vignetteShader;

    bool supportLiveUpdate = false;

    RenderTexture interTexture; // texture for holding the calculated cookie, in case source image is deleted as we do with the ImageProjector to save memory

    public Cookie(CookieData cookieData, int cookieSize, Light spotLight, VignetteData vignetteData, Texture imageToProject = null, bool enableLiveUpdate = false)
    {
        textureSize = cookieSize;
        sourceImage = imageToProject;

        CreateTexture();

        projectorLight = spotLight;
        data = cookieData;
        _vignetteData = vignetteData;
        supportLiveUpdate = enableLiveUpdate;

        Initialise();
    }

    // destructor
    ~Cookie()
    {
        cookie.Release();
        interTexture.Release();
        vignetteCookie.Release();
        GameObject.Destroy(cookie);
        GameObject.Destroy(interTexture);
        GameObject.Destroy(vignetteCookie);
    }

    /// <summary>
    /// Creates the Cookie textures
    /// </summary>
    void CreateTexture()
    {
        cookie = new RenderTexture(textureSize, textureSize, 24, RenderTextureFormat.ARGB32);
        cookie.wrapMode = TextureWrapMode.Clamp;
        //cookie.updateMode = CustomRenderTextureUpdateMode.Realtime;
        cookie.useMipMap = true;
        cookie.filterMode = FilterMode.Trilinear;
        cookie.format = RenderTextureFormat.ARGB32;
        //cookie.Initialize();
        //Material updateMat = Resources.Load("CRTUpdateMat") as Material;
        //if (updateMat)
        //    cookie.material = updateMat;
    }

    /// <summary>
    /// Calculates light cone angle, draws the first Cookie, assigns cookie to light.
    /// </summary>
    void Initialise()
    {
        if (sourceImage != null)
            interTexture = new RenderTexture(sourceImage.width + 2, sourceImage.height + 2, 24, RenderTextureFormat.ARGB32);
        else
        {
            // create white texture
            sourceImage = new Texture2D(256, 256, TextureFormat.RGBA32, false, false);
            Texture2D PI2D = (Texture2D)sourceImage;
            PI2D.wrapMode = TextureWrapMode.Clamp;
            //PI2D.alphaIsTransparency = true;
            PI2D.anisoLevel = 1;
            PI2D.filterMode = FilterMode.Trilinear;
            interTexture = new RenderTexture(PI2D.width + 2, PI2D.height + 2, 24, RenderTextureFormat.ARGB32);
        }
        interTexture.wrapMode = TextureWrapMode.Clamp;

        vignetteCookie = new RenderTexture(sourceImage.width, sourceImage.height, 0, RenderTextureFormat.ARGB32);

        Resources.UnloadUnusedAssets();

        // get shaders
        if (vignetteShader == null)
        {
            singlePassShader = new Material(Shader.Find("Hidden/ProjectorSimSinglePass"));
            vignetteShader = new Material(Shader.Find("Hidden/ProjectorSimVignette"));
        }

        // calculate angle of light cone from throw ratio and possible lens shift amount
        imageWidth = distance / data.ratio;
        imageHeight = imageWidth / data.aspect;

        // calculate shift **IN METERS**
        float shift_H = imageWidth * (data.shift_h / 200.0f);
        float shift_V = imageHeight * (data.shift_v / 200.0f);

        // calculate the furthest image edge with  lens shift applied (in meters from lens centre)
        float imageLimit_h = (imageWidth / 2.0f) + Mathf.Abs(shift_H);
        float imageLimit_v = (imageHeight / 2.0f) + Mathf.Abs(shift_V);
        maxImageEdgeDistance = Mathf.Max(imageLimit_h, imageLimit_v);

        // Calculate the spotlight angle (TODO: scale by magic 0.3535533906f ratio)
        float spotAngle = Mathf.Atan(maxImageEdgeDistance / distance) * 2 * Mathf.Rad2Deg;
        //spotAngle *= 1.4142135623f;
        projectorLight.innerSpotAngle = projectorLight.spotAngle = spotAngle;

        // calculate extent of spotlight coverage at our arbitrary distance
        float totalHeight = distance * Mathf.Tan((spotAngle / 2) * Mathf.Deg2Rad) * 2;
        float totalWidth = totalHeight; // totalWidth is the same as the light coverage is square

        // scale is how many of this dimension can we fit into the cookie
        cookieSpaceScale = new Vector2(totalWidth / imageWidth, totalHeight / imageHeight);

        // offset is from bottom left corner of cookie, 1 unit = 1 image width/height
        float imageLeftEdge_m = totalWidth / 2 + (shift_H - imageWidth / 2);
        // clamp to allow a 1px border around image (otherwise even with our 1px border that can disappeasr at low resolutions)
        float pixelSize_m = totalWidth / textureSize;
        imageLeftEdge_m = Mathf.Clamp(imageLeftEdge_m, pixelSize_m, totalWidth - imageWidth - pixelSize_m);
        float imageLeftEdge_scale = imageLeftEdge_m / imageWidth;
        float imageBottomEdge_m = totalHeight / 2 + (shift_V - imageHeight / 2);
        imageBottomEdge_m = Mathf.Clamp(imageBottomEdge_m, pixelSize_m, totalHeight - imageHeight - pixelSize_m);
        float imageBottomEdge_scale = imageBottomEdge_m / imageHeight;

        cookieSpaceOffset = new Vector2(-imageLeftEdge_scale, -imageBottomEdge_scale);

        // draw the cookie(s)
        UpdateCookie();
    }

    /// <summary>
    /// Called when image size/pos is changed and the image shape in the cookie will change
    /// </summary>
    /// <param name="cookieData"></param>
    public void Reinitialise(CookieData cookieData, VignetteData vd)
    {
        data = cookieData;
        _vignetteData = vd;
        Initialise();
    }

    public void ForceUpdateCookie()
    {
        UpdateCookie();
    }

    /// <summary>
    /// Calculates where the image will be in the cookie and creates the whole cookie texture
    /// </summary>
    void UpdateCookie(bool ignore = true)
    {
        if (sourceImage == null && supportLiveUpdate) // should never happen, as we create a white image when null is passed in
        {
            Debug.Log("ProjectedImage is null!");
            return;
        }
        /*
        if (sourceImage as Texture2D != null)
        {
            if (((Texture2D)sourceImage).format != TextureFormat.RGBA32)
            {
                Debug.Log("Texture " + sourceImage.name + " is not correct TextureFormat RGBA32");
                return;
            }
        }
        if (sourceImage as RenderTexture != null)
        {
            if (((RenderTexture)sourceImage).format != RenderTextureFormat.ARGB32)
            {
                Debug.Log("RenderTexture " + sourceImage.name + " is not correct RenderTextureFormat ARGB32");
                return;
            }
        }
        */
        if (true)
        {
            if (sourceImage != null)
            {
                if (_vignetteData._enabled)
                {
                    vignetteShader.SetFloat("_vignetteOffsetX", _vignetteData._offset.x);
                    vignetteShader.SetFloat("_vignetteOffsetY", _vignetteData._offset.y);
                    vignetteShader.SetFloat("_vignetteRadius", _vignetteData._radius);
                    vignetteShader.SetFloat("_vignetteFadeSize", _vignetteData._fadeSize);
                    vignetteShader.SetInt("circle", _vignetteData._circular ? 1 : 0);
                    vignetteShader.SetFloat("_aspectRatio", _vignetteData._forceCircle ? data.aspect : 1.0f);

                    Graphics.Blit(sourceImage, vignetteCookie as RenderTexture, vignetteShader);
                }
                Shader.SetGlobalInt("_PJSimCookieSize", Mathf.Min(interTexture.width, interTexture.height));
                Shader.SetGlobalVector("_PJSimTransform", new Vector4(cookieSpaceScale.x, cookieSpaceScale.y, cookieSpaceOffset.x, cookieSpaceOffset.y));
                Graphics.Blit(_vignetteData._enabled ? vignetteCookie : sourceImage, interTexture as RenderTexture, singlePassShader);
            }

            // copy to final cookie
            //Graphics.Blit(interTexture, interTexture);

            // scale to inside spotlight circle for URP
            float scale = 1.4142135623f;   // magic number, do not change
            float offset = -0.2071067812f; // magic number, do not change
            Graphics.Blit(interTexture, cookie, new Vector2(scale, scale), new Vector2(offset, offset));
        }
        /*else
        {

            // interTexture is a full-size copy of the original image, but with a 1px black border to enable the lens shift effect
            if (sourceImage != null)
                Graphics.CopyTexture(sourceImage, 0, 0, 0, 0, sourceImage.width, sourceImage.height, interTexture, 0, 0, 1, 1);
            // blatantly copy from the source texture to the cookie (no more pixel-by-pixel operations YAY)
            Graphics.Blit(interTexture, cookie, cookieSpaceScale, cookieSpaceOffset);
        }*/

        // This makes Unity crash when building the exe, even if we have a preprocessor to only execute in editor
        // cookie.Update();
        // crash fixed by assigning a material to the cookie and setting update mode to realtime, now it can update every frame (yay)

        // garbage collection, otherwise RAM usage goes waaaaaaay up
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
#endif
            // deallocate memory now that cookie has been created
            if (!supportLiveUpdate)
            {
                sourceImage = null;
            }
#if UNITY_EDITOR
        }
#endif
    }

    /// <summary>
    /// Set the image to project (does NOT cause a redraw - call Reinitialise to redraw)
    /// </summary>
    /// <param name="image"></param>
    /// <param name="pixels"></param>
    public void SetProjectedImage(Texture image)
    {
        sourceImage = image;
    }
    public void RemoveProjectedImage() { sourceImage = null; }

    public void SetCookieSize(int newSize)
    {
        textureSize = newSize;
        CreateTexture();
        Initialise();
    }

    /// <summary>
    /// Maintains lumen output as cookie fill area changes
    /// </summary>
    /// <param name="value"></param>
    public void SetLumens(float value)
    {
        float hFill = 1f / cookieSpaceScale.x;
        float vFill = 1f / cookieSpaceScale.y;
        float fillArea = hFill * vFill;
        projectorLight.intensity = value / fillArea;

        // enable spot reflector (doesn't seem supported under URP)
        //projectorLight.enableSpotReflector = true;
    }

    public RenderTexture GetCookie() { return cookie; }
}
