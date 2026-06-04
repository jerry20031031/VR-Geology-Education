using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ResetVRcamera : MonoBehaviour
{

    void Start()
    {
       InputTracking.Recenter();
    }

    // 在 Update 中添加按鍵觸發校正
   
}
