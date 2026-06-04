using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class initial : MonoBehaviour
{
    public float detectionRadius = 5f; // 偵測範圍
    public GameObject cubePrefab; // 用來顯示的 Cube 預製件
    private GameObject cubeInstance;

    public CharacterController playerController; // 角色控制器

    private void Start()
    {
        if (cubePrefab != null)
        {
            cubeInstance = Instantiate(cubePrefab, transform.position, Quaternion.identity);
            cubeInstance.SetActive(false); // 初始時隱藏
        }
    }

    private void Update()
    {
        CheckPlayerPosition();
    }

    void CheckPlayerPosition()
    {
        if (playerController == null)
        {
            Debug.LogWarning("PlayerController is null.");
            return;
        }

        float distance = Vector3.Distance(playerController.transform.position, transform.position);
        bool characterFound = distance < detectionRadius;

        // 顯示或隱藏 Cube
        if (cubeInstance != null)
        {
            cubeInstance.SetActive(characterFound);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
