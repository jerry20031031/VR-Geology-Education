using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using cherrydev;
using UnityEngine.Events; // 引入 TextMeshPro 的命名空間

[System.Serializable]
public class TaskUnit3
{
    public int taskID; // 任務編號
    public string taskName; // 任務名稱
    public bool requiresObjects; // 是否需要物件判斷
    public List<int> requiredObjectIndices; // 需要的物件索引列表
    public DialogNodeGraph ForceDialog;
    public bool GirlorTecherTalk = true;
    public bool isCompleted = false; // 任務是否已完成
    public bool hasUnityEvent = false; // 是否需要 UnityEvent

    public UnityEvent OnTaskCompleted; // 任務完成時觸發的 UnityEvent

    // 檢查任務是否完成
    public bool CheckCompletion(List<GameObject> availableObjects)
    {
        if (isCompleted) return true; // 任務已完成直接返回

        if (requiresObjects)
        {
            if (requiredObjectIndices == null || requiredObjectIndices.Count == 0)
            {
                Debug.LogWarning($"任務 {taskName} 需要物件判斷，但沒有設置任何物件！");
                return false;
            }

            foreach (var index in requiredObjectIndices)
            {
                if (index < 0 || index >= availableObjects.Count)
                {
                    Debug.LogWarning($"物件索引 {index} 超出範圍！");
                    continue;
                }

                GameObject obj = availableObjects[index];
                if (obj != null && !obj.activeInHierarchy)
                {
                    return false; // 如果有物件不符合條件，任務未完成
                }
            }

            // 所有物件條件符合，標記任務完成
            isCompleted = true;
            Debug.Log($"任務 {taskName} 已完成！");

            // 只有當需要 UnityEvent 時才觸發
            if (hasUnityEvent)
            {
                OnTaskCompleted?.Invoke();
            }

            return true;
        }

        Debug.LogWarning($"任務 {taskName} 不需要物件判斷，需手動完成！");
        return false;
    }
}

[System.Serializable]
public class TaskGroupUnit3
{
    public int groupID; // 任務組 ID
    public UnityEvent OnGroupStart; // 任務組開始時觸發的事件
}

public class TaskSystemUnit3 : MonoBehaviour
{
    [Header("UI 設置")]
    public GameObject taskUIPrefab; // 預置物：用於顯示任務名稱的 UI 元素
    public Transform taskUIParent; // 父節點：放置生成的任務名稱
    public float initialPosY = 0f; // 起始 Y 座標
    public float defaultOffsetY = -90f; // 預設 Y 偏移量
    public float extraOffsetY = -70f; // 超過 6 個字的額外偏移量

    [Header("任務邏輯")]
    public List<GameObject> availableObjects; // 可選的判斷物件列表
    public List<TaskUnit3> tasks; // 任務列表
    public List<TaskGroupUnit3> taskGroups; // 任務組
    public int currentTaskID; // 當前執行任務的編號
    public List<TaskUnit3> CurrentTask;
    public int CurrentTaskCount;
    public int CompleteTaskCount = 0;

    private bool taskNeedsCheck = true; // 控制是否需要檢查當前任務組

    public Talk GirlTalk;
    public Talk TeacherTalk;

    private void Start()
    {
        currentTaskID = 1;

        if (!SetCurrentTaskGroup(currentTaskID))
        {
            Debug.LogError($"初始化失敗：未找到任務編號 {currentTaskID}");
        }
    }

    private void Update()
    {
        Debug.Log($"任務完成數量: {CompleteTaskCount}");
        Debug.Log($"目前須完成任務輛: {CurrentTaskCount}");
        if (taskNeedsCheck)
        {
            if (CurrentTaskCount == 0)
            {
                Debug.LogError($"未找到編號為 {currentTaskID} 的任務！");
                taskNeedsCheck = false;
                return;
            }

            // 使用臨時列表存儲已完成的任務
            List<TaskUnit3> tasksToRemove = new List<TaskUnit3>();

            foreach (var task in CurrentTask)
            {
                if (!task.CheckCompletion(availableObjects))
                {
                    Debug.Log($"任務 {task.taskName} 未完成！");
                }
                else
                {
                    CompleteTaskCount++;
                    Debug.Log($"任務完成數量: {CompleteTaskCount}");
                    ActivateImageForCompletedTask(task.taskName);

                    if (task.ForceDialog != null)
                    {
                        if (task.GirlorTecherTalk)
                        {
                            GirlTalk.ForceinsertTalk(task.ForceDialog);
                        }
                        else
                        {
                            TeacherTalk.ForceinsertTalk(task.ForceDialog);
                        }
                    }

                    tasksToRemove.Add(task); // 添加到已完成列表
                }
            }

            foreach (var task in tasksToRemove)
            {
                CurrentTask.Remove(task); // 從當前任務列表移除
            }

            if (CompleteTaskCount == CurrentTaskCount)
            {
                CompleteTaskCount = 0;
                Debug.Log($"編號 {currentTaskID} 的所有任務已完成！");
                // OnTaskGroupCompleted(); // 觸發任務組的 UnityEvent
                taskNeedsCheck = false;
                GoToNextTask();
            }
        }
    }



    public void ForceCompleteTask(string taskName)
    {
        var task = CurrentTask.FirstOrDefault(t => t.taskName == taskName);
        if (task == null)
        {
            Debug.LogError($"未找到任務名稱 \"{taskName}\"！");
            return;
        }

        if (!task.isCompleted)
        {
            task.isCompleted = true;
            ActivateImageForCompletedTask(taskName);
            Debug.Log($"任務 \"{task.taskName}\" 已強制完成！");

            // 只有當需要 UnityEvent 時才觸發
            if (task.hasUnityEvent)
            {
                task.OnTaskCompleted?.Invoke();
            }
        }
        else
        {
            Debug.LogWarning($"任務 \"{task.taskName}\" 已完成，無需強制完成。");
        }

    }


    public void ActivateImageForCompletedTask(string taskName)
    {
        foreach (Transform child in taskUIParent)
        {
            if (child.name == "matters(Clone)")
            {
                var textComponent = child.Find("Text (TMP)")?.GetComponent<TMP_Text>();
                if (textComponent != null && textComponent.text == taskName)
                {
                    var imageComponent = child.Find("Image").gameObject;
                    if (imageComponent != null)
                    {
                        imageComponent.SetActive(true);
                        Debug.Log($"任務 {taskName} 的 IMAGE 已啟用！");
                    }
                }
            }
        }
    }

    public void GoToNextTask()
    {
        var nextTask = tasks.FirstOrDefault(task => !task.isCompleted);

        if (nextTask != null)
        {
            SetCurrentTaskGroup(nextTask.taskID);
        }
        else
        {
            Debug.Log("所有任務已完成！");
        }
    }
    public void SetCurrentTask(int taskID, bool allowRepeat = false)
    {
        var foundTasks = tasks.Where(task => task.taskID == taskID).ToList();

        if (foundTasks.Count == 0)
        {
            Debug.LogError($"任務編號 {taskID} 不存在！");
            return;
        }

        // 更新當前任務 ID
        currentTaskID = taskID;

        if (allowRepeat)
        {
            // 如果允許重複，將該任務設為未完成狀態
            foreach (var task in foundTasks)
            {
                task.isCompleted = false;
            }
        }

        // 設定當前任務
        CurrentTask = foundTasks;
        CurrentTaskCount = CurrentTask.Count;
        CompleteTaskCount = 0;
        taskNeedsCheck = true;

        // 觸發該任務組的開始事件
        TaskGroupUnit3 group = taskGroups.FirstOrDefault(g => g.groupID == taskID);
        group?.OnGroupStart?.Invoke();

        // 更新 UI
        UpdateTaskUI();

        Debug.Log($"成功切換到任務 {taskID} (允許重複: {allowRepeat})");
    }

    public bool SetCurrentTaskGroup(int taskID)
    {
        if (!tasks.Any(task => task.taskID == taskID))
        {
            Debug.LogError($"任務編號 {taskID} 不存在！");
            return false;
        }

        currentTaskID = taskID;

        // 觸發當前任務組的開始事件
        TaskGroupUnit3 group = taskGroups.FirstOrDefault(g => g.groupID == taskID);
        group?.OnGroupStart?.Invoke();

        CurrentTask = tasks.Where(task => task.taskID == currentTaskID).ToList();
        CurrentTaskCount = CurrentTask.Count;
        taskNeedsCheck = true;
        UpdateTaskUI();

        Debug.Log($"切換到任務編號 {taskID}");
        return true;
    }

    private void UpdateTaskUI()
    {
        foreach (Transform child in taskUIParent)
        {
            Destroy(child.gameObject);
        }

        var currentTasks = tasks.Where(task => task.taskID == currentTaskID).ToList();
        float currentPosY = initialPosY;

        foreach (var task in currentTasks)
        {
            GameObject taskUI = Instantiate(taskUIPrefab, taskUIParent);

            var taskText = taskUI.transform.Find("Text (TMP)").GetComponent<TMP_Text>();
            taskText.text = task.taskName;

            var taskRect = taskUI.GetComponent<RectTransform>();
            taskRect.anchoredPosition = new Vector2(0, currentPosY);

            currentPosY += task.taskName.Length > 6 ? defaultOffsetY + extraOffsetY : defaultOffsetY;
        }
    }
}
