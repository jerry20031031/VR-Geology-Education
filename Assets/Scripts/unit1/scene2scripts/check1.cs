using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class check1 : MonoBehaviour
{
 public GameObject[] targetObjects; // 需要檢查的物件
    public GameObject cubeToActivate; // 要顯示的 Cube
    //private bool cubeShown = false; // 確保 Cube 只顯示一次

    // 七個物件的相關參數
    public GameObject[] objectsToMove; // 要移動的物件
    public Vector3[] initialPositions; // 初始位置
    public Vector3[] moveSpeeds; // 每秒移動的速度向量（xz 平面）
    public Vector3[] rotationAngles; // 移動期間的旋轉角度
    public float moveDuration = 3f; // 運動持續時間
    private bool isMoving = false; // 防止多次啟動
    void Start()
    {
        // 確保 Cube 一開始是隱藏的
        if (cubeToActivate != null)
        {
            cubeToActivate.SetActive(false);
        }

        
    }

    /*void Update()
    {
        // 如果 Cube 已經顯示，停止檢查
        if (cubeShown) return;

        // 檢查所有物件的 outline22 腳本
        bool allActive = true;
        foreach (GameObject obj in targetObjects)
        {
            outline22 outline = obj.GetComponent<outline22>();
            if (outline == null || !outline.enabled)
            {
                allActive = false;
                break;
            }
        }

        // 如果所有物件的 outline22 都啟用了
        if (allActive)
        {
            if (cubeToActivate != null)
            {
                cubeToActivate.SetActive(true);
                cubeShown = true; // 停止檢查
                                  // 初始化物件位置
                if (objectsToMove != null && initialPositions != null && objectsToMove.Length == initialPositions.Length)
                {
                    for (int i = 0; i < objectsToMove.Length; i++)
                    {
                        objectsToMove[i].transform.position = initialPositions[i];
                    }
                }
                // 啟動七個物件移動功能
                StartCoroutine(DelayedMoveObjects());
            }
        }
    }*/

    public void StartMovingObjects()
    {
        // 防止重複啟動
        if (isMoving) return;
        isMoving = true; // 標記為正在執行
        // 初始化物件位置
        if (objectsToMove != null && initialPositions != null && objectsToMove.Length == initialPositions.Length)
        {
            for (int i = 0; i < objectsToMove.Length; i++)
            {
                objectsToMove[i].transform.position = initialPositions[i];
            }
        }
        if (isMoving)
        {
            StopAllCoroutines();
            ResetObjectsState();
        }

        // 啟動移動功能
        StartCoroutine(DelayedMoveObjects());
    }
    private IEnumerator DelayedMoveObjects()
    {
        yield return new WaitForSeconds(1f); // 延遲 1 秒
        yield return StartCoroutine(MoveObjectsCoroutine());
        isMoving = false; // 移動結束後允許再次執行
    }
    private IEnumerator MoveObjectsCoroutine()
    {
        float elapsedTime = 0f;

        Vector3[] startPositions = new Vector3[objectsToMove.Length];
        Quaternion[] startRotations = new Quaternion[objectsToMove.Length];
        Quaternion[] targetRotations = new Quaternion[objectsToMove.Length];

        for (int i = 0; i < objectsToMove.Length; i++)
        {
            startPositions[i] = objectsToMove[i].transform.position;
            startRotations[i] = objectsToMove[i].transform.rotation;
            targetRotations[i] = Quaternion.Euler(rotationAngles[i]);
        }

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;

            for (int i = 0; i < objectsToMove.Length; i++)
            {
                if (objectsToMove[i] != null)
                {
                    if (i == 4 && elapsedTime < 2f) continue;
                    if (i == 3 && elapsedTime < 1f) continue;
                    // 計算每秒移動量
                    Vector3 deltaMove = moveSpeeds[i] * Time.deltaTime;

                    // 更新位置
                    objectsToMove[i].transform.position += new Vector3(deltaMove.x, 0, deltaMove.z);

                   
                }
            }

            yield return null;
        }

        // 確保到達最終狀態
        for (int i = 0; i < objectsToMove.Length; i++)
        {
            if (objectsToMove[i] != null)
            {
                objectsToMove[i].transform.position = startPositions[i] + moveSpeeds[i] * moveDuration;
                objectsToMove[i].transform.rotation = targetRotations[i];
            }
        }
        for (int i = 1; i < objectsToMove.Length; i++)
        {
            if (objectsToMove[i] != null)
            {
                objectsToMove[i].transform.Rotate(0, 180, 0); // 在 Y 軸上旋轉 180 度
                objectsToMove[0].transform.Rotate(0, -365, 0); // 在 Y 軸上旋轉 180 度
                objectsToMove[1].transform.Rotate(0, 90, 0); // 在 Y 軸上旋轉 180 度
            }
        }
    }
    public void ToggleOption(int optionIndex)
    {
        // 停止所有協程
        StopAllCoroutines();

        // 重置物體狀態
        ResetObjectsState();

        // 顯示對應的選項物體
        ActivateOption(optionIndex);
    }

    private void ResetObjectsState()
    {
        if (objectsToMove != null && initialPositions != null && objectsToMove.Length == initialPositions.Length)
        {
            for (int i = 0; i < objectsToMove.Length; i++)
            {
                if (objectsToMove[i] != null)
                {
                    objectsToMove[i].transform.position = initialPositions[i];
                    
                }
            }
        }

        isMoving = false; // 重置移動狀態
    }

    private void ActivateOption(int optionIndex)
    {
        for (int i = 0; i < targetObjects.Length; i++)
        {
            if (targetObjects[i] != null)
            {
                targetObjects[i].SetActive(i == optionIndex); // 只啟用選中的選項物體
            }
        }
    }
}
