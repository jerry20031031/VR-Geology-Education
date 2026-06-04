using System.Collections;
using System.Collections.Generic;
using cherrydev;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // 引入 UI 命名空間

public class PkainCollider : MonoBehaviour
{
    public Talk Talker;
    public bool Active=false;
    public GameObject Rock1;
    public GameObject Rock2;
    public GameObject Rock3;
    public GameObject SpecialObject; // 特殊物件

    public TextMeshProUGUI generalObjectCounterText; // 一般物件計數的 UI 文本
    public TextMeshProUGUI specialObjectCounterText; // 特殊物件計數的 UI 文本

    private HashSet<GameObject> generalObjectsOnTable = new HashSet<GameObject>();
    private bool specialObjectOnTable = false; // 特殊物件是否在桌上
    private int totalGeneralObjects = 3;

    private void Start()
    {

        UpdateUIText();
    }
    private void OnEnable() {
        Active=true;
        Talker.Conversationindex(2);
    }

    // 當物件進入桌面範圍
    private void OnCollisionStay(Collision other)
    {
        if(Active){
        if (IsGeneralObject(other.gameObject))
        {
            generalObjectsOnTable.Add(other.gameObject);
            UpdateUIText();
        }
        else if (IsSpecialObject(other.gameObject))
        {
            specialObjectOnTable = true;
            UpdateUIText();
        }
        }
    }

    // 當物件離開桌面範圍
    private void OnCollisionExit(Collision other)
    {
        if(Active){
        if (IsGeneralObject(other.gameObject))
        {
            generalObjectsOnTable.Remove(other.gameObject);
            UpdateUIText();
        }
        else if (IsSpecialObject(other.gameObject))
        {
            specialObjectOnTable = false;
            UpdateUIText();
        }
        }
    }

    // 檢查是否是一般物件
    private bool IsGeneralObject(GameObject obj)
    {
        return obj == Rock1 || obj == Rock2 || obj == Rock3;
    }

    // 檢查是否是特殊物件
    private bool IsSpecialObject(GameObject obj)
    {
        return obj == SpecialObject;
    }

    // 更新 UI 文本顯示
    private void UpdateUIText()
    {
        // 更新一般物件的計數
        int generalCount = generalObjectsOnTable.Count;
        generalObjectCounterText.text = $"一般物件: {generalCount}/{totalGeneralObjects}";

        // 更新特殊物件的狀態
        specialObjectCounterText.text = specialObjectOnTable ? "特殊線索: 1/1" : "特殊線索: 0/1";
        if(specialObjectOnTable&&generalCount==3){
            Talker.Conversationindex(1);
        }
        else{
            Talker.Conversationindex(2);
        }
        
    }
     public void ManualUpdateSpecialObjectStatus(bool isOnTable)
    {
        specialObjectOnTable = isOnTable;
        UpdateUIText();
    }

}
