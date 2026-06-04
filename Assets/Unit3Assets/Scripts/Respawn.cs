using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform respawnPoint; // 重生點
   private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            other.transform.position = respawnPoint.position;
            other.transform.rotation = respawnPoint.rotation; 
        }
        else
        {
            Debug.Log("碰撞物體不是玩家，無需重生。");
        }
    }
}
