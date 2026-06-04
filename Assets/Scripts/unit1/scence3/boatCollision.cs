using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatCollision : MonoBehaviour
{
    private GameObject currentTarget; // 儲存當前碰撞的目標物件

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("碰撞到物件：" + other.gameObject.name);
    
        // 檢查碰撞的物件名稱
        if (other.gameObject.name == "target" || other.gameObject.name == "l_target1"|| other.gameObject.name == "l_target2"|| other.gameObject.name == "r_target1"|| other.gameObject.name == "r_target2")
        {
            // 記錄當前選中的目標物件
            currentTarget = other.gameObject;

            Debug.Log("碰撞到目標物件：" + currentTarget.name);

            // 可以選擇改變顏色來表示選中的目標
            Renderer targetRenderer = currentTarget.GetComponent<Renderer>();
            if (targetRenderer != null)
            {
                targetRenderer.material.color = Color.cyan; // 改變顏色 //cyan//green
            }

        }
    }
    void OnTriggerExit(Collider other)
    {
        // 檢查離開的物件是否是當前選中的目標物件
        if (other.gameObject == currentTarget)
        {
            Debug.Log("離開目標物件：" + currentTarget.name);

            // 將顏色變回原來的顏色
            Renderer targetRenderer = currentTarget.GetComponent<Renderer>();
            if (targetRenderer != null)
            {
                targetRenderer.material.color = Color.gray;
            }

            // 清除當前選中的目標物件
            currentTarget = null;
        }
    }
}
