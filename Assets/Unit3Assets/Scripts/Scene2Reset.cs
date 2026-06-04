using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Reset : MonoBehaviour
{
    public Transform positionToReset; // 重置位置
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision other) {
        // 檢查碰撞的物體是否是玩家
        if (other.gameObject.CompareTag("Player"))
        {
            // 重置場景
            other.gameObject.transform.position = positionToReset.position;
        }
    }
}
