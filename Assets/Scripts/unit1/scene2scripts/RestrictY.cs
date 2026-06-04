using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class RestrictY : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.up; // 移動方向
    public float moveSpeed = 1f;              // 移動速度
    private bool isMoving = false;            // 是否正在移動
    public float movestopx = 5f;              // 移動速度
    public float movestopz = 5f;              // 移動速度

    private XRSimpleInteractable simpleInteractor;
    public GameObject arrow;

    private void Awake()
    {
        // 獲取 XR Simple Interactor 組件
        simpleInteractor = GetComponent<XRSimpleInteractable>();
        if (simpleInteractor == null)
        {
            Debug.LogError("XRSimpleInteractor 組件未找到！");
        }

        arrow.SetActive(false);
    }

    private void OnEnable()
    {
        if (simpleInteractor != null)
        {
            // 註冊 selectEntered 和 selectExited 事件
            simpleInteractor.selectEntered.AddListener(OnSelectEntered);
            simpleInteractor.selectExited.AddListener(OnSelectExited);
        }
    }

    private void OnDisable()
    {
        if (simpleInteractor != null)
        {
            // 取消註冊事件
            simpleInteractor.selectEntered.RemoveListener(OnSelectEntered);
            simpleInteractor.selectExited.RemoveListener(OnSelectExited);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        isMoving = true; // 開始移動
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        isMoving = false; // 停止移動
        arrow.SetActive(false);
    }

    private void Update()
    {
        if (isMoving)
        {
            Debug.Log($"x = {transform.position.x}");
            Debug.Log($"z = {transform.position.z}");
            // 檢查物體的 x 座標是否小於 -0.9
            if (transform.position.x < movestopx)
            {
                if (transform.position.z < movestopz)
                {
                    isMoving = false; // 停止移動
                    Debug.Log($"物體停止移動，x = {transform.position.x}z = {transform.position.z}y = {transform.position.y}");

                    // 啟用 Outline22 腳本

                    return;
                }
              
            }

            // 移動物體
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }
}
