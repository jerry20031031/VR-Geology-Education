using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check : MonoBehaviour
{
    public GameObject[] targetObjects; // 需要檢查的五個物件
    public GameObject cube; // 目標 Cube 物件

    public float delayTime = 3f; // 延遲時間（秒）

    private bool isActivated = false; // 防止重複執行協程

    void Update()
    {
        if (!isActivated && AllObjectsActive())
        {
            isActivated = true;
            StartCoroutine(ActivateCubeWithDelay());
        }
    }

    private bool AllObjectsActive()
    {
        foreach (GameObject obj in targetObjects)
        {
            if (obj == null || !obj.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator ActivateCubeWithDelay()
    {

        LogSystem.instance.UpdateTaskRowByLine(3);
        LogSystem.instance.playEnd3();
        yield return new WaitForSeconds(delayTime); // 等待 delayTime 秒
        cube.SetActive(true);
    }
}
