using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit3Scene2Talking : MonoBehaviour
{
    public GameObject[] NPCs; // NPCs陣列
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetNPCActive(int index)
    {
        for (int i = 0; i < NPCs.Length; i++)
        {
            if (i == index)
            {
                NPCs[i].SetActive(true); // 啟用指定的NPC
            }
            else
            {
                NPCs[i].SetActive(false); // 禁用其他NPC
            }
        }
    }
    public void SetAllNPCsActive(bool isActive)
    {
        foreach (GameObject npc in NPCs)
        {
            npc.SetActive(isActive); // 設定所有NPC的啟用狀態
        }
    }
}
