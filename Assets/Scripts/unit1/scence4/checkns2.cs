using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkns2 : MonoBehaviour
{
    private bool hasLogged = false;

    private void OnEnable()
    {
        if (!hasLogged)
        {
            hasLogged = true;
            LogSystem.LogAction("互動錯誤(學習者選擇錯誤方向)");
            LogSystem.instance.InteractionFailures++;
        }
    }
}
