using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtNpc : MonoBehaviour
{
    public Transform player; // 玩家 Transform
    public float rotationSpeed = 2.0f; // 旋轉速度
    public bool Talking = false;

    void Update()
    {
        if (Talking)
        {
            if (player == null) return;

            // 計算方向
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0; // 確保只在水平面旋轉

            // 計算目標旋轉
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // 平滑旋轉
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
    public void ChangeTalkStatu()
    {
        Talking = !Talking;
    }
}
