using UnityEngine;

public class MaterialUpdater : MonoBehaviour
{
    public Renderer[] groupRenderers; // 群組內的所有 Renderer（包含 6 個物件）
    private Material currentMaterial;  // 儲存當前材質

    void Start()
    {
        // 初始化，將群組中第一個物件的材質作為基準
        if (groupRenderers.Length > 0)
        {
            currentMaterial = groupRenderers[0].sharedMaterial; // 使用 sharedMaterial
        }
    }

    void Update()
    {
        // 檢查群組內物件的材質是否有變化
        foreach (var renderer in groupRenderers)
        {
            if (renderer != null && renderer.sharedMaterial != currentMaterial) // 使用 sharedMaterial
            {
                // 如果有物件的材質變化，更新所有物件的材質
                currentMaterial = renderer.sharedMaterial; // 使用 sharedMaterial
                UpdateGroupMaterials();
                break;  // 只需一次檢測即可更新，然後退出
            }
        }
    }

    // 更新群組內所有物件的材質
    void UpdateGroupMaterials()
    {
        foreach (var renderer in groupRenderers)
        {
            if (renderer != null)
            {
                renderer.sharedMaterial = currentMaterial; // 使用 sharedMaterial
                LineRenderer lineRenderer = renderer.GetComponent<LineRenderer>();

                if (lineRenderer != null)
                {
                    lineRenderer.enabled = false;
                }
            }
        }
    }
}
