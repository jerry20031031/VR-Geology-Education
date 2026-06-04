using UnityEngine;

public class DashedOutlineScript : MonoBehaviour
{
    public Material dashedLineMaterial;

    void Start()
    {
        CreateDashedOutline();
    }

  void CreateDashedOutline()
{
    // 檢查是否已經有 LineRenderer 組件
    LineRenderer lineRenderer = GetComponent<LineRenderer>();
    if (lineRenderer == null)
    {
        // 如果沒有，則添加一個新的 LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    lineRenderer.material = dashedLineMaterial; // 設置虛線材質

    lineRenderer.startWidth = 0.02f;  // 起始線寬
    lineRenderer.endWidth = 0.02f;    // 結束線寬
    lineRenderer.loop = false;         // 閉環
    lineRenderer.positionCount = 16;   // 設置8個角點位置

    Renderer objectRenderer = GetComponent<Renderer>();
    if (objectRenderer == null)
    {
        Debug.LogError("Renderer component is missing on this object!");
        return; // 如果沒有Renderer組件，就退出
    }

    Bounds bounds = objectRenderer.bounds;

    lineRenderer.SetPositions(new Vector3[]
    {
        new Vector3(bounds.max.x, bounds.min.y, bounds.min.z),
        new Vector3(bounds.min.x, bounds.min.y, bounds.min.z),
        new Vector3(bounds.min.x, bounds.min.y, bounds.max.z),
        new Vector3(bounds.max.x, bounds.min.y, bounds.max.z),
        new Vector3(bounds.max.x, bounds.min.y, bounds.min.z),
        new Vector3(bounds.max.x, bounds.max.y, bounds.min.z),
        new Vector3(bounds.max.x, bounds.max.y, bounds.max.z),
        new Vector3(bounds.min.x, bounds.max.y, bounds.max.z),
        new Vector3(bounds.min.x, bounds.max.y, bounds.min.z),
        new Vector3(bounds.max.x, bounds.max.y, bounds.min.z),
        new Vector3(bounds.max.x, bounds.max.y, bounds.max.z),
        new Vector3(bounds.max.x, bounds.min.y, bounds.max.z),
        new Vector3(bounds.min.x, bounds.min.y, bounds.max.z),
        new Vector3(bounds.min.x, bounds.max.y, bounds.max.z),
        new Vector3(bounds.min.x, bounds.max.y, bounds.min.z),
        new Vector3(bounds.min.x, bounds.min.y, bounds.min.z),

    });
}

}
