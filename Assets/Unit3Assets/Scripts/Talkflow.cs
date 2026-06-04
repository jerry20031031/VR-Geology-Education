using UnityEngine;
using UnityEngine.Events; // 引入 UnityEvent
using System.Collections.Generic;

public class Talkflow : MonoBehaviour
{
    [System.Serializable]
    public class DialoguePart
    {
        public bool isTeacherSpeaking;   // NPC 1 是否說話
        public bool isSchoolGirlSpeaking;// NPC 2 是否說話
        public bool autoStart;           // 是否自動開始該部分對話
    }

    [Header("NPC 對話顯示物件")]
    public GameObject Teacher;    // NPC 1 的對話顯示物件
    public GameObject SchoolGirl; // NPC 2 的對話顯示物件
    public TaskSystemUnit3 taskSystem;

    [Header("完整對話列表")]
    [Tooltip("在這裡可手動新增所有對話部分")]
    public List<DialoguePart> dialogueParts = new List<DialoguePart>();

    [SerializeField] private int currentPartIndex = 0; // 當前對話部分的索引

    void Start()
    {
        Debug.Log(dialogueParts.Count);
        if (dialogueParts == null || dialogueParts.Count == 0)
        {
            Debug.LogError("請在 Inspector 中新增至少一部分對話！");
            return;
        }

        if (Teacher == null || SchoolGirl == null)
        {
            Debug.LogError("請指定 NPC 的對話顯示物件！");
            return;
        }
        StartDialoguePart(currentPartIndex);
    }

    /// <summary>
    /// 開始指定部分的對話
    /// </summary>
    /// <param name="index">對話部分索引</param>
    void StartDialoguePart(int index)
    {
        if (index < 0 || index >= dialogueParts.Count)
        {
            Debug.LogWarning("對話部分索引超出範圍！");
            return;
        }

        DialoguePart part = dialogueParts[index];
        Debug.Log($"開始第 {index + 1} 部分對話");

        // 控制 NPC 1 的對話顯示
        Teacher.SetActive(part.isTeacherSpeaking);
        Debug.Log($"NPC 1 的對話物件 {(part.isTeacherSpeaking ? "顯示" : "隱藏")}");

        // 控制 NPC 2 的對話顯示
        SchoolGirl.SetActive(part.isSchoolGirlSpeaking);
        Debug.Log($"NPC 2 的對話物件 {(part.isSchoolGirlSpeaking ? "顯示" : "隱藏")}");


        if (part.autoStart)
        {
            if (part.isTeacherSpeaking)
            {
                Teacher.GetComponent<Talk>().MissionComplete = true;
            }
            else if (part.isSchoolGirlSpeaking)
            {
                SchoolGirl.GetComponent<Talk>().MissionComplete = true;
            }
        }
    }

    /// <summary>
    /// 切換到下一部分對話。如果所有對話結束則顯示結束訊息。
    /// </summary>
    public void NextDialoguePart()
    {
        currentPartIndex++;

        if (currentPartIndex >= dialogueParts.Count)
        {
            Debug.Log("所有對話已經結束！");
            // 可在這裡加入對話結束後的處理邏輯，如關閉對話框或切換場景
            return;
        }
        StartDialoguePart(currentPartIndex);
    }

    /// <summary>
    /// 暫停對話（隱藏兩個 NPC 的對話顯示物件）
    /// </summary>
    public void PauseDialogue()
    {
        Teacher.SetActive(false);
        SchoolGirl.SetActive(false);
        Debug.Log("對話已暫停！");
    }
}
