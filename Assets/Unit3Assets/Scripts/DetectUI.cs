using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DetectUI : MonoBehaviour
{
    [System.Serializable]
    public class TextImageData
    {
        public int id; // 唯一編號
        public string title;
        public string text; // 顯示的文字
        public Sprite image; // 顯示的圖片
        public string relatedTaskName; // 對應的任務名稱（可選）
        public UnityEvent Event; // 點擊事件（可選）
    }

    public List<TextImageData> textImageList = new List<TextImageData>(); // 文字和圖片的組合列表

    [Header("UI References")]

    public TextMeshProUGUI displayTitle;
    public TextMeshProUGUI displayText; // 用於顯示文字的 UI Text
    public Image displayImage; // 用於顯示圖片的 UI Image
    public GameObject Detail;
    public AudioClip DetectSfx;

    [Header("Task System Reference")]
    [SerializeField] private TaskSystemUnit3 taskSystem; // 引用 TaskSystem，用於調用強制完成任務的功能

    void Start()
    {
        taskSystem = GameObject.Find("TaskSystem").GetComponent<TaskSystemUnit3>();
        if (textImageList.Count > 0)
        {
            // 初始化顯示第一組文字和圖片（根據 ID=1 顯示）
            ShowTextImageByID(0);
        }
        else
        {
            Debug.LogWarning("文字和圖片列表為空，請手動新增！");
        }
    }


    // 顯示特定 ID 的文字和圖片，並檢查是否需要強制完成任務
    public void ShowTextImageByID(int id)
    {
        TextImageData data = textImageList.Find(item => item.id == id);
        if (data != null)
        {

            if (displayTitle != null)
            {
                displayTitle.text = data.title; // 更新文字
            }
            if (displayText != null)
            {
                displayText.text = data.text; // 更新文字
            }

            if (displayImage != null)
            {
                displayImage.sprite = data.image; // 更新圖片
            }

            if (Detail.activeSelf)
            {
                if (id != 0)
                {
                    LogSystemUnit3.LogAction("探測" + data.title + "資訊");
                    Debug.Log("探測" + data.title + "資訊");
                    AudioSystem.instance.PlaySFX(DetectSfx);
                    data.Event?.Invoke(); // 調用點擊事件（如果有的話）
                }
            }

            // Debug.Log($"成功顯示編號 {id} 的文字和圖片！");

            // 如果有對應的任務名稱，調用強制完成方法
            if (!string.IsNullOrEmpty(data.relatedTaskName) && taskSystem != null && Detail.activeInHierarchy)
            {
                taskSystem.ForceCompleteTask(data.relatedTaskName);
            }
        }
        else
        {
            Debug.LogError($"未找到編號為 {id} 的文字和圖片！");
        }
    }

    // 新增一組文字和圖片
    public void AddTextImage(int id, string text, string title, Sprite image, string relatedTaskName = "")
    {
        // 確保 ID 唯一
        if (textImageList.Exists(item => item.id == id))
        {
            Debug.LogError($"編號 {id} 已存在，無法新增！");
            return;
        }

        TextImageData newData = new TextImageData
        {
            id = id,
            text = text,
            title = title,
            image = image,
            relatedTaskName = relatedTaskName
        };

        textImageList.Add(newData);
        Debug.Log($"新增文字和圖片：編號 {id}，文字 - {text}，圖片 - {image?.name}，對應任務名稱 - {relatedTaskName}");
    }
}
