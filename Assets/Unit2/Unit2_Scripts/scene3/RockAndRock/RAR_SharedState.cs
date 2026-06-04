using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RAR_SharedState : MonoBehaviour
{
    public bool RightClose_RockActive = false;  // RAR_Right 的 close 是否未歸零
    public bool RightUp_RockActive = false; // RAR_Right 的 up 是否未歸零

    public bool RightClose_L1Active = false;  // RAR_Right 的 close 是否未歸零
    public bool RightUp_L1Active = false; // RAR_Right 的 up 是否未歸零
    
    public bool RightUp_L2Active = false; // RAR_Right 的 up 是否未歸零
    public bool RightBlow_L2Active = false; // RAR_Right 的 blow 是否未歸零

    public bool RightUp_L3Active = false; // RAR_Right 的 up_1 是否未歸零
    public bool RightBlow_L3Active = false; // RAR_Right 的 blow_1 是否未歸零

    public bool RightUp_RockisActive = false; // RAR_Right 的 up 是否100
    public bool RightUp_L1isActive = false; // RAR_Right 的 up 是否100
    public bool RightUp_L2isActive = false; // RAR_Right 的 up 是否100
    public bool RightBlow_L2isActive = false; // RAR_Right 的 blow 是否100
    //---------------------------------------------------------------------------
    public bool LeftClose_RockActive = false;  // RAR_Left 的 close 是否未歸零
    public bool LeftUp_RockActive = false; // RAR_Left 的 up 是否未歸零

    public bool LeftClose_L1Active = false;  // RAR_Left 的 close 是否未歸零
    public bool LeftUp_L1Active = false; // RAR_Left 的 up 是否未歸零
    
    public bool LeftUp_L2Active = false; // RAR_Left 的 up 是否未歸零
    public bool LeftBlow_L2Active = false; // RAR_Left 的 blow 是否未歸零

    public bool LeftUp_L3Active = false; // RAR_Left 的 up_1 是否未歸零
    public bool LeftBlow_L3Active = false; // RAR_Left 的 blow_1 是否未歸零

    public bool LeftUp_RockisActive = false; // RAR_Left 的 up 是否100
    public bool LeftUp_L1isActive = false; // RAR_Left 的 up 是否100
    public bool LeftUp_L2isActive = false; // RAR_Left 的 up 是否100
    public bool LeftBlow_L2isActive = false; // RAR_Left 的 blow 是否100
    //---------------------------------------------------------------------------
    public bool RightClose = false;// RAR_Right 的 up 是否達到100
    public bool RightUp = false; // RAR_Right 的 down 是否達到100
    public bool LeftClose = false;   // RAR_Left 的 up 是否達到100
    public bool LeftUp = false; // RAR_Left 的 down 是否達到100

    public bool PushActive = false;
    public bool PushDown = false; 

    private bool StopActive = false;

    [SerializeField] private UnityEvent onUp;
    [SerializeField] private UnityEvent onClose;  
    [SerializeField] private UnityEvent onPush;  
    [SerializeField] private UnityEvent DisableMove;    

    public Scene3Session s3;

    void Update()
    {
        if(RightUp == true && LeftUp == true && StopActive == false)
        {
            onUp.Invoke();
            StopActive = true;
            s3.SessionAction("移動大陸板塊互相遠離，產生裂谷");
            s3.SessionDivergent();
        }

        if(RightClose == true && LeftClose == true && StopActive == false)
        {
            onClose.Invoke();
            StopActive = true;
            s3.SessionAction("移動大陸板塊互相靠近，產生造山帶");
            s3.SessionConvergent();
        }

        if(PushDown == true && StopActive == false)
        {
            onPush.Invoke();
            StopActive = true;
            s3.SessionAction("移動大陸板塊錯動，產生轉形斷層");
            s3.SessionTransform();
        }

        if(StopActive)
        {
            DisableMove.Invoke();
        }

    }
}
