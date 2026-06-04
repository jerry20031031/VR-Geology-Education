using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class image2 : MonoBehaviour
{
    public GameObject cube1; // 第一個Cube
    public GameObject cube2; // 第二個Cube

    public Button rawImage1Interactable; // RawImage1的互動組件
    public Button rawImage2Interactable; // RawImage2的互動組件


    public RawImage rawImage1; // RawImage1組件
    public RawImage rawImage2; // RawImage2組件

    private Color defaultColor1; // RawImage1的預設顏色
    private Color defaultColor2; // RawImage2的預設顏色

    private void Start()
    {
        // 初始化隱藏所有的Cube
        cube1.SetActive(false);
        cube2.SetActive(false);

        // 記錄每個RawImage的預設顏色
        defaultColor1 = rawImage1.color;
        defaultColor2 = rawImage2.color;


        // 為每個RawImage添加互動事件
        rawImage1Interactable.onClick.AddListener(OnRawImage1Selected);
        rawImage2Interactable.onClick.AddListener(OnRawImage2Selected);


    }

    private void OnDestroy()
    {
        // 移除事件訂閱（防止內存洩漏）
        rawImage1Interactable.onClick.RemoveListener(OnRawImage1Selected);
        rawImage2Interactable.onClick.RemoveListener(OnRawImage2Selected);
    }

    private void ResetRawImages()
    {
        // 隱藏所有Cube
        cube1.SetActive(false);
        cube2.SetActive(false);

        // 恢復所有RawImage的顏色
        rawImage1.color = defaultColor1;
        rawImage2.color = defaultColor2;
    }

    private void OnRawImage1Selected()
    {
        ResetRawImages();
        cube1.SetActive(true);
       // rawImage1.color = Color.red;
    }

    private void OnRawImage2Selected()
    {
        ResetRawImages();
        cube2.SetActive(true);
        //rawImage2.color = Color.red;
    }
}
