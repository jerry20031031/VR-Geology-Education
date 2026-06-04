using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OneGrabOne : MonoBehaviour
{
    // 清單，包含所有需要控制的 XRGrabInteractable 物體
    public List<XRGrabInteractable> grabInteractables;
    // 清單，包含所有需要控制的 XR Simple Interactable 物體
    public List<XRSimpleInteractable> simpleInteractables;

    private void Start()
    {
        // 為每個 XRGrabInteractable 添加事件監聽器
        foreach (var grabInteractable in grabInteractables)
        {
            grabInteractable.selectEntered.AddListener(OnObjectGrabbed); // 當物體被抓取時觸發
            grabInteractable.selectExited.AddListener(OnObjectReleased); // 當物體被釋放時觸發
        }

        // 為每個 XR Simple Interactable 添加事件監聽器
        foreach (var simpleInteractable in simpleInteractables)
        {
            simpleInteractable.selectEntered.AddListener(OnSimpleObjectGrabbed); // 當物體被抓取時觸發
            simpleInteractable.selectExited.AddListener(OnSimpleObjectReleased); // 當物體被釋放時觸發
        }
    }

    private void OnDestroy()
    {
        // 在物體銷毀時移除事件監聽器，防止內存洩漏
        foreach (var grabInteractable in grabInteractables)
        {
            grabInteractable.selectEntered.RemoveListener(OnObjectGrabbed);
            grabInteractable.selectExited.RemoveListener(OnObjectReleased);
        }

        // 移除每個 XR Simple Interactable 的事件監聽器
        foreach (var simpleInteractable in simpleInteractables)
        {
            simpleInteractable.selectEntered.RemoveListener(OnSimpleObjectGrabbed);
            simpleInteractable.selectExited.RemoveListener(OnSimpleObjectReleased);
        }
    }

    // 當某個 XRGrabInteractable 被抓取時觸發
    private void OnObjectGrabbed(SelectEnterEventArgs args)
    {
        XRGrabInteractable grabbedObject = args.interactableObject as XRGrabInteractable;
        if (grabbedObject == null) return;
        DisableAllInteractionsExcept(grabbedObject);
    }

    // 當某個 XRGrabInteractable 被釋放時觸發
    private void OnObjectReleased(SelectExitEventArgs args)
    {
        EnableAllInteractions();
    }

    // 當某個 XR Simple Interactable 被抓取時觸發
    private void OnSimpleObjectGrabbed(SelectEnterEventArgs args)
    {
        XRSimpleInteractable grabbedObject = args.interactableObject as XRSimpleInteractable;
        if (grabbedObject == null) return;
        DisableAllInteractionsExcept(grabbedObject);
    }

    // 當某個 XR Simple Interactable 被釋放時觸發
    private void OnSimpleObjectReleased(SelectExitEventArgs args)
    {
        EnableAllInteractions();
    }

    // 禁用除指定物體外的所有 XRGrabInteractable 和 XR Simple Interactable
    private void DisableAllInteractionsExcept(IXRSelectInteractable activeObject)
    {
        foreach (var grabInteractable in grabInteractables)
        {
            if (!ReferenceEquals(grabInteractable, activeObject))
            {
                grabInteractable.enabled = false;
            }
        }

        foreach (var simpleInteractable in simpleInteractables)
        {
            if (!ReferenceEquals(simpleInteractable, activeObject))
            {
                simpleInteractable.enabled = false;
            }
        }
    }

    // 啟用所有 XRGrabInteractable 和 XR Simple Interactable
    private void EnableAllInteractions()
    {
        foreach (var grabInteractable in grabInteractables)
        {
            grabInteractable.enabled = true;
        }

        foreach (var simpleInteractable in simpleInteractables)
        {
            simpleInteractable.enabled = true;
        }
    }
}
