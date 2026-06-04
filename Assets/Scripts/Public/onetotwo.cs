using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onetotwo : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;

    private float checkInterval = 1f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= checkInterval)
        {
            timer = 0f;

            if (!object1.activeSelf && !object2.activeSelf)
            {
                object2.SetActive(true);
            }
        }
    }
}
