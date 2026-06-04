using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trans : MonoBehaviour
{
    public Vector3 targetPosition = new Vector3(5, 2, 3);
    public Vector3 targetRotationEuler = new Vector3(0, 90, 0); // 使用 Euler 角度表示旋轉

    public void MoveObject()
    {
        transform.position = targetPosition;
        transform.rotation = Quaternion.Euler(targetRotationEuler);
    }
}
