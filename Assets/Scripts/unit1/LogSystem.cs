using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class LogSystem : MonoBehaviour
{
    static private float elapsedTime = 0f;
    private bool isRunning = false;
    public static LogSystem instance;
    private static string csvFilePath;
    private static string persistentPath;

    public ClassAndNumber classAndNumber;
    public int TotalKnowledgePoints; // 總共知識點數
    public int GainedKnowledgePoints; // 已解所知識點數

    public float CompletionRate; // 完成率 (百分比)
    public string CompletionTime; // 完成時間
    public bool? PredictionResult; // 預測答案: 正確(true)/錯誤(false)
    public int InteractionFailures; // 互動失敗次數
    public int HintCount; // 提示次數

    private void Awake()
    {
        persistentPath = Application.persistentDataPath;

        csvFilePath = Path.Combine(persistentPath, classAndNumber.Class + classAndNumber.Number + ".csv");

        Debug.Log($"CSV files initialized at: {csvFilePath}");

        string currentDate = DateTime.Now.ToString("yyyy/MM/dd");
        string currentTime = DateTime.Now.ToString("HH:mm:ss");

        string logHeader = $"使用者ID：{classAndNumber.Class}{classAndNumber.Number}\n記錄開始日期：{currentDate}\n記錄開始時間：{currentTime}\n";
        string csvHeader = $"使用者ID,{classAndNumber.Class}{classAndNumber.Number}\n開始日期,{currentDate}\n開始時間,{currentTime}\nUSER,Timestamp,Action Name\n";


        InitializeFile(csvFilePath, csvHeader);
        InitInteractionRecord();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject); // 如果已經有實例，銷毀當前物件
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        instance = this;
        instance.Reset();
        // 使用 Application.persistentDataPath 作為基準目錄
        // string persistentPath = Application.persistentDataPath;
        // Directory.GetParent(Application.dataPath).FullName;
        //persistentPath = Directory.GetParent(Application.dataPath).FullName; //this is the path of the project
        //Application.persistentDataPat

    }
    void Update()
    {

        if (isRunning)
        {
            elapsedTime += Time.deltaTime; // 計算經過的時間
            Debug.Log($"Elapsed Time: {FormatTime(elapsedTime)}"); // 格式化時間顯示
        }
    }
    public void StartTimer()
    {
        isRunning = true;
        elapsedTime = 0f; // 重新開始
    }

    public void StopTimer()
    {
        isRunning = false;
    }


    public static void LogAction(string actionName)
    {
        string persistentPath = Application.persistentDataPath;
        try
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            string logEntry = $"[{timestamp}] 動作：{actionName}";

            string csvEntry = $"user1,{timestamp},{actionName}\n";
            using (StreamWriter writer = new StreamWriter(csvFilePath, true, new UTF8Encoding(false)))
            {
                writer.Write(csvEntry);
            }

            Debug.Log($"Action logged: {actionName}");
        }
        catch (Exception e)
        {
            Debug.LogError($"寫入日誌檔案時發生錯誤：{e.Message}");
        }
    }
    public void LogActionAndTime(string actionName)
    {
        StopTimer();
        string persistentPath = Application.persistentDataPath;
        try
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            string logEntry = $"[{timestamp}] 動作：{actionName}";

            string csvEntry = $"user1,{timestamp},{actionName + FormatTime(elapsedTime)}\n";
            using (StreamWriter writer = new StreamWriter(csvFilePath, true, new UTF8Encoding(false)))
            {
                writer.Write(csvEntry);
            }

            Debug.Log($"Action logged: {actionName}");
        }
        catch (Exception e)
        {
            Debug.LogError($"寫入日誌檔案時發生錯誤：{e.Message}");
        }
    }

    private void InitializeFile(string filePath, string content)
    {
        File.WriteAllText(filePath, content, new UTF8Encoding(true));
        LogAction("進入大陸漂移與海底擴張單元");
    }


    private void InitInteractionRecord()
    {
        string csvPath = Path.Combine(persistentPath, "互動任務紀錄.csv");

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("任務項目,完成度,完成時間,知識點解鎖,預測內容,動作失敗次數,提示次數");

        string[] tasks = new string[]
        {
            "任務一:完成盤古大陸",
            "任務二:選擇漂移路徑",
            "任務三:探查海底模型",
            "任務四:沉積物和海洋地殼分布判讀",
            "任務五:完成海底磁場表"

        };

        foreach (var task in tasks)
        {
            sb.AppendLine($"{task},,,,,,");
        }

        File.WriteAllText(csvPath, sb.ToString(), new UTF8Encoding(true));
        Debug.Log($"已輸出到 {csvPath}");
    }

    public void UpdateTaskRowByLine(int lineIndex)
    {
        string csvPath = Path.Combine(persistentPath, "互動任務紀錄.csv");

        List<string> lines = new List<string>(File.ReadAllLines(csvPath, new UTF8Encoding(true)));

        // 檢查範圍（lineIndex: 1 是第一筆任務資料，0 是表頭）
        if (lineIndex < 1 || lineIndex >= lines.Count)
        {
            Debug.LogWarning($"指定的行數 {lineIndex} 超出範圍，總行數為 {lines.Count}");
            return;
        }

        // 擷取任務名稱（不變）
        string[] fields = lines[lineIndex].Split(',');
        string taskName = fields[0];

        // 預測結果
        string prediction = PredictionResult.HasValue ? (PredictionResult.Value ? "正確" : "錯誤") : "-";

        // 組合新的資料列
        UpdateCompletionRate();
        CompletionTime = FormatTime(elapsedTime); // 使用當前時間作為完成時間
        string completionRateStr = $"{CompletionRate:F0}%";
        string knowledgePointStr = $"'{GainedKnowledgePoints}/{TotalKnowledgePoints}";
        string interactionFailuresStr = $"{InteractionFailures}次";
        string hintCountStr = HintCount == -1 ? "-" : $"{HintCount}次";

        // 格式需與表頭一致
        lines[lineIndex] = $"{taskName},{completionRateStr},{CompletionTime},{knowledgePointStr},{prediction},{interactionFailuresStr},{hintCountStr}";

        File.WriteAllLines(csvPath, lines, new UTF8Encoding(true));
        Debug.Log($"✅ 成功更新第 {lineIndex} 行（{taskName}）");
        TotalKnowledgePoints = 0; // 總共知識點數
        GainedKnowledgePoints = 0; ; // 已解所知識點數
        CompletionRate = 0; // 完成率 (百分比)
        PredictionResult = null; // 預測答案: 正確(true)/錯誤(false)
        InteractionFailures = 0; // 互動失敗次數
        HintCount = 0;
    }

    public void UpdateCompletionRate()
    {
        if (TotalKnowledgePoints > 0)
        {
            CompletionRate = (float)GainedKnowledgePoints / TotalKnowledgePoints * 100f;
        }
        else
        {
            CompletionRate = 0f;
        }
    }
    // ----------------- 題目紀錄 CSV 初始化 -----------------
    public void InitQuestionRecord()
    {
        string questionCsvPath = Path.Combine(persistentPath, "題目作答紀錄.csv");

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("題號,題目,回答內容,正確答案,是否正確");

        // 題目 + 正確答案
        string[,] questions = new string[,]
        {
        { "Q1", "哪位科學家提出盤古大陸?", "韋格納" },
        { "Q2", "韋格納在提出「大陸漂移說」時，曾以哪些主要證據支持各大陸曾經連在一起的理論？", "化石分布、相似的海岸線、相對應的山脈構造。" },
        { "Q3", "海底擴張學說中認為海底岩漿沿著海底的哪個位置上升?", "中洋脊" },
        { "Q4", "中洋脊噴發岩漿中的礦物，礦物冷卻後磁化並記錄當時地球磁場的海洋地殼往兩旁擴張，這個現象最後會記錄地球磁場的甚麼變化?", "地磁反轉" },
        { "Q5", "海底擴張岩漿沿著海底的中洋脊上升，中洋脊岩漿冷卻後會形成新的甚麼地質?", "海洋地殼" }
        };

        for (int i = 0; i < questions.GetLength(0); i++)
        {
            string qNumber = questions[i, 0];
            string qTitle = questions[i, 1];
            string correctAnswer = questions[i, 2];

            sb.AppendLine($"{qNumber},{qTitle},,{correctAnswer},");
        }

        File.WriteAllText(questionCsvPath, sb.ToString(), new UTF8Encoding(true));
        Debug.Log($"題目作答紀錄表(含正確答案) 已初始化到 {questionCsvPath}");
    }
    // ----------------- 題目紀錄 CSV 寫入 -----------------
    public void UpdateQuestionRowByIndex(int rowIndex, string answer, bool isCorrect)
    {
        string qNumber = "Q" + rowIndex.ToString(); // 題號格式化為 Q1, Q2, ... Qn
        string questionCsvPath = Path.Combine(persistentPath, "題目作答紀錄.csv");

        // 讀取 CSV 所有行
        List<string> lines = new List<string>(File.ReadAllLines(questionCsvPath, new UTF8Encoding(true)));
        rowIndex = rowIndex + 1;
        if (rowIndex < 1 || rowIndex >= lines.Count)
        {
            Debug.LogWarning($"指定的列號 {rowIndex} 無效（應介於 1 和 {lines.Count - 1} 之間）");
            return;
        }

        string[] columns = lines[rowIndex].Split(',');

        if (columns.Length < 5)
        {
            Debug.LogWarning($"第 {rowIndex} 行格式錯誤，無法更新");
            return;
        }

        // 取得現有資料
        string question = columns[1];
        string correctAnswer = columns[3];
        string correctText = isCorrect ? "正確" : "錯誤";

        // 建立更新後的行
        lines[rowIndex] = $"{qNumber},{question},{answer},{correctAnswer},{correctText}";

        // 寫回 CSV
        File.WriteAllLines(questionCsvPath, lines, new UTF8Encoding(true));
        Debug.Log($"第 {rowIndex} 列（題號 {qNumber}）已更新");
    }

    public void ExportAllToExcel()
    {

        string mergedPath = Path.Combine(persistentPath, "單元一總報告.csv");
        string interactionCSV = Path.Combine(persistentPath, "互動任務紀錄.csv");
        string questionCSV = Path.Combine(persistentPath, "題目作答紀錄.csv");
        using (StreamWriter writer = new StreamWriter(mergedPath, true, new UTF8Encoding(true)))
        {
            // 合併 1：行為紀錄
            if (File.Exists(csvFilePath))
            {
                writer.WriteLine("=======分隔線===========");
                writer.WriteLine("===== 行為紀錄 =====");
                string[] lines = File.ReadAllLines(csvFilePath, new UTF8Encoding(true));
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }
                writer.WriteLine();
            }

            // 合併 2：互動任務紀錄

            if (File.Exists(interactionCSV))
            {
                writer.WriteLine("===== 互動任務紀錄 =====");
                string[] lines = File.ReadAllLines(interactionCSV, new UTF8Encoding(true));
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }
                writer.WriteLine();
            }

            // 合併 3：題目作答紀錄

            if (File.Exists(questionCSV))
            {
                writer.WriteLine("===== 題目作答紀錄 =====");
                string[] lines = File.ReadAllLines(questionCSV, new UTF8Encoding(true));
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }
                writer.WriteLine();
            }

        }

        Debug.Log($"✅ 成功合併成：{mergedPath}");
        // 🧹 刪除原始 CSV 檔案
        try
        {
            if (File.Exists(csvFilePath)) File.Delete(csvFilePath);
            if (File.Exists(interactionCSV)) File.Delete(interactionCSV);
            if (File.Exists(questionCSV)) File.Delete(questionCSV);

            Debug.Log("🗑️ 成功刪除原始記錄檔案");
        }
        catch (Exception e)
        {
            Debug.LogError("❌ 刪除原始 CSV 檔案失敗：" + e.Message);
        }
    }
    /// <summary>
    /// 通用 CSV to Sheet
    /// </summary>

    private void Reset()
    {
        TotalKnowledgePoints = 0; // 總共知識點數
        GainedKnowledgePoints = 0; ; // 已解所知識點數
        CompletionRate = 0; // 完成率 (百分比)
        CompletionTime = ""; // 完成時間
        PredictionResult = null; // 預測答案: 正確(true)/錯誤(false)
        InteractionFailures = 0; // 互動失敗次數
        HintCount = 0;

    }
    public void AggregateTaskRows(int parentRowIndex)
    {
        List<string> lines = new List<string>(File.ReadAllLines(csvFilePath, new UTF8Encoding(true)));

        if (parentRowIndex < 1 || parentRowIndex >= lines.Count)
        {
            Debug.LogWarning("指定行號不在範圍內");
            return;
        }

        int totalSeconds = 0;
        int totalGotPoints = 0;
        int totalPoints = 0;
        int totalFailures = 0;

        string parentLine = lines[parentRowIndex];
        string[] parentFields = parentLine.Split(',');

        int currentIndex = parentRowIndex + 1;
        while (currentIndex < lines.Count && lines[currentIndex].StartsWith("└"))
        {
            string[] fields = lines[currentIndex].Split(',');

            // === 加總時間 ===
            if (TryParseTime_MMSS(fields[2], out int seconds))
            {
                totalSeconds += seconds;
            }

            // === 加總知識點 ===
            string[] kp = fields[3].Split('/');
            if (kp.Length == 2 && int.TryParse(kp[0], out int got) && int.TryParse(kp[1], out int total))
            {
                totalGotPoints += got;
                totalPoints += total;
            }

            // === 加總互動失敗次數 ===
            if (fields[5].EndsWith("次") && int.TryParse(fields[5].Replace("次", ""), out int fails))
            {
                totalFailures += fails;
            }

            currentIndex++;
        }

        // ✅ 使用 MM:SS 格式時間
        string newTime = FormatTime(totalSeconds);
        string newKP = $"{totalGotPoints}/{totalPoints}";
        string newFails = $"{totalFailures}次";
        string completionRate = totalPoints > 0 ? $"{(int)((float)totalGotPoints / totalPoints * 100)}%" : "0%";
        parentFields[1] = completionRate; // 第 1 欄是完成度
        parentFields[2] = newTime;
        parentFields[3] = newKP;
        parentFields[4] = "-";
        parentFields[5] = newFails;
        parentFields[6] = "-";

        lines[parentRowIndex] = string.Join(",", parentFields);
        File.WriteAllLines(csvFilePath, lines, new UTF8Encoding(true));

        Debug.Log($"✅ 已加總更新第 {parentRowIndex} 行");
    }

    // 工具：解析 MM:SS 格式時間成總秒數
    private bool TryParseTime_MMSS(string timeStr, out int totalSeconds)
    {
        totalSeconds = 0;
        try
        {
            if (string.IsNullOrWhiteSpace(timeStr)) return false;

            var parts = timeStr.Split(':');
            if (parts.Length == 2)
            {
                int minutes = int.Parse(parts[0]);
                int seconds = int.Parse(parts[1]);
                totalSeconds = minutes * 60 + seconds;
                return true;
            }
        }
        catch
        {
            // 忽略格式錯誤
        }
        return false;
    }
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void playEnd1()
    {
        LogSystem.LogAction("學習者結束任務(盤古大陸拼圖拼湊完成時間" + CompletionTime + ")");
    }
    public void playEnd22()
    {
        LogSystem.LogAction("學習者結束任務(找到大陸漂移過程完成時間" + CompletionTime + ")");
    }
    public void playEnd2()
    {
        StopTimer();
        CompletionTime = FormatTime(elapsedTime);
        LogSystem.LogAction("探查(學習者探查各大陸模型完成時間" + CompletionTime + ")");
    }
    public void playEnd3()
    {
        LogSystem.LogAction("學習者結束任務(互動三過程:" + CompletionTime + ")");
    }
    public void playEnd4()
    {
        CompletionTime = FormatTime(elapsedTime);
        LogSystem.LogAction("學習者結束任務(互動四過程:" + CompletionTime + ")");
    }
    public void playEnd5()
    {
        CompletionTime = FormatTime(elapsedTime);
        LogSystem.LogAction("學習者結束任務(互動五過程:" + CompletionTime + ")");
    }
    public void wagana1()
    {

    }
}