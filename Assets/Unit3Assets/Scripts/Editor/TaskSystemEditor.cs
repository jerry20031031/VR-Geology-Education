using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(TaskSystemUnit3))]
public class TaskSystemEditor : Editor
{
    private int inputTaskID = 1;
    private Dictionary<int, bool> foldoutStates = new Dictionary<int, bool>(); // 儲存任務編號的折疊狀態
    private List<bool> foldouts = new List<bool>();

    public override void OnInspectorGUI()
    {
        // 獲取目標腳本
        TaskSystemUnit3 taskSystem = (TaskSystemUnit3)target;

        serializedObject.Update();

        // 顯示 UI 設置
        EditorGUILayout.LabelField("UI 設置", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taskUIPrefab"), new GUIContent("任務 UI 預置物"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taskUIParent"), new GUIContent("任務 UI 父節點"));

        // 顯示所有可用的判斷物件
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("所有可以選擇的判斷物件", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("availableObjects"), true);

        // 顯示當前執行任務編號
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("目前執行的任務", EditorStyles.boldLabel);
        EditorGUILayout.LabelField($"執行任務編號: {taskSystem.currentTaskID}", EditorStyles.helpBox);

        // 顯示任務組
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("任務組", EditorStyles.boldLabel);
        SerializedProperty taskGroupsProp = serializedObject.FindProperty("taskGroups");

        while (foldouts.Count < taskGroupsProp.arraySize)
        {
            foldouts.Add(true);
        }
        for (int i = 0; i < taskGroupsProp.arraySize; i++)
        {
            SerializedProperty groupProp = taskGroupsProp.GetArrayElementAtIndex(i);
            SerializedProperty groupIDProp = groupProp.FindPropertyRelative("groupID");
            SerializedProperty onGroupStartProp = groupProp.FindPropertyRelative("OnGroupStart");

            EditorGUILayout.BeginVertical("box");

            // 创建可展开的 Foldout UI
            foldouts[i] = EditorGUILayout.Foldout(foldouts[i], $"任務組 ID: {groupIDProp.intValue}", true);

            if (foldouts[i])
            {
                EditorGUILayout.PropertyField(groupIDProp, new GUIContent("任務組 ID"));
                EditorGUILayout.PropertyField(onGroupStartProp, new GUIContent("任務組開始事件 (OnGroupStart)"));

                if (GUILayout.Button("刪除任務組"))
                {
                    taskGroupsProp.DeleteArrayElementAtIndex(i);
                    foldouts.RemoveAt(i); // 移除对应的展开状态
                    break;
                }
            }

            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("新增任務組"))
        {
            int newGroupID = taskSystem.taskGroups.Count > 0 ? taskSystem.taskGroups.Max(g => g.groupID) + 1 : 1;
            taskSystem.taskGroups.Add(new TaskGroupUnit3
            {
                groupID = newGroupID
            });
        }

        // 顯示手動生成的任務 (分組顯示)
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("手動生成的任務", EditorStyles.boldLabel);
        SerializedProperty tasksProp = serializedObject.FindProperty("tasks");

        Dictionary<int, List<int>> taskGroups = new Dictionary<int, List<int>>();
        for (int i = 0; i < tasksProp.arraySize; i++)
        {
            SerializedProperty taskProp = tasksProp.GetArrayElementAtIndex(i);
            int tID = taskProp.FindPropertyRelative("taskID").intValue;

            if (!taskGroups.ContainsKey(tID))
            {
                taskGroups[tID] = new List<int>();
            }

            taskGroups[tID].Add(i);
        }

        foreach (var kvp in taskGroups)
        {
            if (!foldoutStates.ContainsKey(kvp.Key))
            {
                foldoutStates[kvp.Key] = false;
            }

            foldoutStates[kvp.Key] = EditorGUILayout.Foldout(foldoutStates[kvp.Key], $"任務組: {kvp.Key}", true);

            if (foldoutStates[kvp.Key])
            {
                EditorGUILayout.BeginVertical("box");

                foreach (int index in kvp.Value)
                {
                    SerializedProperty task = tasksProp.GetArrayElementAtIndex(index);
                    SerializedProperty taskID = task.FindPropertyRelative("taskID");
                    SerializedProperty taskName = task.FindPropertyRelative("taskName");
                    SerializedProperty requiresObjects = task.FindPropertyRelative("requiresObjects");
                    SerializedProperty requiredObjectIndices = task.FindPropertyRelative("requiredObjectIndices");
                    SerializedProperty forceDialog = task.FindPropertyRelative("ForceDialog");
                    SerializedProperty girlOrTecherTalk = task.FindPropertyRelative("GirlorTecherTalk");
                    SerializedProperty hasUnityEvent = task.FindPropertyRelative("hasUnityEvent");
                    SerializedProperty onTaskCompleted = task.FindPropertyRelative("OnTaskCompleted");

                    EditorGUILayout.PropertyField(taskID, new GUIContent("任務編號"));
                    EditorGUILayout.PropertyField(taskName, new GUIContent("任務名稱"));
                    EditorGUILayout.PropertyField(requiresObjects, new GUIContent("需要物件"));

                    if (requiresObjects.boolValue)
                    {
                        EditorGUILayout.PropertyField(requiredObjectIndices, new GUIContent("需要的物件索引列表"), true);
                    }

                    EditorGUILayout.PropertyField(forceDialog, new GUIContent("強制對話 (ForceDialog)"));
                    EditorGUILayout.PropertyField(girlOrTecherTalk, new GUIContent("對話者 (GirlorTecherTalk)"));

                    EditorGUILayout.PropertyField(hasUnityEvent, new GUIContent("需要觸發事件"));

                    if (hasUnityEvent.boolValue)
                    {
                        EditorGUILayout.PropertyField(onTaskCompleted, new GUIContent("完成時觸發事件 (OnTaskCompleted)"));
                    }

                    if (GUILayout.Button("強制完成此任務"))
                    {
                        taskSystem.ForceCompleteTask(taskName.stringValue);
                    }

                    if (GUILayout.Button("刪除任務"))
                    {
                        tasksProp.DeleteArrayElementAtIndex(index);
                        break;
                    }

                    EditorGUILayout.Space();
                }

                EditorGUILayout.EndVertical();
            }
        }

        serializedObject.ApplyModifiedProperties();
        GUILayout.Label("插入任務", EditorStyles.boldLabel);
        inputTaskID = EditorGUILayout.IntField("輸入任務 ID", inputTaskID);

        if (GUILayout.Button("插入任務"))
        {
            int newTaskID = inputTaskID;
            foreach (var task in taskSystem.tasks)
            {
                if (task.taskID >= newTaskID)
                {
                    task.taskID += 1;
                }
            }
            foreach (var task in taskSystem.taskGroups)
            {
                if (task.groupID >= newTaskID)
                {
                    task.groupID += 1;
                }
            }


            // 先插入新的 Task
            taskSystem.tasks.Add(new TaskUnit3
            {
                taskID = newTaskID,
                taskName = $"新任務 {newTaskID}"
            });

            // 調整所有 taskID 大於等於 newTaskID 的任務

            // 重新排序列表，確保按 taskID 排序
            taskSystem.tasks = taskSystem.tasks.OrderBy(t => t.taskID).ToList();
            taskSystem.taskGroups = taskSystem.taskGroups.OrderBy(t => t.groupID).ToList();
        }
        if (GUILayout.Button("新增任務"))
        {
            int newTaskID = inputTaskID;
            taskSystem.tasks.Add(new TaskUnit3
            {
                taskID = newTaskID,
                taskName = $"新任務 {newTaskID}"
            });
            taskSystem.tasks = taskSystem.tasks.OrderBy(t => t.taskID).ToList();
            taskSystem.taskGroups = taskSystem.taskGroups.OrderBy(t => t.groupID).ToList();
        }
        GUILayout.Space(20);
        if (GUILayout.Button("重新排序"))
        {
            taskSystem.tasks = taskSystem.tasks.OrderBy(t => t.taskID).ToList();
            taskSystem.taskGroups = taskSystem.taskGroups.OrderBy(t => t.groupID).ToList();
        }
        if (GUILayout.Button("所有任務UI收合"))
        {
            for (int i = 0; i < foldouts.Count; i++)
            {
                foldouts[i] = false;
            }

            foreach (var key in foldoutStates.Keys.ToList())
            {
                foldoutStates[key] = false;
            }
        }
    }
}