using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color2 : MonoBehaviour
{
    private Material material;             // 材質引用
    private Color endColor = Color.white;   // 冷卻後的顏色 (灰色)
    private float coolingTime = 300f;        // 冷卻時間 (秒)
    private float timer = 0f;              // 計時器
    private bool isCooling = false;        // 是否正在冷卻


    void Start()
    {
        // 獲取物件的材質
        material = GetComponent<Renderer>().material;
    }

    /*void Update()
    {
        // 計時器遞增，進行顏色過渡
        if (timer < coolingTime)
        {
            timer += Time.deltaTime;

            // 使用 Lerp 漸變到灰色，紅色是材質的初始顏色
            Color newColor = Color.Lerp(material.color, endColor, timer / coolingTime);

            // 更新 Base Map 的顏色
            material.SetColor("_BaseColor", newColor);
        }
    }*/
    void Update()
    {
        if (isCooling)
        {
            Debug.Log($"冷卻！");
            // 計時器遞增，進行顏色過渡
            if (timer < coolingTime)
            {
                timer += Time.deltaTime;

                // 使用 Lerp 漸變到灰色
                Color newColor = Color.Lerp(material.color, endColor, timer / coolingTime);

                // 更新 Base Map 的顏色
                material.SetColor("_BaseColor", newColor);
            }
            else
            {
                isCooling = false; // 冷卻完成
            }
        }
    }

    // 外部啟動冷卻
    public void StartCooling()
    {
        isCooling = true;
        timer = 0f; // 重置計時器
    }
}