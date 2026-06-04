using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SimpleInteractable : MonoBehaviour
{
    public GameObject textTMP;//
    public GameObject textTMP1;
    public GameObject Image;
    public GameObject sound;
    private XRSimpleInteractable interactable;
    public GameObject plane;
    public GameObject arrow;
    public GameObject[] Else; // 新增一個陣列來存取
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
            Image.SetActive(true);
            textTMP.SetActive(true);
            textTMP1.SetActive(true);
            sound.SetActive(true);
            foreach (GameObject obj in Else)
            {
                obj.SetActive(false);
            }
        }
        if (plane.activeInHierarchy)
        {
            arrow.SetActive(true);
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // 隱藏Text (TMP)
        if (textTMP != null)
        {
            Image.SetActive(false);
            textTMP.SetActive(false);
            textTMP1.SetActive(false);
            sound.SetActive(false);
        }
    }
}
