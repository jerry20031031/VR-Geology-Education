using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport1 : MonoBehaviour
{
    public void TeleportToTarget(Transform target)
    {
        if (target != null)
        {
            transform.position = target.position;
            Debug.Log("¶¶²¾¨ì¥Ø¼Ð: " + target.name);
        }
    }
}
