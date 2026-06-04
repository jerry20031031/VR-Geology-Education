using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flow : MonoBehaviour
{
   public float floatStrength = 0.5f; // 漂浮高度幅度
    public float floatSpeed = 1.0f;    // 漂浮速度

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * floatSpeed) * floatStrength;
        transform.position = startPos + new Vector3(0, offset, 0);
    }
}
