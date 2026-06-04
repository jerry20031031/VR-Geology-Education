using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OAO_Push : MonoBehaviour
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
        [HideInInspector] public float pushedDistance;
    }

    public TargetModel[] targetModels;
    public Transform mainCamera;
    public float directionThreshold = 0.2f;

    public OAO_SharedState sharedState;

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
            if (model.rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit) && model.grabInteractable.isSelected)
            {
                Vector3 rayDir = model.rayInteractor.transform.forward;

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
        newDistance = Mathf.Clamp(newDistance, 0, model.maxPushDistance);

        float delta = newDistance - model.pushedDistance;
        model.pushedDistance = newDistance;

        model.pushTarget.position += model.pushTarget.forward * delta;

    }
}
