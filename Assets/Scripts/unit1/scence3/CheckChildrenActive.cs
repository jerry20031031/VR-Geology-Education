using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckChildrenActive : MonoBehaviour
{
    void OnEnable()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                switch (child.name)
                {
                    case "Yes":
                        LogSystem.LogAction("學習者預測正確(沉積物厚度與海洋地殼年齡)");
                        LogSystem.LogAction("知識點解鎖(沉積物厚度與海洋地殼年齡分布)");
                        LogSystem.instance.TotalKnowledgePoints = 1;
                        LogSystem.instance.GainedKnowledgePoints++;
                        LogSystem.instance.UpdateTaskRowByLine(4);
                        Debug.Log("學習者預測海洋地殼年齡和沉積物厚度的結果正確");
                        break;
                    case "No":
                        LogSystem.LogAction("學習者預測錯誤(沉積物厚度與海洋地殼年齡)");
                        LogSystem.LogAction("知識點解鎖(沉積物厚度與海洋地殼年齡分布)");
                        LogSystem.instance.TotalKnowledgePoints = 1;
                        LogSystem.instance.GainedKnowledgePoints++;
                        LogSystem.instance.UpdateTaskRowByLine(4);
                        Debug.Log("學習者預測海洋地殼年齡和沉積物厚度的結果錯誤");
                        break;
                    case "No (1)":
                        LogSystem.LogAction("學習者預測錯誤(沉積物厚度與海洋地殼年齡)");
                        LogSystem.LogAction("知識點解鎖(沉積物厚度與海洋地殼年齡分布)");
                        LogSystem.instance.TotalKnowledgePoints = 1;
                        LogSystem.instance.GainedKnowledgePoints++;
                        LogSystem.instance.UpdateTaskRowByLine(4);
                        Debug.Log("學習者預測海洋地殼年齡和沉積物厚度的結果錯誤");
                        break;
                    default:
                        //LogSystem.LogAction("錯誤"); 
                        break;
                }

            }
        }
    }
}
