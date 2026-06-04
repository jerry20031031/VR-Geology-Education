using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class unclockUI : MonoBehaviour
{
    public void unclock(int id)
    {
        // 找到指定的子物件

        GameObject knowledge = transform.GetChild(id).gameObject;

        // 修改 Image 的透明度
        UnityEngine.UI.Image imageComponent = knowledge.GetComponent<UnityEngine.UI.Image>();
        if (imageComponent != null)
        {
            Color color = imageComponent.color;
            color.a = 0f; // 設定 alpha 為 0
            imageComponent.color = color;
        }
        else
        {
            Debug.LogWarning("Image component not found on knowledge object.");
        }

        Transform depictionTransform = knowledge.transform.Find("depiction");
        if (depictionTransform != null)
        {
            if (depictionTransform.gameObject.activeSelf == false)
            {
                LogSystemUnit3.LogAction("解鎖知識點" + id);
                LogSystemUnit3.instance.GainedKnowledgePoints++;
                depictionTransform.gameObject.SetActive(true);

            }
        }


    }
    public void knowledgePoint()
    {
        LogSystemUnit3.instance.TotalKnowledgePoints = transform.childCount;
    }
    public void childknowledgePoint(int amount)
    {
        LogSystemUnit3.instance.TotalKnowledgePoints = amount;
    }
}
