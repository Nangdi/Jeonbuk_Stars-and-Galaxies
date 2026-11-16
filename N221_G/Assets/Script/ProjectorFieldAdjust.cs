using System;
using System.IO;
using MultiProjectorWarpSystem;
using UnityEngine;

/// <summary>
/// 현장(설치 장소)에서 프로젝터 2대 화면을 맞추기 위한 보정 컴포넌트.
///
/// 1) 카메라 거리(줌)   : Source Camera 들의 직교 크기 배율(orthographicSizeScale)을 조절해
///                        투사 영상 전체를 크게/작게 만든다. 실제 원형 테이블 크기에 맞출 때 사용.
/// 2) 화면 중심 이동     : Source Camera 묶음(Source Cameras)을 XY로 밀어서, 프로젝터가
///                        물리적으로 조금 어긋나게 설치됐을 때 영상 중심을 맞춘다.
/// 3) 원 마스크 바깥 가림 : 줌아웃하면 화면이 원 마스크(영역마스크)보다 커져서 원 바깥이
///                        그대로 보인다. 마스크 바깥의 검은 테두리(외각) 4장을 현재 화면 크기에
///                        맞춰 자동으로 키워서 원 밖으로는 아무것도 나오지 않게 한다.
/// 4) 화면 늘리기/줄이기 : 출력 영상을 가로/세로 따로 늘리거나 줄인다. 외부 블렌딩 장비가
///                        입력(예: 1920x2160)을 블렌딩된 영역(1:1)에 맞춰 한쪽 방향으로 눌러
///                        내보낼 때, 그만큼 미리 반대로 늘려서 벽에서 정원이 되게 맞춘다.
///
/// 보정값은 StreamingAssets/field_adjust.json 에 저장되고 다음 실행 때 자동으로 복원된다.
/// </summary>
[ExecuteAlways]
[DefaultExecutionOrder(200)] // ProjectionWarpSystem.Start(캘리브레이션 로드) 이후에 적용되어야 한다
public class ProjectorFieldAdjust : MonoBehaviour
{
    public const string SaveFileName = "field_adjust.json";

    [Serializable]
    public class Button
    {
        public KeyCode key = KeyCode.None;
        public KeyCode altKey = KeyCode.None;
        [NonSerialized] public float nextRepeatTime;
    }

    [Header("참조")]
    [Tooltip("비워두면 같은 오브젝트/씬에서 자동으로 찾는다.")]
    public ProjectionWarpSystem warpSystem;

    [Tooltip("원 마스크 이미지(마스크영역). 검은 테두리를 붙일 기준 사각형 크기를 여기서 읽는다.")]
    public RectTransform circleMask;

    [Tooltip("원 바깥을 가리는 검은 테두리 4장(외각 하위의 Image). 순서: 상, 하, 좌, 우")]
    public RectTransform[] surroundBars = new RectTransform[4];

    [Tooltip("사용자 테이블 보정. 테이블 조정 모드일 때는 방향키/초기화키를 그쪽에 양보한다.")]
    public UserTableFieldAdjust tableAdjust;

    [Header("프로젝터 출력 카메라에서 제외할 레이어")]
    [Tooltip("Starfield 처럼 경계가 무한한 오브젝트는 워프 영상 바깥(카메라 배경)까지 그려진다.\n" +
             "그 영역에는 원 마스크가 존재하지 않아 가릴 방법이 없으므로, 출력 카메라가 아예 보지 않게 한다.\n" +
             "게임 영상(Source Camera)에는 그대로 남는다.")]
    public LayerMask projectionCameraExclude;

    [Header("카메라 거리 (줌)")]
    [Tooltip("Source Camera 직교 크기 배율. 값이 커질수록 더 넓게 보이고(줌아웃) 투사 영상은 작아진다.")]
    public float zoom = 2.4f;
    public float minZoom = 0.5f;
    public float maxZoom = 12f;
    public float zoomStep = 0.05f;
    public float zoomFineStep = 0.01f;

    [Header("화면 중심 이동 (월드 유닛)")]
    public Vector2 centerOffset = Vector2.zero;
    [Tooltip("중심 이동 허용 범위(±).")]
    public float centerRange = 20f;
    public float centerStep = 0.2f;
    public float centerFineStep = 0.02f;

    [Header("화면 늘리기/줄이기 (1 = 원래 비율)")]
    [Tooltip("x = 가로 배율, y = 세로 배율. 1보다 크면 늘어나고 작으면 줄어든다.")]
    public Vector2 stretch = Vector2.one;
    public float minStretch = 0.5f;
    public float maxStretch = 2f;
    public float stretchStep = 0.01f;
    public float stretchFineStep = 0.001f;

    [Header("원이 화면 밖으로 잘리지 않게")]
    [Tooltip("투사 영역보다 원이 크면 좌우(또는 상하)가 일자로 잘린다.\n" +
             "켜두면 원 전체가 들어오는 최소 배율 아래로는 줌인되지 않게 자동으로 막는다.")]
    public bool keepCircleInsideFrame = true;
    [Tooltip("원 바깥에 남길 검은 여백 비율. 1.0 = 딱 맞음, 1.02 = 2% 여유")]
    public float circleFitMargin = 1.02f;

    [Header("원 마스크 바깥 가림")]
    [Tooltip("켜두면 줌/중심 이동에 맞춰 검은 테두리 크기를 매번 다시 계산한다.")]
    public bool autoFitSurround = true;
    [Tooltip("화면 크기 대비 여유 배율. 1보다 크게 둬야 워프(모서리 보정)로 늘어난 영역까지 덮는다.")]
    public float surroundMargin = 1.6f;

    [Header("조작 (기본값: 캘리브레이션 GUI가 켜져 있을 때만 동작)")]
    public bool onlyWhileCalibrationGui = true;
    public Button zoomIn = new Button { key = KeyCode.RightBracket, altKey = KeyCode.KeypadPlus };
    public Button zoomOut = new Button { key = KeyCode.LeftBracket, altKey = KeyCode.KeypadMinus };
    public Button moveUp = new Button { key = KeyCode.UpArrow };
    public Button moveDown = new Button { key = KeyCode.DownArrow };
    public Button moveLeft = new Button { key = KeyCode.LeftArrow };
    public Button moveRight = new Button { key = KeyCode.RightArrow };
    [Tooltip("이 키를 누른 채 방향키 = 화면 늘리기/줄이기 (↑↓ 세로, ←→ 가로)")]
    public KeyCode stretchModifier = KeyCode.LeftControl;
    [Tooltip("보정값을 파일로 저장")]
    public KeyCode saveKey = KeyCode.F8;
    [Tooltip("보정값을 초기값으로 되돌린다(저장하지는 않는다)")]
    public KeyCode resetKey = KeyCode.F7;
    [Tooltip("누르고 있는 동안 미세 조정")]
    public KeyCode fineModifier = KeyCode.LeftShift;
    public float holdDelay = 0.45f;
    public float repeatRate = 14f;

    // 기준값 — 최초 1회 캐시해 두고 리셋할 때 되돌린다.
    float defaultZoom;
    // 실제로 화면에 적용된 배율. zoom 은 저장된/요청한 값이고, 원이 잘리지 않게 하는 최소 배율에
    // 걸리면 이 값만 올라간다. (예전 파일의 배율이 늘리기를 맞추기 전에 잘려서 사라지지 않게)
    float appliedZoom;
    Vector3 sourceCamerasBasePosition;
    bool baseCaptured;
    bool applyRequested;

    [Serializable]
    class SaveData
    {
        public float zoom;
        public float centerOffsetX;
        public float centerOffsetY;
        public float stretchX = 1f;
        public float stretchY = 1f;
    }

    public static string SavePath
    {
        // persistentDataPath (없으면 StreamingAssets 기본값을 복사해 옴). 빌드를 덮어써도 현장 보정값 유지
        get { return ConfigPath.Get(SaveFileName); }
    }

    Transform SourceCamerasRoot
    {
        get { return warpSystem != null ? warpSystem.sourceCamerasContainer : null; }
    }

    void OnEnable()
    {
        ResolveReferences();
        CaptureBase();
        applyRequested = true;
    }

    void Start()
    {
        ResolveReferences();
        CaptureBase();

        if (Application.isPlaying && !Load())
        {
            // 저장된 현장 보정값이 없으면 캘리브레이션 파일이 정한 배율을 그대로 따른다.
            // (씬에 적혀 있는 값으로 덮어써서 운영자가 저장해 둔 설정을 무시하면 안 된다)
            if (warpSystem != null && warpSystem.orthographicSizeScale > 0f)
            {
                zoom = warpSystem.orthographicSizeScale;
            }
        }
        Apply();
    }

    void OnValidate()
    {
        // OnValidate 안에서 곧바로 카메라를 건드리면 경고가 나므로 다음 Update로 미룬다.
        applyRequested = true;
    }

    void Update()
    {
        if (applyRequested)
        {
            applyRequested = false;
            ResolveReferences();
            CaptureBase();
            Apply();
        }

        if (!Application.isPlaying) return;
        if (warpSystem == null) return;

        // 워프 시스템이 출력 카메라를 다시 만들면 컬링 설정이 초기화되므로 매 프레임 확인한다(카메라 2대, 비용 무시 가능).
        ApplyProjectionCameraCulling();

        // 캘리브레이션을 다시 불러오면 UpdateSourceCameras 가 직교 크기를 바꾸므로 늘리기 행렬도 매 프레임 다시 맞춘다.
        ApplyStretch();

        // 캘리브레이션 파일을 다시 불러오면 배율이 밖에서 바뀐다.
        // 그 값을 그대로 받아들이고 마스크 테두리만 다시 맞춘다.
        if (!Mathf.Approximately(warpSystem.orthographicSizeScale, appliedZoom))
        {
            zoom = warpSystem.orthographicSizeScale;
            appliedZoom = zoom;
            if (autoFitSurround) FitSurround();
        }

        if (onlyWhileCalibrationGui && !warpSystem.showProjectionWarpGUI) return;
        if (IsAnyInputFieldFocused()) return;
        if (ExternalUiBlocking) return;

        bool fine = fineModifier != KeyCode.None &&
                    (Input.GetKey(fineModifier) || Input.GetKey(KeyCode.RightShift));
        float zStep = fine ? zoomFineStep : zoomStep;
        float cStep = fine ? centerFineStep : centerStep;

        bool changed = false;

        // 줌 키는 지금 화면에 보이는 배율에서 출발한다(최소 배율에 걸려 있던 요청값은 버린다).
        if (Repeat(zoomIn)) { zoom = appliedZoom - zStep; changed = true; }   // 줌인 = 더 좁게 보기 = 배율 감소
        if (Repeat(zoomOut)) { zoom = appliedZoom + zStep; changed = true; }

        // Ctrl + 방향키 = 화면 늘리기/줄이기. 이때는 중심 이동/테이블 조정으로 새어 나가지 않는다.
        bool stretchMode = IsStretchModifierHeld;
        float sStep = fine ? stretchFineStep : stretchStep;

        if (stretchMode)
        {
            if (Repeat(moveUp)) { stretch.y += sStep; changed = true; }
            if (Repeat(moveDown)) { stretch.y -= sStep; changed = true; }
            if (Repeat(moveRight)) { stretch.x += sStep; changed = true; }
            if (Repeat(moveLeft)) { stretch.x -= sStep; changed = true; }
        }

        // 테이블 조정 모드에서는 방향키와 초기화키를 UserTableFieldAdjust 가 가져간다.
        bool tableMode = tableAdjust != null && tableAdjust.HasSelection;

        if (!tableMode && !stretchMode)
        {
            if (Repeat(moveUp)) { centerOffset.y += cStep; changed = true; }
            if (Repeat(moveDown)) { centerOffset.y -= cStep; changed = true; }
            if (Repeat(moveLeft)) { centerOffset.x -= cStep; changed = true; }
            if (Repeat(moveRight)) { centerOffset.x += cStep; changed = true; }
        }

        if (!tableMode && resetKey != KeyCode.None && Input.GetKeyDown(resetKey))
        {
            zoom = defaultZoom;
            centerOffset = Vector2.zero;
            stretch = Vector2.one;
            changed = true;
            Notify("Field adjust reset");
        }

        if (changed)
        {
            Apply();
            Notify(string.Format("Zoom {0:0.000}{5}   Center {1:0.00}, {2:0.00}   Stretch W {3:0.0}% H {4:0.0}%",
                appliedZoom, centerOffset.x, centerOffset.y, stretch.x * 100f, stretch.y * 100f,
                Mathf.Approximately(zoom, appliedZoom) ? "" : string.Format(" (saved {0:0.000}, held by circle fit)", zoom)));
        }

        if (saveKey != KeyCode.None && Input.GetKeyDown(saveKey))
        {
            Save();
        }
    }

    #region Apply

    /// <summary>줌 / 중심 이동 / 마스크 가림을 현재 값으로 모두 반영한다.</summary>
    public void Apply()
    {
        if (warpSystem == null) return;

        // NaN/Infinity 는 Mathf.Clamp 를 그대로 통과해 화면이 통째로 날아가므로 먼저 걸러낸다.
        zoom = Safe(zoom, defaultZoom > 0f ? defaultZoom : 1f);
        centerOffset.x = Safe(centerOffset.x, 0f);
        centerOffset.y = Safe(centerOffset.y, 0f);
        stretch = ClampStretch(stretch);

        float lowerZoom = Mathf.Max(0.001f, minZoom);
        if (keepCircleInsideFrame)
        {
            float fit = GetMinimumZoomForCircle();
            if (fit > 0f) lowerZoom = Mathf.Max(lowerZoom, fit);
        }

        // 요청값(zoom)은 고정 한계로만 자르고, 원 맞춤 최소 배율은 적용값에만 건다.
        zoom = Mathf.Clamp(zoom, Mathf.Max(0.001f, minZoom), Mathf.Max(minZoom, maxZoom));
        appliedZoom = Mathf.Clamp(zoom, lowerZoom, Mathf.Max(lowerZoom, maxZoom));
        centerOffset.x = Mathf.Clamp(centerOffset.x, -centerRange, centerRange);
        centerOffset.y = Mathf.Clamp(centerOffset.y, -centerRange, centerRange);

        warpSystem.orthographicSizeScale = appliedZoom;
        warpSystem.UpdateSourceCameras();
        ApplyStretch();

        Transform root = SourceCamerasRoot;
        if (root != null && baseCaptured)
        {
            root.localPosition = sourceCamerasBasePosition + new Vector3(centerOffset.x, centerOffset.y, 0f);
        }

        if (autoFitSurround) FitSurround();
        ApplyProjectionCameraCulling();
    }

    /// <summary>
    /// Source Camera 투영 행렬을 가로/세로 따로 조절해 출력 영상을 늘리거나 줄인다.
    /// 배율이 1 이면 원래 투영으로 되돌린다. 워프 메시/캘리브레이션 값은 건드리지 않는다.
    /// </summary>
    public void ApplyStretch()
    {
        if (warpSystem == null || warpSystem.sourceCameras == null) return;

        stretch = ClampStretch(stretch);
        bool identity = Mathf.Approximately(stretch.x, 1f) && Mathf.Approximately(stretch.y, 1f);

        for (int i = 0; i < warpSystem.sourceCameras.Count; i++)
        {
            Camera cam = warpSystem.sourceCameras[i];
            if (cam == null) continue;

            if (identity || !cam.orthographic)
            {
                cam.ResetProjectionMatrix();
                continue;
            }

            // 보이는 월드 범위를 배율로 나누면 화면에서는 그만큼 늘어난다.
            float halfHeight = cam.orthographicSize / stretch.y;
            float halfWidth = cam.orthographicSize * cam.aspect / stretch.x;
            cam.projectionMatrix = Matrix4x4.Ortho(-halfWidth, halfWidth, -halfHeight, halfHeight,
                cam.nearClipPlane, cam.farClipPlane);
        }
    }

    Vector2 ClampStretch(Vector2 value)
    {
        float lo = Mathf.Max(0.01f, minStretch);
        float hi = Mathf.Max(lo, maxStretch);
        return new Vector2(
            Mathf.Clamp(Safe(value.x, 1f), lo, hi),
            Mathf.Clamp(Safe(value.y, 1f), lo, hi));
    }

    bool IsStretchModifierHeld
    {
        get
        {
            if (stretchModifier == KeyCode.None) return false;
            if (Input.GetKey(stretchModifier)) return true;
            // 왼쪽 Ctrl 을 지정했으면 오른쪽 Ctrl 도 같이 받는다.
            return stretchModifier == KeyCode.LeftControl && Input.GetKey(KeyCode.RightControl);
        }
    }

    /// <summary>
    /// 프로젝터 출력 카메라가 지정 레이어를 렌더하지 않게 한다.
    /// ProjectionWarpSystem 이 OnEnable 에서 카메라를 새로 만들기 때문에 매번 다시 적용해야 한다.
    /// </summary>
    public void ApplyProjectionCameraCulling()
    {
        if (warpSystem == null || projectionCameraExclude.value == 0) return;
        if (warpSystem.projectionCameras == null) return;

        for (int i = 0; i < warpSystem.projectionCameras.Count; i++)
        {
            var mesh = warpSystem.projectionCameras[i];
            if (mesh == null || mesh.targetCamera == null) continue;
            mesh.targetCamera.cullingMask &= ~projectionCameraExclude.value;
        }
    }

    /// <summary>월드 기준 원 마스크의 반지름.</summary>
    public float CircleRadiusWorld
    {
        get
        {
            if (circleMask == null) return 0f;
            return circleMask.rect.width * 0.5f * Mathf.Abs(circleMask.lossyScale.x);
        }
    }

    /// <summary>
    /// 원 전체가 투사 영역 안에 들어오는 최소 배율.
    /// 투사 영역은 배율에 정비례하므로 (원 반지름 / 배율 1일 때의 화면 반폭) 으로 구한다.
    /// 구할 수 없으면 0 을 돌려준다.
    /// </summary>
    public float GetMinimumZoomForCircle()
    {
        if (warpSystem == null || circleMask == null) return 0f;

        float radius = CircleRadiusWorld;
        if (radius <= 0f || warpSystem.viewportSize <= 0f) return 0f;

        float aspect = warpSystem.renderTextureSize.y > 0f
            ? warpSystem.renderTextureSize.x / warpSystem.renderTextureSize.y
            : 1.6f;
        int count = Mathf.Max(1, warpSystem.projectorCount);

        // 배율 1일 때의 화면 반폭 / 반높이 (카메라가 쌓이는 방향으로만 대수를 곱한다)
        float halfWidthPerZoom = warpSystem.viewportSize * aspect;
        float halfHeightPerZoom = warpSystem.viewportSize;

        switch (warpSystem.arrangement)
        {
            case ProjectionWarpSystem.CameraArragement.VERTICAL_ORTHOGRAPHIC:
            case ProjectionWarpSystem.CameraArragement.VERTICAL_PERSPECTIVE:
                halfHeightPerZoom *= count;
                break;
            default:
                halfWidthPerZoom *= count;
                break;
        }

        // 늘린 방향은 그만큼 보이는 범위가 좁아진다.
        Vector2 s = ClampStretch(stretch);
        halfWidthPerZoom /= s.x;
        halfHeightPerZoom /= s.y;

        float tightest = Mathf.Min(halfWidthPerZoom, halfHeightPerZoom);
        if (tightest <= 0f) return 0f;

        return radius * Mathf.Max(1f, circleFitMargin) / tightest;
    }

    /// <summary>
    /// 원 마스크 사각형 바깥을 덮는 검은 테두리 4장을 현재 화면 크기에 맞춰 키운다.
    /// 줌아웃해도 원 밖으로 화면이 새어 나오지 않게 하는 부분.
    /// </summary>
    public void FitSurround()
    {
        if (warpSystem == null || circleMask == null || surroundBars == null) return;
        if (surroundBars.Length < 4) return;
        for (int i = 0; i < 4; i++) if (surroundBars[i] == null) return;

        // 월드 → 테두리가 속한 캔버스의 로컬 단위
        float scale = Mathf.Abs(surroundBars[0].parent != null ? surroundBars[0].parent.lossyScale.x : surroundBars[0].lossyScale.x);
        if (scale < 1e-6f) return;

        // 원 마스크 사각형의 반지름(테두리 부모의 로컬 단위). 테두리는 이 사각형 바로 바깥부터 시작한다.
        float inner = CircleRadiusWorld / scale;
        if (inner <= 0f) return;

        // 테두리 부모(외각)가 원 중심과 어긋나 있어도 테두리는 원 중심에 맞춘다.
        // 어긋난 만큼 그대로 두면 한쪽 테두리가 원을 파고들어 윗부분 등이 일자로 잘린다.
        Vector2 center = GetCircleCenterInBarSpace();

        // 소스 카메라가 보는 월드 범위 (수직 배열이면 카메라 대수만큼 세로로 쌓인다)
        int cameraCount = Mathf.Max(1, warpSystem.projectorCount);
        float halfHeightWorld = warpSystem.viewportSize * appliedZoom;
        float aspect = warpSystem.renderTextureSize.y > 0f
            ? warpSystem.renderTextureSize.x / warpSystem.renderTextureSize.y
            : 1.6f;
        float halfWidthWorld = halfHeightWorld * aspect;

        switch (warpSystem.arrangement)
        {
            case ProjectionWarpSystem.CameraArragement.VERTICAL_ORTHOGRAPHIC:
            case ProjectionWarpSystem.CameraArragement.VERTICAL_PERSPECTIVE:
                halfHeightWorld *= cameraCount;
                break;
            default:
                halfWidthWorld *= cameraCount;
                break;
        }

        // 줄인 방향은 그만큼 더 넓은 월드가 보이므로 테두리도 더 크게 덮어야 한다.
        Vector2 s = ClampStretch(stretch);
        halfWidthWorld /= s.x;
        halfHeightWorld /= s.y;

        float neededWorld = Mathf.Max(halfWidthWorld, halfHeightWorld) * Mathf.Max(1f, surroundMargin)
                            + centerRange + centerOffset.magnitude;

        float outer = Mathf.Max(neededWorld / scale + center.magnitude, inner * 1.05f);

        float band = outer - inner;
        float bandCenter = (outer + inner) * 0.5f;

        // 상 / 하 : 좌우 끝까지 덮어 모서리까지 처리한다.  좌 / 우 : 사각형 높이만큼만.
        SetBar(0, center + new Vector2(0f, bandCenter), new Vector2(outer * 2f, band));
        SetBar(1, center + new Vector2(0f, -bandCenter), new Vector2(outer * 2f, band));
        SetBar(2, center + new Vector2(-bandCenter, 0f), new Vector2(band, inner * 2f));
        SetBar(3, center + new Vector2(bandCenter, 0f), new Vector2(band, inner * 2f));
    }

    /// <summary>
    /// 원 마스크 중심을 테두리 부모 기준 anchoredPosition(앵커 = 부모 중앙) 좌표로 돌려준다.
    /// </summary>
    Vector2 GetCircleCenterInBarSpace()
    {
        RectTransform parent = surroundBars[0].parent as RectTransform;
        if (parent == null) return Vector2.zero;

        Vector3 world = circleMask.TransformPoint(circleMask.rect.center);
        Vector2 local = parent.InverseTransformPoint(world);
        return local - parent.rect.center;
    }

    void SetBar(int index, Vector2 anchoredPosition, Vector2 size)
    {
        RectTransform bar = surroundBars[index];

        bar.localScale = Vector3.one;
        bar.anchorMin = new Vector2(0.5f, 0.5f);
        bar.anchorMax = new Vector2(0.5f, 0.5f);
        bar.pivot = new Vector2(0.5f, 0.5f);
        bar.anchoredPosition = anchoredPosition;
        bar.sizeDelta = size;
    }

    #endregion

    #region Save / Load

    public void Save()
    {
        SaveData data = new SaveData
        {
            zoom = zoom,
            centerOffsetX = centerOffset.x,
            centerOffsetY = centerOffset.y,
            stretchX = stretch.x,
            stretchY = stretch.y
        };

        try
        {
            string path = SavePath;
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, JsonUtility.ToJson(data, true));
            Debug.Log(path + " has been saved.");

            // 테이블 보정값도 같은 키로 함께 저장한다.
            bool tableOk = tableAdjust == null || tableAdjust.Save();
            Notify(tableOk ? "Field + table adjust saved" : "Field saved, table save failed");
        }
        catch (Exception e)
        {
            Debug.LogError("현장 보정값 저장 실패: " + e.Message);
            Notify("Field adjust save failed");
        }
    }

    public bool Load()
    {
        string path = SavePath;
        if (!File.Exists(path)) return false;

        try
        {
            SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
            if (data == null) return false;

            zoom = data.zoom;
            centerOffset = new Vector2(data.centerOffsetX, data.centerOffsetY);
            // 늘리기 값이 없던 예전 파일은 0 으로 읽히므로 1(원래 비율)로 본다.
            stretch = new Vector2(
                data.stretchX > 0f ? data.stretchX : 1f,
                data.stretchY > 0f ? data.stretchY : 1f);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("현장 보정값 로드 실패: " + e.Message);
            return false;
        }
    }

    #endregion

    #region Helpers

    void ResolveReferences()
    {
        if (warpSystem == null) warpSystem = GetComponent<ProjectionWarpSystem>();
        if (warpSystem == null) warpSystem = FindObjectOfType<ProjectionWarpSystem>();
    }

    void CaptureBase()
    {
        if (baseCaptured || warpSystem == null) return;

        Transform root = SourceCamerasRoot;
        if (root == null) return;

        // 씬에 저장된 값을 기준으로 삼는다. centerOffset 이 이미 들어가 있으면 빼고 기록한다.
        sourceCamerasBasePosition = root.localPosition - new Vector3(centerOffset.x, centerOffset.y, 0f);
        defaultZoom = warpSystem.orthographicSizeScale > 0f ? warpSystem.orthographicSizeScale : zoom;
        baseCaptured = true;
    }

    bool Repeat(Button button)
    {
        return Repeat(button, holdDelay, repeatRate);
    }

    /// <summary>NaN / Infinity 를 걸러내고 정상 값만 돌려준다.</summary>
    public static float Safe(float value, float fallback)
    {
        return (float.IsNaN(value) || float.IsInfinity(value)) ? fallback : value;
    }

    /// <summary>키를 처음 누르면 한 칸, 계속 누르고 있으면 holdDelay 뒤부터 repeatRate 속도로 반복한다.</summary>
    public static bool Repeat(Button button, float holdDelay, float repeatRate)
    {
        if (button == null) return false;
        bool a = RepeatKey(button, button.key, holdDelay, repeatRate);
        bool b = RepeatKey(button, button.altKey, holdDelay, repeatRate);
        return a || b;
    }

    static bool RepeatKey(Button button, KeyCode key, float holdDelay, float repeatRate)
    {
        if (key == KeyCode.None) return false;

        if (Input.GetKeyDown(key))
        {
            button.nextRepeatTime = Time.unscaledTime + holdDelay;
            return true;
        }
        if (Input.GetKey(key) && Time.unscaledTime >= button.nextRepeatTime)
        {
            button.nextRepeatTime = Time.unscaledTime + 1f / Mathf.Max(1f, repeatRate);
            return true;
        }
        return false;
    }

    bool IsAnyInputFieldFocused()
    {
        return IsCalibrationInputFocused(warpSystem != null ? warpSystem.calibrationManager : null);
    }

    /// <summary>
    /// 캘리브레이션 창의 입력칸에 커서가 들어가 있는지. 하나라도 포커스돼 있으면
    /// 타이핑이 보정 단축키로 새어 들어가지 않도록 모든 키 처리를 멈춘다.
    /// </summary>
    public static bool IsCalibrationInputFocused(CalibrationManager cm)
    {
        if (cm == null) return false;

        return Focused(cm.filename) || Focused(cm.overlapInputField) ||
               Focused(cm.redInputField) || Focused(cm.greenInputField) || Focused(cm.blueInputField) ||
               Focused(cm.topRangeInputField) || Focused(cm.topChokeInputField) || Focused(cm.topGammaInputField) ||
               Focused(cm.bottomRangeInputField) || Focused(cm.bottomChokeInputField) || Focused(cm.bottomGammaInputField) ||
               Focused(cm.leftRangeInputField) || Focused(cm.leftChokeInputField) || Focused(cm.leftGammaInputField) ||
               Focused(cm.rightRangeInputField) || Focused(cm.rightChokeInputField) || Focused(cm.rightGammaInputField);
    }

    static bool Focused(UnityEngine.UI.InputField field)
    {
        return field != null && field.isFocused;
    }

    GameSettingsPanel settingsPanel;
    bool settingsPanelSearched;

    /// <summary>
    /// ESC 설정 패널(IMGUI)이 열려 있는지. IMGUI 입력칸은 Input.GetKey 를 가로채지 않으므로,
    /// 열려 있는 동안에는 보정 단축키를 모두 무시해야 한다.
    /// </summary>
    public bool ExternalUiBlocking
    {
        get
        {
            if (!settingsPanelSearched)
            {
                settingsPanel = FindObjectOfType<GameSettingsPanel>();
                settingsPanelSearched = true;
            }
            return settingsPanel != null && settingsPanel.IsOpen;
        }
    }

    /// <summary>다른 보정 컴포넌트가 같은 알림창을 쓰기 위한 진입점.</summary>
    public void NotifyExternal(string message)
    {
        Notify(message);
    }

    void Notify(string message)
    {
        if (!Application.isPlaying) return; // NotificationMessage 는 Start()에서 Animator를 잡으므로 에디터 모드에선 건너뛴다
        if (warpSystem == null || warpSystem.notificationMessage == null) return;

        if (warpSystem.notificationMessage.messageText != null)
        {
            warpSystem.notificationMessage.messageText.text = message;
        }

        try
        {
            warpSystem.notificationMessage.Show();
        }
        catch (Exception)
        {
            // 알림 애니메이터가 아직 준비되지 않았을 뿐이므로 보정 동작 자체는 계속 진행한다.
        }
    }

    #endregion
}
