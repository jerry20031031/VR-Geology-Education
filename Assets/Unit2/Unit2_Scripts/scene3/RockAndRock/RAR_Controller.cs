using UnityEngine;

public class RAR_Controller : MonoBehaviour
{
    public RAR_Left Left;
    public RAR_Right Right;
    public RAR_Push Push_Left; // 可以是任何類型的 OAO_Push script，確保它有啟用開關
    public RAR_Push Push_Right; // 可以是任何類型的 OAO_Push script，確保它有啟用開關
    public float checkInterval = 0.1f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= checkInterval)
        {
            timer = 0f;
            CheckGrabStates();
        }
    }

    void CheckGrabStates()
    {
        bool leftGrabbed = false;
        bool rightGrabbed = false;

        // 左邊：直接檢查三個模型
        if ((Left.Rock?.grabInteractable != null && Left.Rock.grabInteractable.isSelected) ||
            (Left.Land_1?.grabInteractable != null && Left.Land_1.grabInteractable.isSelected) ||
            (Left.Land_2?.grabInteractable != null && Left.Land_2.grabInteractable.isSelected) ||
            (Left.Land_3?.grabInteractable != null && Left.Land_3.grabInteractable.isSelected))
        {
            leftGrabbed = true;
        }

        // 右邊：直接檢查三個模型
        if ((Right.Rock?.grabInteractable != null && Right.Rock.grabInteractable.isSelected) ||
            (Right.Land_1?.grabInteractable != null && Right.Land_1.grabInteractable.isSelected) ||
            (Right.Land_2?.grabInteractable != null && Right.Land_2.grabInteractable.isSelected) ||
            (Right.Land_3?.grabInteractable != null && Right.Land_3.grabInteractable.isSelected))
        {
            rightGrabbed = true;
        }

        // 根據抓取狀態啟用/停用元件
        if (leftGrabbed && rightGrabbed)
        {
            Left.enabled = true;
            Right.enabled = true;
            Push_Left.enabled = false;
            Push_Right.enabled = false;
        }
        else
        {
            Left.enabled = false;
            Right.enabled = false;
            Push_Left.enabled = true;
            Push_Right.enabled = true;
        }
    }

}
