using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSystemUnit1 : MonoBehaviour
{
    public void StartTimer() => LogSystem.instance.StartTimer();
    public void StopTimer() => LogSystem.instance.StopTimer();

    public void LogAction(string actionName) => LogSystem.LogAction(actionName);
    public void LogActionWithTime(string actionName) => LogSystem.instance.LogActionAndTime(actionName);

    // --- 互動任務記錄 ---
    public void UpdateTaskRowByLine(int rowIndex) => LogSystem.instance.UpdateTaskRowByLine(rowIndex);
    public void ExportAllToExcel() => LogSystem.instance.ExportAllToExcel();

    // --- 題目記錄 ---
    public void UpdateQuestionRow(int rowIndex, string answer, bool isCorrect) =>
        LogSystem.instance.UpdateQuestionRowByIndex(rowIndex, answer, isCorrect);

    // --- 初始化 ---
    public void InitQuestionRecord() => LogSystem.instance.InitQuestionRecord();

    // --- 加總 ---
    public void AggregateTaskRows(int parentRowIndex) => LogSystem.instance.AggregateTaskRows(parentRowIndex);

    // --- 重設狀態（若需要外部觸發） ---
    public void ResetState() => typeof(LogSystem).GetMethod("Reset", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.Invoke(LogSystem.instance, null);

    // --- 更新完成率 ---
    public void UpdateCompletionRate() => LogSystem.instance.UpdateCompletionRate();

    // --- 個別設定 ---
    public void SetTotalKnowledgePoints(int value)
    {
        LogSystem.instance.TotalKnowledgePoints = value;
    }
    public void SetGainedKnowledgePoints()
    {
        LogSystem.instance.GainedKnowledgePoints++;
    }
    public void SetCompletionRate(float value)
    {
        LogSystem.instance.CompletionRate = value;
    }
    public void SetCompletionTime(string time)
    {
        LogSystem.instance.CompletionTime = time;
    }
    public void SetPredictionResult(bool isCorrect)
    {
        LogSystem.instance.PredictionResult = isCorrect;
    }
    public void SetPredictionResultToNull()
    {
        LogSystem.instance.PredictionResult = null;
    }
    public void SetInteractionFailures(int count)
    {
        LogSystem.instance.InteractionFailures = count;
    }
    public void SetHintCount()
    {
        LogSystem.instance.HintCount++;
    }

    public void PlayEnd3() => LogSystem.instance.playEnd3();
    public void PlayEnd4() => LogSystem.instance.playEnd4();
    public void PlayEnd5() => LogSystem.instance.playEnd5();
}
