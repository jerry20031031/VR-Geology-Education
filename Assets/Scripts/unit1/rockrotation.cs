using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockrotation : MonoBehaviour
{
    // 每秒旋轉的角度
    public float rotationSpeed = 20f;

    void Update()
    {
        // 計算每幀應該旋轉的角度
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // 執行旋轉
        transform.Rotate(0, rotationAmount, 0);
    }
}
