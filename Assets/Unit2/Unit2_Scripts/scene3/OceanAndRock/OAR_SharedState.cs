using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OAR_SharedState : MonoBehaviour
{
    public bool RightDownActive = false; // OAR_Right 的 down 是否未歸零
    public bool LeftDownActive = false; // OAR_Left 的 down 是否未歸零

    public bool RightDown = false; // OAR_Right 的 down 是否達到100
    public bool LeftDown = false; // OAR_Left 的 down 是否達到100

    public bool PushActive = false;
    public bool PushDown = false; 

    private bool StopActive = false;

    [SerializeField] private UnityEvent onUp;
    [SerializeField] private UnityEvent onDown;  
    [SerializeField] private UnityEvent onPush;  
    [SerializeField] private UnityEvent DisableMove;    
    
    public Scene3Session s3;

    void Update()
    {
        if(RightDown == true && LeftDown == true && StopActive == false)
        {
            onDown.Invoke();
            StopActive = true;
            s3.SessionAction("移動海洋板塊和大陸板塊互相遠離，產生海溝及造山帶");
            s3.SessionConvergent();
        }

        if(PushDown == true && StopActive == false)
        {
            onPush.Invoke();
            StopActive = true;
            s3.SessionAction("移動海洋板塊和大陸板塊錯動，產生轉形斷層");
            s3.SessionTransform();
        }

        if(StopActive)
        {
            DisableMove.Invoke();
        }

    }
}
