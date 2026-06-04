using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class image : MonoBehaviour
{
    public GameObject cube1; // 第一個Cube
    public GameObject cube2; // 第二個Cube
    public GameObject cube3; // 第三個Cube
    public GameObject cube4; // 第四個Cube

    public Button rawImage1Interactable; // RawImage1的互動組件
    public Button rawImage2Interactable; // RawImage2的互動組件
    public Button rawImage3Interactable; // RawImage3的互動組件
    public Button rawImage4Interactable; // RawImage4的互動組件

    public Image rawImage1; // RawImage1組件
    public Image rawImage2; // RawImage2組件
    public Image rawImage3; // RawImage3組件
    public Image rawImage4; // RawImage4組件

    private Color defaultColor1; // RawImage1的預設顏色
    private Color defaultColor2; // RawImage2的預設顏色
    private Color defaultColor3; // RawImage3的預設顏色
    private Color defaultColor4; // RawImage4的預設顏色

    public GameObject error1;
    public GameObject error3;
    public GameObject error4;

    public GameObject cube11; // 第一個Cube
    public GameObject cube21; // 第二個Cube
    public GameObject cube31; // 第三個Cube
    public GameObject cube41; // 第四個Cube
    private void Start()
    {
        // 初始化隱藏所有的Cube
        cube1.SetActive(false);
        cube2.SetActive(false);
        cube3.SetActive(false);
        cube4.SetActive(false);

        cube11.SetActive(false);
        cube21.SetActive(false);
        cube31.SetActive(false);
        cube41.SetActive(false);

        // 記錄每個RawImage的預設顏色
        defaultColor1 = rawImage1.color;
        defaultColor2 = rawImage2.color;
        defaultColor3 = rawImage3.color;
        defaultColor4 = rawImage4.color;

        // 為每個RawImage添加互動事件
        rawImage1Interactable.onClick.AddListener(OnRawImage1Selected);
        rawImage2Interactable.onClick.AddListener(OnRawImage2Selected);
        rawImage3Interactable.onClick.AddListener(OnRawImage3Selected);
        rawImage4Interactable.onClick.AddListener(OnRawImage4Selected);
    }

    private void OnDestroy()
    {
        // 移除事件訂閱（防止內存洩漏）
        rawImage1Interactable.onClick.RemoveListener(OnRawImage1Selected);
        rawImage2Interactable.onClick.RemoveListener(OnRawImage2Selected);
        rawImage3Interactable.onClick.RemoveListener(OnRawImage3Selected);
        rawImage4Interactable.onClick.RemoveListener(OnRawImage4Selected);
    }

    private void ResetRawImages()
    {
        // 隱藏所有Cube
        cube1.SetActive(false);
        cube2.SetActive(false);
        cube3.SetActive(false);
        cube4.SetActive(false);

        // 恢復所有RawImage的顏色
        rawImage1.color = defaultColor1;
        rawImage2.color = defaultColor2;
        rawImage3.color = defaultColor3;
        rawImage4.color = defaultColor4;
    }

    private void OnRawImage1Selected()
    {
        ResetRawImages();
        cube1.SetActive(true);
        rawImage1.color = Color.green;
        error3.SetActive(false);
        error4.SetActive(false);
        cube11.SetActive(true);
        cube21.SetActive(false);
        cube31.SetActive(false);
        cube41.SetActive(false);

    }

    private void OnRawImage2Selected()
    {
        ResetRawImages();
        cube2.SetActive(true);
        rawImage2.color = Color.green;
        error1.SetActive(false);
        error3.SetActive(false);
        error4.SetActive(false);
        cube21.SetActive(true);
        cube11.SetActive(false);
        cube31.SetActive(false);
        cube41.SetActive(false);
    }

    private void OnRawImage3Selected()
    {
        ResetRawImages();
        cube3.SetActive(true);
        rawImage3.color = Color.green;
        error1.SetActive(false);
        error4.SetActive(false);
        cube31.SetActive(true);
        cube11.SetActive(false);
        cube21.SetActive(false);
        cube41.SetActive(false);
    }

    private void OnRawImage4Selected()
    {
        ResetRawImages();
        cube4.SetActive(true);
        rawImage4.color = Color.green;
        error1.SetActive(false);
        error3.SetActive(false);
        cube41.SetActive(true);
        cube11.SetActive(false);
        cube21.SetActive(false);
        cube31.SetActive(false);
    }
}
