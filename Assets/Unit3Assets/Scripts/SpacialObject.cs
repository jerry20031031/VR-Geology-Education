using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacialObject : MonoBehaviour
{
    private bool  CanDisplay = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCanDisplay()
    {
        if (CanDisplay)
        {
            CanDisplay = false;
            gameObject.SetActive(true);
        }
    }
    public void ResetDisplay()
    {
       CanDisplay = true;
    }
}
