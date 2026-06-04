using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
///  限制物件只能沿 +Z 移動，抓取時顯示 Outline 與圓圈，放開後關閉並啟動冷卻。<br/>
///  - 需掛 URP 相容的 <see cref="Outline"/>。<br/>
///  - LineRenderer 畫出允許區域。<br/>
/// </summary>
[RequireComponent(typeof(Outline))]
public class RestrictMovementToZAxis2 : XRGrabInteractable
{
    /* ====== 基本參數 ====== */
    private Vector3 initialPosition;
    private Vector3 previousPosition;

    public float moveSpeed = 0.01f;   // 位移倍率
    public float maxSpeed  = 0.1f;    // 每偵最大速度(+Z)
    public float maxZ      = 615.34f; // 允許的最大 Z

    /* ====== 冷卻 ====== */
    public float cooldownTime = 2f;
    private float currentCooldown = 0f;
    private bool  isCooldown      = false;

    /* ====== 抓取範圍 ====== */
    public Transform allowedAreaCenter;
    public float allowedAreaRadius = 5f;

    /* ====== UI 與圓圈 ====== */
    public LineRenderer circleRenderer;
    public int circleSegments = 100;
    public GameObject canvasHint;

    /* ====== Outline ====== */
    private Outline outline;

    protected override void Awake()
    {
        base.Awake();
        outline = GetComponent<Outline>();
        if (outline != null) outline.enabled = false;
    }

    /* ---------- 抓取允許判斷 ---------- */
    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        if (isCooldown) return false;

        if (allowedAreaCenter != null)
        {
            Vector3 p = interactor.transform.position;
            p.y = allowedAreaCenter.position.y;
            if (Vector3.Distance(p, allowedAreaCenter.position) > allowedAreaRadius)
                return false;
        }
        return base.IsSelectableBy(interactor);
    }

    /* ---------- 抓取開始 ---------- */
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (isCooldown) return;

        base.OnSelectEntered(args);
        initialPosition  = transform.position;
        previousPosition = initialPosition;

        if (attachTransform) attachTransform.position = transform.position;

        if (circleRenderer != null) circleRenderer.gameObject.SetActive(true);
        if (outline != null)        outline.enabled = true;
    }

    /* ---------- 抓取結束 ---------- */
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        /* 回到最後合法位置並清除剛體速度 */
        transform.position = previousPosition;
        if (TryGetComponent(out Rigidbody rb))
        {
            rb.velocity        = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (canvasHint != null) canvasHint.SetActive(false);
        if (circleRenderer != null) circleRenderer.gameObject.SetActive(false);
        if (outline != null)        outline.enabled = false;

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

        /* 更新圓圈 (僅作示意，可移除效能更佳) */
        if (circleRenderer && allowedAreaCenter) DrawCircle();
    }

    /* ---------- 位移處理 ---------- */
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase phase)
    {
        if (isCooldown) return;
        base.ProcessInteractable(phase);

        if (!isSelected) return;

        Vector3 pos     = transform.position;
        pos.x           = initialPosition.x;
        pos.y           = initialPosition.y;
        float delta     = pos.z - previousPosition.z; // +Z

        /* 禁止向 -Z，僅允許 +Z */
        if (delta < 0) delta = 0;

        delta = Mathf.Clamp(delta, 0, maxSpeed);
        pos.z = Mathf.Min(previousPosition.z + delta * moveSpeed, maxZ);

        transform.position = pos;
        previousPosition   = pos;

        /* 提示 UI */
        if (Mathf.Approximately(pos.z, maxZ))
        {
            if (canvasHint && !canvasHint.activeSelf) canvasHint.SetActive(true);
        }
        else
        {
            if (canvasHint && canvasHint.activeSelf)  canvasHint.SetActive(false);
        }
    }

    /* ---------- 畫圓 ---------- */
    private void DrawCircle()
    {
        circleRenderer.positionCount = circleSegments + 1;
        float dTheta  = 2f * Mathf.PI / circleSegments;
        float theta   = 0f;
        Vector3 c     = allowedAreaCenter.position;

        for (int i = 0; i <= circleSegments; i++)
        {
            float x = allowedAreaRadius * Mathf.Cos(theta);
            float z = allowedAreaRadius * Mathf.Sin(theta);
            circleRenderer.SetPosition(i, c + new Vector3(x, 0f, z));
            theta += dTheta;
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
