using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RAO_Push : MonoBehaviour
{
    [System.Serializable]
    public class TargetModel
    {
        public Transform pushTarget;
        public XRGrabInteractable grabInteractable;
        public XRRayInteractor rayInteractor;
        public float moveSpeed = 0.5f;
        public float maxPushDistance = 1f;

        [HideInInspector] public Vector3 startPos;
        [HideInInspector] public float pushedDistance; // 累計推的距離
    }

    public TargetModel[] targetModels;
    public Transform mainCamera;
    public float directionThreshold = 0.2f;
    
    public OAR_SharedState sharedState;

    void Start()
    {
        foreach (var model in targetModels)
        {
            model.startPos = model.pushTarget.position;
            model.pushedDistance = 0f;
        }
    }

    void Update()
    {
        foreach (var model in targetModels)
        {
            if (!model.grabInteractable.isSelected)
                continue;

            if (model.rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                Vector3 rayDir = model.rayInteractor.transform.forward;

                // 攝影機前方向（忽略y）
                Vector3 cameraForward = mainCamera.forward;
                cameraForward.y = 0;
                cameraForward.Normalize();

                rayDir.y = 0;
                rayDir.Normalize();

                float dot = Vector3.Dot(rayDir, cameraForward);
                float moveAmount = model.moveSpeed * Time.deltaTime;

                
                if (dot > directionThreshold)
                {
                    MoveInLocalZ(model, moveAmount);
                    sharedState.PushActive = true;

                    // ✅ 判斷是否觸發 PushDown
                    if (model.pushedDistance >= model.maxPushDistance * 0.95f)
                    {
                        sharedState.PushDown = true;
                    }
                    else
                    {
                        sharedState.PushDown = false;
                    }

                }
                else if (dot < -directionThreshold)
                {
                    MoveInLocalZ(model, -moveAmount);
                    sharedState.PushActive = true;


                    // ✅ 判斷是否觸發 PushDown
                    if (model.pushedDistance >= model.maxPushDistance * 0.95f)
                    {
                        sharedState.PushDown = true;
                    }
                    else
                    {
                        sharedState.PushDown = false;
                    }
                }
            }
        }
    }

    void MoveInLocalZ(TargetModel model, float amount)
    {
        float newDistance = model.pushedDistance + amount;

        // 限制距離在 [0, maxPushDistance]
        newDistance = Mathf.Clamp(newDistance, 0, model.maxPushDistance);

        float delta = newDistance - model.pushedDistance;
        model.pushedDistance = newDistance;

        // 沿著物體本地 forward 移動（正數為推，負數為拉）
        model.pushTarget.position -= model.pushTarget.forward * delta;
    }
}
