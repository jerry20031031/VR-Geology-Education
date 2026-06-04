using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class InteractUnit2 : MonoBehaviour
{
    public static bool? PredeitResult = null; // 使用 nullable bool 來確認是否有設過
    private bool hasLogged = false; // 避免重複記錄

    // 儲存預測結果的方法，可由其他程式呼叫設定
    public void SetResult(bool result)
    {
        PredeitResult = result;
        hasLogged = false; // 重設 log 狀態（如果你想要能多次記錄）
    }

    // 由外部在適當時機呼叫，才會執行 LogTask
    public void LogResultIfReady()
    {
        if (PredeitResult.HasValue && !hasLogged)
        {
            string correctness = PredeitResult.Value ? "正確" : "錯誤";
            SessionLogger.LogTask("完成板塊模型", "100%", TimeSpan.FromSeconds(3), "-", correctness, 0, 0);
            hasLogged = true;
        }
        else if (!PredeitResult.HasValue)
        {
            SessionLogger.LogTask("完成板塊模型", "0%", TimeSpan.FromSeconds(3), "-", "未預測", 0, 0);
        }
    }
}
