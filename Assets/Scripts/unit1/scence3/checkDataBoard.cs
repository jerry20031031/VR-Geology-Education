using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class checkDataBoard : MonoBehaviour
{
    public XRSocketInteractor socket1;
    public XRSocketInteractor socket2;
    public string ans1 = "ans1-3";
    public string ans2 = "ans2";
    public GameObject finishcube;  // 正確時顯示
    public GameObject Ycube;  // 正確時顯示
    public GameObject Ncube; // 錯誤時顯示
    public float errorDisplayTime = 2f;
    public float successDelayTime = 10f; // 延遲時間

    public bool true1=false; // 判斷是否正確
    public bool true2 = false; // 判斷是否正確

    public void CheckSockets()
    {
        bool isCorrect = IsSocketCorrect(socket1, ans1) && IsSocketCorrect(socket2, ans2);

        Ycube.SetActive(isCorrect);
        //finishcube.SetActive(true);
        if (isCorrect)
        {
            if (!true2)
            {
                true2 = true;
                LogSystem.instance.playEnd4();
                LogSystem.instance.StopTimer();
            }
            Debug.Log("Correct!");
            StartCoroutine(ShowSuccessWithDelay());
        }
        if (!isCorrect)
        {
            StartCoroutine(ShowErrorTemporarily());
            if(!true1)
            {
                true1 = true;
            }

        }
    }

    private bool IsSocketCorrect(XRSocketInteractor socket, string correctName)
    {
        if (socket.hasSelection)
        {
            IXRSelectInteractable interactable = socket.GetOldestInteractableSelected();
            if (interactable != null)
            {
                return interactable.transform.name == correctName;
            }
        }
        return false;
    }

    private IEnumerator ShowErrorTemporarily()
    {
        Ncube.SetActive(true);
        yield return new WaitForSeconds(errorDisplayTime);
        Ncube.SetActive(false);
    }
    private IEnumerator ShowSuccessWithDelay()
    {
        yield return new WaitForSeconds(successDelayTime);
        finishcube.SetActive(true);
    }
}
