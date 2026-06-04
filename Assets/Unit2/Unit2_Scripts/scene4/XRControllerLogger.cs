using System;
using System.IO;
using UnityEngine;

public class ActionLogger : MonoBehaviour
{
    private static string logFilePath;
    private static string csvFilePath;

    private void Awake()
    {
        // 設定檔案路徑為專案根目錄
        string projectRoot = Directory.GetParent(Application.dataPath).FullName;
        logFilePath = Path.Combine(projectRoot, "ActionLogs.txt");
        csvFilePath = Path.Combine(projectRoot, "ActionLogs.csv");

        // 確保檔案存在，並在檔案開頭寫入當天日期與時間
        string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string logHeader = $"Log Start: {currentDateTime}\n";

        if (!File.Exists(logFilePath))
        {
            File.WriteAllText(logFilePath, logHeader);
        }
        else
        {
            // 如果檔案已存在，覆蓋第一行為新的日期和時間
            string[] existingLogs = File.ReadAllLines(logFilePath);
            existingLogs[0] = logHeader.TrimEnd();
            File.WriteAllLines(logFilePath, existingLogs);
        }

        // 初始化 CSV 檔案，加入標題列
        if (!File.Exists(csvFilePath))
        {
            string csvHeader = "Timestamp,Action Name,Details\n";
            File.WriteAllText(csvFilePath, csvHeader);
        }
    }

    public static void LogAction(string actionName, string details)
    {
        try
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = $"[{timestamp}] 動作：{actionName}，詳細內容：{details}";

            // 寫入文字檔案
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine(logEntry);
            }

            // 寫入 CSV 檔案
            string csvEntry = $"{timestamp},{actionName},{details}\n";
            using (StreamWriter writer = new StreamWriter(csvFilePath, true))
            {
                writer.Write(csvEntry);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"寫入日誌檔案時發生錯誤：{e.Message}");
        }
    }

    public static void ExportToCSV(string destinationPath)
    {
        try
        {
            if (File.Exists(csvFilePath))
            {
                File.Copy(csvFilePath, destinationPath, true);
                Debug.Log($"CSV 檔案已匯出至：{destinationPath}");
            }
            else
            {
                Debug.LogWarning("CSV 檔案不存在，無法匯出。");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"匯出 CSV 檔案時發生錯誤：{e.Message}");
        }
    }
}


// ActionLogger.LogAction("按鍵觸發", "玩家按下了空白鍵");