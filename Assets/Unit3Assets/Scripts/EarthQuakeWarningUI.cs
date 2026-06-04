using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EarthQuakeWarningUI : MonoBehaviour
{
    public int hideDelay = 11;
    private int WarningCount=3;
    public GameObject Count;
    public GameObject LAB;
    public GameObject DirectionLight;
    public GameObject SpotLight;
    public TaskSystemUnit3 taskSystem;
    

    private void OnEnable()
    {
        taskSystem=GameObject.Find("TaskSystem").GetComponent<TaskSystemUnit3>();
        Debug.Log("EARTHQUAKE");
        DirectionLight.SetActive(false);    
        SpotLight.SetActive(true);
        StartCoroutine(Warning());
    }



    private IEnumerator Warning(){
        for(int i=hideDelay; i>0;i--){
        hideDelay--;
        
        if(hideDelay==6){
            StartCoroutine(CountDown());
        }
        if(hideDelay==3){
            Count.SetActive(false);
            LAB.GetComponent<Animator>().SetBool("Shaking",true);
        }
        if(hideDelay==0){
            LAB.GetComponent<Animator>().SetBool("Shaking",false); 
         DirectionLight.SetActive(true);
         SpotLight.SetActive(false);
         taskSystem.ForceCompleteTask("地震流程");

            this.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(1) ;
        }

    }
    private IEnumerator CountDown(){
         for(int i=WarningCount; i>0;i--){
        Count.GetComponent<TextMeshProUGUI>().text=WarningCount.ToString();
        WarningCount--;
        yield return new WaitForSeconds(1);
         }

    }
     private void OnDisable() {
             LAB.GetComponent<Animator>().SetBool("Shaking",false); 
         DirectionLight.SetActive(true);
         SpotLight.SetActive(false);
         taskSystem.ForceCompleteTask("地震流程");
    }

}
