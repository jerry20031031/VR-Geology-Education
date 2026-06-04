using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.TextCore;
using TMPro;

public class Backpack : MonoBehaviour
{
    public XRRayInteractor leftHandInteractor; // 左手控制器
    public XRRayInteractor rightHandInteractor; // 右手控制器
    public InputActionReference Save;
    [System.Serializable]
    public class BackpackItem
    {
        public Image itemImage; // 物件的圖片
        public GameObject itemPrefab; // 物件的預製體
        public BackpackItem(Image itemImage, GameObject itemPrefab)
        {
            this.itemImage = itemImage;
            this.itemPrefab = itemPrefab;
        }
        public void BackpackItemSet(Sprite Image, GameObject itemPrefab)
        {
            itemImage.sprite = Image;
            this.itemPrefab = itemPrefab;
        }
        public void Remove()
        {
            itemImage.sprite = null;
            this.itemPrefab = null;

        }
    }

    public List<BackpackItem> backpackItems = new List<BackpackItem>(); // 背包列表
    public Transform itemSpawnPoint; // 物件生成位置


    private void Start()
    {
        backpackItems.Add(new BackpackItem(null, null));
    }

    public void AddItemToBackpack(Sprite itemImage, GameObject itemPrefab)
    {
        // 检查背包中是否有空位
        for (int i = 0; i < backpackItems.Count; i++)
        {
            if (backpackItems[i].itemPrefab == null)
            {
                // 更新该空位
                backpackItems[i].BackpackItemSet(itemImage, itemPrefab);
                backpackItems[i].itemImage.gameObject.SetActive(true);
                return; // 找到空位后直接退出方法
            }
        }
        // 如果背包已满，没有空位
        Debug.LogWarning("背包已满，无法添加新物品！");
    }


    public void SpawnItemFromBackpack(int index)
    {
        if (index < 0 || index >= backpackItems.Count)
        {
            Debug.LogError("背包索引無效！");
            return;
        }

        BackpackItem item = backpackItems[index];

        if (item.itemPrefab == null)
        {
            Debug.LogError("物品Prefab為null，無法生成！");
            return;
        }

        // 在指定位置生成物件
        item.itemPrefab.transform.position = itemSpawnPoint.position;
        item.itemPrefab.SetActive(true);

        // 檢查並關閉Hint
        Transform hintTransform = item.itemPrefab.transform.Find("Hint");
        if (hintTransform != null)
        {
            Hint hint = hintTransform.GetComponent<Hint>();
            if (hint != null)
            {
                hint.gameObject.SetActive(false);
            }
        }

        // 清空背包槽
        backpackItems[index].itemImage.sprite = null;
        backpackItems[index].itemImage.gameObject.SetActive(false);
        backpackItems[index].itemPrefab = null; // **這行應該放到最後，確保不影響前面的操作**
    }

    public void ClearBackpack()
    {
        foreach (BackpackItem item in backpackItems)
        {
            if (item.itemImage != null)
            {
                item.itemImage.sprite = null;
                item.itemImage.gameObject.SetActive(false);
            }
            item.itemPrefab = null;
        }

        Debug.Log("背包已清空！");
    }

    private void Update()
    {
        Debug.Log(leftHandInteractor.interactablesSelected.Count);

        // 偵測玩家抓取並按下 A 鍵時將物品加入背包
        if (Save.action.triggered)
        {
            GameObject heldItem = null;
            if (leftHandInteractor.interactablesSelected.Count > 0)
            {
                heldItem = GetHeldItem(leftHandInteractor);
            }
            else if (rightHandInteractor.interactablesSelected.Count > 0)
            {
                heldItem = GetHeldItem(rightHandInteractor);
            }
            // 獲取玩家當前抓取的物件



            if (heldItem != null)
            {

                Sprite itemImage = heldItem.GetComponent<ItemData>().itemImage; // 假設物件上有 ItemData 腳本
                GameObject itemPrefab = heldItem;


                AddItemToBackpack(itemImage, itemPrefab);
                heldItem.SetActive(false);

            }
        }
    }

    private GameObject GetHeldItem(XRRayInteractor interactor)
    {
        return interactor.interactablesSelected[0].transform.gameObject;
    }
}
