using System.Collections;
using UnityEngine;
using Obi;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;  // 引用 XR Interaction Toolkit
using System;
using System.IO;

public class FluidTriggerMove : MonoBehaviour
{
    private ObiSolver solver;

    public GameObject targetObject; // **🟢 設定「液體要碰撞的物體」**
    public List<GameObject> cubes;  // **🟢 8 塊 Cube，按照順序放入**
    public List<GameObject> objectsToReset; // **🟢 需要重置的場景物件**

    public float moveDuration = 3.0f;  // **Cube 移動的總時間（秒），越大越慢**
    public float resetDelay = 3.0f;    // **場景重置前的等待時間**
    public float sideMoveDistance = 1.0f; // **左右偏移的距離**

    public static int phase = -1;  // **🟢 `phase` 為全局變數，所有腳本可修改**

    private bool isMoving = false;  // **✅ 確保只觸發一次**
    private Dictionary<GameObject, Vector3> initialPositions = new Dictionary<GameObject, Vector3>(); // **存儲場景物件初始位置**

    // ▼ 新增：紀錄「上次已經成功觸發」的階段，避免同一階段重複觸發
    private int lastTriggeredPhase = -999;

    // ▼ 新增：在 case 3 顯示的物件
    public GameObject objectToShow;
    public bool isCompleted = false;

    public TaskSystemUnit3 taskSystemUnit3;

    // ▼ 新增：fluidEmitter 與兩個需緩慢移動的物件（請依需求在 Inspector 指定）
    public ObiEmitter fluidEmitter;  // 連結 ObiEmitter
    public GameObject movingObject1;
    public GameObject movingObject2;
    public Vector3 movingObject1TargetPosition;
    public Vector3 movingObject2TargetPosition;

    void Start()
    {
        if (cubes.Count != 8)
        {
            return;
        }

        // 僅儲存需要重置的場景物件初始位置
        foreach (var obj in objectsToReset)
        {
            if (obj != null)
                initialPositions[obj] = obj.transform.position;
        }

        // 確保指定顯示的物件一開始為隱藏狀態
        if (objectToShow != null)
        {
            objectToShow.SetActive(false);
        }

        solver = FindObjectOfType<ObiSolver>();
        if (solver != null)
        {
            solver.OnCollision += HandleCollision;
        }
        else
        {
            Debug.LogError("❌ 未找到 ObiSolver！");
        }
    }

    void OnDestroy()
    {
        if (solver != null)
        {
            solver.OnCollision -= HandleCollision;
        }
    }

    private void HandleCollision(ObiSolver sender, ObiNativeContactList contacts)
    {
        // 1. 如果目前正在移動，代表同一階段尚未完成，直接略過
        if (isMoving) return;

        // 2. 如果現在的 phase 與上次已觸發的 phase 相同，代表這個階段已經啟動過了
        if (phase == lastTriggeredPhase) return;

        // 以下為 Obi 碰撞判斷邏輯
        var world = ObiColliderWorld.GetInstance();
        List<ObiColliderHandle> colliderHandles = world.colliderHandles; // 取得所有已註冊的碰撞器

        foreach (var contact in contacts.AsNativeArray())
        {
            if (contact.distance < 0.01f) // 確保發生有效碰撞
            {
                int otherIndex = contact.bodyB; // bodyB 為與液體碰撞的物件

                if (otherIndex >= 0 && otherIndex < colliderHandles.Count)
                {
                    ObiColliderBase collider = colliderHandles[otherIndex].owner;
                    if (collider != null && collider.gameObject == targetObject) // 僅在碰到 targetObject 時觸發
                    {
                        // 記錄此階段已觸發
                        lastTriggeredPhase = phase;
                        fluidEmitter.speed = 0f;
                        // 啟動移動
                        isMoving = true;
                        StartCoroutine(ProcessPhase());
                        return; // 一次碰撞觸發即可
                    }
                }
            }
        }
    }

    // 新增：將 cube 的顏色乘以指定係數，使其變暗
    private void DarkenCube(GameObject cube, float factor)
    {
        if (cube != null)
        {
            Renderer rend = cube.GetComponent<Renderer>();
            if (rend != null)
            {
                Color original = rend.material.color;
                rend.material.color = new Color(original.r * factor, original.g * factor, original.b * factor, original.a);
            }
        }
    }

    IEnumerator ProcessPhase()
    {
        switch (phase)
        {
            case 0: // **第一階段：Cube 1 & Cube 2 上升**
                yield return MoveCubes(new List<GameObject> { cubes[0], cubes[1] }, -0.5f, 'y');
                yield return new WaitForSeconds(resetDelay);
                if (fluidEmitter != null)
                {
                    fluidEmitter.speed = 0f;
                }
                if (movingObject1 != null)
                    yield return MoveObject(movingObject1, movingObject1TargetPosition, moveDuration);
                if (movingObject2 != null)
                    yield return MoveObject(movingObject2, movingObject2TargetPosition, moveDuration);
                yield return ResetSceneObjects();
                isMoving = false;
                SessionLogger.LogAction("張裂型板塊第一階段完成");
                SessionLogger.LogAction("第一層板塊突起");
                break;

            case 1: // **第二階段：Cube 1 往前 (Z+)、Cube 2 往後 (Z-)，Cube 3 & Cube 4 上升**
                // 執行移動
                StartCoroutine(MoveCubes(new List<GameObject> { cubes[0] }, cubes[0].transform.position.z + 1.5f, 'z'));
                StartCoroutine(MoveCubes(new List<GameObject> { cubes[1] }, cubes[1].transform.position.z - 1.5f, 'z'));
                StartCoroutine(MoveCubes(new List<GameObject> { cubes[2], cubes[3] }, -0.6f, 'y'));
                // 新增：將 cubes[0]、cubes[1] 顏色變暗（變深一點）
                DarkenCube(cubes[0], 0.8f);
                DarkenCube(cubes[1], 0.8f);
                yield return new WaitForSeconds(resetDelay);
                if (fluidEmitter != null)
                {
                    fluidEmitter.speed = 0f;
                }
                if (movingObject1 != null)
                    yield return MoveObject(movingObject1, movingObject1TargetPosition, moveDuration);
                if (movingObject2 != null)
                    yield return MoveObject(movingObject2, movingObject2TargetPosition, moveDuration);
                yield return ResetSceneObjects();
                isMoving = false;
                SessionLogger.LogAction("張裂型板塊第二階段完成");
                SessionLogger.LogAction("第二層板塊突起把第一層板塊推開");
                break;

            case 2: // **第三階段：Cube 1-4 繼續前後移動，Cube 5 & Cube 6 上升**
                // 執行移動
                StartCoroutine(MoveCubes(new List<GameObject> { cubes[0] }, cubes[0].transform.position.z + 1.5f, 'z'));
                StartCoroutine(MoveCubes(new List<GameObject> { cubes[1] }, cubes[1].transform.position.z - 1.5f, 'z'));
                StartCoroutine(MoveCubes(new List<GameObject> { cubes[2] }, cubes[2].transform.position.z + 1.3f, 'z'));
                StartCoroutine(MoveCubes(new List<GameObject> { cubes[3] }, cubes[3].transform.position.z - 1.3f, 'z'));
                StartCoroutine(MoveCubes(new List<GameObject> { cubes[4], cubes[5] }, -0.65f, 'y'));
                // 新增：讓 cubes[2]、cubes[3] 變暗（顏色深一點），同時 cubes[0]、cubes[1] 再變暗（更深）
                DarkenCube(cubes[2], 0.8f);
                DarkenCube(cubes[3], 0.8f);
                DarkenCube(cubes[0], 1f);
                DarkenCube(cubes[1], 1f);
                yield return new WaitForSeconds(resetDelay);
                if (fluidEmitter != null)
                {
                    fluidEmitter.speed = 0f;
                }
                if (movingObject1 != null)
                    yield return MoveObject(movingObject1, movingObject1TargetPosition, moveDuration);
                if (movingObject2 != null)
                    yield return MoveObject(movingObject2, movingObject2TargetPosition, moveDuration);
                yield return ResetSceneObjects();
                isMoving = false;
                SessionLogger.LogAction("張裂型板塊第三階段完成");
                SessionLogger.LogAction("第三層板塊突起把第二層與第一層板塊推開");
                break;

            case 3: // **第四階段：Cube 1-6 移動到 Z=615.91，Cube 7 & Cube 8 上升，並顯示指定物件**
                for (int i = 0; i < 6; i++)
                {
                    StartCoroutine(MoveCubes(new List<GameObject> { cubes[i] }, 615.91f, 'z'));
                }
                StartCoroutine(MoveCubes(new List<GameObject> { cubes[6], cubes[7] }, -0.5f, 'y'));
                // 新增：依序讓 cubes[4]、cubes[5] 變暗（顏色深一點），cubes[2]、cubes[3] 再變暗（更深），以及 cubes[0]、cubes[1] 再變暗（再更深）
                DarkenCube(cubes[4], 0.8f);
                DarkenCube(cubes[5], 0.8f);
                DarkenCube(cubes[2], 1f);
                DarkenCube(cubes[3], 1f);
                DarkenCube(cubes[0], 1.2f);
                DarkenCube(cubes[1], 1.2f);
                yield return new WaitForSeconds(resetDelay);
                if (fluidEmitter != null)
                {
                    fluidEmitter.speed = 0f;
                }
                if (movingObject1 != null)
                    yield return MoveObject(movingObject1, movingObject1TargetPosition, moveDuration);
                if (movingObject2 != null)
                    yield return MoveObject(movingObject2, movingObject2TargetPosition, moveDuration);
                // 顯示指定物件
                if (objectToShow != null)
                {
                    isCompleted = true;
                    objectToShow.SetActive(true);
                    SessionLogger.LogAction("第四層板塊突起把第三層與第二層與第一層板塊推開");
                    taskSystemUnit3.ForceCompleteTask("完成張裂型板塊邊界互動");
                    SessionLogger.LogAction("張裂型板塊互動完成");
                    string path = "/Users/eric/Desktop/My project/" +
                        $"Eric123_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                    Directory.CreateDirectory("/Users/eric/Desktop/My project/");
                    SessionLogger.Instance.SaveToFile(path);
                }
                yield return ResetSceneObjects();
                isMoving = false;
                // 若不想再觸發，可將 phase 設成 -999
                break;

            default:
                yield break;
        }
    }

    // 原有的移動 Cube 協程，依照指定軸進行線性補間
    IEnumerator MoveCubes(List<GameObject> targetCubes, float targetValue, char axis = 'y')
    {
        float elapsedTime = 0f;
        Dictionary<GameObject, Vector3> startPositions = new Dictionary<GameObject, Vector3>();
        Dictionary<GameObject, Vector3> targetPositions = new Dictionary<GameObject, Vector3>();

        foreach (var cube in targetCubes)
        {
            if (cube != null)
            {
                startPositions[cube] = cube.transform.position;
                Vector3 targetPos = startPositions[cube];

                // 根據指定軸調整目標數值
                if (axis == 'x')
                    targetPos.x = targetValue;
                else if (axis == 'y')
                    targetPos.y = targetValue;
                else if (axis == 'z')
                    targetPos.z = targetValue;

                targetPositions[cube] = targetPos;
            }
        }

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;

            foreach (var cube in targetCubes)
            {
                if (cube != null)
                {
                    cube.transform.position = Vector3.Lerp(startPositions[cube], targetPositions[cube], t);
                }
            }
            yield return null;
        }

        // 確保最終位置精準
        foreach (var cube in targetCubes)
        {
            if (cube != null)
            {
                cube.transform.position = targetPositions[cube];
            }
        }
    }

    // 新增：針對單一物件從目前位置移動到指定完整座標的協程
    // 在移動期間先禁用 XRSimpleInteractable，移動完成後再啟用
    IEnumerator MoveObject(GameObject obj, Vector3 targetPosition, float duration)
    {
        // 嘗試取得 XRSimpleInteractable 元件
        XRSimpleInteractable interactable = obj.GetComponent<XRSimpleInteractable>();
        if (interactable != null)
        {
            // 禁用 XRSimpleInteractable，避免移動期間被抓取
            interactable.enabled = false;
        }

        float elapsedTime = 0f;
        Vector3 startPosition = obj.transform.position;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
        obj.transform.position = targetPosition;

        // 移動完成後，重新啟用 XRSimpleInteractable
        if (interactable != null)
        {
            interactable.enabled = true;
        }
    }

    IEnumerator ResetSceneObjects()
    {
         yield return new WaitForSeconds(7f);

        foreach (var obj in objectsToReset)
        {
            if (obj != null && initialPositions.ContainsKey(obj))
            {
                // 回復物件至初始位置
                obj.transform.position = initialPositions[obj];
            }
        }
        yield return null;
    }
}
