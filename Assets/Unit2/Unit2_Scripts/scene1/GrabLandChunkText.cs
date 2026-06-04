using UnityEngine;
using TMPro; // 確保使用 TextMeshPro 命名空間
using UnityEngine.XR.Interaction.Toolkit;

public class GrabLandChunkText : MonoBehaviour
{
    public XRGrabInteractable grabInteractable; // XR Grab Interactable 組件
    public TextMeshProUGUI title; // Canvas 中的文字元件
    public TextMeshProUGUI text; // Canvas 中的文字元件
    public Scene1Session s1;
    int times;

    void Start()
    {
        // 獲取 XR Grab Interactable 組件
        if (grabInteractable == null)
        {
            grabInteractable = GetComponent<XRGrabInteractable>();
        }

        if (s1 == null)
        {
            s1 = GetComponent<Scene1Session>();
        }


        // 確保文字元件已經正確設置
        if (text == null)
        {
            Debug.LogError("未設置 TextMeshProUGUI！請在 Inspector 中指定");
            return;
        }

        // 註冊抓取和放開事件
        grabInteractable.onSelectEntered.AddListener(OnGrab);
        grabInteractable.onSelectExited.AddListener(OnRelease);
    }
    // 當物體被抓取時更改文字
    private void OnGrab(XRBaseInteractor interactor)
    {
        title.text = "岩石圈(大陸區域)";
        text.text = "包含大陸地殼的岩石圈主要由較輕之矽鋁質岩石組成，如：花崗岩，偏向酸性。相對於海洋地殼，大陸地殼的密度較小";
        if (s1 != null && s1.enabled)
        {
            StartTimer();
            s1.SessionLandStart();
        }
    }

    // 當物體被放開時更改文字
    private void OnRelease(XRBaseInteractor interactor)
    {
        title.text = "請選取板塊!";
        text.text = "選取一個板塊來認識板塊構造吧!";
        if (s1 != null && s1.enabled)
        {
            StopTimer();
            s1.SessionLand(elapsedTime);
        }
    }

    #region 計時器
    static private float elapsedTime = 0f;
    private bool isRunning = false;

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime; // 計算經過的時間
        }
    }
    public void StartTimer()
    {
        isRunning = true;
        elapsedTime = 0f; // 重新開始
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    #endregion
}
