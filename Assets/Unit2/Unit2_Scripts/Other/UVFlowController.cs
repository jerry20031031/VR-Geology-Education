using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class UVFlowController : MonoBehaviour
{
    private Renderer rend;
    private MaterialPropertyBlock block;
    private XRGrabInteractable grab;

    // 控制流速
    public float flowingSpeed = 0.5f; // 抓取時速度
    private float currentSpeed = 0f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        block = new MaterialPropertyBlock();
        grab = GetComponent<XRGrabInteractable>();

        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);

        SetFlowSpeed(0f);
    }

    void Update()
    {
        // 持續更新材質偏移（讓 Shader 使用 _Time 有意義）
        SetFlowSpeed(currentSpeed);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        currentSpeed = flowingSpeed;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        currentSpeed = 0f;
    }

    void SetFlowSpeed(float speed)
    {
        rend.GetPropertyBlock(block);
        block.SetFloat("_FlowSpeed", speed);
        rend.SetPropertyBlock(block);

    }
}
