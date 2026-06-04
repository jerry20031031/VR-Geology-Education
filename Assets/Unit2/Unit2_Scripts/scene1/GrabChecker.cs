using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; // 引入 TextMeshPro
using System.Collections; // 引入協程命名空間
using UnityEngine.Events;
using System;
using System.IO;

public class GrabChecker : MonoBehaviour
{

    public XRGrabInteractable[] grabInteractables; // 三個物件的陣列
    public MonoBehaviour moveScript; // 要啟用的 Move 腳本
    public TextMeshProUGUI messageText; // 用於顯示訊息的 TextMeshPro 元件
    public string message = "現在開始拼湊板塊模型"; // 顯示的訊息
    public float messageDisplayDuration = 3f; // 顯示訊息的時間（秒）
    public GameObject objectToActivate; // 要啟用的物件

    private bool[] grabStatus; // 追蹤每個物件是否已被抓取並放下
    private int releasedCount = 0; // 已經放下的物件數量

    public Scene1Session s1;

    [SerializeField] private UnityEvent onEventFinished;

    void Start()
    {
        // 初始化抓取狀態
        grabStatus = new bool[grabInteractables.Length];

        // 初始狀態禁用 Move 腳本
        if (moveScript != null)
        {
            moveScript.enabled = false;
        }

        // 初始狀態禁用啟用物件
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }

        // 註冊每個物件的放下事件
        for (int i = 0; i < grabInteractables.Length; i++)
        {
            int index = i; // 避免閉包問題
            grabInteractables[i].onSelectExited.AddListener(interactor => OnRelease(index));
        }
    }

    // 當某個物件被放下時觸發
    private void OnRelease(int index)
    {
        if (!grabStatus[index])
        {
            grabStatus[index] = true;
            releasedCount++;

            // 檢查是否所有物件都已放下
            if (releasedCount == grabInteractables.Length)
            {
                ActivateMoveScript();
                
            }
        }
    }

    // 啟用 Move 腳本並顯示訊息
    private void ActivateMoveScript()
    {
        // 啟用 Move 腳本
        if (moveScript != null)
        {
            moveScript.enabled = true;
            Debug.Log("所有物件已放下，啟用 Move 腳本！");
        }

        // 顯示訊息
        if (messageText != null)
        {
            messageText.text = message;
            Debug.Log(message); // 顯示訊息
        }

        // 啟用物件
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
            Debug.Log("啟用物件：" + objectToActivate.name);
        }

        // 啟動協程來停用文字和物件
        StartCoroutine(HideMessageAndObjectAfterDelay());
    }

    // 協程：顯示訊息後等待幾秒鐘並停用文字和物件
    private IEnumerator HideMessageAndObjectAfterDelay()
    {
        // 等待指定的時間
        yield return new WaitForSeconds(messageDisplayDuration);

        // 停用文字顯示
        if (messageText != null)
        {
            messageText.text = ""; // 清空文字
            Debug.Log("訊息已隱藏");
        }

        // 停用物件
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
            Debug.Log("物件已隱藏");
        }
        
        onEventFinished?.Invoke();
    }

    public void AddListenerToEventFinished(UnityAction action)
    {
        onEventFinished.AddListener(action);
    }
}
