using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RegionSocket : MonoBehaviour
{
    private List<XRSocketInteractor> colliders = new List<XRSocketInteractor>(); // 存放 XRSocketInteractor
    public List<GameObject> gameObjects = new List<GameObject>(); // 一般物件
    public GameObject specialObject; // 特殊物件

    public TextMeshProUGUI generalObjectCounterText; // 一般物件计数 UI
    public TextMeshProUGUI specialObjectCounterText; // 特殊物件计数 UI
    public Talk Talker; // 对话控制

    private int matchedCount = 0; // 一般物件匹配计数
    private bool specialObjectOnTable = false; // 是否有特殊物件
    private bool spacial = false;

    void Start()
    {
        colliders.AddRange(GetComponentsInChildren<XRSocketInteractor>());

    }
    private void OnEnable()
    {
        DetectObject();

    }

    public void DetectObject()
    {
        matchedCount = 0; // 重置一般物件计数
        specialObjectOnTable = false; // 重置特殊物件状态

        foreach (var socket in colliders)
        {
            IXRSelectInteractable interactable = socket.GetOldestInteractableSelected();
            if (interactable != null)
            {
                GameObject socketObject = interactable.transform.gameObject;

                // 检查是否是一般物件
                if (gameObjects.Contains(socketObject))
                {
                    matchedCount++;
                }

                // 检查是否是特殊物件
                if (socketObject == specialObject)
                {
                    specialObjectOnTable = true;
                }
                socketObject.transform.Find("Hint").GetComponent<Hint>().ExitPoint();
            }
        }

        Debug.Log($"匹配的一般物件数量: {matchedCount}");
        UpdateUIText(); // 更新 UI
    }
    public void DetectObjectWithDelay()
    {
        StartCoroutine(DelayDetect());
    }

    private IEnumerator DelayDetect()
    {
        yield return new WaitForEndOfFrame(); // 或 yield return null;
        DetectObject();
    }

    private void UpdateUIText()
    {
        // 更新一般物件的计数
        generalObjectCounterText.text = $"一般物件: {matchedCount}/3";
        // 更新特殊物件的状态
        specialObjectCounterText.text = (specialObjectOnTable || spacial) ? "特殊線索: 1/1" : "特殊線索: 0/1";


        // 触发对话
        if ((specialObjectOnTable || spacial) && matchedCount == 3 && this.gameObject.activeSelf)
        {
            Talker.Conversationindex(1);
        }
        else
        {
            Talker.Conversationindex(2);
        }
    }
    public void spacialComplete()
    {
        spacial = true;
        generalObjectCounterText.text = $"一般物件: {matchedCount}/3";
        // 更新特殊物件的状态
        specialObjectCounterText.text = (specialObjectOnTable || spacial) ? "特殊線索: 1/1" : "特殊線索: 0/1";
    }
}
