using System;
using System.IO;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLogSystem", menuName = "Systems/Log System")]
public class LogSystemScriptable : ScriptableObject
{
    private string persistentPath;
    private string logFilePath;
    private string csvFilePath;
    public ClassAndNumber classAndNumber; // 這個類別需要在 Unity 編輯器中設置

    // 在 ScriptableObject 被啟用時初始化路徑
    private void OnEnable()
    {
        persistentPath = Directory.GetParent(Application.dataPath).FullName; ;
        csvFilePath = Path.Combine(persistentPath, classAndNumber.Class + classAndNumber.Number + ".csv");
    }

    public  void LogAction(string actionName)
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
}
