using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class scene2ConSim : MonoBehaviour
{
    public GameObject textTMP;
    private XRSimpleInteractable interactable;

    //public GameObject[] Else; // 新增一個陣列來存取
    private void Awake()
    {
        // 獲取 XR Simple Interactable
        interactable = GetComponent<XRSimpleInteractable>();

    }

    private void OnEnable()
    {
        // 訂閱互動事件
        interactable.selectEntered.AddListener(OnSelectEntered);
        interactable.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        // 取消訂閱互動事件
        interactable.selectEntered.RemoveListener(OnSelectEntered);
        interactable.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (textTMP != null)
        {
            textTMP.SetActive(true);
            /*foreach (GameObject obj in Else)
            {
                obj.SetActive(false);
            }*/
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // 隱藏Text (TMP)
        if (textTMP != null)
        {
            textTMP.SetActive(false);
        }
    }
}
