#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode]
public class ProjectorSim_RenderTexture : MonoBehaviour
{
    public enum CookieSizes { c_128, c_256, c_512, c_1024, c_2048, c_4096};
    public enum VignetteShape { circle, square };

    public bool live = false;
    [Header("Image size")]
    [Space(10)]
    [Tooltip("Aspect ratio of the projected image (width/height)")]
    public float aspectRatio = 1.6f;
    float lastAspect = 1.6f;
    [Tooltip("Throw ratio of the projector (distance/image width) - smaller is wider")]
    public float throwRatio = 1.0f;
    float lastThrowRatio = 1.0f;
    [Tooltip("Vertical lens shift as a percentage of image width/2 (100% shift = lens level with image edge)")]
    public float shift_v = 0.0f;
    float lastShiftV = 0.0f;
    [Tooltip("Horizontal lens shift as a percentage of image width/2 (100% shift = lens level with image edge)")]
    public float shift_h = 0.0f;
    float lastShiftH = 0.0f;
    [Tooltip("Use this to make a trapezoidal image square again")]
    [Range(-100.0f, 100.0f)]
    public float keystoneH = 0.0f;
    float lastKeystoneH;
    [Tooltip("Use this to make a trapezoidal image square again")]
    [Range(-100.0f, 100.0f)]
    public float keystoneV = 0.0f;
    float lastKeystoneV;
    [Header("Brightness")]
    [Space(10)]
    [Tooltip("Allows control of the brightness of the projector")]
    public float brightness = 60f;
    float lastBrightness = 60f;
    [Tooltip("Controls the reach of the projector's light")]
    public float range = 20.0f;
    float lastRange = 20.0f;
    [Header("Projected content")]
    [Space(10)]
    [Tooltip("Texture to project. If empty, only white will be projected.")]
    public RenderTexture renderTexture;
    [Tooltip("The time to show each image, if more than one image is supplied. Otherwise this value is ignored.")]
    public float imageInterval = 5.0f;
    [Tooltip("Only used with an image. If unchecked, image will be in greyscale (loads slightly faster). Recommended to leave greyscale while setting aspect, zoom, and shift.")]
    public bool colour = false;
    [Tooltip("The resolution of the Cookie texture. Higher will have better clarity in the image, lower will be faster to generate. Recommended to keep this at 256 untl the image is positioned.")]
    public CookieSizes cookieSize = CookieSizes.c_256;
    CookieSizes lastCookieSize = CookieSizes.c_256;
    [Tooltip("The maximum time allowed for the projector to process a frame in seconds. This setting can ensure your game maintains a playable framerate. The resolution/colour will automatically drop in order to process within the given time.")]
    public float maxProcessingTime = 0.015f;

    [Header("Vignette")]
    [Space(10)]
    public bool useVignette = false;
    bool lastUseVignette = false;
    public VignetteShape vignetteShape = VignetteShape.circle;
    VignetteShape lastVignetteShape;
    [Range(0f, 1f)]
    public float vignetteSize = 0.5f;
    float lastVignetteSize = 0.5f;
    [Range(0f, 1f)]
    public float vignetteFade = 0.2f;
    float lastVignetteFade = 0.2f;
    [Range(0f, 1f)]
    public float vignetteOffsetX = 0.5f;
    float lastVignetteOffsetX = 0.5f;
    [Range(0f, 1f)]
    public float vignetteOffsetY = 0.5f;
    float lastVignetteOffsetY = 0.5f;
    public bool vignetteForceCircular = true;
    bool lastVignetteForceCircular = true;

    [Header("Light path")]
    [Space(10)]
    [Tooltip("Toggles light path geometry.")]
    public bool showLightPath = true;
    bool lastShowLightPath = true;
    [Tooltip("The material of the light path.")]
    public Material lightPathMaterial;
    [Tooltip("The distance the light path reaches - 0 or below makes the light stop at the furthest geometry found at the image corners. This is most useful for curved screens when you want the light path to continue beyond the corners.")]
    public float lightPathRange = 0.0f;

    float[] previousProcessingTimes;
    int processingTimeIndex = 0;
    bool optimisedProcessingTimeYet = false;

    bool isPlaying = true;

    // Cookies and lights
    Cookie cookie;
    Light projectorLight;
    ThrowBuilder tb;

    bool lightPathChanged = false;

    // prevent multiple projectors from trying to update their image in the same frame
    static bool projectorProcessedThisFrame = false;
    bool requiresProcessing = false;
    // Use this for initialization
    void Awake ()
    {
        projectorLight = GetComponentInChildren<Light>(true);
        projectorLight.gameObject.SetActive(false);

        tb = GetComponentInChildren<ThrowBuilder>(true);

        int cookieSizeInt = int.Parse(cookieSize.ToString().Substring(2));

        VignetteData vd = new VignetteData(useVignette, vignetteSize, vignetteFade, new Vector2(vignetteOffsetX, vignetteOffsetY), vignetteForceCircular, vignetteShape == VignetteShape.circle);

#if UNITY_EDITOR

        // if in editor play mode, calculate all cookies (because we want to play the slideshow)
        if (EditorApplication.isPlaying)
        {
#endif
            previousProcessingTimes = new float[5];

            // do first cookie as per usual
            cookie = new Cookie(new CookieData(shift_v, shift_h, keystoneH, keystoneV, throwRatio, aspectRatio), cookieSizeInt, projectorLight, vd, renderTexture, true);

            //UpdateImage();

#if UNITY_EDITOR
        }
        else // EDITOR EDIT MODE - don't pass in RenderTexture as we want to project white
        {
            cookie = new Cookie(new CookieData(shift_v, shift_h, keystoneH, keystoneV, throwRatio, aspectRatio), cookieSizeInt, projectorLight, vd);
        }
#endif
        cookie.SetLumens(brightness);
    }

    void Start()
    {
        BuildLightPath();

        Invoke("UpdateImage", 0.01f);
    }

    void BuildLightPath()
    {
        // build the light path geometry
        if (tb)
        {
            if (lightPathRange <= 0) // automatically calculate light path distance, max range = range of projector
                tb.BuildThrow(throwRatio, aspectRatio, new Vector2(shift_h, shift_v), range, true, lightPathMaterial);
            else // use user-defined distance
                tb.BuildThrow(throwRatio, aspectRatio, new Vector2(shift_h, shift_v), lightPathRange, false, lightPathMaterial);
        }
    }

    // When enabled, turn light on. Also start slideshow if necessary.
    void OnEnable()
    {
        projectorLight.gameObject.SetActive(true);

        if (showLightPath)
            tb.gameObject.SetActive(true);

            // allow slideshow in editor play mode
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
        {
#endif
            // stuff to do in play mode
            Invoke("UpdateImage", imageInterval);
#if UNITY_EDITOR
        }
#endif
    }

    // When disabled, turn lights off
    void OnDisable()
    {
        projectorLight.gameObject.SetActive(false);

        if (showLightPath)
            tb.gameObject.SetActive(false);

        CancelInvoke("UpdateImage");
    }

    // Allow external scripts to pause and play the projector
    public void Pause()
    {
        isPlaying = false;
        CancelInvoke("UpdateImage");
    }
    public void Play()
    {
        isPlaying = true;
        Invoke("UpdateImage", imageInterval);
    }

    /// <summary>
    /// Gives each light a cookie for its relevant channel, using the slideshowIndex value.
    /// </summary>
    void AssignLightCookies()
    {
        projectorLight.cookie = cookie.GetCookie();
    }

    void CheckProcessingTime()
    {
        float averageTime = 0f;
        for (int i = 0; i < previousProcessingTimes.Length; i++)
        {
            averageTime += previousProcessingTimes[i];
        }
        averageTime /= (float)previousProcessingTimes.Length;

        if (averageTime > maxProcessingTime)
        {
            // reduce cookie resolution, unless it is already at the minimum
            if (cookieSize.ToString() != "c_128")
            {
                Debug.Log(gameObject.name + " is reducing cookie resolution from " + cookieSize.ToString() + " to " + (cookieSize - 1).ToString());
                cookieSize = (CookieSizes)(cookieSize - 1);
                cookie.SetCookieSize(int.Parse(cookieSize.ToString().Substring(2)));

                // reset the processing time array so we get a new measurement next time
                for (int i = 0; i < previousProcessingTimes.Length; i++)
                {
                    previousProcessingTimes[i] = 0f;
                }
            }
        }
        else
        {
            // we processed within the required time - set the flag to stop checking
            optimisedProcessingTimeYet = true;
        }

    }

    public void UpdateImage()
    {
        // start timing how long it takes the image to update
        float startTime = Time.realtimeSinceStartup;

        // update the cookie
        cookie.ForceUpdateCookie();

        AssignLightCookies();

#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
#endif
            // automatically adjust the cookie resolution in order to maintain framerate
            if (!optimisedProcessingTimeYet)
            {
                float stopTime = Time.realtimeSinceStartup;
                previousProcessingTimes[processingTimeIndex] = stopTime - startTime;
                processingTimeIndex++;
                if (processingTimeIndex >= previousProcessingTimes.Length)
                {
                    processingTimeIndex = 0;
                    CheckProcessingTime();
                }
            }

            if (isPlaying && this.enabled)
                Invoke("UpdateImage", imageInterval);
#if UNITY_EDITOR
        }
#endif
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
#endif
            if (requiresProcessing && !projectorProcessedThisFrame)
            {
                UpdateImage();
            }
#if UNITY_EDITOR
        }
#endif
        if (lightPathChanged)
        {
            BuildLightPath();
            lightPathChanged = false;
        }
    }

    private void LateUpdate()
    {
        projectorProcessedThisFrame = false;
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (!EditorApplication.isPlaying)
        {
            if (cookie != null)
            {
                if (live && cookie != null)
                {
                    // recalculate cookie
                    if (aspectRatio != lastAspect ||
                        throwRatio != lastThrowRatio ||
                        shift_h != lastShiftH ||
                        shift_v != lastShiftV ||
                        keystoneH != lastKeystoneH ||
                        keystoneV != lastKeystoneV ||
                        useVignette != lastUseVignette ||
                        vignetteFade != lastVignetteFade ||
                        vignetteSize != lastVignetteSize ||
                        vignetteOffsetX != lastVignetteOffsetX ||
                        vignetteOffsetY != lastVignetteOffsetY ||
                        lastVignetteForceCircular != vignetteForceCircular ||
                        vignetteShape != lastVignetteShape)
                    {
                        // reset vignette size if shape changed
                        if (vignetteShape != lastVignetteShape)
                        {
                            vignetteSize = vignetteShape == VignetteShape.circle ? 0.5f : 0f;
                            lastVignetteShape = vignetteShape;
                        }

                        cookie.Reinitialise(new CookieData(shift_v, shift_h, keystoneH, keystoneV, throwRatio, aspectRatio), new VignetteData(useVignette, vignetteSize, vignetteFade, new Vector2(vignetteOffsetX, vignetteOffsetY), vignetteForceCircular, vignetteShape == VignetteShape.circle));

                        lastAspect = aspectRatio;
                        lastThrowRatio = throwRatio;
                        lastShiftH = shift_h;
                        lastShiftV = shift_v;
                        lastKeystoneH = keystoneH;
                        lastKeystoneV = keystoneV;

                        lastUseVignette = useVignette;
                        lastVignetteSize = vignetteSize;
                        lastVignetteFade = vignetteFade;
                        lastVignetteOffsetX = vignetteOffsetX;
                        lastVignetteOffsetY = vignetteOffsetY;
                        lastVignetteForceCircular = vignetteForceCircular;

                        cookie.SetLumens(brightness);

                        AssignLightCookies();

                        lightPathChanged = true;
                    }
                    if (cookieSize != lastCookieSize)
                    {
                        cookie.SetCookieSize(int.Parse(cookieSize.ToString().Substring(2)));
                        lastCookieSize = cookieSize;
                        AssignLightCookies();
                    }
                    if (lastBrightness != brightness)
                    {
                        if (brightness < 0f)
                            brightness = 0f;

                        cookie.SetLumens(brightness);
                        lastBrightness = brightness;
                    }
                    if (lastRange != range)
                    {
                        projectorLight.range = range;
                        lastRange = range;
                    }
                    if (showLightPath != lastShowLightPath)
                    {
                        if (tb)
                            tb.gameObject.SetActive(showLightPath);
                        lastShowLightPath = showLightPath;

                        if (showLightPath)
                            BuildLightPath();
                    }
                }
            }
        }
    }
#endif

}
