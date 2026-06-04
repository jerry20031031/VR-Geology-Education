using UnityEngine;
using System.Collections.Generic;

public class DragObject:MonoBehaviour
{
    public Transform rayOrigin;        // 射线的起点（控制器位置）
    public LayerMask DraggableLayer; // 可拖動的物件層
    public float columnThreshold = 0.1f; // 同一縱列的 X 或 Z 差值閾值

    private List<GameObject> columnObjects = new List<GameObject>(); // 存儲同縱列物件
    private Vector3 initialOffset;      // 拖動偏移量
    private Plane movementPlane;        // 移動平面
    private bool isDragging = false;    // 是否正在拖動
    private Vector3 dragStartPosition;  // 拖動起始位置

    void Update()
    {
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        RaycastHit hit;

        if (Input.GetButtonDown("Fire1")) // 按下按鍵檢測射线碰撞
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, DraggableLayer))
            {
                GameObject selectedObject = hit.collider.gameObject;

                // 找到同一縱列的物件
                FindColumnObjects(selectedObject);

                // 啟用 Outline
                HighlightColumnObjects(true);

                // 設定移動平面
                movementPlane = new Plane(Vector3.up, selectedObject.transform.position);
                float distance;
                if (movementPlane.Raycast(ray, out distance))
                {
                    Vector3 hitPoint = ray.GetPoint(distance);
                    initialOffset = selectedObject.transform.position - hitPoint;
                    dragStartPosition = selectedObject.transform.position;
                }

                isDragging = true;
            }
        }

        if (isDragging && Input.GetButton("Fire1")) // 按住按鍵進行拖動
        {
            float distance;
            if (movementPlane.Raycast(ray, out distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                Vector3 dragDelta = hitPoint + initialOffset - dragStartPosition;

                // 更新所有物件的位置
                foreach (GameObject obj in columnObjects)
                {
                    obj.transform.position += dragDelta;
                }

                // 更新起始位置
                dragStartPosition = hitPoint + initialOffset;
            }
        }

        if (Input.GetButtonUp("Fire1")) // 釋放按鍵
        {
            // 禁用 Outline
            HighlightColumnObjects(false);

            // 重置狀態
            columnObjects.Clear();
            isDragging = false;
        }
    }

    void FindColumnObjects(GameObject selectedObject)
    {
        columnObjects.Clear();

        // 獲取選中物件的 X 或 Z 坐標
        Vector3 selectedPosition = selectedObject.transform.position;

        // 搜索所有可拖動物件
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("TargetObject"); // 需要分配統一的標籤
        foreach (GameObject obj in allObjects)
        {
            Vector3 objPosition = obj.transform.position;

            // 判斷是否屬於同一縱列
            if (Mathf.Abs(selectedPosition.x - objPosition.x) < columnThreshold)
            {
                columnObjects.Add(obj);
            }
        }
    }

    void HighlightColumnObjects(bool enable)
    {
        foreach (GameObject obj in columnObjects)
        {
            Outline outline = obj.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = enable;
            }
        }
    }
}
