using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class MaterialChecker : MonoBehaviour
{
    // 修改為六個一維陣列，每個陣列有三個位置
    public GameObject[] targetObjectGroup1 = new GameObject[3];
    public GameObject[] targetObjectGroup2 = new GameObject[3];
    public GameObject[] targetObjectGroup3 = new GameObject[3];
    public GameObject[] targetObjectGroup4 = new GameObject[3];
    public GameObject[] targetObjectGroup5 = new GameObject[3];
    public GameObject[] targetObjectGroup6 = new GameObject[3];

    public Material lavaMaterial; // Lava 材質
    public Material originalMaterials; // 每組的初始材質
    public Vector3[][] originalPositions = new Vector3[6][]; // 每組的初始位置
    public GameObject  OKUI, resetUI, bgUI;

    public LavaFlowController lavaFlowController;

    private const float lowerLayerMax = 0.65f;
    private const float middleLayerMax = 1.5f;
    private bool[] groupErrors = new bool[6];
    private bool isdown = false;
    private bool stopcheck = false;
    private bool hasError = false;
    
    [SerializeField] private UnityEvent onEventTrue;
    [SerializeField] private UnityEvent onEventFalse;

    void Start()
    {
        // 初始時禁用 UI
        OKUI.SetActive(false);
        resetUI.SetActive(false);
        bgUI.SetActive(false);
        ResetModels();
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
        uiElement.SetActive(false); // 隱藏重置 UI
        bgUI.SetActive(false);
        onEventFalse?.Invoke();
        ResetModels();
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
        onEventTrue?.Invoke();
    }


    void ResetModels()
    {
        GameObject[][] targetObjectGroups = new GameObject[6][]
        {
            targetObjectGroup1,
            targetObjectGroup2,
            targetObjectGroup3,
            targetObjectGroup4,
            targetObjectGroup5,
            targetObjectGroup6
        };

        for (int i = 0; i < targetObjectGroups.Length; i++)
        {
            for (int j = 0; j < targetObjectGroups[i].Length; j++)
            {
                targetObjectGroups[i][j].GetComponent<Renderer>().sharedMaterial = originalMaterials;
            }
        }

        for(int i = 0; i < 6; i++)
        {
            groupErrors[i] = false;
        }

        hasError = false;
        stopcheck = false; // **修正點：允許新一輪檢查**
        isdown = false;
        lavaFlowController.StopLavaFlow();
    }


}
