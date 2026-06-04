using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class checkNS : MonoBehaviour
{
    public XRSocketInteractor socket_N;
    public XRSocketInteractor socket_S;
    public GameObject point;
    public GameObject uiImage;
    public GameObject falseUI; 

    void Update()
    {
        GameObject objN = GetAttachedObject(socket_N);
        GameObject objS = GetAttachedObject(socket_S);

        if (objN != null && objS != null)
        {
            if (objN.name == "N" && objS.name == "S")
            {
                point.SetActive(true);
                uiImage.SetActive(true);
                falseUI.SetActive(false);
            }
            else
            {
                point.SetActive(false);
                uiImage.SetActive(false);
                falseUI.SetActive(true);
            }
        }
        else
        {
            point.SetActive(false);
            uiImage.SetActive(false);
            falseUI.SetActive(false);
        }
    }

    GameObject GetAttachedObject(XRSocketInteractor socket)
    {
        if (socket.selectTarget != null) // 確保 Socket 有連接的物件
        {
            return socket.selectTarget.gameObject;
        }
        return null;
    }
}
