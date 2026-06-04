using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logDataBoard : MonoBehaviour
{
    private void OnEnable()
    {
            LogSystem.LogAction("互動錯誤(學習者選擇錯誤沉積物厚度分布)");
            LogSystem.instance.InteractionFailures++;
    }
}
