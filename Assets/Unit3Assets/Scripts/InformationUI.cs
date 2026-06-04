using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InformationUI : MonoBehaviour
{
    public InputActionReference ControlUi;
    public GameObject Informationui;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (ControlUi.action.triggered)
        {
            Informationui.SetActive(!Informationui.activeSelf);

        }
        if (!Informationui.transform.Find("Detail").gameObject.activeInHierarchy)
        {
            Informationui.GetComponent<DetectUI>().ShowTextImageByID(0);

        }

    }
}
