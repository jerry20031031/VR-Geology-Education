using UnityEngine;

public class TeleportController : MonoBehaviour
{
    // 要被傳送的物件
    public GameObject objectToTeleport;

    // 目標位置
    public Transform targetPosition;

    // 按下 Button 時直接瞬移
    public void Teleport()
    {
        if (objectToTeleport != null && targetPosition != null)
        {
            objectToTeleport.transform.position = targetPosition.position;
        }
    }
}
