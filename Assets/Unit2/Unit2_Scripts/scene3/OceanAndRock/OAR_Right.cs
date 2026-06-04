using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OAR_Right : MonoBehaviour
{
    [System.Serializable]
    public class TargetModel
    {
        public Transform targetObject;
        public SkinnedMeshRenderer skinnedMeshRenderer;
        public XRGrabInteractable grabInteractable;
        public int downKey;
        public float currentDown;
    }

    public XRRayInteractor rayInteractor;
    public TargetModel[] targetModels;
    public float blendSpeed = 10f;
    public OAR_SharedState sharedState; // 共享狀態
    public Transform mainCamera;

    void Start()
    {
        foreach (var model in targetModels)
        {
            model.downKey = model.skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("down");

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
                    if (isLeft) AdjustBlendshape(model, true);
                    if (isRight) AdjustBlendshape(model, false);
                }
            }
        }
    }

    private void AdjustBlendshape(TargetModel model, bool isLeft)
    {
        if (isLeft) // **向左調整 (增加 down)**
        {
            model.currentDown = Mathf.Min(model.currentDown + blendSpeed * Time.deltaTime, 100);
        }
        else // **向右調整 (減少 down)**
        {
            model.currentDown = Mathf.Max(model.currentDown - blendSpeed * Time.deltaTime, 0);
        }

        model.skinnedMeshRenderer.SetBlendShapeWeight(model.downKey, model.currentDown);

        // **更新共享狀態**
        sharedState.RightDownActive = model.currentDown > 0;

        if(model.currentDown == 100)
            sharedState.RightDown = true;
        else if(model.currentDown == 99)
            sharedState.RightDown = false;
    }
}
