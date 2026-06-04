using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Obi;

/// <summary>
///  限制物件只能沿 Z 軸向負方向拖動，並在抓取時顯示 Outline、高亮允許區域圓圈。<br/>
///  放開後關閉 Outline、啟動冷卻，避免連續拖動。<br/>
///  ※ 使用前請確認物件已掛 <see cref="Outline"/>，且 URP 生效。<br/>
/// </summary>
[RequireComponent(typeof(Outline))]
public class RestrictMovementToZAxis : XRGrabInteractable
{
    /* ---------- 粒子 ---------- */
    public ObiEmitter fluidEmitter;
    public ObiEmitter fluidEmitter2;
    public ObiEmitter fluidEmitter3;

    /* ---------- 位移限制 ---------- */
    private Vector3 initialPosition;     // 抓取瞬間的位置
    private Vector3 previousPosition;    // 上一偵位置
    public float moveSpeed = 0.01f;      // Z 方向位移倍率
    public float maxSpeed = 0.1f;        // Z 方向最大速度
    public float minZ = 615.47f;         // 可達到的最小 Z（移動範圍極限）

    /* ---------- 冷卻 ---------- */
    public float cooldownTime = 2f;
    private float currentCooldown = 0f;
    private bool  isCooldown      = false;

    /* ---------- 抓取範圍 ---------- */
    public Transform allowedAreaCenter;
    public float allowedAreaRadius = 5f;

    /* ---------- UI 與提示 ---------- */
    public GameObject canvasHint;

    /* ---------- 圓圈 ---------- */
    public LineRenderer circleRenderer;
    public int circleSegments = 100;
    private static RestrictMovementToZAxis activeInstance = null;

    /* ---------- Outline ---------- */
    private Outline outline;            // 只抓自己身上的 Outline

    /* ---------- 圓圈進入/離開追蹤 ---------- */
    private HashSet<XRBaseInteractor> interactorsInArea = new HashSet<XRBaseInteractor>(); // 追蹤目前在圓圈內的 interactor
    private List<XRBaseInteractor> allInteractors = new List<XRBaseInteractor>(); // 所有可能的 interactor
    private float areaCheckInterval = 0.1f; // 檢測間隔時間（秒）
    private float lastAreaCheckTime = 0f;   // 上次檢測時間
    private bool hasLoggedInitialization = false; // 確保初始化日誌只列印一次
    private bool LogEnter = false,LogOut = false;
    
    // 進入/離開事件冷卻機制
    private Dictionary<XRBaseInteractor, float> interactorLastEventTime = new Dictionary<XRBaseInteractor, float>(); // 記錄每個 interactor 上次觸發事件的時間
    private float eventCooldownTime = 1f; // 同一個 interactor 事件冷卻時間（秒）

    /* ---------- Life-cycle ---------- */
    protected override void Awake()
    {
        base.Awake();

        /* 取得並關閉 Outline（節省效能） */
        outline = GetComponent<Outline>();
        if (outline != null) outline.enabled = false;
    }

    private void Start()
    {
        if (circleRenderer != null)
        {
            circleRenderer.gameObject.SetActive(false);
            circleRenderer.startColor = Color.red;
            circleRenderer.endColor   = Color.red;
            circleRenderer.startWidth = 0.05f;
            circleRenderer.endWidth   = 0.05f;
            DrawCircle();
        }

        // 初始化所有 XR Interactor（通常是手部控制器）
        InitializeInteractors();
    }

    /// <summary>
    /// 初始化並找到場景中的所有 XR Interactor
    /// </summary>
    private void InitializeInteractors()
    {
        // 尋找場景中所有的 XRBaseInteractor
        XRBaseInteractor[] foundInteractors = FindObjectsOfType<XRBaseInteractor>();
        allInteractors.Clear();
        allInteractors.AddRange(foundInteractors);
        
        // 只在第一次初始化時列印日誌
        if (!hasLoggedInitialization)
        {
            Debug.Log($"[RestrictMovementToZAxis] 找到 {allInteractors.Count} 個 Interactor");
            hasLoggedInitialization = true;
        }
    }

    /* ---------- 可否被抓取 ---------- */
    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        if (allowedAreaCenter != null)
        {
            Vector3 p = interactor.transform.position;
            p.y = allowedAreaCenter.position.y; // 只比水平距離
            if (Vector3.Distance(p, allowedAreaCenter.position) > allowedAreaRadius)
                return false;
        }
        return base.IsSelectableBy(interactor);
    }

    /* ---------- 開始抓取 ---------- */
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (isCooldown) return;          // 冷卻中阻擋
        base.OnSelectEntered(args);

        initialPosition  = transform.position;
        previousPosition = initialPosition;

        /* 只有一個物件顯示圈 */
        if (activeInstance != null && activeInstance != this)
            activeInstance.HideCircle();
        activeInstance = this;

        /* 顯示圈並停用三個 Emitter */
        //ShowCircle();
        fluidEmitter.speed  = 0f;
        fluidEmitter2.speed = 0f;
        fluidEmitter3.speed = 0f;

        /* 開啟 Outline */
        if (outline != null) outline.enabled = true;
    }

    /* ---------- 放開 ---------- */
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        /* 重設位置並清除剛體速度 */
        transform.position = previousPosition;
        if (TryGetComponent(out Rigidbody rb))
        {
            rb.velocity        = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (canvasHint != null) canvasHint.SetActive(false);

        /* 關閉 Outline */
        if (outline != null) outline.enabled = false;

        /* 啟動冷卻 */
        isCooldown      = true;
        currentCooldown = cooldownTime;
    }

    /* ---------- 每幀 ---------- */
    private void Update()
    {
        /* 冷卻倒數 */
        if (isCooldown)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0f)
            {
                isCooldown      = false;
                currentCooldown = 0f;
            }
        }

        /* 只有目前被抓取的物件更新圓圈 */
        if (activeInstance == this && circleRenderer != null)
            DrawCircle();

        /* 檢測 Interactor 進入/離開圓圈 */
        CheckInteractorAreaStatus();
    }

    /// <summary>
    /// 檢測 Interactor 是否進入或離開允許區域
    /// 使用時間間隔控制，避免過於頻繁的檢測
    /// 添加冷卻機制防止重複觸發
    /// </summary>
    private void CheckInteractorAreaStatus()
    {
        if (allowedAreaCenter == null) return;

        // 時間間隔控制，每 0.1 秒檢查一次
        float currentTime = Time.time;
        if (currentTime - lastAreaCheckTime < areaCheckInterval) return;
        lastAreaCheckTime = currentTime;

        // 重新掃描一次 interactor（處理動態生成的情況，但不重複列印日誌）
        if (allInteractors.Count == 0)
        {
            InitializeInteractors();
        }

        // 清理已被銷毀的 interactor
        allInteractors.RemoveAll(interactor => interactor == null);
        
        // 清理已銷毀的 interactor 的事件時間記錄
        var keysToRemove = new List<XRBaseInteractor>();
        foreach (var key in interactorLastEventTime.Keys)
        {
            if (key == null || !allInteractors.Contains(key))
            {
                keysToRemove.Add(key);
            }
        }
        foreach (var key in keysToRemove)
        {
            interactorLastEventTime.Remove(key);
        }

        foreach (var interactor in allInteractors)
        {
            if (interactor == null) continue;

            // 計算是否在區域內（使用與 IsSelectableBy 相同的邏輯）
            Vector3 interactorPos = interactor.transform.position;
            interactorPos.y = allowedAreaCenter.position.y; // 只比水平距離
            bool isInArea = Vector3.Distance(interactorPos, allowedAreaCenter.position) <= allowedAreaRadius;

            // 檢查狀態變化
            bool wasInArea = interactorsInArea.Contains(interactor);

            // 檢查是否在冷卻期間
            bool isInCooldown = false;
            if (interactorLastEventTime.ContainsKey(interactor))
            {
                float timeSinceLastEvent = currentTime - interactorLastEventTime[interactor];
                isInCooldown = timeSinceLastEvent < eventCooldownTime;
            }

            if (isInArea && !wasInArea && !isInCooldown)
            {
                // 進入圓圈 - 只會觸發一次，且不在冷卻期間
                interactorsInArea.Add(interactor);
                interactorLastEventTime[interactor] = currentTime;
                if(LogEnter == false)
                {
                    SessionLogger.LogAction("進入互動區域圓圈");
                    LogEnter = true;
                    LogOut = false;
                }
            }
            else if (!isInArea && wasInArea && !isInCooldown)
            {
                // 離開圓圈 - 只會觸發一次，且不在冷卻期間
                interactorsInArea.Remove(interactor);
                interactorLastEventTime[interactor] = currentTime;
                if(LogOut == false)
                {
                    SessionLogger.LogAction("離開互動區域圓圈");
                    LogOut = true;
                    LogEnter = false;
                }
            }
        }
    }

    /* ---------- 拖動處理 ---------- */
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase phase)
    {
        if (isCooldown) return;
        base.ProcessInteractable(phase);

        if (!isSelected) return;

        Vector3 pos          = transform.position;
        pos.x                = initialPosition.x;
        pos.y                = initialPosition.y;
        float delta          = pos.z - previousPosition.z;

        /* 禁止向 +Z，僅允許往 -Z */
        if (delta > 0) delta = 0;

        delta = Mathf.Clamp(delta, -maxSpeed, 0);
        pos.z = Mathf.Max(previousPosition.z + delta * moveSpeed, minZ);

        transform.position = pos;
        previousPosition   = pos;

        /* 到達邊界時顯示提示 */
        if (Mathf.Approximately(pos.z, minZ))
        {
            if (canvasHint != null && !canvasHint.activeSelf) canvasHint.SetActive(true);
        }
        else
        {
            if (canvasHint != null && canvasHint.activeSelf)  canvasHint.SetActive(false);
        }
    }

    /* ---------- 區域圓圈 ---------- */
    private void ShowCircle()
    {
        if (circleRenderer != null) circleRenderer.gameObject.SetActive(true);
        SessionLogger.LogAction("張裂型板塊邊界互動圓圈顯示");
    }
    private void HideCircle()
    {
        if (circleRenderer != null) circleRenderer.gameObject.SetActive(false);
        SessionLogger.LogAction("張裂型板塊邊界互動圓圈隱藏");
    }
    private void DrawCircle()
    {
        if (allowedAreaCenter == null || circleRenderer == null) return;

        circleRenderer.positionCount = circleSegments + 1;
        float deltaTheta = 2f * Mathf.PI / circleSegments;
        float theta      = 0f;
        Vector3 center   = allowedAreaCenter.position;

        for (int i = 0; i <= circleSegments; i++)
        {
            float x = allowedAreaRadius * Mathf.Cos(theta);
            float z = allowedAreaRadius * Mathf.Sin(theta);
            circleRenderer.SetPosition(i, center + new Vector3(x, 0f, z));
            theta += deltaTheta;
        }
    }

    /* ---------- Scene Gizmo ---------- */
    private void OnDrawGizmos()
    {
        if (allowedAreaCenter == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(allowedAreaCenter.position, allowedAreaRadius);
    }
}
