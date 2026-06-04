using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RockMaterialScript : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Renderer aRenderer;
    private Vector3 originalPosition;
    public Material rockMaterial; // 指定的 Rock 材質

    void Start()
    {
        originalPosition = transform.position;
        grabInteractable = GetComponent<XRGrabInteractable>();
        aRenderer = GetComponent<Renderer>();

        if (grabInteractable != null)
        {
            grabInteractable.onSelectExited.AddListener(OnRelease);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (grabInteractable.isSelected && other.CompareTag("TargetObject"))
        {
            Renderer bRenderer = other.GetComponent<Renderer>();
            if (bRenderer != null)
            {
                bRenderer.material = rockMaterial;

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
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.onSelectExited.RemoveListener(OnRelease);
        }
    }
}
