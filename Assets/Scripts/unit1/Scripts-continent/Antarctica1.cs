using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class Antarctica1 : MonoBehaviour
{
    private XRSocketInteractor socketInteractor;
    public AudioSource audioSource;
    public GameObject error;
    public RawImage rawImage1; // RawImage1組件
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
        if (args.interactableObject.transform.name == "Antarctica (1)")
        {
            Debug.Log($"物件 {args.interactableObject.transform.name} 被插入到插槽中");
            outline22 outlineScript = args.interactableObject.transform.GetComponent<outline22>();
            if (outlineScript != null)
            {
                outlineScript.enabled = true; //  Outline22 腳本
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("啟用找不到 Outline22 腳本");
            }
        }

        else
        {
            // 嘗試強制取消與 Socket 的互動
            XRBaseInteractable interactable = args.interactableObject as XRBaseInteractable;
            if (interactable != null)
            {
                // 強制取消與插槽的互動
                var interactionManager = interactable.interactionManager;
                interactionManager.SelectExit(args.interactorObject, interactable);
            }

            testtttt222 snapBackScript = args.interactableObject.transform.GetComponent<testtttt222>();
            if (snapBackScript != null)
            {
                snapBackScript.ResetPosition();
            }
            error.SetActive(true);
            rawImage1.color = Color.red;
        }
    }
    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactableObject.transform.name == "Antarctica (1)")
        {
            outline22 outlineScript = args.interactableObject.transform.GetComponent<outline22>();
            if (outlineScript != null)
            {
                outlineScript.enabled = false; //  Outline22 腳本
            }
            else
            {
                Debug.LogWarning("取消找不到 Outline22 腳本");
            }
            /*XRGrabInteractable interactable = args.interactableObject as XRGrabInteractable;
            if (interactable != null)
            {
                interactable.enabled = false;
                Debug.Log($"物件 {args.interactableObject.transform.name} 的抓取功能已被禁用");
            }
            socketInteractor.enabled = false;
            Debug.Log($"插槽 {socketInteractor.name} 的功能已被禁用");*/
        }

    }
}
