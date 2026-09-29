using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 현장에서 사용자 테이블의 위치를 원(360도) 기준으로 맞추는 보정 컴포넌트.
///
/// - 좌 / 우 : 원 둘레를 따라 회전한다(각도). 테이블 자체도 같은 각도만큼 같이 돌아서
///             항상 원 중심을 향한 자세를 유지한다.
/// - 상 / 하 : 원 중심에서 멀어지거나 가까워진다(반지름).
/// - 테이블마다 따로 고를 수 있고, ALL 을 고르면 4대를 한꺼번에 돌린다.
/// - 테이블별 세부 조정으로 자체 회전각과 크기를 따로 만질 수 있다.
///
/// 보정값은 StreamingAssets/table_adjust.json 에 저장된다.
/// (저장은 ProjectorFieldAdjust 의 저장키와 함께 처리된다)
/// </summary>
[ExecuteAlways]
[DefaultExecutionOrder(150)] // ProjectorFieldAdjust(200)보다 먼저 돌아서 선택 상태가 먼저 정해지도록
public class UserTableFieldAdjust : MonoBehaviour
{
    public const string SaveFileName = "table_adjust.json";

    /// <summary>선택 대상. -1 = 해제, 0 이상 = 해당 테이블, ALL_SELECTED = 전체</summary>
    public const int NoSelection = -1;
    public const int AllSelected = -2;

    [Serializable]
    public class TableEntry
    {
        [Tooltip("알림 메시지에 표시할 이름")]
        public string label = "";
        public Transform target;
        [Tooltip("끄면 선택 순환에서 제외된다. (예: 사용하지 않는 테이블)")]
        public bool adjustable = true;

        [Header("원 기준 보정")]
        [Tooltip("원 둘레를 따라 돌린 각도(도). 좌/우 키로 조정")]
        public float angleOffset;
        [Tooltip("원 중심에서 멀어진 거리(월드 유닛). 상/하 키로 조정")]
        public float radiusOffset;

        [Header("테이블별 세부 조정")]
        [Tooltip("테이블 자체 회전 미세조정(도)")]
        public float rotationOffset;
        [Tooltip("테이블 크기 배율")]
        public float scaleMultiplier = 1f;

        // 씬에 놓여 있던 원래 배치 — 보정값은 여기에 더해진다.
        [NonSerialized] public bool baseCaptured;
        [NonSerialized] public float baseAngle;
        [NonSerialized] public float baseRadius;
        [NonSerialized] public float baseZ;
        [NonSerialized] public Quaternion baseRotation;
        [NonSerialized] public Vector3 baseScale;

        // 보정값이 0일 때 되돌릴 원본 값. 극좌표나 월드 좌표를 거쳐 다시 계산하면
        // 아주 미세한 오차가 남아 씬 파일이 괜히 바뀌므로, 읽은 로컬 값을 그대로 보관해 둔다.
        [NonSerialized] public bool hasExactBase;
        [NonSerialized] public Vector3 exactLocalPosition;
        [NonSerialized] public Quaternion exactLocalRotation;
        [NonSerialized] public Vector3 exactLocalScale;

        public bool IsDefault
        {
            get
            {
                return angleOffset == 0f && radiusOffset == 0f && rotationOffset == 0f &&
                       Mathf.Approximately(scaleMultiplier, 1f);
            }
        }
    }

    [Header("원 중심 (월드 XY)")]
    [Tooltip("비워두면 아래 centerWorld 값을 쓴다.")]
    public Transform circleCenter;
    public Vector2 centerWorld = Vector2.zero;

    [Header("테이블")]
    public List<TableEntry> tables = new List<TableEntry>();

    [Header("조정 한계")]
    public float angleLimit = 45f;
    public float radiusLimit = 10f;
    public float rotationLimit = 45f;
    public float minScale = 0.5f;
    public float maxScale = 2f;

    [Header("조정 단위")]
    public float angleStep = 0.5f;
    public float angleFineStep = 0.1f;
    public float radiusStep = 0.2f;
    public float radiusFineStep = 0.05f;
    public float rotationStep = 0.5f;
    public float rotationFineStep = 0.1f;
    public float scaleStep = 0.01f;
    public float scaleFineStep = 0.002f;

    [Header("조작 (캘리브레이션 GUI가 켜져 있을 때만 동작)")]
    public bool onlyWhileCalibrationGui = true;
    [Tooltip("조정 대상 순환: 해제 → 1 → 2 → ... → 전체 → 해제")]
    public KeyCode selectKey = KeyCode.M;
    public ProjectorFieldAdjust.Button rotateCcw = new ProjectorFieldAdjust.Button { key = KeyCode.LeftArrow };
    public ProjectorFieldAdjust.Button rotateCw = new ProjectorFieldAdjust.Button { key = KeyCode.RightArrow };
    public ProjectorFieldAdjust.Button radiusOut = new ProjectorFieldAdjust.Button { key = KeyCode.UpArrow };
    public ProjectorFieldAdjust.Button radiusIn = new ProjectorFieldAdjust.Button { key = KeyCode.DownArrow };
    public ProjectorFieldAdjust.Button spinCcw = new ProjectorFieldAdjust.Button { key = KeyCode.Comma };
    public ProjectorFieldAdjust.Button spinCw = new ProjectorFieldAdjust.Button { key = KeyCode.Period };
    public ProjectorFieldAdjust.Button scaleUp = new ProjectorFieldAdjust.Button { key = KeyCode.PageUp };
    public ProjectorFieldAdjust.Button scaleDown = new ProjectorFieldAdjust.Button { key = KeyCode.PageDown };
    [Tooltip("선택된 대상의 보정값을 되돌린다(저장하지는 않는다)")]
    public KeyCode resetKey = KeyCode.F7;
    public KeyCode fineModifier = KeyCode.LeftShift;
    public float holdDelay = 0.45f;
    public float repeatRate = 14f;

    [Header("상태")]
    [SerializeField] int selected = NoSelection;

    ProjectorFieldAdjust fieldAdjust;
    bool applyRequested;

    /// <summary>테이블 조정 모드인지. 켜져 있으면 화면 중심 조정 쪽은 방향키를 양보한다.</summary>
    public bool HasSelection { get { return selected != NoSelection; } }

    public static string SavePath
    {
        get { return Path.Combine(Application.streamingAssetsPath, SaveFileName); }
    }

    Vector2 Center
    {
        get { return circleCenter != null ? (Vector2)circleCenter.position : centerWorld; }
    }

    #region Unity

    void OnEnable()
    {
        CaptureBase();
        applyRequested = true;
    }

    void Start()
    {
        CaptureBase();
        if (Application.isPlaying)
        {
            selected = NoSelection; // 운영 시작은 항상 조정 해제 상태로
            Load();
        }
        Apply();
    }

    void OnValidate()
    {
        applyRequested = true;
    }

    void Update()
    {
        if (applyRequested)
        {
            applyRequested = false;
            CaptureBase();
            Apply();
        }

        if (!Application.isPlaying) return;
        if (onlyWhileCalibrationGui && !CalibrationGuiVisible()) return;
        if (InputBlocked()) return;

        if (selectKey != KeyCode.None && Input.GetKeyDown(selectKey))
        {
            SelectNext();
            return;
        }

        if (!HasSelection) return;

        bool fine = fineModifier != KeyCode.None &&
                    (Input.GetKey(fineModifier) || Input.GetKey(KeyCode.RightShift));
        bool changed = false;

        float dAngle = fine ? angleFineStep : angleStep;
        float dRadius = fine ? radiusFineStep : radiusStep;
        float dRotation = fine ? rotationFineStep : rotationStep;
        float dScale = fine ? scaleFineStep : scaleStep;

        if (Repeat(rotateCcw)) { ForEachSelected(t => t.angleOffset += dAngle); changed = true; }
        if (Repeat(rotateCw)) { ForEachSelected(t => t.angleOffset -= dAngle); changed = true; }
        if (Repeat(radiusOut)) { ForEachSelected(t => t.radiusOffset += dRadius); changed = true; }
        if (Repeat(radiusIn)) { ForEachSelected(t => t.radiusOffset -= dRadius); changed = true; }
        if (Repeat(spinCcw)) { ForEachSelected(t => t.rotationOffset += dRotation); changed = true; }
        if (Repeat(spinCw)) { ForEachSelected(t => t.rotationOffset -= dRotation); changed = true; }
        if (Repeat(scaleUp)) { ForEachSelected(t => t.scaleMultiplier += dScale); changed = true; }
        if (Repeat(scaleDown)) { ForEachSelected(t => t.scaleMultiplier -= dScale); changed = true; }

        if (resetKey != KeyCode.None && Input.GetKeyDown(resetKey))
        {
            ForEachSelected(t =>
            {
                t.angleOffset = 0f;
                t.radiusOffset = 0f;
                t.rotationOffset = 0f;
                t.scaleMultiplier = 1f;
            });
            Apply();
            Notify("Table " + SelectionLabel() + " reset");
            return;
        }

        if (changed)
        {
            Apply();
            NotifySelection();
        }
    }

    #endregion

    #region Selection

    public void SelectNext()
    {
        int start = selected;
        int next = start;

        // 해제 → 조정 가능한 첫 테이블 → ... → 전체 → 해제
        if (next == AllSelected) next = NoSelection;
        else
        {
            int i = (next == NoSelection) ? 0 : next + 1;
            while (i < tables.Count && !IsAdjustable(i)) i++;
            next = (i < tables.Count) ? i : AllSelected;
        }

        // 조정 가능한 테이블이 하나도 없으면 전체 선택도 의미가 없다.
        if (next == AllSelected && AdjustableCount() == 0) next = NoSelection;

        selected = next;
        Notify(HasSelection ? ("Table select: " + SelectionLabel()) : "Table select: OFF");
    }

    public void ClearSelection()
    {
        selected = NoSelection;
    }

    bool IsAdjustable(int index)
    {
        return index >= 0 && index < tables.Count &&
               tables[index] != null && tables[index].adjustable && tables[index].target != null;
    }

    int AdjustableCount()
    {
        int n = 0;
        for (int i = 0; i < tables.Count; i++) if (IsAdjustable(i)) n++;
        return n;
    }

    string SelectionLabel()
    {
        if (selected == AllSelected) return "ALL";
        if (!IsAdjustable(selected)) return "-";
        string label = tables[selected].label;
        return string.IsNullOrEmpty(label) ? (selected + 1).ToString() : label;
    }

    void ForEachSelected(Action<TableEntry> action)
    {
        if (selected == AllSelected)
        {
            for (int i = 0; i < tables.Count; i++) if (IsAdjustable(i)) action(tables[i]);
        }
        else if (IsAdjustable(selected))
        {
            action(tables[selected]);
        }
    }

    #endregion

    #region Apply

    /// <summary>모든 테이블을 현재 보정값대로 배치한다.</summary>
    public void Apply()
    {
        Vector2 center = Center;

        for (int i = 0; i < tables.Count; i++)
        {
            TableEntry t = tables[i];
            if (t == null || t.target == null || !t.baseCaptured) continue;

            // NaN/Infinity 는 Mathf.Clamp 를 그대로 통과해 테이블이 사라지므로 먼저 걸러낸다.
            t.angleOffset = ProjectorFieldAdjust.Safe(t.angleOffset, 0f);
            t.radiusOffset = ProjectorFieldAdjust.Safe(t.radiusOffset, 0f);
            t.rotationOffset = ProjectorFieldAdjust.Safe(t.rotationOffset, 0f);
            t.scaleMultiplier = ProjectorFieldAdjust.Safe(t.scaleMultiplier, 1f);

            t.angleOffset = Mathf.Clamp(t.angleOffset, -angleLimit, angleLimit);
            t.radiusOffset = Mathf.Clamp(t.radiusOffset, -radiusLimit, radiusLimit);
            t.rotationOffset = Mathf.Clamp(t.rotationOffset, -rotationLimit, rotationLimit);
            t.scaleMultiplier = Mathf.Clamp(t.scaleMultiplier, minScale, maxScale);

            // 보정값이 없으면 씬에 있던 값을 건드리지 않는다.
            if (t.IsDefault && t.hasExactBase)
            {
                // 값이 실제로 다를 때만 쓴다. 같은 값을 다시 대입해도 Unity 가 정규화하면서
                // 미세하게 다른 값으로 저장돼 씬 파일이 괜히 바뀐다.
                // Vector3/Quaternion 의 != 는 근사 비교라서 미세 오차를 "같다"고 판단해
                // 초기화해도 원래 값으로 안 돌아온다. 여기서는 비트 단위로 비교한다.
                if (!Same(t.target.localPosition, t.exactLocalPosition)) t.target.localPosition = t.exactLocalPosition;
                if (!Same(t.target.localRotation, t.exactLocalRotation)) t.target.localRotation = t.exactLocalRotation;
                if (!Same(t.target.localScale, t.exactLocalScale)) t.target.localScale = t.exactLocalScale;
                continue;
            }

            float angle = t.baseAngle + t.angleOffset;
            float radius = Mathf.Max(0.01f, t.baseRadius + t.radiusOffset);
            float rad = angle * Mathf.Deg2Rad;

            t.target.position = new Vector3(
                center.x + Mathf.Cos(rad) * radius,
                center.y + Mathf.Sin(rad) * radius,
                t.baseZ);

            // 둘레를 따라 움직인 만큼 테이블도 같이 돌려서 중심을 향한 자세를 유지한다.
            t.target.rotation = Quaternion.Euler(0f, 0f, t.angleOffset + t.rotationOffset) * t.baseRotation;
            t.target.localScale = t.baseScale * t.scaleMultiplier;
        }
    }

    /// <summary>Unity 의 == 는 근사 비교라서, 비트 단위로 같은지 직접 확인한다.</summary>
    static bool Same(Vector3 a, Vector3 b)
    {
        return a.x == b.x && a.y == b.y && a.z == b.z;
    }

    static bool Same(Quaternion a, Quaternion b)
    {
        return a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w;
    }

    /// <summary>씬에 놓여 있던 원래 배치를 기준값으로 저장한다(이미 반영된 보정값은 빼고 기록).</summary>
    public void CaptureBase()
    {
        Vector2 center = Center;

        for (int i = 0; i < tables.Count; i++)
        {
            TableEntry t = tables[i];
            if (t == null || t.target == null || t.baseCaptured) continue;

            Vector3 p = t.target.position;
            Vector2 d = new Vector2(p.x - center.x, p.y - center.y);

            t.baseRadius = d.magnitude - t.radiusOffset;
            t.baseAngle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg - t.angleOffset;
            t.baseZ = p.z;
            t.baseRotation = Quaternion.Euler(0f, 0f, -(t.angleOffset + t.rotationOffset)) * t.target.rotation;
            t.baseScale = t.scaleMultiplier > 0.0001f
                ? t.target.localScale / t.scaleMultiplier
                : t.target.localScale;

            // 캡처 시점에 보정값이 0이면 지금 읽은 값이 곧 원본이다.
            t.hasExactBase = t.IsDefault;
            if (t.hasExactBase)
            {
                t.exactLocalPosition = t.target.localPosition;
                t.exactLocalRotation = t.target.localRotation;
                t.exactLocalScale = t.target.localScale;
            }

            t.baseCaptured = true;
        }
    }

    #endregion

    #region Save / Load

    [Serializable]
    class SaveEntry
    {
        public string label;
        public float angleOffset;
        public float radiusOffset;
        public float rotationOffset;
        public float scaleMultiplier = 1f;
    }

    [Serializable]
    class SaveData
    {
        public List<SaveEntry> tables = new List<SaveEntry>();
    }

    public bool Save()
    {
        SaveData data = new SaveData();
        for (int i = 0; i < tables.Count; i++)
        {
            TableEntry t = tables[i];
            if (t == null) continue;
            data.tables.Add(new SaveEntry
            {
                label = string.IsNullOrEmpty(t.label) ? (i + 1).ToString() : t.label,
                angleOffset = t.angleOffset,
                radiusOffset = t.radiusOffset,
                rotationOffset = t.rotationOffset,
                scaleMultiplier = t.scaleMultiplier
            });
        }

        try
        {
            string path = SavePath;
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, JsonUtility.ToJson(data, true));
            Debug.Log(path + " has been saved.");
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("테이블 보정값 저장 실패: " + e.Message);
            return false;
        }
    }

    public bool Load()
    {
        string path = SavePath;
        if (!File.Exists(path)) return false;

        try
        {
            SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
            if (data == null || data.tables == null) return false;

            for (int i = 0; i < tables.Count; i++)
            {
                TableEntry t = tables[i];
                if (t == null) continue;

                string label = string.IsNullOrEmpty(t.label) ? (i + 1).ToString() : t.label;
                SaveEntry e = data.tables.Find(x => x != null && x.label == label);
                if (e == null) continue;

                t.angleOffset = e.angleOffset;
                t.radiusOffset = e.radiusOffset;
                t.rotationOffset = e.rotationOffset;
                t.scaleMultiplier = e.scaleMultiplier;
            }
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("테이블 보정값 로드 실패: " + e.Message);
            return false;
        }
    }

    #endregion

    #region Helpers

    ProjectorFieldAdjust FieldAdjust
    {
        get
        {
            if (fieldAdjust == null) fieldAdjust = GetComponent<ProjectorFieldAdjust>();
            if (fieldAdjust == null) fieldAdjust = FindObjectOfType<ProjectorFieldAdjust>();
            return fieldAdjust;
        }
    }

    bool CalibrationGuiVisible()
    {
        ProjectorFieldAdjust fa = FieldAdjust;
        return fa != null && fa.warpSystem != null && fa.warpSystem.showProjectionWarpGUI;
    }

    /// <summary>
    /// 캘리브레이션 입력칸에 타이핑 중이거나 ESC 설정 패널이 열려 있으면 단축키를 받지 않는다.
    /// (타이핑한 글자가 테이블을 움직이는 것을 막는다)
    /// </summary>
    bool InputBlocked()
    {
        ProjectorFieldAdjust fa = FieldAdjust;
        if (fa == null) return false;
        if (fa.ExternalUiBlocking) return true;
        return ProjectorFieldAdjust.IsCalibrationInputFocused(
            fa.warpSystem != null ? fa.warpSystem.calibrationManager : null);
    }

    bool Repeat(ProjectorFieldAdjust.Button button)
    {
        return ProjectorFieldAdjust.Repeat(button, holdDelay, repeatRate);
    }

    void NotifySelection()
    {
        TableEntry t = (selected == AllSelected)
            ? (AdjustableCount() > 0 ? FirstAdjustable() : null)
            : (IsAdjustable(selected) ? tables[selected] : null);
        if (t == null) return;

        Notify(string.Format("Table {0}  angle {1:+0.0;-0.0;0.0}  radius {2:+0.00;-0.00;0.00}  spin {3:+0.0;-0.0;0.0}  scale {4:0.000}",
            SelectionLabel(), t.angleOffset, t.radiusOffset, t.rotationOffset, t.scaleMultiplier));
    }

    TableEntry FirstAdjustable()
    {
        for (int i = 0; i < tables.Count; i++) if (IsAdjustable(i)) return tables[i];
        return null;
    }

    void Notify(string message)
    {
        ProjectorFieldAdjust fa = FieldAdjust;
        if (fa != null) fa.NotifyExternal(message);
    }

    #endregion
}
