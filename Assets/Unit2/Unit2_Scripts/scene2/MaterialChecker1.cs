using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System.IO;
using System;

public class MaterialChecker1 : MonoBehaviour
{
    public TaskSystemUnit3 taskSystem; // <-- 拖進來連結    
    public Scene2Session s2;

    // 修改為六個一維陣列，每個陣列有三個位置
    public GameObject[] targetObjectGroup1 = new GameObject[3];
    public GameObject[] targetObjectGroup2 = new GameObject[3];
    public GameObject[] targetObjectGroup3 = new GameObject[3];
    public GameObject[] targetObjectGroup4 = new GameObject[3];
    public GameObject[] targetObjectGroup5 = new GameObject[3];
    public GameObject[] targetObjectGroup6 = new GameObject[3];

    public Material lavaMaterial; // Lava 材質
    public GameObject  OKUI, resetUI, bgUI;
    public LavaFlowController lavaFlowController;
    public ResetManager resetManager;

    public Material originalMaterials; // 每組的初始材質
    public Vector3[][] originalPositions = new Vector3[6][]; // 每組的初始位置

    private const float lowerLayerMax = 0.65f;
    private const float middleLayerMax = 1.5f;

    private bool[] groupErrors = new bool[6];
    public bool isdown = false;
    public bool stopcheck = false;
    public bool hasError = false;
    public int ErrorTimes;

    bool first = false;
    
    [SerializeField] private UnityEvent onEventTrue;
    [SerializeField] private UnityEvent onEventFalse;

    void Start()
    {
        OKUI.SetActive(false);
        resetUI.SetActive(false);
        bgUI.SetActive(false);

        GameObject[][] groups = new GameObject[6][]
        {
            targetObjectGroup1, targetObjectGroup2, targetObjectGroup3,
            targetObjectGroup4, targetObjectGroup5, targetObjectGroup6
        };

        resetManager.Initialize(groups, groupErrors, this); // **讓 ResetManager 可以存取 MaterialChecker1**
    }

    void Update()
    {
        if (AreAllMaterialsChanged()) 
        {
            hasError = false; // 先重置錯誤狀態
        
            CheckLavaPositionForGroup(targetObjectGroup1, 0);
            CheckLavaPositionForGroup(targetObjectGroup2, 1);
            CheckLavaPositionForGroup(targetObjectGroup3, 2);
            CheckLavaPositionForGroup(targetObjectGroup4, 3);
            CheckLavaPositionForGroup(targetObjectGroup5, 4);
            CheckLavaPositionForGroup(targetObjectGroup6, 5);

            // **修正點：只有當所有組檢查完畢後，才決定是否觸發 UI**
            if (hasError)
            {
                if (!stopcheck) // 避免重複觸發
                {
                    stopcheck = true;
                    ShowresetUI();
                }
            }
            else
            {
                if (!stopcheck) // 確保不會誤觸發
                {
                    stopcheck = true;
                    ShowOKUI();
                    taskSystem.ForceCompleteTask("修改模型");
                    taskSystem.ForceCompleteTask("完成模型");
                    s2.SessionAction("模型拼湊正確");
                    s2.TaskOutput();
                }
            }
        }
    }

    bool AreAllMaterialsChanged()
    {
        for (int i = 0; i < 3; i++)
        {
            Material currentMaterial1 = targetObjectGroup1[i].GetComponent<Renderer>().sharedMaterial;
            Material currentMaterial2 = targetObjectGroup2[i].GetComponent<Renderer>().sharedMaterial;
            Material currentMaterial3 = targetObjectGroup3[i].GetComponent<Renderer>().sharedMaterial;
            Material currentMaterial4 = targetObjectGroup4[i].GetComponent<Renderer>().sharedMaterial;
            Material currentMaterial5 = targetObjectGroup5[i].GetComponent<Renderer>().sharedMaterial;
            Material currentMaterial6 = targetObjectGroup6[i].GetComponent<Renderer>().sharedMaterial;
            if (currentMaterial1 == originalMaterials || currentMaterial2 == originalMaterials || currentMaterial3 == originalMaterials
            || currentMaterial4 == originalMaterials || currentMaterial5 == originalMaterials || currentMaterial6 == originalMaterials)
            {
                return false;
            }
        }
        return true; // 所有物件的材質都被更換
    }

    void CheckLavaPositionForGroup(GameObject[] targetObjects, int groupIndex)
    {
        bool groupHasError = false; // 追蹤該組是否有錯誤

        foreach (var obj in targetObjects)
        {
            float yPosition = obj.transform.position.y;
            Material currentMaterial = obj.GetComponent<Renderer>().sharedMaterial;

            if (currentMaterial == lavaMaterial)
            {
                if (yPosition > lowerLayerMax)
                {
                    groupErrors[groupIndex] = true;
                    groupHasError = true; // 這組確定有錯誤
                    hasError = true; // 整體有錯誤
                }
            }
        }

        // **修正點**：如果該組有錯誤，確保 `lavaFlowController` 正確執行
        if (groupHasError)
        {
            lavaFlowController.StartLavaFlow(groupIndex);
        }
    }

    void ShowresetUI()
    {
        isdown = true;
        resetUI.SetActive(true);
        bgUI.SetActive(true);
        OKUI.SetActive(false);
        StartCoroutine(HideresetUIAfterDelay(resetUI));
    }


    IEnumerator HideresetUIAfterDelay(GameObject uiElement)
    {
        yield return new WaitForSeconds(5f);
        uiElement.SetActive(false);
        bgUI.SetActive(false);
        onEventFalse.Invoke();
        ErrorTimes++;
        s2.SessionAction($"模型拼湊錯誤:{ErrorTimes}次");
        s2.SessionError();
        if(first == false)
        {
            taskSystem.ForceCompleteTask("完成模型");
            first = true;
        }
        taskSystem.SetCurrentTaskGroup(20);
    }

    void ShowOKUI()
    {
        if (!isdown)
        {
            OKUI.SetActive(true);
            bgUI.SetActive(true);
            resetUI.SetActive(false);
            StartCoroutine(HideOKUIAfterDelay(OKUI));
            isdown = true;
        }
    }



    IEnumerator HideOKUIAfterDelay(GameObject uiElement)
    {
        yield return new WaitForSeconds(5f); // 等待 5 秒
        uiElement.SetActive(false); // 隱藏 OK UI
        bgUI.SetActive(false);
        onEventTrue.Invoke();
    }


}
