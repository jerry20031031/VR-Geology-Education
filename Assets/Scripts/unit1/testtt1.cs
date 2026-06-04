using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class testtt1 : MonoBehaviour
{
    private XRSocketInteractor socketInteractor;
    private void Awake()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.AddListener(OnSelectEntered);
            socketInteractor.selectExited.AddListener(OnSelectExited);
        }
    }

    private void OnDestroy()
    {
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.RemoveListener(OnSelectEntered);
            socketInteractor.selectExited.RemoveListener(OnSelectExited);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.name == "South America (1)")
        {
            Debug.Log($"物件 {args.interactableObject.transform.name} 被插入到插槽中");
          
        }

        else
        {

            Debug.Log("South America (1) 放置在 india 的 Socket 上");

            // 嘗試強制取消與 Socket 的互動
            XRBaseInteractable interactable = args.interactableObject as XRBaseInteractable;
            if (interactable != null)
            {
                // 強制取消與插槽的互動
                var interactionManager = interactable.interactionManager;
                interactionManager.SelectExit(args.interactorObject, interactable);
            }

            // 呼叫 South America (1) 的腳本方法
            testtttt222 snapBackScript = args.interactableObject.transform.GetComponent<testtttt222>();
            if (snapBackScript != null)
            {
                snapBackScript.ResetPosition();
            }
        }
    }
    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactableObject.transform.name == "South America (1)")
        {
            XRGrabInteractable interactable = args.interactableObject as XRGrabInteractable;
            if (interactable != null)
            {
                interactable.enabled = false;
                Debug.Log($"物件 {args.interactableObject.transform.name} 的抓取功能已被禁用");
            }
            socketInteractor.enabled = false;
            Debug.Log($"插槽 {socketInteractor.name} 的功能已被禁用");
        }

    }
}
