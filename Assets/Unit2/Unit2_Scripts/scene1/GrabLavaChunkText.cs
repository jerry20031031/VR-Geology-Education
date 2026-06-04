using UnityEngine;
using TMPro; // 確保使用 TextMeshPro 命名空間
using UnityEngine.XR.Interaction.Toolkit;

public class GrabLavaChunkText : MonoBehaviour
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
        title.text = "軟流圈";
        text.text = "軟流圈是地球地函的一部分弱塑性變形區域，位於岩石圈之下、中間圈之 上";
        if (s1 != null && s1.enabled)
        {   
            StartTimer();
            times++;
            s1.SessionLavaStart();
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
            s1.SessionLava(elapsedTime);
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
