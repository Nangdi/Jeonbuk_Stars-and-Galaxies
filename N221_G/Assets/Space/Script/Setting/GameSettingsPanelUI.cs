using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 현장 운영용 게임 설정 패널 (uGUI 버전).
/// 기존 OnGUI 방식(GameSettingsPanel)을 대체한다.
/// 실제 Canvas/InputField/Toggle/Button 오브젝트를 씬에서 직접 관리할 수 있으며,
/// 값 수정 후 [저장 & 적용]을 누르면 MissionText.json에 기록되고 게임에 실시간 반영된다.
/// 패널 오브젝트는 GameSettingsPanelBuilder(에디터 메뉴)로 생성한다.
/// </summary>
public class GameSettingsPanelUI : MonoBehaviour
{
    // 편집 대상 필드 식별자
    public enum FieldKey
    {
        ConnectionTime, PracticeTime, Mission1Time, Mission1EndDelay, Mission2Time,
        Range1, Range2,
        M1Right, M1Must, M1Wrong,
        M2Right, M2Must, M2Wrong,
        PLittleRot, PLittleResult, PLittleMax, PBigRot, PBigResult, PBigMax,
        CursorVisible
    }

    [System.Serializable]
    public class FieldBinding
    {
        public FieldKey key;
        public TMP_InputField input;   // 숫자/문자 필드용
        public Toggle toggle;          // bool 필드용
    }

    [Header("데이터 로더 (비우면 자동 검색)")]
    public MissionDataLoader missionDataLoader;

    [Header("토글 대상 패널 (비우면 이 오브젝트)")]
    public GameObject panelRoot;

    [Header("토글 키")]
    public KeyCode toggleKey = KeyCode.Escape;

    [Header("시작 시 패널 표시")]
    public bool showOnStart = false;

    [Header("버튼")]
    public Button saveButton;
    public Button revertButton;
    public Button closeButton;

    [Header("상태 텍스트")]
    public TMP_Text statusText;

    [Header("필드 바인딩")]
    public List<FieldBinding> bindings = new List<FieldBinding>();

    private bool show;

    private MissionDataLoader.JsonLoadData Data
    {
        get { return missionDataLoader != null ? missionDataLoader.jsonLoadData : null; }
    }

    void Start()
    {
        if (missionDataLoader == null)
            missionDataLoader = FindObjectOfType<MissionDataLoader>();

        WireButtons();

        show = showOnStart;
        ApplyShow();
        if (show) LoadAll();
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            show = !show;
            ApplyShow();
            if (show) LoadAll();
        }
    }

    private void WireButtons()
    {
        if (saveButton != null)
        {
            saveButton.onClick.RemoveListener(SaveAll);
            saveButton.onClick.AddListener(SaveAll);
        }
        if (revertButton != null)
        {
            revertButton.onClick.RemoveListener(LoadAll);
            revertButton.onClick.AddListener(LoadAll);
        }
        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(Close);
            closeButton.onClick.AddListener(Close);
        }
    }

    private void ApplyShow()
    {
        var target = panelRoot != null ? panelRoot : gameObject;
        // 이 컴포넌트가 붙은 오브젝트 자신을 끄면 토글키를 못 받으므로 그 경우엔 끄지 않는다.
        if (target == gameObject) return;
        target.SetActive(show);
    }

    public void Close()
    {
        show = false;
        ApplyShow();
    }

    // ─────────────────────────── 불러오기 ───────────────────────────
    public void LoadAll()
    {
        var d = Data;
        if (d == null) { SetStatus("jsonLoadData가 아직 로드되지 않았습니다."); return; }

        foreach (var b in bindings)
        {
            if (b == null) continue;
            if (b.toggle != null) b.toggle.isOn = GetBool(d, b.key);
            else if (b.input != null) b.input.text = GetString(d, b.key);
        }
        SetStatus("");
    }

    // ─────────────────────────── 저장 & 적용 ───────────────────────────
    public void SaveAll()
    {
        var d = Data;
        if (d == null) { SetStatus("저장 실패: jsonLoadData 없음"); return; }

        foreach (var b in bindings)
        {
            if (b == null) continue;
            if (b.toggle != null) SetBool(d, b.key, b.toggle.isOn);
            else if (b.input != null) SetString(d, b.key, b.input.text);
        }

        try
        {
            missionDataLoader.SaveAndApply();
            SetStatus("저장 & 적용 완료: " + missionDataLoader.MissionTextPath);
        }
        catch (System.Exception e)
        {
            SetStatus("저장 오류: " + e.Message);
        }
    }

    // ─────────────────────────── 값 매핑 ───────────────────────────
    private static int PI(string s, int fallback)
    {
        int v; return int.TryParse(s, out v) ? v : fallback;
    }

    private string GetString(MissionDataLoader.JsonLoadData d, FieldKey key)
    {
        var t = d.timeSetting;
        var p = d.practiceScoreSetting;
        switch (key)
        {
            case FieldKey.ConnectionTime: return t.connectionTime.ToString();
            case FieldKey.PracticeTime: return t.practiceTime.ToString();
            case FieldKey.Mission1Time: return t.mission1Time.ToString();
            case FieldKey.Mission1EndDelay: return t.mission1EndTimeDelay.ToString();
            case FieldKey.Mission2Time: return t.mission2Time.ToString();
            case FieldKey.Range1: return HasMission(d, 0) ? d.missionDataList[0].rightAnswerRange : "";
            case FieldKey.Range2: return HasMission(d, 1) ? d.missionDataList[1].rightAnswerRange : "";
            case FieldKey.M1Right: return d.mission1ScoreSetting.rightAnswerScore.ToString();
            case FieldKey.M1Must: return d.mission1ScoreSetting.mustScore.ToString();
            case FieldKey.M2Right: return d.mission2ScoreSetting.rightAnswerScore.ToString();
            case FieldKey.M2Must: return d.mission2ScoreSetting.mustScore.ToString();
            case FieldKey.PLittleRot: return p.littleRotScore.ToString();
            case FieldKey.PLittleResult: return p.littleRotResultScore.ToString();
            case FieldKey.PLittleMax: return p.littleRotatingNumberMaxRot.ToString();
            case FieldKey.PBigRot: return p.bigRotScore.ToString();
            case FieldKey.PBigResult: return p.bigRotResultScore.ToString();
            case FieldKey.PBigMax: return p.bigRotatingNumberMaxRot.ToString();
            default: return "";
        }
    }

    private void SetString(MissionDataLoader.JsonLoadData d, FieldKey key, string s)
    {
        var t = d.timeSetting;
        var p = d.practiceScoreSetting;
        switch (key)
        {
            case FieldKey.ConnectionTime: t.connectionTime = PI(s, t.connectionTime); break;
            case FieldKey.PracticeTime: t.practiceTime = PI(s, t.practiceTime); break;
            case FieldKey.Mission1Time: t.mission1Time = PI(s, t.mission1Time); break;
            case FieldKey.Mission1EndDelay: t.mission1EndTimeDelay = PI(s, t.mission1EndTimeDelay); break;
            case FieldKey.Mission2Time: t.mission2Time = PI(s, t.mission2Time); break;
            case FieldKey.Range1:
                if (HasMission(d, 0) && !string.IsNullOrEmpty(s)) d.missionDataList[0].rightAnswerRange = s.Trim();
                break;
            case FieldKey.Range2:
                if (HasMission(d, 1) && !string.IsNullOrEmpty(s)) d.missionDataList[1].rightAnswerRange = s.Trim();
                break;
            case FieldKey.M1Right: d.mission1ScoreSetting.rightAnswerScore = PI(s, d.mission1ScoreSetting.rightAnswerScore); break;
            case FieldKey.M1Must: d.mission1ScoreSetting.mustScore = PI(s, d.mission1ScoreSetting.mustScore); break;
            case FieldKey.M2Right: d.mission2ScoreSetting.rightAnswerScore = PI(s, d.mission2ScoreSetting.rightAnswerScore); break;
            case FieldKey.M2Must: d.mission2ScoreSetting.mustScore = PI(s, d.mission2ScoreSetting.mustScore); break;
            case FieldKey.PLittleRot: p.littleRotScore = PI(s, p.littleRotScore); break;
            case FieldKey.PLittleResult: p.littleRotResultScore = PI(s, p.littleRotResultScore); break;
            case FieldKey.PLittleMax: p.littleRotatingNumberMaxRot = PI(s, p.littleRotatingNumberMaxRot); break;
            case FieldKey.PBigRot: p.bigRotScore = PI(s, p.bigRotScore); break;
            case FieldKey.PBigResult: p.bigRotResultScore = PI(s, p.bigRotResultScore); break;
            case FieldKey.PBigMax: p.bigRotatingNumberMaxRot = PI(s, p.bigRotatingNumberMaxRot); break;
        }
    }

    private bool GetBool(MissionDataLoader.JsonLoadData d, FieldKey key)
    {
        switch (key)
        {
            case FieldKey.M1Wrong: return d.mission1ScoreSetting.applyWrongScore;
            case FieldKey.M2Wrong: return d.mission2ScoreSetting.applyWrongScore;
            case FieldKey.CursorVisible: return d.windowsSetting.isMouseCursor;
            default: return false;
        }
    }

    private void SetBool(MissionDataLoader.JsonLoadData d, FieldKey key, bool v)
    {
        switch (key)
        {
            case FieldKey.M1Wrong: d.mission1ScoreSetting.applyWrongScore = v; break;
            case FieldKey.M2Wrong: d.mission2ScoreSetting.applyWrongScore = v; break;
            case FieldKey.CursorVisible: d.windowsSetting.isMouseCursor = v; break;
        }
    }

    private static bool HasMission(MissionDataLoader.JsonLoadData d, int i)
    {
        return d.missionDataList != null && d.missionDataList.Count > i && d.missionDataList[i] != null;
    }

    private void SetStatus(string msg)
    {
        if (statusText != null) statusText.text = msg;
    }
}
