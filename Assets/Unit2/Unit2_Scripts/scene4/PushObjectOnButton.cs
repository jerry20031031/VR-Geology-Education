using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PushObjectOnButton : MonoBehaviour
{
    [Header("XR Interactor Settings")]
    public XRBaseInteractor xrInteractor; // 指向手把上的 XRDirectInteractor 或 XRRayInteractor

    [Header("Input Settings")]
    public InputActionProperty pushAction; // 綁定 B 鍵動作

    [Header("Push Settings")]
    [Tooltip("每秒往前推的距離 (公尺/秒)")]
    public float pushSpeed = 0.1f; // 一秒推多少公尺
    private bool isPushing = false; // 用來紀錄目前是否在推

    private void OnEnable()
    {
        if (pushAction != null && pushAction.action != null)
        {
            // 啟用動作
            pushAction.action.Enable();

            // 監聽「按鍵開始」和「按鍵結束」
            pushAction.action.started += OnPushStarted;
            pushAction.action.canceled += OnPushCanceled;
        }
    }

    private void OnDisable()
    {
        if (pushAction != null && pushAction.action != null)
        {
            pushAction.action.Disable();
            pushAction.action.started -= OnPushStarted;
            pushAction.action.canceled -= OnPushCanceled;
        }
    }

    private void OnPushStarted(InputAction.CallbackContext context)
    {
        // B 鍵剛被壓下
        isPushing = true;
    }

    private void OnPushCanceled(InputAction.CallbackContext context)
    {
        // B 鍵放開
        isPushing = false;
    }

    private void Update()
    {
        // 若正在推，且手上真的有抓取物件
        if (isPushing && xrInteractor.selectTarget != null)
        {
            // 將 attachTransform (抓取的錨點) 往控制器前方移動
            xrInteractor.attachTransform.position +=
                xrInteractor.transform.forward * (pushSpeed * Time.deltaTime);
        }
    }
}
