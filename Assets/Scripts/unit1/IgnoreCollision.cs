using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    public Collider object1; // 指定第一個物體的 Collider
    public Collider object2; // 指定第二個物體的 Collider
    public Collider object3; // 指定第三個物體的 Collider
    public Collider object4; // 指定第四個物體的 Collider
    public Collider object5; // 指定第五個物體的 Collider
    public Collider object6; // 指定第六個物體的 Collider

    void Start()
    {
        Collider[] colliders = { object1, object2, object3, object4, object5, object6 };

        // 確保每對物體之間都忽略碰撞
        for (int i = 0; i < colliders.Length; i++)
        {
            for (int j = i + 1; j < colliders.Length; j++)
            {
                if (colliders[i] != null && colliders[j] != null)
                {
                    Physics.IgnoreCollision(colliders[i], colliders[j]);
                }
            }
        }
    }
}
