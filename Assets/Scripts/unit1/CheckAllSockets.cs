using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CheckAllSockets : MonoBehaviour
{
    // 將六個 XR Socket Interactor 指定在 Inspector 中
    public List<XRSocketInteractor> socketInteractors;

    // 控制門的兩側 Transform（例如門的左側和右側）
    public Transform leftDoor;
    public Transform rightDoor;
    public GameObject checkAllCube;
    public GameObject schoolGirl;    
    public GameObject professor;
    // 新增：目標位置
    public Vector3 schoolGirlTarget;
    public Vector3 professorTarget;


    // 開門的目標位置和速度
    public float openPositionY = 3f;
    public float openSpeed = 2f;

    private bool allObjectsInserted = false;
    private bool hasWegnerPointGiven = false; // ← 加在 class 最上面當旗標
    public TaskSystem taskSystem;

    void Start()
    {
        // 訂閱每個 Socket 的 SelectEntered 事件
        foreach (var socket in socketInteractors)
        {
            socket.selectEntered.AddListener(CheckAllSocketsStatus);
        }
        LogSystem.instance.TotalKnowledgePoints = 2;
    }

    private void CheckAllSocketsStatus(SelectEnterEventArgs args)
    {
        // 檢查是否所有的 Socket 都有物件插入
        allObjectsInserted = true;
        foreach (var socket in socketInteractors)
        {
            if (!socket.hasSelection)
            {
                allObjectsInserted = false;
                break;
            }
        }

        // 如果所有插槽都插入了物件開始開門
        if (allObjectsInserted)
        {
            taskSystem.ForceCompleteTask("拼湊盤古大陸");
            checkAllCube.SetActive(true);
            OpenDoors();
            // 直接設定位置
            schoolGirl.transform.position = schoolGirlTarget;
            professor.transform.position = professorTarget;
        }
    }

    private void OpenDoors()
    {
        if (!hasWegnerPointGiven)
        {
            LogSystem.instance.UpdateTaskRowByLine(1);
            LogSystem.instance.playEnd1();
            hasWegnerPointGiven = true; // 設定為已給予
        }
        // 使用 Coroutine 讓門平滑移動
        StartCoroutine(OpenDoorRoutine());
    }

    private System.Collections.IEnumerator OpenDoorRoutine()
    {
        Vector3 leftTarget = new Vector3(leftDoor.position.x, openPositionY, leftDoor.position.z);
        Vector3 rightTarget = new Vector3(rightDoor.position.x, openPositionY, rightDoor.position.z);

        while (Vector3.Distance(leftDoor.position, leftTarget) > 0.01f || Vector3.Distance(rightDoor.position, rightTarget) > 0.01f)
        {
            leftDoor.position = Vector3.MoveTowards(leftDoor.position, leftTarget, openSpeed * Time.deltaTime);
            rightDoor.position = Vector3.MoveTowards(rightDoor.position, rightTarget, openSpeed * Time.deltaTime);
            yield return null;
        }
    }
    

    private void OnDestroy()
    {
        // 解除事件訂閱
        foreach (var socket in socketInteractors)
        {
            socket.selectEntered.RemoveListener(CheckAllSocketsStatus);
        }
    }
}
