using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPoint1 : MonoBehaviour
{
    private bool hasCollided = false;

    // 提供方法供外部檢查碰撞狀態
    public bool HasCollided() => hasCollided;

    private void OnTriggerEnter(Collider other)
    {
        // 使用特定標識腳本來檢測
        if (other.gameObject.GetComponent<SnapToPosition1>() != null)
        {
            hasCollided = true;
            GetComponent<MeshRenderer>().enabled = false; // 隱藏 Sphere
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<SnapToPosition1>() != null)
        {
            hasCollided = false;
            GetComponent<MeshRenderer>().enabled = true; // 顯示 Sphere
        }
    }
}
