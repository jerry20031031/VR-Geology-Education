using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class rockcount : MonoBehaviour
{
    private int requiredRocks = 5;
    private int currentRocks = 0;
    private bool isActivated = false; // 是否已經啟動變色腳本

    public TMP_Text rockCountText;

    public GameObject finishcube;

    private void Start()
    {
        UpdateRockCountUI(); // 初始化時更新 UI
    }

    // 當石頭進入桌子的範圍
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Rock") && !isActivated)
    //     {
    //         currentRocks++;
    //         Debug.Log($"桌上目前有 {currentRocks} 顆石頭");
    //         UpdateRockCountUI();

    //         if (currentRocks >= requiredRocks)
    //         {
    //             ActivateRockColorChange();
    //         }
    //     }
    // }

    // // 當石頭離開桌子的範圍
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Rock") && !isActivated)
    //     {
    //         currentRocks--;
    //         Debug.Log($"桌上目前有 {currentRocks} 顆石頭");
    //         UpdateRockCountUI();
    //     }
    // }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock") && !isActivated)
        {
            Rock rockScript = other.GetComponent<Rock>();
            if (rockScript != null && !rockScript.isOnTable) // 確保石頭是第一次進入桌子
            {
                currentRocks++;
                rockScript.isOnTable = true; // 標記為在桌子上
                Debug.Log($"桌上目前有 {currentRocks} 顆石頭");
                UpdateRockCountUI();

                if (currentRocks >= requiredRocks)
                {
                    ActivateRockColorChange();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rock") && !isActivated)
        {
            Rock rockScript = other.GetComponent<Rock>();
            if (rockScript != null && rockScript.isOnTable) // 確保石頭是從桌子上離開
            {
                currentRocks--;
                rockScript.isOnTable = false; // 標記為不在桌子上
                Debug.Log($"桌上目前有 {currentRocks} 顆石頭");
                UpdateRockCountUI();
            }
        }
    }

    // 啟動變色腳本
    private void ActivateRockColorChange()
    {
        isActivated = true;
        Debug.Log("石頭達到目標數量，啟動變色腳本！");

        // 找到所有在桌子上的石頭並啟動變色
        Collider[] rocks = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity); // 桌子區域
        foreach (var rock in rocks)
        {
            if (rock.CompareTag("Rock"))
            {
                // Debug.Log("變色腳本！");
                // color2 colorScript = rock.GetComponent<color2>();
                // if (colorScript != null)
                // {
                //     // 啟動冷卻過程
                //     colorScript.StartCooling();
                // }
                Rock rockScript = rock.GetComponent<Rock>();
                if (rockScript != null && rockScript.isOnTable) // 只處理在桌子上的石頭
                {
                    Debug.Log("變色腳本！");
                    color2 colorScript = rock.GetComponent<color2>();
                    if (colorScript != null)
                    {
                        // 啟動冷卻過程
                        colorScript.StartCooling();
                        StartCoroutine(EnableCubeWithDelay(10f));
                    }
                }
            }
        }
    }

    private IEnumerator EnableCubeWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (finishcube != null)
        {
            finishcube.SetActive(true);
        }
    }
    private void UpdateRockCountUI()
    {
        if (rockCountText != null)
        {
            rockCountText.text = $"{currentRocks}/{requiredRocks}";
        }
    }
}

