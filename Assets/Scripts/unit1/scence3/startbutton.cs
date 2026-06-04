using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startbutton : MonoBehaviour
{
    public Transform sediment; // 沉積物的 Transform
    public float riseSpeed = 2.0f; // 上升速度
    public float targetHeight = 5.0f; // 目標高度
    public GameObject showUI; // UI 物件
    public GameObject closeUI; // UI 物件

    private bool isRising = false; // 是否正在上升

    // 當 Select 事件觸發時，啟動上升
    public void StartRising()
    {
        if (sediment != null)
        {
            isRising = true;
        }
    }

    private void Update()
    {
        if (isRising && sediment != null)
        {
            // 上升處理
            sediment.position = Vector3.MoveTowards(
                sediment.position, 
                new Vector3(sediment.position.x, targetHeight, sediment.position.z),
                riseSpeed * Time.deltaTime
            );

            // 當到達目標高度時停止上升並顯示 UI
            if (Mathf.Abs(sediment.position.y - targetHeight) < 0.01f)
            {
                isRising = false;

                if (showUI != null)
                {
                    closeUI.SetActive(false);
                    showUI.SetActive(true);
                }
            }
        }
    }
}
