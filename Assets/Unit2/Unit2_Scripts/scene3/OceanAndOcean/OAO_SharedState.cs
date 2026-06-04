using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OAO_SharedState : MonoBehaviour
{
    public bool RightUpActive = false;  // OAO_Right 的 up 是否未歸零
    public bool RightDownActive = false; // OAO_Right 的 down 是否未歸零
    public bool LeftUpActive = false;   // OAO_Left 的 up 是否未歸零
    public bool LeftDownActive = false; // OAO_Left 的 down 是否未歸零

    public bool RightUp = false;// OAO_Right 的 up 是否達到100
    public bool RightDown = false; // OAO_Right 的 down 是否達到100
    public bool LeftUp = false;   // OAO_Left 的 up 是否達到100
    public bool LeftDown = false; // OAO_Left 的 down 是否達到100

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
        if(RightUp == true && LeftUp == true && StopActive == false)
        {
            onUp.Invoke();
            StopActive = true;
            s3.SessionAction("移動海洋板塊互相遠離，產生中洋脊");
            s3.SessionDivergent();
        }

        if(RightDown == true && LeftDown == true && StopActive == false)
        {
            onDown.Invoke();
            StopActive = true;
            s3.SessionAction("移動海洋板塊互相靠近，產生海溝及火山島弧");
            s3.SessionConvergent();
        }

        if(PushDown == true && StopActive == false)
        {
            onPush.Invoke();
            StopActive = true;
            s3.SessionAction("移動海洋板塊錯動，產生轉形斷層");
            s3.SessionTransform();
        }

        if(StopActive)
        {
            DisableMove.Invoke();
        }

    }
}
