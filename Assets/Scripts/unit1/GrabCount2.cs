using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabCount2 : MonoBehaviour
{
    public AudioSource audioSource; // 音效播放器
    [SerializeField] private int clickCount = 0; // 計數器 (Inspector 可見)
    private void Start()
    {
        // 確保音效來源已分配
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // 訂閱 XR Simple Interactable 事件
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnSelectEntered);
        }
        else
        {
            Debug.LogError("XR Simple Interactable 未找到，請確保此腳本掛在具有 XR Simple Interactable 的物體上！");
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        clickCount++;

        if (clickCount == 1 && audioSource != null)
        {
            audioSource.Play();
        }
    }
}
