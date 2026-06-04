using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabCount : MonoBehaviour
{
    [SerializeField] private int grabCount = 0; // 在 Inspector 顯示當前抓取次數
    public AudioSource audioSource; // 拖曳你的 AudioSource 進來
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        grabCount++;

        if (grabCount == 1 && audioSource != null)
        {
            audioSource.Play();
        }
    }
}
