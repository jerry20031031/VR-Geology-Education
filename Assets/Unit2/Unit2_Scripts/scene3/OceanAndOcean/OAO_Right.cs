using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OAO_Right : MonoBehaviour
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
    public Transform BObject; // B 物件
    private float targetScaleY; // 目標 Y 軸縮放值
    public float minScaleY = 0f; // 最小縮放值（完全縮小）
    public float maxScaleY = 2f; // 最大縮放值（完全展開）
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
        
        if (BObject != null)
            targetScaleY = BObject.localScale.y;
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

            if (BObject != null)
            {
                Vector3 scale = BObject.localScale;
                Vector3 position = BObject.position;

                float prevScaleY = scale.y; // 儲存之前的 scale.y
                scale.y = Mathf.Lerp(scale.y, targetScaleY, Time.deltaTime * 5f);

                // **計算 position.y 的變化量**
                float deltaY = (prevScaleY - scale.y) * BObject.lossyScale.y * 0.5f; // 0.5 確保位移是縮放的一半
                position.y += deltaY; // 上移 Y 位置

                BObject.localScale = scale;
                BObject.position = position;
            }        
    }

    private void AdjustBlendshape(TargetModel model, bool isLeft)
    {
        if (isLeft) // **向左調整 (增加 down, 減少 up)**
        {
            if (model.currentUp > 0)
                model.currentUp = Mathf.Max(model.currentUp - blendSpeed * 2 * Time.deltaTime, 0);
            else if (!sharedState.LeftUpActive && model.currentUp == 0) // **允許改變 down**
                model.currentDown = Mathf.Min(model.currentDown + blendSpeed * Time.deltaTime, 100);
        }
        else // **向右調整 (增加 up, 減少 down)**
        {
            if (model.currentDown > 0)
                model.currentDown = Mathf.Max(model.currentDown - blendSpeed * Time.deltaTime, 0);
            else if (!sharedState.LeftDownActive && model.currentDown == 0) // **允許改變 up**
                model.currentUp = Mathf.Min(model.currentUp + blendSpeed * 2 * Time.deltaTime, 100);
        }

        model.skinnedMeshRenderer.SetBlendShapeWeight(model.upKey, model.currentUp);
        model.skinnedMeshRenderer.SetBlendShapeWeight(model.downKey, model.currentDown);

        // **更新共享狀態**
        sharedState.RightUpActive = model.currentUp > 0;
        sharedState.RightDownActive = model.currentDown > 0;

        // 設定 B 物件的目標 Y 軸縮放 (根據 upKey 動態調整)
        targetScaleY = Mathf.Lerp(maxScaleY, minScaleY, model.currentUp / 100f);

        if(model.currentUp == 100)
            sharedState.RightUp = true;
        else if(model.currentUp == 99)
            sharedState.RightUp = false;

        if(model.currentDown == 100)
            sharedState.RightDown = true;
        else if(model.currentDown == 99)
            sharedState.RightDown = false;
    }
}
