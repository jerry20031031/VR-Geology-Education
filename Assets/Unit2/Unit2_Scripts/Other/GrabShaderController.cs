using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabShaderController : MonoBehaviour
{
    private Renderer objRenderer;
    private MaterialPropertyBlock propBlock;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        propBlock = new MaterialPropertyBlock();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // 訂閱 Grab 事件
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);

        // 確保水流預設為靜止
        SetFlowSpeed(0);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        SetFlowSpeed(1); // 抓取時讓水開始流動
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        SetFlowSpeed(0); // 釋放時讓水靜止
    }

    private void SetFlowSpeed(float speed)
    {
        objRenderer.GetPropertyBlock(propBlock);
        propBlock.SetFloat("_ScrollSpeed", speed);
        objRenderer.SetPropertyBlock(propBlock);
    }
}
