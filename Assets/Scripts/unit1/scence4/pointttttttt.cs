using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointttttttt : MonoBehaviour
{
    public Transform rectangle; // 長方體的 Transform
    public float maxDistance = 10f; // 最大距離限制
    public GameObject finishcube;

    void Update()
    {
        if (rectangle == null)
        {
            Debug.LogWarning("請指定長方體的 Transform！");
            return;
        }

        // 獲取所有帶有 "Rock" 標籤的物件
        GameObject[] rocks = GameObject.FindGameObjectsWithTag("Rock");

        Transform closestRock = null;
        float closestDistance = maxDistance; // 設定初始最近距離為最大距離

        foreach (GameObject rock in rocks)
        {
            Rock rockScript = rock.GetComponent<Rock>();

            // 確保該物件有 Rock 腳本，且 isOnTable 為 true
            if (rockScript != null && rockScript.isOnTable)
            {
                float distance = Vector3.Distance(rock.transform.position, rectangle.position);

                // 找到距離最近且小於 maxDistance 的 Rock
                if (distance < closestDistance)
                {
                    closestRock = rock.transform;
                    closestDistance = distance;

                    StartCoroutine(EnableCubeWithDelay(5f));
                    //LogSystem.LogAction("學習者得知冷卻後晶體有磁性");
                }
            }
        }

        // 如果找到了符合條件的最近 Rock，讓長方體朝向它
        if (closestRock != null)
        {
            Vector3 direction = closestRock.position - rectangle.position;
            rectangle.rotation = Quaternion.LookRotation(direction);
        }
    }
    IEnumerator EnableCubeWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (finishcube != null)
        {
            finishcube.SetActive(true);
        }
    }
}