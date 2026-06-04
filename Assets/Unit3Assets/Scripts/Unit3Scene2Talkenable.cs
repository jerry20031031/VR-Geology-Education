using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit3Scene2Talkenable : MonoBehaviour
{
    public List<GameObject> talks = new List<GameObject>();
    public void talking(int index)
    {
        for (int i = 0; i < talks.Count; i++)
        {
            if (i == index)
            {
                talks[i].SetActive(true);
            }
            else
            {
                talks[i].SetActive(false);
            }
        }
    }
    public void EndTalk()
    {
        for (int i = 0; i < talks.Count; i++)
        {
            talks[i].SetActive(true);
        }
    }
}
