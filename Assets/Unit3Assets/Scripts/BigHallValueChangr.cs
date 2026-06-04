using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigHallValueChangr : MonoBehaviour
{
    // Start is called before the first frame update
    public Dropdown Dropdown1;
    public Dropdown Dropdown2;
    public GameObject ConfirmBtn;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Check()
    {
        if (Dropdown1.value != 0 && Dropdown2.value != 0)
        {
            ConfirmBtn.SetActive(true);

        }
        else
        {
            ConfirmBtn.SetActive(false);
        }

    }
    public void SetClassAndNumber(ClassAndNumber Data){
        Data.Class=Dropdown1.options[Dropdown1.value].text;
        Data.Number=Dropdown2.options[Dropdown2.value].text;


    }
}
