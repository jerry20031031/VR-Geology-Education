using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.InputSystem;

public class XRControllerLogger : MonoBehaviour
{
    public InputActionProperty leftJoystickMove;  // 左手搖桿
    public InputActionProperty rightJoystickMove; // 右手搖桿
    public string logFilePath;

    private Vector2 lastLeftJoystickValue = Vector2.zero;
    private Vector2 lastRightJoystickValue = Vector2.zero;

    private void Start()
    {
        if (!leftJoystickMove.action.enabled)
        {
            leftJoystickMove.action.Enable();
        }

        if (!rightJoystickMove.action.enabled)
        {
            rightJoystickMove.action.Enable();
        }

        // 設定檔案路徑為專案根目錄
        string projectRoot = Directory.GetParent(Application.dataPath).FullName;
        logFilePath = Path.Combine(projectRoot, "JoystickLogs.txt");
        Debug.Log($"Log file path: {logFilePath}");

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
    }

    private void Update()
    {
        // 監控左手搖桿移動
        if (leftJoystickMove.action != null)
        {
            Vector2 leftJoystickValue = leftJoystickMove.action.ReadValue<Vector2>();

            if (leftJoystickValue != lastLeftJoystickValue)
            {
                string direction = GetDirectionDescription(leftJoystickValue);
                LogAction("左手移動", direction);
                lastLeftJoystickValue = leftJoystickValue;
            }
        }

        // 監控右手搖桿移動
        if (rightJoystickMove.action != null)
        {
            Vector2 rightJoystickValue = rightJoystickMove.action.ReadValue<Vector2>();

            if (rightJoystickValue != lastRightJoystickValue)
            {
                string direction = GetDirectionDescription(rightJoystickValue);
                LogAction("右手移動", direction);
                lastRightJoystickValue = rightJoystickValue;
            }
        }

        // 監控滑鼠左鍵
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            LogAction("左手滑鼠左鍵", "按下");
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            LogAction("左手滑鼠左鍵", "釋放");
        }

        // 監控滑鼠右鍵
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            LogAction("右手滑鼠右鍵", "按下");
        }
        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            LogAction("右手滑鼠右鍵", "釋放");
        }

        // 監控鍵盤按鍵 N、M、B、G
        CheckKeyPress(Key.N, "N");
        CheckKeyPress(Key.M, "M");
        CheckKeyPress(Key.B, "B");
        CheckKeyPress(Key.G, "G");
    }

    private void CheckKeyPress(Key key, string keyName)
    {
        if (Keyboard.current[key].wasPressedThisFrame)
        {
            LogAction($"按鍵 {keyName}", "按下");
        }
        if (Keyboard.current[key].wasReleasedThisFrame)
        {
            LogAction($"按鍵 {keyName}", "釋放");
        }
    }

    private string GetDirectionDescription(Vector2 joystickValue)
    {
        // 判斷方向
        if (joystickValue == Vector2.zero)
            return "停下";

        if (joystickValue.y > 0)
            return "往前";
        if (joystickValue.y < 0)
            return "往後";
        if (joystickValue.x > 0)
            return "往右";
        if (joystickValue.x < 0)
            return "往左";

        // 斜方向的情況
        if (joystickValue.x > 0 && joystickValue.y > 0)
            return "右上";
        if (joystickValue.x > 0 && joystickValue.y < 0)
            return "右下";
        if (joystickValue.x < 0 && joystickValue.y > 0)
            return "左上";
        if (joystickValue.x < 0 && joystickValue.y < 0)
            return "左下";

        return "未知方向";
    }

    private void LogAction(string actionName, string details)
    {
        try
        {
            string logEntry = $"在 {Time.time:F2} 秒後，執行了動作：{actionName}，詳細內容：{details}";

            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine(logEntry);
            }

           
        }
        catch (System.Exception e)
        {
            Debug.LogError($"寫入日誌檔案時發生錯誤：{e.Message}");
        }
    }
}
