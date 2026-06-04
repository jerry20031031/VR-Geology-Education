using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class oneGrabone2 : MonoBehaviour
{
    // 清單，包含所有需要控制的 XR Simple Interactable 物體
    public List<XRSimpleInteractable> simpleInteractables;

    // 當前被抓取的物體
    private XRSimpleInteractable currentGrabbedObject;

    private void Start()
    {
        // 為每個 XR Simple Interactable 添加事件監聽器
        foreach (var simpleInteractable in simpleInteractables)
        {
            simpleInteractable.selectEntered.AddListener(OnSimpleObjectGrabbed); // 當物體被抓取時觸發
            simpleInteractable.selectExited.AddListener(OnSimpleObjectReleased); // 當物體被釋放時觸發
        }
    }

    private void OnDestroy()
    {
        // 移除每個 XR Simple Interactable 的事件監聽器
        foreach (var simpleInteractable in simpleInteractables)
        {
            simpleInteractable.selectEntered.RemoveListener(OnSimpleObjectGrabbed);
            simpleInteractable.selectExited.RemoveListener(OnSimpleObjectReleased);
        }
    }

    // 當某個 XR Simple Interactable 被抓取時觸發
    private void OnSimpleObjectGrabbed(SelectEnterEventArgs args)
    {
        XRSimpleInteractable grabbedObject = args.interactableObject as XRSimpleInteractable;
        if (grabbedObject == null || currentGrabbedObject != null) return;

        currentGrabbedObject = grabbedObject;

        // 禁用其他物體的交互
        DisableOtherInteractions(grabbedObject);
    }

    // 當某個 XR Simple Interactable 被釋放時觸發
    private void OnSimpleObjectReleased(SelectExitEventArgs args)
    {
        XRSimpleInteractable releasedObject = args.interactableObject as XRSimpleInteractable;
        if (releasedObject == null || !ReferenceEquals(releasedObject, currentGrabbedObject)) return;

        currentGrabbedObject = null;

        // 啟用所有物體的交互
        EnableAllInteractions();
    }

    // 禁用除指定物體外的所有 XR Simple Interactable
    private void DisableOtherInteractions(XRSimpleInteractable activeObject)
    {
        foreach (var simpleInteractable in simpleInteractables)
        {
            if (!ReferenceEquals(simpleInteractable, activeObject))
            {
                simpleInteractable.enabled = false;
            }
        }
    }

    // 啟用所有 XR Simple Interactable
    private void EnableAllInteractions()
    {
        foreach (var simpleInteractable in simpleInteractables)
        {
            simpleInteractable.enabled = true;
        }
    }
}
