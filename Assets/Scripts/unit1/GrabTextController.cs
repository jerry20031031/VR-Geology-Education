using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabTextController : MonoBehaviour
{
    public GameObject textTMP; 
    public GameObject textTMP1;
    public GameObject Image;
    public GameObject sound;
    public Vector3 targetPosition; // 新增目標位置變數
    private Vector3 originalPosition;
    private Quaternion initialRotation; // 儲存物件的初始旋轉
    private XRSocketInteractor currentSocket; // 儲存當前的Socket Interactor
    public GameObject plane;
    public GameObject arrow;
    public GameObject rocktext;  //每次抓取都要把證據的那個text關掉

    public GameObject mountainText;  //每次抓取都要把證據的那個text關掉
    public GameObject[] Else; // 新增一個陣列來存取
    public RawImage rawImage2; // RawImage2組件
    private void Start()
    {
        // 確保Text一開始是隱藏的
        if (textTMP != null)
        {
            textTMP.SetActive(false);
            textTMP1.SetActive(false);
            Image.SetActive(false);
            sound.SetActive(false);
        }

        // 記錄物體的原本位置
        originalPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void OnEnable()
    {
        // 訂閱XR Grab Interactable的事件
        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }
    }

    private void OnDisable()
    {
        // 取消訂閱事件
        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
            grabInteractable.selectExited.RemoveListener(OnReleased);
        }

    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // 檢查是否抓取發生在XRSocketInteractor中
        if (args.interactorObject is XRSocketInteractor socketInteractor)
        {
            // 確保Text保持隱藏
            if (textTMP != null)
            {
                textTMP.SetActive(false);
                textTMP1.SetActive(false);
                Image.SetActive(false);
                sound.SetActive(false);
            }
        }
        else
        {
            // 顯示Text (TMP)
            if (textTMP != null)
            {
                foreach (GameObject obj in Else)
                {
                    obj.SetActive(false);
                }
                rocktext.SetActive(false);
                mountainText.SetActive(false);
                textTMP.SetActive(true);
                textTMP1.SetActive(true);
                Image.SetActive(true);
                sound.SetActive(true);
                rawImage2.color = Color.white;
            }
        }
        if(plane.activeInHierarchy)
        {
            arrow.SetActive(true);
        }
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        // 隱藏Text (TMP)
        if (textTMP != null)
        {
            textTMP.SetActive(false);
            textTMP1.SetActive(false);
            Image.SetActive(false);
            sound.SetActive(false);
        }
        // 檢查物體是否在原本位置和目標位置之間的其他點
        /*if (transform.position != originalPosition && transform.position != targetPosition)
        {
            // 將物體移回原本位置
            transform.position = originalPosition;
            transform.rotation = initialRotation;
        }*/
    }
}
