using UnityEngine;
using TMPro; // 確保使用 TextMeshPro 命名空間
using UnityEngine.XR.Interaction.Toolkit;

public class GrabRockChunkText : MonoBehaviour
{
    public XRGrabInteractable grabInteractable; // XR Grab Interactable 組件
    public TextMeshProUGUI title; // Canvas 中的文字元件
    public TextMeshProUGUI text; // Canvas 中的文字元件

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
        title.text = "岩石圈";
        text.text = "岩石圈位於地球的表層，薄而堅硬。岩石圈在軟流圈之上，包含部分上地函和地殼。地殼在地函之上，由莫氏不連續面作為分界";
    }

    // 當物體被放開時更改文字
    private void OnRelease(XRBaseInteractor interactor)
    {
        title.text = "請選取板塊!";
        text.text = "選取一個板塊來認識板塊構造吧!";
    }
}
