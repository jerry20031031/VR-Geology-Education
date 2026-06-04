using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class RockUpdate: MonoBehaviour
{
    public GameObject[] targetObjectGroup1 = new GameObject[2];
    public GameObject[] targetObjectGroup2 = new GameObject[2];
    public GameObject[] targetObjectGroup3 = new GameObject[2];
    public GameObject[] targetObjectGroup4 = new GameObject[2];
    public GameObject[] targetObjectGroup5 = new GameObject[2];
    public GameObject[] targetObjectGroup6 = new GameObject[2];
    
    public Material OriginalMaterial;  // 儲存當前材質
    public Material UpdateMaterial;  // 儲存當前材質

    Material currentMaterial1;
    Material currentMaterial2;
    Material currentMaterial3;
    Material currentMaterial4;
    Material currentMaterial5;
    Material currentMaterial6;

    void Start()
    {
        
    }

    void Update()
    {

        currentMaterial1 = targetObjectGroup1[0].GetComponent<Renderer>().sharedMaterial;
        currentMaterial2 = targetObjectGroup2[0].GetComponent<Renderer>().sharedMaterial;
        currentMaterial3 = targetObjectGroup3[0].GetComponent<Renderer>().sharedMaterial;
        currentMaterial4 = targetObjectGroup4[0].GetComponent<Renderer>().sharedMaterial;
        currentMaterial5 = targetObjectGroup5[0].GetComponent<Renderer>().sharedMaterial;
        currentMaterial6 = targetObjectGroup6[0].GetComponent<Renderer>().sharedMaterial;

        if(currentMaterial1 != OriginalMaterial)
        {
            UpdateGroupMaterials(targetObjectGroup1);
        }
        if(currentMaterial2 != OriginalMaterial)
        {
            UpdateGroupMaterials(targetObjectGroup2);
        }
        if(currentMaterial3 != OriginalMaterial)
        {
            UpdateGroupMaterials(targetObjectGroup3);
        }
        if(currentMaterial4 != OriginalMaterial)
        {
            UpdateGroupMaterials(targetObjectGroup4);
        }
        if(currentMaterial5 != OriginalMaterial)
        {
            UpdateGroupMaterials(targetObjectGroup5);
        }
        if(currentMaterial6 != OriginalMaterial)
        {
            UpdateGroupMaterials(targetObjectGroup6);
        }
    }

    // 更新群組內所有物件的材質 
    
    void UpdateGroupMaterials(GameObject[] targetObjectGroup)
    {
        targetObjectGroup[1].GetComponent<Renderer>().sharedMaterial = UpdateMaterial; // 使用 sharedMaterial
    }
   }
