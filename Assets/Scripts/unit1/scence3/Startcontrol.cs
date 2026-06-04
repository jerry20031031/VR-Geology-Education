using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Startcontrol : MonoBehaviour
{
    public XRBaseInteractable button; // VR 按鈕
    public GameObject object1; // 第一個移動的物體
    public GameObject object2; // 第二個移動的物體
    public float moveSpeed = 1.0f; // Z 軸移動速度
    public float moveDistance = 5.0f; // **設定移動距離**

    private bool isMoving = false;
    private Vector3 startPos1; // `object1` 的起始位置
    private Vector3 startPos2; // `object2` 的起始位置

    public GameObject riseObject; // 上升的物體
    public float riseDistance = 2.0f; // 上升距離
    public float moveDelay = 2.0f; // 延遲秒數
    private Vector3 riseStartPos; // `riseObject` 的起始位置
    public GameObject effect;


    void Start()
    {
        if (button != null)
        {
            button.selectEntered.AddListener(OnButtonPressed);
        }
    }

    /*void OnButtonPressed(SelectEnterEventArgs args)
    {
        Debug.Log("Button pressed!");
        isMoving = true;
        startPos1 = object1.transform.position; // 記錄 `object1` 初始位置
        startPos2 = object2.transform.position; // 記錄 `object2` 初始位置

        ActivateAllRockRotations();
    }*/

    void OnButtonPressed(SelectEnterEventArgs args)
    {
        Debug.Log("Button pressed!");
        riseStartPos = riseObject.transform.position; // 記錄 `riseObject` 初始位置
        StartCoroutine(StartMovementSequence());
        ActivateAllRockRotations();
    }

    IEnumerator StartMovementSequence()
    {
        // 上升物件先動作
        StartCoroutine(RiseObject(riseObject, riseDistance, 2.0f));
        yield return new WaitForSeconds(moveDelay);
        effect.SetActive(true);

        isMoving = true;
        startPos1 = object1.transform.position; // 記錄 `object1` 初始位置
        startPos2 = object2.transform.position; // 記錄 `object2` 初始位置
    }

    IEnumerator RiseObject(GameObject obj, float distance, float duration)
    {
        float elapsed = 0f;
        Vector3 start = obj.transform.position;
        Vector3 end = start + new Vector3(0, distance, 0);

        while (elapsed < duration)
        {
            obj.transform.position = Vector3.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = end;
    }

    void Update()
    {
        if (isMoving)
        {
            bool object1Reached = HasMovedEnough(object1, startPos1);
            bool object2Reached = HasMovedEnough(object2, startPos2);

            if (!object1Reached)
            {
                MoveObjectWithGroundHeight(object1, moveSpeed);
            }

            if (!object2Reached)
            {
                MoveObjectWithGroundHeight(object2, -moveSpeed);
            }

            // 當兩個物體都移動足夠距離後，停止
            if (object1Reached && object2Reached)
            {
                isMoving = false;
            }
        }
    }

    // **判斷物體是否已經移動足夠距離**
    bool HasMovedEnough(GameObject obj, Vector3 startPos)
    {
        return Vector3.Distance(startPos, obj.transform.position) >= moveDistance;
    }

    void MoveObjectWithGroundHeight(GameObject obj, float zSpeed)
    {
        Vector3 newPosition = obj.transform.position + new Vector3(0, 0, zSpeed * Time.deltaTime);

        // 用 Raycast 來找到地形高度
        RaycastHit hit;
        if (Physics.Raycast(newPosition + Vector3.up * 5, Vector3.down, out hit, 10f))
        {
            newPosition.y = hit.point.y - 0.1f; // 讓物體稍微重疊地形，避免懸空
        }

        obj.transform.position = newPosition;
    }

    void ActivateAllRockRotations()
    {
        rockrotation[] rockRotations = FindObjectsOfType<rockrotation>(); // 找到場景內所有 `RockRotation` 腳本
        foreach (rockrotation rock in rockRotations)
        {
            if (!rock.enabled) // 確保未啟動時才啟動
            {
                rock.enabled = true;
            }
        }
    }
}