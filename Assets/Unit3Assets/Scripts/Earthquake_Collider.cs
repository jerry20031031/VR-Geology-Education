using System.Collections;
using System.Collections.Generic;
using cherrydev;
using UnityEngine;

public class Earthquake_Collider : MonoBehaviour
{
    public Talk Girltalk;
    public DialogNodeGraph fail;
    public DialogNodeGraph Successful;
    private bool isTouch=false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isTouch){
            Girltalk.ForceInsertDialog=Successful;
        }
        else{
            Girltalk.ForceInsertDialog=fail;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name=="CENTERMAP"){
            isTouch=true;
        }
        else{
           isTouch=false;

        }


    }
}
