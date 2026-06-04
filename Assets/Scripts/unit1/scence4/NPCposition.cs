using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NPCposition : MonoBehaviour
{
    public Transform npc; // NPC 角色
    public Vector3 targetPosition; // 目標位置
    public Vector3 targetScale = Vector3.one; // 目標縮放大小（預設為 1,1,1）
    public Quaternion targetRotation = Quaternion.identity; // 目標旋轉角度（預設為 0,0,0）


    void Start()
    {
        MoveNPC();
        ScaleNPC(targetScale);
        RotateNPC(targetRotation);
    }

    // 直接瞬移 NPC
    public void MoveNPC()
    {
        if (npc != null)
        {
            npc.position = targetPosition;
            Debug.Log($"NPC 已瞬移到 {targetPosition}");
        }
        else
        {
            Debug.LogError("NPC 尚未設定！");
        }
    }

    // 調整 NPC 的大小
    public void ScaleNPC(Vector3 newScale)
    {
        if (npc != null)
        {
            npc.localScale = newScale;
            Debug.Log($"NPC 縮放為 {newScale}");
        }
        else
        {
            Debug.LogError("NPC 尚未設定！");
        }
    }

    // 旋轉 NPC
    public void RotateNPC(Quaternion newRotation)
    {
        if (npc != null)
        {
            npc.rotation = newRotation;
            Debug.Log($"NPC 旋轉為 {newRotation.eulerAngles}");
        }
        else
        {
            Debug.LogError("NPC 尚未設定！");
        }
    }
}
