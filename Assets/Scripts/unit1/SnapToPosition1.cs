using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapToPosition1 : MonoBehaviour
{
    public TriggerPoint1 collisionHandler; // 拖入 Sphere 上的腳本
    public GameObject southAmericaOriginal;  // 對齊的參考物件
    private XRGrabInteractable grabInteractable;


    private void Awake()
    {
        // 取得 XR Grab Interactable 組件
        grabInteractable = GetComponent<XRGrabInteractable>();

        // 訂閱抓取相關的事件
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnDestroy()
    {
        // 取消訂閱事件（防止記憶體洩漏）
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {

    }

    private void OnReleased(SelectExitEventArgs args)
    {

        // 檢查是否碰撞過 Sphere
        if (collisionHandler.HasCollided())
        {
            // 將位置和旋轉對齊到 southAmericaOriginal
            transform.position = southAmericaOriginal.transform.position;
            transform.rotation = southAmericaOriginal.transform.rotation;

            // 隱藏 Sphere
            collisionHandler.gameObject.SetActive(false);
        }
    }
}
