using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Obi;

public class RestrictMovementToZAxis3 : XRGrabInteractable
{
    /* ---------- 粒子 ---------- */
    public ObiEmitter fluidEmitter;
    public ObiEmitter fluidEmitter2;
    public ObiEmitter fluidEmitter3;

    /* ---------- 位移限制 ---------- */
    private Vector3 initialPosition;
    public float moveSpeed = 0.01f; // 控制移動速度
    public float maxSpeed = 0.1f;   // 限制最大移動速度
    private Vector3 previousPosition;
    private float minZ = 637.79f;   // Z 軸最小值

    /* ---------- 冷卻 ---------- */
    public float cooldownTime = 2.0f; // 冷卻時間（秒）
    private float currentCooldown = 0f; // 當前冷卻計時器
    private bool isCooldown = false;    // 是否處於冷卻中

    /* ---------- 抓取範圍 ---------- */
    public Transform allowedAreaCenter;  // 拖動允許區域的中心點
    public float allowedAreaRadius = 5.0f; // 圓形區域半徑

    /* ---------- 圓圈 ---------- */
    public LineRenderer circleRenderer;
    public int circleSegments = 100; // 繪製圓圈的段數
    private static RestrictMovementToZAxis3 activeInstance = null; // 靜態變數：目前顯示圓圈的物件實例

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

    private void Start()
    {
        circleRenderer.gameObject.SetActive(false);

        // 設定顏色、寬度，方便測試
        circleRenderer.startColor = Color.red;
        circleRenderer.endColor = Color.red;
        circleRenderer.startWidth = 0.05f;
        circleRenderer.endWidth = 0.05f;

        // 遊戲一開始就先畫一次圈，看能不能在 Scene/Game 視窗看到
        DrawCircle();
        
        // 初始化所有 XR Interactor（通常是手部控制器）
        InitializeInteractors();
    }

    // 覆寫 IsSelectableBy 來判斷選取是否允許
    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        if (allowedAreaCenter != null)
        {
            Vector3 interactorPos = interactor.transform.position;
            Vector3 centerPos = allowedAreaCenter.position;
            // 僅考慮水平距離
            interactorPos.y = centerPos.y;
            if (Vector3.Distance(interactorPos, centerPos) > allowedAreaRadius)
            {
                // 超出允許範圍，不允許選取
                return false;
            }
        }
        return base.IsSelectableBy(interactor);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (isCooldown)
            return;

        base.OnSelectEntered(args);
        initialPosition = transform.position;
        previousPosition = initialPosition;

        if (attachTransform != null)
            attachTransform.position = transform.position;

        // 當開始抓取時，若已有其他物件的圓圈在顯示，先隱藏它
        if (activeInstance != null && activeInstance != this)
        {
            activeInstance.HideCircle();
        }
        activeInstance = this;
        //ShowCircle();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // 強制將物件位置設定回上一個正確的位置
        transform.position = previousPosition;

        // 如果有 Rigidbody，清除其速度，避免後續物理效果
        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // 離開抓取後不隱藏圓圈，讓其持續顯示，直到下一次切換
        isCooldown = true;
        currentCooldown = cooldownTime;
    }

    private void Update()
    {
        // 冷卻計時邏輯
        if (isCooldown)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0)
            {
                isCooldown = false;
                currentCooldown = 0;
            }
        }

        // 僅當目前物件為 activeInstance 時更新圓圈
        if (activeInstance == this && circleRenderer != null && allowedAreaCenter != null)
        {
            DrawCircle();
        }

        /* 檢測 Interactor 進入/離開圓圈 */
        CheckInteractorAreaStatus();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (isCooldown) return; // 冷卻期間禁止交互

        base.ProcessInteractable(updatePhase);

        if (isSelected)
        {
            Vector3 currentPosition = transform.position;
            // 限制 X 和 Y 軸，使物件僅沿 Z 軸移動
            currentPosition.x = initialPosition.x;
            currentPosition.y = initialPosition.y;

            // 計算移動量（Z 軸）
            float movementDelta = currentPosition.z - previousPosition.z;

            // 若使用者試圖往右拉（增加 Z 值），則不變更 Z 軸
            if (movementDelta > 0)
            {
                movementDelta = 0;
            }

            // 限制最大移動速度
            movementDelta = Mathf.Clamp(movementDelta, -maxSpeed, 0);

            // 只讓 Z 軸移動，並確保不超過 minZ
            currentPosition.z = Mathf.Max(previousPosition.z + movementDelta * moveSpeed, minZ);

            transform.position = currentPosition;
            previousPosition = currentPosition;
        }
    }

    // 利用 LineRenderer 畫出圓圈
    private void DrawCircle()
    {
        if (circleRenderer == null || allowedAreaCenter == null)
            return;

        circleRenderer.positionCount = circleSegments + 1;
        float deltaTheta = (2f * Mathf.PI) / circleSegments;
        float theta = 0f;

        for (int i = 0; i < circleSegments + 1; i++)
        {
            float x = allowedAreaRadius * Mathf.Cos(theta);
            float z = allowedAreaRadius * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, 0, z);
            circleRenderer.SetPosition(i, allowedAreaCenter.position + pos);
            theta += deltaTheta;
        }
    }

    // 若只想在 Editor 的 Scene 視窗中看到圓圈，可使用 Gizmos
    private void OnDrawGizmos()
    {
        if (allowedAreaCenter != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(allowedAreaCenter.position, allowedAreaRadius);
        }
    }

    // 顯示圓圈
    public void ShowCircle()
    {
        fluidEmitter.speed = 0f;
        fluidEmitter2.speed = 0f;
        fluidEmitter3.speed = 0f;
        SessionLogger.LogAction("聚合型板塊邊界互動圓圈顯示");
        if (circleRenderer != null)
            circleRenderer.gameObject.SetActive(true);
    }

    // 隱藏圓圈
    public void HideCircle()
    {
        if (circleRenderer != null)
            circleRenderer.gameObject.SetActive(false);
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
            Debug.Log($"[RestrictMovementToZAxis3] 找到 {allInteractors.Count} 個 Interactor");
            hasLoggedInitialization = true;
        }
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
            Vector3 centerPos = allowedAreaCenter.position;
            // 僅考慮水平距離
            interactorPos.y = centerPos.y;
            bool isInArea = Vector3.Distance(interactorPos, centerPos) <= allowedAreaRadius;

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
}
