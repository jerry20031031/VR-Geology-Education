using UnityEngine;

public class OAR_Controller : MonoBehaviour
{
    public OAR_Left Left;
    public OAR_Right Right;
    public OAR_Push Push_Left;
    public OAR_Push Push_Right; 
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
