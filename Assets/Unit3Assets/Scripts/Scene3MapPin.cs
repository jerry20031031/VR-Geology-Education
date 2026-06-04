using System.Collections;
using System.Collections.Generic;
using cherrydev;
using UnityEngine;

public class Scene3MapPin : MonoBehaviour
{
    public GameObject Region1;
    public GameObject Region2;
    public GameObject Region3;
    public GameObject Region4;
    public GameObject Region5;
    public GameObject Region6;
    private GameObject Region;
    public Talk Girltalk;
    private bool isStart = false;
    void Start()
    {
        Region = this.gameObject.transform.GetChild(0).gameObject;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Mission25()
    {
        isStart = true;
    }
    public void ChangeStatus()
    {
        Region.SetActive(!Region.activeSelf);
        if (isStart)
        {
            if (Region1.activeSelf && Region2.activeSelf && Region3.activeSelf)
            {
                Girltalk.Conversationindex(15);

            }
            else
            {
                Girltalk.Conversationindex(14);
            }
        }

    }
}
