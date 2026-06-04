using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    public float resetThresholdY = -5f;  // 當物件低於這個 Y 值時重置
    private Vector3 initialPosition;    // 記錄物件初始位置
    private Quaternion initialRotation;

    void Start()
    {
        // 記錄初始位置
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        //Debug.Log("transform.position.y: " + transform.position.y);
        if (transform.position.y < resetThresholdY)
        {
            ResetObjectPosition();
        }
    }
    void ResetObjectPosition()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}
