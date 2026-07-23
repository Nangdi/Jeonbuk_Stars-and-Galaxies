using UnityEngine;

/// <summary>
/// 현장 운영용 게임 설정 패널.
/// ESC로 열고, 값 수정 후 [저장 & 적용]을 누르면
/// MissionText.json에 기록되고 게임에 실시간 반영된다.
/// (편집 대상은 MissionDataLoader.jsonLoadData)
/// </summary>
public class GameSettingsPanel : MonoBehaviour
{
    [Header("설정 데이터 로더 (비우면 자동 검색)")]
    public MissionDataLoader missionDataLoader;

    [Header("패널 토글 키")]
    public KeyCode toggleKey = KeyCode.Escape;

    [Header("패널 크기·위치 (픽셀)")]
    public float panelWidth = 470f;
    public float panelHeight = 640f;
    public float panelX = 20f;
    public float panelY = 20f;

    [Header("글자 크기")]
    public int fontSize = 15;

    [Header("행 라벨/입력칸 너비 (픽셀)")]
    public float labelWidth = 160f;
    public float fieldWidth = 130f;

    private int builtFontSize = -1;
    private bool show = false;
    private Vector2 scroll;
    private Font uiFont;
    private string status = "";

    // 편집 버퍼 (저장 전까지 실제 값 미변경)
    private string connectionTime, practiceTime, mission1Time, mission1EndDelay, mission2Time;
    private string range1, range2;
    private string m1Right, m1Must; private bool m1Wrong;
    private string m2Right, m2Must; private bool m2Wrong;
    private string pLittleRot, pLittleResult, pLittleMax, pBigRot, pBigResult, pBigMax;
    private bool cursorVisible;

    private MissionDataLoader.JsonLoadData Data {
        get { return missionDataLoader != null ? missionDataLoader.jsonLoadData : null; }
    }

    void Start()
    {
        if (missionDataLoader == null)
            missionDataLoader = FindObjectOfType<MissionDataLoader>();
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            show = !show;
            if (show) LoadBuffers();
        }
    }

    private void EnsureFont()
    {
        if (fontSize < 1) fontSize = 1;
        if (uiFont == null || builtFontSize != fontSize)
        {
            uiFont = Font.CreateDynamicFontFromOSFont(
                new string[] { "Malgun Gothic", "맑은 고딕", "Gulim", "Dotum", "Arial" }, fontSize);
            builtFontSize = fontSize;
        }
    }

    private void LoadBuffers()
    {
        var d = Data;
        if (d == null) { status = "jsonLoadData가 아직 로드되지 않았습니다."; return; }

        var t = d.timeSetting;
        connectionTime = t.connectionTime.ToString();
        practiceTime = t.practiceTime.ToString();
        mission1Time = t.mission1Time.ToString();
        mission1EndDelay = t.mission1EndTimeDelay.ToString();
        mission2Time = t.mission2Time.ToString();

        if (d.missionDataList != null && d.missionDataList.Count > 0) range1 = d.missionDataList[0].rightAnswerRange;
        if (d.missionDataList != null && d.missionDataList.Count > 1) range2 = d.missionDataList[1].rightAnswerRange;

        m1Right = d.mission1ScoreSetting.rightAnswerScore.ToString();
        m1Must = d.mission1ScoreSetting.mustScore.ToString();
        m1Wrong = d.mission1ScoreSetting.applyWrongScore;
        m2Right = d.mission2ScoreSetting.rightAnswerScore.ToString();
        m2Must = d.mission2ScoreSetting.mustScore.ToString();
        m2Wrong = d.mission2ScoreSetting.applyWrongScore;

        var p = d.practiceScoreSetting;
        pLittleRot = p.littleRotScore.ToString();
        pLittleResult = p.littleRotResultScore.ToString();
        pLittleMax = p.littleRotatingNumberMaxRot.ToString();
        pBigRot = p.bigRotScore.ToString();
        pBigResult = p.bigRotResultScore.ToString();
        pBigMax = p.bigRotatingNumberMaxRot.ToString();

        cursorVisible = d.windowsSetting.isMouseCursor;
        status = "";
    }

    private static int PI(string s, int fallback)
    {
        int v; return int.TryParse(s, out v) ? v : fallback;
    }

    private void SaveBuffers()
    {
        var d = Data;
        if (d == null) { status = "저장 실패: jsonLoadData 없음"; return; }

        var t = d.timeSetting;
        t.connectionTime = PI(connectionTime, t.connectionTime);
        t.practiceTime = PI(practiceTime, t.practiceTime);
        t.mission1Time = PI(mission1Time, t.mission1Time);
        t.mission1EndTimeDelay = PI(mission1EndDelay, t.mission1EndTimeDelay);
        t.mission2Time = PI(mission2Time, t.mission2Time);

        if (d.missionDataList != null && d.missionDataList.Count > 0) d.missionDataList[0].rightAnswerRange = string.IsNullOrEmpty(range1) ? d.missionDataList[0].rightAnswerRange : range1.Trim();
        if (d.missionDataList != null && d.missionDataList.Count > 1) d.missionDataList[1].rightAnswerRange = string.IsNullOrEmpty(range2) ? d.missionDataList[1].rightAnswerRange : range2.Trim();

        d.mission1ScoreSetting.rightAnswerScore = PI(m1Right, d.mission1ScoreSetting.rightAnswerScore);
        d.mission1ScoreSetting.mustScore = PI(m1Must, d.mission1ScoreSetting.mustScore);
        d.mission1ScoreSetting.applyWrongScore = m1Wrong;
        d.mission2ScoreSetting.rightAnswerScore = PI(m2Right, d.mission2ScoreSetting.rightAnswerScore);
        d.mission2ScoreSetting.mustScore = PI(m2Must, d.mission2ScoreSetting.mustScore);
        d.mission2ScoreSetting.applyWrongScore = m2Wrong;

        var p = d.practiceScoreSetting;
        p.littleRotScore = PI(pLittleRot, p.littleRotScore);
        p.littleRotResultScore = PI(pLittleResult, p.littleRotResultScore);
        p.littleRotatingNumberMaxRot = PI(pLittleMax, p.littleRotatingNumberMaxRot);
        p.bigRotScore = PI(pBigRot, p.bigRotScore);
        p.bigRotResultScore = PI(pBigResult, p.bigRotResultScore);
        p.bigRotatingNumberMaxRot = PI(pBigMax, p.bigRotatingNumberMaxRot);

        d.windowsSetting.isMouseCursor = cursorVisible;

        try
        {
            missionDataLoader.SaveAndApply();
            status = "저장 & 적용 완료: " + missionDataLoader.MissionTextPath;
        }
        catch (System.Exception e)
        {
            status = "저장 오류: " + e.Message;
        }
    }

    void OnGUI()
    {
        if (!show) return;
        EnsureFont();
        if (uiFont != null) GUI.skin.font = uiFont;
        // 글자 크기 반영 (주요 위젯 스타일에 적용)
        GUI.skin.label.fontSize = fontSize;
        GUI.skin.textField.fontSize = fontSize;
        GUI.skin.button.fontSize = fontSize;
        GUI.skin.toggle.fontSize = fontSize;
        GUI.skin.box.fontSize = fontSize;

        float w = panelWidth;
        float h = Mathf.Min(Screen.height - 40f, panelHeight);
        float x = panelX, y = panelY;

        // 배경(가독성용 어두운 박스)
        Color prev = GUI.color;
        GUI.color = new Color(0f, 0f, 0f, 0.85f);
        GUI.Box(new Rect(x, y, w, h), GUIContent.none);
        GUI.color = prev;
        GUI.Box(new Rect(x, y, w, h), "게임 설정  (ESC 토글)");

        GUILayout.BeginArea(new Rect(x + 14f, y + 30f, w - 28f, h - 44f));
        scroll = GUILayout.BeginScrollView(scroll);

        GUILayout.Label("── 타이머 (초) ──");
        connectionTime = Row("접속 대기시간", connectionTime);
        practiceTime = Row("연습 시간", practiceTime);
        mission1Time = Row("미션1 시간", mission1Time);
        mission1EndDelay = Row("미션1 종료 딜레이", mission1EndDelay);
        mission2Time = Row("미션2 시간", mission2Time);

        GUILayout.Space(8f);
        GUILayout.Label("── 정답 범위 (예: 5  또는  1,4) ──");
        range1 = Row("미션1 정답범위", range1);
        range2 = Row("미션2 정답범위", range2);

        GUILayout.Space(8f);
        GUILayout.Label("── 점수 · 미션1 ──");
        m1Right = Row("정답 점수", m1Right);
        m1Must = Row("기본 점수", m1Must);
        m1Wrong = RowToggle("오답 감점 적용", m1Wrong);
        GUILayout.Label("── 점수 · 미션2 ──");
        m2Right = Row("정답 점수", m2Right);
        m2Must = Row("기본 점수", m2Must);
        m2Wrong = RowToggle("오답 감점 적용", m2Wrong);
        GUILayout.Label("── 연습모드 회전 점수 ──");
        pLittleRot = Row("작은별 회전점수", pLittleRot);
        pLittleResult = Row("작은별 결과점수", pLittleResult);
        pLittleMax = Row("작은별 회전카운트", pLittleMax);
        pBigRot = Row("큰별 회전점수", pBigRot);
        pBigResult = Row("큰별 결과점수", pBigResult);
        pBigMax = Row("큰별 회전카운트", pBigMax);

        GUILayout.Space(8f);
        GUILayout.Label("── 기타 ──");
        cursorVisible = RowToggle("마우스 커서 표시", cursorVisible);

        GUILayout.EndScrollView();

        GUILayout.Space(8f);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("저장 & 적용", GUILayout.Height(32f))) SaveBuffers();
        if (GUILayout.Button("되돌리기", GUILayout.Height(32f))) LoadBuffers();
        if (GUILayout.Button("닫기", GUILayout.Height(32f))) show = false;
        GUILayout.EndHorizontal();

        if (!string.IsNullOrEmpty(status)) GUILayout.Label(status);

        GUILayout.EndArea();
    }

    private string Row(string label, string val)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(label, GUILayout.Width(labelWidth));
        val = GUILayout.TextField(val ?? "", GUILayout.Width(fieldWidth));
        GUILayout.EndHorizontal();
        return val;
    }

    private bool RowToggle(string label, bool val)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(label, GUILayout.Width(labelWidth));
        val = GUILayout.Toggle(val, val ? " ON" : " OFF");
        GUILayout.EndHorizontal();
        return val;
    }
}
