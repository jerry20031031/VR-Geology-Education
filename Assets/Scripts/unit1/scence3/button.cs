using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public boat boat; 
    public miniboat miniboat;

    public void OnRightButtonPressed()
    {
        boat.MoveRightStart();
        miniboat.MoveRightStart();
    }
    public void OffRightButtonPressed()
    {
        boat.MoveRightStop();
        miniboat.MoveRightStop();
    }

    public void OnLeftButtonPressed()
    {
        boat.MoveLeftStart();
        miniboat.MoveLeftStart();
    }
    public void OffLeftButtonPressed()
    {
        boat.MoveLeftStop();
        miniboat.MoveLeftStop();
    }

    public void OnUpButtonPressed()
    {
        boat.MoveUpStart();
        miniboat.MoveUpStart();
    }
    public void OffUpButtonPressed()
    {
        boat.MoveUpStop();
        miniboat.MoveUpStop();
    }

    public void OnDownButtonPressed()
    {
        boat.MoveDownStart();
        miniboat.MoveDownStart();
    }
    public void OffDownButtonPressed()
    {
        boat.MoveDownStop();
        miniboat.MoveDownStop();
    }
}
