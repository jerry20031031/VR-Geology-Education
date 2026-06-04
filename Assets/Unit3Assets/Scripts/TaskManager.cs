using System.Collections.Generic;
using UnityEngine;
using TMPro; // 引入 TextMeshPro 的命名空間
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public GameObject itemPrefab; // 預置物：matters
    public Transform contentParent; // 父節點，用於放置生成的事項

    // 任務事項的字典
    private Dictionary<string, List<string>> tasks = new Dictionary<string, List<string>>();

    private float initialPosY = 0f; // 起始的 Y 座標
    private float defaultOffsetY = -90f; // 預設的 Y 偏移量
    private float extraOffsetY = -70f; // 超過 6 個字時的額外偏移量
     private  int index = 1; // 初始化索引，從 1 開始

    void Start()
    {
        // 初始化任務和事項的數據
        InitializeTasks();

        // 動態生成某一任務的事項
        GenerateItems(tasks["任務 0"]);
    }

    // 初始化任務和事項的數據
    void InitializeTasks()
    {
        tasks.Add("任務 0", new List<string> { "與教授對話"});
        tasks.Add("任務 1", new List<string> { "探查十字鎬", "探查標誌x2", "解鎖琉球島弧隱藏資訊", "解鎖呂宋島弧隱藏資訊" });
        tasks.Add("任務 2", new List<string> { "短字 1", "短字 2" });
        tasks.Add("任務 3", new List<string> { "六個字" });
    }

    // 動態生成事項
    void GenerateItems(List<string> taskItems)
    {
        // 清空之前的 UI
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        float currentPosY = initialPosY; // 初始化當前的 Y 位置

        foreach (var item in taskItems)
        {
            // 創建事項物件
            GameObject itemObject = Instantiate(itemPrefab, contentParent);

            // 設置 Text (TMP) 的內容
            TMP_Text itemText = itemObject.transform.Find("Text (TMP)").GetComponent<TMP_Text>();
            itemText.text = item; // 僅顯示事項內容

            // 可選：設置 Image 的 Sprite 或顏色
            Image itemImage = itemObject.transform.Find("Image").GetComponent<Image>();
            itemImage.name = index.ToString();

            // 動態調整位置
            RectTransform itemRect = itemObject.GetComponent<RectTransform>();
            itemRect.anchoredPosition = new Vector2(0, currentPosY);

            // 更新下一個物件的 Y 位置
            if (item.Length > 6)
            {
                // 如果字數超過 6，增加額外的偏移量
                currentPosY += defaultOffsetY + extraOffsetY;
            }
            else
            {
                // 預設偏移量
                currentPosY += defaultOffsetY;
            }
        }
    }
}
