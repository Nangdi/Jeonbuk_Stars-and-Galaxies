using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// GameSettingsPanelUI(uGUI 설정 패널)를 현재 씬에 생성하는 에디터 도구.
/// 메뉴: Tools/게임 설정/설정 패널 생성
/// 생성 후에는 씬에 실제 오브젝트로 남으므로 위치·크기·글자 등을 자유롭게 관리할 수 있다.
/// </summary>
public static class GameSettingsPanelBuilder
{
    private const string KorFontPath = "Assets/Font/NanumSquareNeo-aLt SDF.asset";
    private static TMP_FontAsset _korFont;

    [MenuItem("Tools/GameSettings/Create Settings Panel", false, 0)]
    public static void CreatePanel()
    {
        _korFont = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(KorFontPath);

        var res = BuildResources();
        var legacy = BuildLegacyResources();

        // 1) 캔버스 + EventSystem
        Canvas canvas = CreateCanvas();
        EnsureEventSystem();

        // 2) 루트(설정 관리 오브젝트) - 컴포넌트가 붙고 항상 활성
        GameObject rootGO = new GameObject("게임설정패널", typeof(RectTransform));
        Undo.RegisterCreatedObjectUndo(rootGO, "Create Game Settings Panel");
        GameObjectUtility.SetParentAndAlign(rootGO, canvas.gameObject);
        var rootRT = rootGO.GetComponent<RectTransform>();
        Stretch(rootRT, Vector2.zero, Vector2.zero);
        var ui = rootGO.AddComponent<GameSettingsPanelUI>();

        // 3) 패널(토글 대상)
        GameObject panel = NewUI("Panel", rootGO.transform);
        var panelImg = panel.AddComponent<Image>();
        panelImg.color = new Color(0f, 0f, 0f, 0.88f);
        var panelRT = panel.GetComponent<RectTransform>();
        panelRT.anchorMin = new Vector2(0f, 1f);
        panelRT.anchorMax = new Vector2(0f, 1f);
        panelRT.pivot = new Vector2(0f, 1f);
        panelRT.anchoredPosition = new Vector2(24f, -24f);
        panelRT.sizeDelta = new Vector2(620f, 820f);

        // 4) 타이틀
        var title = CreateText("Title", panel.transform, "게임 설정  (ESC 토글)", 26, FontStyles.Bold);
        var titleRT = title.rectTransform;
        titleRT.anchorMin = new Vector2(0f, 1f);
        titleRT.anchorMax = new Vector2(1f, 1f);
        titleRT.pivot = new Vector2(0.5f, 1f);
        titleRT.offsetMin = new Vector2(16f, -52f);
        titleRT.offsetMax = new Vector2(-16f, -10f);

        // 5) 스크롤뷰 (타이틀과 버튼 사이)
        GameObject scrollView = DefaultControls.CreateScrollView(legacy);
        scrollView.name = "ScrollView";
        GameObjectUtility.SetParentAndAlign(scrollView, panel);
        var scrollRT = scrollView.GetComponent<RectTransform>();
        scrollRT.anchorMin = Vector2.zero;
        scrollRT.anchorMax = Vector2.one;
        scrollRT.offsetMin = new Vector2(14f, 62f);   // 아래 62 (버튼/상태 영역 확보)
        scrollRT.offsetMax = new Vector2(-14f, -58f);  // 위 58 (타이틀 확보)
        var scrollRect = scrollView.GetComponent<ScrollRect>();
        scrollRect.horizontal = false;
        scrollRect.vertical = true;
        var svImg = scrollView.GetComponent<Image>();
        if (svImg != null) svImg.color = new Color(1f, 1f, 1f, 0.04f);

        // 스크롤 콘텐츠 세로 레이아웃 구성
        RectTransform content = scrollRect.content;
        var vlg = content.gameObject.AddComponent<VerticalLayoutGroup>();
        vlg.padding = new RectOffset(10, 10, 10, 10);
        vlg.spacing = 6f;
        vlg.childControlWidth = true;
        vlg.childControlHeight = true;
        vlg.childForceExpandWidth = true;
        vlg.childForceExpandHeight = false;
        var fitter = content.gameObject.AddComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        content.anchorMin = new Vector2(0f, 1f);
        content.anchorMax = new Vector2(1f, 1f);
        content.pivot = new Vector2(0.5f, 1f);

        // 6) 행 구성 + 바인딩 수집
        var bindings = new List<GameSettingsPanelUI.FieldBinding>();
        Transform c = content;

        Header(c, "── 타이머 (초) ──");
        AddInput(c, res, "접속 대기시간", GameSettingsPanelUI.FieldKey.ConnectionTime, bindings);
        AddInput(c, res, "연습 시간", GameSettingsPanelUI.FieldKey.PracticeTime, bindings);
        AddInput(c, res, "미션1 시간", GameSettingsPanelUI.FieldKey.Mission1Time, bindings);
        AddInput(c, res, "미션1 종료 딜레이", GameSettingsPanelUI.FieldKey.Mission1EndDelay, bindings);
        AddInput(c, res, "미션2 시간", GameSettingsPanelUI.FieldKey.Mission2Time, bindings);

        Header(c, "── 정답 범위 (예: 5  또는  1,4) ──");
        AddInput(c, res, "미션1 정답범위", GameSettingsPanelUI.FieldKey.Range1, bindings, false);
        AddInput(c, res, "미션2 정답범위", GameSettingsPanelUI.FieldKey.Range2, bindings, false);

        Header(c, "── 점수 · 미션1 ──");
        AddInput(c, res, "정답 점수", GameSettingsPanelUI.FieldKey.M1Right, bindings);
        AddInput(c, res, "기본 점수", GameSettingsPanelUI.FieldKey.M1Must, bindings);
        AddToggle(c, legacy, "오답 감점 적용", GameSettingsPanelUI.FieldKey.M1Wrong, bindings);

        Header(c, "── 점수 · 미션2 ──");
        AddInput(c, res, "정답 점수", GameSettingsPanelUI.FieldKey.M2Right, bindings);
        AddInput(c, res, "기본 점수", GameSettingsPanelUI.FieldKey.M2Must, bindings);
        AddToggle(c, legacy, "오답 감점 적용", GameSettingsPanelUI.FieldKey.M2Wrong, bindings);

        Header(c, "── 연습모드 회전 점수 ──");
        AddInput(c, res, "작은별 회전점수", GameSettingsPanelUI.FieldKey.PLittleRot, bindings);
        AddInput(c, res, "작은별 결과점수", GameSettingsPanelUI.FieldKey.PLittleResult, bindings);
        AddInput(c, res, "작은별 회전카운트", GameSettingsPanelUI.FieldKey.PLittleMax, bindings);
        AddInput(c, res, "큰별 회전점수", GameSettingsPanelUI.FieldKey.PBigRot, bindings);
        AddInput(c, res, "큰별 결과점수", GameSettingsPanelUI.FieldKey.PBigResult, bindings);
        AddInput(c, res, "큰별 회전카운트", GameSettingsPanelUI.FieldKey.PBigMax, bindings);

        Header(c, "── 기타 ──");
        AddToggle(c, legacy, "마우스 커서 표시", GameSettingsPanelUI.FieldKey.CursorVisible, bindings);

        // 7) 버튼 행
        GameObject buttons = NewUI("Buttons", panel.transform);
        var btnRT = buttons.GetComponent<RectTransform>();
        btnRT.anchorMin = new Vector2(0f, 0f);
        btnRT.anchorMax = new Vector2(1f, 0f);
        btnRT.pivot = new Vector2(0.5f, 0f);
        btnRT.offsetMin = new Vector2(14f, 34f);
        btnRT.offsetMax = new Vector2(-14f, 74f);
        var hlg = buttons.AddComponent<HorizontalLayoutGroup>();
        hlg.spacing = 8f;
        hlg.childControlWidth = true;
        hlg.childControlHeight = true;
        hlg.childForceExpandWidth = true;
        hlg.childForceExpandHeight = true;

        Button saveBtn = CreateButton(buttons.transform, res, "저장 & 적용");
        Button revertBtn = CreateButton(buttons.transform, res, "되돌리기");
        Button closeBtn = CreateButton(buttons.transform, res, "닫기");

        // 8) 상태 텍스트
        var status = CreateText("Status", panel.transform, "", 16, FontStyles.Normal);
        status.color = new Color(0.8f, 1f, 0.8f, 1f);
        var statusRT = status.rectTransform;
        statusRT.anchorMin = new Vector2(0f, 0f);
        statusRT.anchorMax = new Vector2(1f, 0f);
        statusRT.pivot = new Vector2(0.5f, 0f);
        statusRT.offsetMin = new Vector2(16f, 8f);
        statusRT.offsetMax = new Vector2(-16f, 30f);

        // 9) 컴포넌트 참조 연결
        ui.panelRoot = panel;
        ui.saveButton = saveBtn;
        ui.revertButton = revertBtn;
        ui.closeButton = closeBtn;
        ui.statusText = status;
        ui.bindings = bindings;
        ui.missionDataLoader = Object.FindObjectOfType<MissionDataLoader>();

        EditorUtility.SetDirty(ui);
        EditorSceneManager.MarkSceneDirty(rootGO.scene);
        Selection.activeGameObject = panel;

        Debug.Log("[GameSettingsPanelBuilder] 설정 패널 생성 완료. 필요 시 위치/크기를 조정하고 씬을 저장하세요.");
    }

    // ─────────────────────────── 헬퍼 ───────────────────────────

    private static TMP_DefaultControls.Resources BuildResources()
    {
        var r = new TMP_DefaultControls.Resources();
        r.standard = LoadBuiltin("UI/Skin/UISprite.psd");
        r.background = LoadBuiltin("UI/Skin/Background.psd");
        r.inputField = LoadBuiltin("UI/Skin/InputFieldBackground.psd");
        r.knob = LoadBuiltin("UI/Skin/Knob.psd");
        r.checkmark = LoadBuiltin("UI/Skin/Checkmark.psd");
        r.dropdown = LoadBuiltin("UI/Skin/DropdownArrow.psd");
        r.mask = LoadBuiltin("UI/Skin/UIMask.psd");
        return r;
    }

    private static DefaultControls.Resources BuildLegacyResources()
    {
        var r = new DefaultControls.Resources();
        r.standard = LoadBuiltin("UI/Skin/UISprite.psd");
        r.background = LoadBuiltin("UI/Skin/Background.psd");
        r.inputField = LoadBuiltin("UI/Skin/InputFieldBackground.psd");
        r.knob = LoadBuiltin("UI/Skin/Knob.psd");
        r.checkmark = LoadBuiltin("UI/Skin/Checkmark.psd");
        r.dropdown = LoadBuiltin("UI/Skin/DropdownArrow.psd");
        r.mask = LoadBuiltin("UI/Skin/UIMask.psd");
        return r;
    }

    private static Sprite LoadBuiltin(string path)
    {
        return AssetDatabase.GetBuiltinExtraResource<Sprite>(path);
    }

    private static Canvas CreateCanvas()
    {
        GameObject go = new GameObject("GameSettingsCanvas", typeof(RectTransform));
        Undo.RegisterCreatedObjectUndo(go, "Create Settings Canvas");
        var canvas = go.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999; // 최상단 표시
        var scaler = go.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;
        go.AddComponent<GraphicRaycaster>();
        return canvas;
    }

    private static void EnsureEventSystem()
    {
        if (Object.FindObjectOfType<EventSystem>() != null) return;
        GameObject es = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        Undo.RegisterCreatedObjectUndo(es, "Create EventSystem");
    }

    private static GameObject NewUI(string name, Transform parent)
    {
        GameObject go = new GameObject(name, typeof(RectTransform));
        GameObjectUtility.SetParentAndAlign(go, parent != null ? parent.gameObject : null);
        return go;
    }

    private static void Stretch(RectTransform rt, Vector2 offMin, Vector2 offMax)
    {
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = offMin;
        rt.offsetMax = offMax;
    }

    private static TextMeshProUGUI CreateText(string name, Transform parent, string text, float size, FontStyles style)
    {
        GameObject go = NewUI(name, parent);
        var t = go.AddComponent<TextMeshProUGUI>();
        t.text = text;
        t.fontSize = size;
        t.fontStyle = style;
        t.color = Color.white;
        t.enableWordWrapping = false;
        t.overflowMode = TextOverflowModes.Overflow;
        t.verticalAlignment = VerticalAlignmentOptions.Middle;
        if (_korFont != null) t.font = _korFont;
        return t;
    }

    private static void Header(Transform content, string text)
    {
        var t = CreateText("Header", content, text, 18, FontStyles.Bold);
        t.color = new Color(0.85f, 0.9f, 1f, 1f);
        var le = t.gameObject.AddComponent<LayoutElement>();
        le.minHeight = 30f;
        le.preferredHeight = 30f;
    }

    private static GameObject CreateRow(Transform content, string label)
    {
        GameObject row = NewUI("Row", content);
        var le = row.AddComponent<LayoutElement>();
        le.minHeight = 38f;
        le.preferredHeight = 38f;
        var hlg = row.AddComponent<HorizontalLayoutGroup>();
        hlg.spacing = 10f;
        hlg.childControlWidth = true;
        hlg.childControlHeight = true;
        hlg.childForceExpandWidth = false;
        hlg.childForceExpandHeight = true;
        hlg.childAlignment = TextAnchor.MiddleLeft;

        var lbl = CreateText("Label", row.transform, label, 18, FontStyles.Normal);
        var lblLE = lbl.gameObject.AddComponent<LayoutElement>();
        lblLE.preferredWidth = 200f;
        lblLE.flexibleWidth = 0f;
        return row;
    }

    private static void AddInput(Transform content, TMP_DefaultControls.Resources res, string label,
        GameSettingsPanelUI.FieldKey key, List<GameSettingsPanelUI.FieldBinding> bindings, bool numeric = true)
    {
        GameObject row = CreateRow(content, label);

        GameObject inputGO = TMP_DefaultControls.CreateInputField(res);
        inputGO.name = "Input";
        GameObjectUtility.SetParentAndAlign(inputGO, row);
        var inputLE = inputGO.AddComponent<LayoutElement>();
        inputLE.flexibleWidth = 1f;
        inputLE.minWidth = 120f;
        inputLE.preferredHeight = 34f;

        var field = inputGO.GetComponent<TMP_InputField>();
        if (numeric) field.contentType = TMP_InputField.ContentType.IntegerNumber;
        field.text = "";
        ApplyFont(field.textComponent);
        ApplyFont(field.placeholder as TMP_Text);
        if (field.placeholder is TMP_Text ph) ph.text = numeric ? "숫자" : "예: 1,4";

        bindings.Add(new GameSettingsPanelUI.FieldBinding { key = key, input = field });
    }

    private static void AddToggle(Transform content, DefaultControls.Resources legacy, string label,
        GameSettingsPanelUI.FieldKey key, List<GameSettingsPanelUI.FieldBinding> bindings)
    {
        GameObject row = CreateRow(content, label);

        GameObject toggleGO = DefaultControls.CreateToggle(legacy);
        toggleGO.name = "Toggle";
        GameObjectUtility.SetParentAndAlign(toggleGO, row);
        var toggleLE = toggleGO.AddComponent<LayoutElement>();
        toggleLE.flexibleWidth = 1f;
        toggleLE.minWidth = 120f;

        var toggle = toggleGO.GetComponent<Toggle>();
        // 토글 자체 라벨(레거시 Text)은 비우고 왼쪽 라벨을 사용
        var toggleLabel = toggleGO.GetComponentInChildren<Text>();
        if (toggleLabel != null) toggleLabel.text = "";

        bindings.Add(new GameSettingsPanelUI.FieldBinding { key = key, toggle = toggle });
    }

    private static Button CreateButton(Transform parent, TMP_DefaultControls.Resources res, string label)
    {
        GameObject go = TMP_DefaultControls.CreateButton(res);
        go.name = "Button_" + label;
        GameObjectUtility.SetParentAndAlign(go, parent != null ? parent.gameObject : null);
        var txt = go.GetComponentInChildren<TMP_Text>();
        if (txt != null)
        {
            txt.text = label;
            txt.fontSize = 18;
            ApplyFont(txt);
        }
        return go.GetComponent<Button>();
    }

    private static void ApplyFont(TMP_Text t)
    {
        if (t != null && _korFont != null) t.font = _korFont;
    }
}
