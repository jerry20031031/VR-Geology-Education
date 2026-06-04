using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Earthquake_Time : MonoBehaviour
{

    public string Pwave;
    public string Swave;
    public TextMeshProUGUI Ptime;
    
    public TextMeshProUGUI Stime;
    

    void Start()
    {
        
    }


    void Update()
    {
        
    }
    public void modifyText(){
        Ptime.text=Pwave;
        Stime.text=Swave; 

    }
}
