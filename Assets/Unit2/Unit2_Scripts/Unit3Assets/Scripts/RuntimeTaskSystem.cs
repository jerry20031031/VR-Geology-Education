using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class RuntimeTaskSystem : MonoBehaviour
{
    public TaskSystemUnit3 taskSystem;
    public UnityEvent onTaskAdded;

    private void Awake()
    {
        if (taskSystem == null)
        {
            taskSystem = GetComponent<TaskSystemUnit3>();
        }
    }

    public void AddTask()
    {
        // 新增任務
        taskSystem.tasks.Add(new TaskUnit3
        {
            taskID = 4,
            taskName = "與NPC對話3"
        });
        taskSystem.tasks = taskSystem.tasks.OrderBy(t => t.taskID).ToList();
        taskSystem.taskGroups = taskSystem.taskGroups.OrderBy(t => t.groupID).ToList();
        onTaskAdded?.Invoke(); // 觸發事件
    }
}