using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatControl : MonoBehaviour
{
    private GameObject currentTarget; // 儲存當前碰撞的目標物件
    public GameObject fakeButton; // 假按鈕物件
    public Dictionary<string, GameObject> targetButtonMap; // 儲存目標與按鈕的映射

    public GameObject leftMoveButton, rightMoveButton, upMoveButton, downMoveButton; // 移動按鈕

    void Start()
    {
        // 初始化目標與按鈕的映射
        targetButtonMap = new Dictionary<string, GameObject>
        {
            { "target", GameObject.Find("Button_Target") },
            { "l_target1", GameObject.Find("Button_L_Target1") },
            { "l_target2", GameObject.Find("Button_L_Target2") },
            { "r_target1", GameObject.Find("Button_R_Target1") },
            { "r_target2", GameObject.Find("Button_R_Target2") }
        };

        leftMoveButton = GameObject.Find("l_move");
        rightMoveButton = GameObject.Find("r_move");
        upMoveButton = GameObject.Find("u_move");
        downMoveButton = GameObject.Find("d_move");

        // 確保所有按鈕都初始化為禁用狀態
        foreach (var button in targetButtonMap.Values)
        {
            if (button != null)
            {
                button.SetActive(false);
            }
        }

        // 假按鈕
        if (fakeButton != null)
        {
            fakeButton.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("碰撞到物件：" + other.gameObject.name);

        // 檢查碰撞物件是否在目標列表中
        if (targetButtonMap.ContainsKey(other.gameObject.name))
        {
            // 記錄當前選中的目標物件
            currentTarget = other.gameObject;

            //Debug.Log("碰撞到目標物件：" + currentTarget.name);

            // 切換按鈕狀態
            if (fakeButton != null)
            {
                fakeButton.SetActive(false); // 隱藏假按鈕
            }

            GameObject targetButton = targetButtonMap[currentTarget.name];
            if (targetButton != null)
            {
                targetButton.SetActive(true); // 啟用對應按鈕
            }

            // 改變目標顏色
            Renderer targetRenderer = currentTarget.GetComponent<Renderer>();
            if (targetRenderer != null)
            {
                targetRenderer.material.color = Color.cyan;
            }
        }
        else if (other.gameObject.name == "left_wall")
        {
            if (leftMoveButton != null)
            {
                leftMoveButton.SetActive(false);
            }
        }
        else if (other.gameObject.name == "right_wall")
        {
            if (rightMoveButton != null)
            {
                rightMoveButton.SetActive(false);
            }
        }
        else if (other.gameObject.name == "up_wall")
        {
            if (upMoveButton != null)
            {
                upMoveButton.SetActive(false);
            }
        }
        else if (other.gameObject.name == "down_wall")
        {
            if (downMoveButton != null)
            {
                downMoveButton.SetActive(false);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 檢查離開的物件是否是當前選中的目標物件
        if (other.gameObject == currentTarget)
        {
            //Debug.Log("離開目標物件：" + currentTarget.name);

            // 還原目標顏色
            Renderer targetRenderer = currentTarget.GetComponent<Renderer>();
            if (targetRenderer != null)
            {
                targetRenderer.material.color = Color.gray;
            }

            // 切換按鈕狀態
            GameObject targetButton = targetButtonMap[currentTarget.name];
            if (targetButton != null)
            {
                targetButton.SetActive(false); // 禁用對應按鈕
            }

            if (fakeButton != null)
            {
                fakeButton.SetActive(true); // 顯示假按鈕
            }

            // 清除當前選中的目標物件
            currentTarget = null;
        }
        else if (other.gameObject.name == "left_wall")
        {
            if (leftMoveButton != null)
            {
                leftMoveButton.SetActive(true);
            }
        }
        else if (other.gameObject.name == "right_wall")
        {
            if (rightMoveButton != null)
            {
                rightMoveButton.SetActive(true);
            }
        }
        else if (other.gameObject.name == "up_wall")
        {
            if (upMoveButton != null)
            {
                upMoveButton.SetActive(true);
            }
        }
        else if (other.gameObject.name == "down_wall")
        {
            if (downMoveButton != null)
            {
                downMoveButton.SetActive(true);
            }
        }
    }
}
