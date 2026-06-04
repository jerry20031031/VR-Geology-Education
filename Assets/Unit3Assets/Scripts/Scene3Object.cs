using System.Collections;
using System.Collections.Generic;
using cherrydev;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class Scene3Object : MonoBehaviour
{
    public XRSocketInteractor XRSocket1;
    public XRSocketInteractor XRSocket2;
    public XRSocketInteractor XRSocket3;
    public GameObject Target1;
    public GameObject Target2;
    public GameObject Target3;
    public DialogNodeGraph Currect;
    
    public DialogNodeGraph Wrong;
    public Talk GirlTalk;



    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DetectObject(){
      
     bool hasAllObjects = XRSocket1.interactablesSelected.Count > 0 &&
                     XRSocket2.interactablesSelected.Count > 0 &&
                     XRSocket3.interactablesSelected.Count > 0;

if (hasAllObjects)
{
    var obj1 = XRSocket1.interactablesSelected[0].transform.gameObject;
    var obj2 = XRSocket2.interactablesSelected[0].transform.gameObject;
    var obj3 = XRSocket3.interactablesSelected[0].transform.gameObject;

    if (obj1.name == Target1.name &&
        obj2.name == Target2.name &&
        obj3.name == Target3.name)
    {
        GirlTalk.ForceInsertDialog = Currect;
        Debug.Log("correct");
    }
    else
    {
        Debug.Log(obj1.name);
        Debug.Log(obj2.name);
        Debug.Log(obj3.name);
        GirlTalk.ForceInsertDialog = Wrong;
    }
}
else
{
    // 有任一 Socket 沒有物件時也進入錯誤處理
    Debug.LogWarning("有空的 Socket，不能判斷為正確！");
    
    // 可印出目前三個 socket 各自的狀態
    Debug.Log("Socket1: " + (XRSocket1.interactablesSelected.Count > 0 ? XRSocket1.interactablesSelected[0].transform.gameObject.name : "空"));
    Debug.Log("Socket2: " + (XRSocket2.interactablesSelected.Count > 0 ? XRSocket2.interactablesSelected[0].transform.gameObject.name : "空"));
    Debug.Log("Socket3: " + (XRSocket3.interactablesSelected.Count > 0 ? XRSocket3.interactablesSelected[0].transform.gameObject.name : "空"));

    GirlTalk.ForceInsertDialog = Wrong;
}

    } 
}
