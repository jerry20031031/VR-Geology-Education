using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabAndChangeMaterial : MonoBehaviour
{
    private XRGrabInteractable grabInteractable; // 用來抓取物體的 XRGrabInteractable 組件
    private Renderer aRenderer; // A 物體的 Renderer，用來提取材質
    private Vector3 originalPosition;
    public string scriptName = "DashedOutlineScript";

    void Start()
    {
        originalPosition = transform.position;
        grabInteractable = GetComponent<XRGrabInteractable>(); // 獲取 A 物體的 XRGrabInteractable 組件
        aRenderer = GetComponent<Renderer>(); // 獲取 A 物體的 Renderer 組件 
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            // 註冊放開事件
            grabInteractable.onSelectExited.AddListener(OnRelease);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (grabInteractable.isSelected && other.CompareTag("TargetObject"))
        {
            // 如果 A 物體正在被抓取，且碰到的是 B 物體
            Renderer bRenderer = other.GetComponent<Renderer>(); // 獲取 B 物體的 Renderer 組件
            if (bRenderer != null)
            {
                // 改變 B 物體的材質為 A 物體的材質
                bRenderer.material = aRenderer.material;
                Destroy(gameObject);
            }
            LineRenderer lineRenderer = other.gameObject.GetComponent<LineRenderer>();

            if (lineRenderer != null)
            {
                lineRenderer.enabled = false;
            }
            transform.position = originalPosition;
        }
    }

     private void OnRelease(XRBaseInteractor interactor)
    {
        // 銷毀物件
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // 確保事件在物件銷毀時被解除
        if (grabInteractable != null)
        {
            grabInteractable.onSelectExited.RemoveListener(OnRelease);
        }
    }
}
