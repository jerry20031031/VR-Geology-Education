using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OAO_Left : MonoBehaviour
{
    [System.Serializable]
    public class TargetModel
    {
        public Transform targetObject;
        public SkinnedMeshRenderer skinnedMeshRenderer;
        public XRGrabInteractable grabInteractable;
        public int upKey, downKey;
        public float currentUp, currentDown;
    }

    public XRRayInteractor rayInteractor;
    public TargetModel[] targetModels;
    public float blendSpeed = 10f;
    public OAO_SharedState sharedState; // 共享狀態
    public Transform mainCamera;

    void Start()
    {
        foreach (var model in targetModels)
        {
            model.upKey = model.skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("up");
            model.downKey = model.skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("down");

            if (model.upKey == -1 || model.downKey == -1)
                Debug.LogError($"Blendshape 'up' 或 'down' 未找到於 {model.targetObject.name}");

            model.currentUp = model.skinnedMeshRenderer.GetBlendShapeWeight(model.upKey);
            model.currentDown = model.skinnedMeshRenderer.GetBlendShapeWeight(model.downKey);
        }
    }

    void Update()
    {
            if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                Vector3 rayDirection = rayInteractor.transform.forward; // **使用 XR Controller 的前方向**
                Vector3 localDirection = mainCamera.InverseTransformDirection(rayDirection); // **轉換為相機的本地座標**
                bool isLeft = localDirection.x < -0.1f;
                bool isRight = localDirection.x > 0.1f;

                foreach (var model in targetModels)
                {
                    if (model.grabInteractable.isSelected)
                    {
                        if (isLeft) AdjustBlendshape(model, false);
                        if (isRight) AdjustBlendshape(model, true);
                    }
                }
            }        
    }

    private void AdjustBlendshape(TargetModel model, bool isRight)
    {
        if (isRight) // **向右調整 (增加 down, 減少 up)**
        {
            if (model.currentUp > 0)
                model.currentUp = Mathf.Max(model.currentUp - blendSpeed * 2 * Time.deltaTime, 0);
            else if (!sharedState.RightUpActive && model.currentUp == 0) // **允許改變 down**
                model.currentDown = Mathf.Min(model.currentDown + blendSpeed * Time.deltaTime, 100);
        }
        else // **向左調整 (增加 up, 減少 down)**
        {
            if (model.currentDown > 0)
                model.currentDown = Mathf.Max(model.currentDown - blendSpeed * Time.deltaTime, 0);
            else if (!sharedState.RightDownActive && model.currentDown == 0) // **允許改變 up**
                model.currentUp = Mathf.Min(model.currentUp + blendSpeed * 2 * Time.deltaTime, 100);
        }

        model.skinnedMeshRenderer.SetBlendShapeWeight(model.upKey, model.currentUp);
        model.skinnedMeshRenderer.SetBlendShapeWeight(model.downKey, model.currentDown);

        // **更新共享狀態**
        sharedState.LeftUpActive = model.currentUp > 0;
        sharedState.LeftDownActive = model.currentDown > 0;

        if(model.currentUp == 100)
            sharedState.LeftUp = true;
        else if(model.currentUp == 99)
            sharedState.LeftUp = false;

        if(model.currentDown == 100)
            sharedState.LeftDown = true;
        else if(model.currentDown == 99)
            sharedState.LeftDown = false;
    }
}
