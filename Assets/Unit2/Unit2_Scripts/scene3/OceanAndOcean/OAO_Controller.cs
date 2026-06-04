using UnityEngine;

public class OAO_Controller : MonoBehaviour
{
    public OAO_Left Left;
    public OAO_Right Right;
    public OAO_Push Push_Left; // 可以是任何類型的 OAO_Push script，確保它有啟用開關
    public OAO_Push Push_Right; // 可以是任何類型的 OAO_Push script，確保它有啟用開關
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

        foreach (var model in Left.targetModels)
        {
            if (model.grabInteractable != null && model.grabInteractable.isSelected)
            {
                leftGrabbed = true;
                break;
            }
        }

        foreach (var model in Right.targetModels)
        {
            if (model.grabInteractable != null && model.grabInteractable.isSelected)
            {
                rightGrabbed = true;
                break;
            }
        }

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
