using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class testtttt222 : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        // 記錄初始位置和旋轉
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    // 提供讓物件回到原始位置
    public void ResetPosition()
    {
        Debug.Log("錯誤的回到初始位置");
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}
