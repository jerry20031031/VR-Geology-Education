using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MoveMidRight : MonoBehaviour
{
    [System.Serializable]
    public class TargetModel
    {
        public Transform targetObject; // 目標物件
        public SkinnedMeshRenderer skinnedMeshRenderer; // 包含 Blendshapes 的模型
        public XRGrabInteractable grabInteractable; // XR Grab Interactable 組件
        public int[] blendShapeIndices; // Blendshape 的索引數組
        [HideInInspector] public float originalX; // 初始 X 值
        [HideInInspector] public float[] currentBlendWeights; // 當前 Blendshape 權重
        [HideInInspector] public bool isReturningToOriginalX; // 是否正在返回初始 X 值
        public float maxX; // 向右移動的最大 X 值
    }

    public XRRayInteractor rayInteractor; // 射線交互器
    public TargetModel[] targetModels; // 可控制的多個目標模型
    public float blendSpeed = 10f; // Blendshape 的變化速度
    public float moveSpeed = 0.5f; // 移動速度

    void Start()
    {
        // 初始化每個目標模型的初始狀態並註冊事件
        foreach (var model in targetModels)
        {       
            model.originalX = model.targetObject.position.x;
            model.currentBlendWeights = new float[model.blendShapeIndices.Length];
            model.isReturningToOriginalX = false;
        }
    }

    void Update()
    {
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            Vector3 rayDirection = rayInteractor.transform.forward;

            foreach (var model in targetModels)
            {
                if (model.grabInteractable.isSelected) // 確保目標正在被抓取
                {
                    if (rayDirection.x < 0) // 向左
                    {
                        HandleLeft(model);
                    }
                    else if (rayDirection.x > 0) // 向右
                    {
                        HandleRight(model);
                    }
                }
            }
        }
    }

    private void HandleLeft(TargetModel model)
    {
        if (!Mathf.Approximately(this.transform.position.x, model.originalX))
        {
            // 如果腳本所在物件的位置與原始位置不同，則先返回原始位置
            ReturnToOriginalX(model);
        }
        else
        {
            // 當腳本物件位置回到原始位置後，更新 Blendshape 權重
            for (int i = 0; i < model.blendShapeIndices.Length; i++)
            {
                model.currentBlendWeights[i] = Mathf.Clamp(model.currentBlendWeights[i] + blendSpeed * Time.deltaTime, 0f, 100f);
                model.skinnedMeshRenderer.SetBlendShapeWeight(model.blendShapeIndices[i], model.currentBlendWeights[i]);
            }
        }
    }


    private void HandleRight(TargetModel model)
    {
        bool allBlendShapesReset = true;

        for (int i = 0; i < model.blendShapeIndices.Length; i++)
        {
            if (model.currentBlendWeights[i] > 0f)
            {
                // 減少 Blendshape 權重直到完全復原
                model.currentBlendWeights[i] = Mathf.Clamp(model.currentBlendWeights[i] - blendSpeed * Time.deltaTime, 0f, 100f);
                model.skinnedMeshRenderer.SetBlendShapeWeight(model.blendShapeIndices[i], model.currentBlendWeights[i]);
                allBlendShapesReset = false;
            }
        }

        if (allBlendShapesReset)
        {
            // 在所有 Blendshape 完全復原後，檢查是否可以繼續向右移動
            Vector3 position = this.transform.position; // 使用腳本所在物件的 Transform
            if (position.x < model.maxX)
            {
                position.x += moveSpeed * Time.deltaTime;
                this.transform.position = position; // 更新腳本所在物件的位置
            }
        }
    }


    private void ReturnToOriginalX(TargetModel model)
    {
        Vector3 position = this.transform.position; // 使用腳本所在物件的 Transform
        position.x = Mathf.MoveTowards(position.x, model.originalX, moveSpeed * Time.deltaTime);
        this.transform.position = position;

        if (Mathf.Approximately(position.x, model.originalX))
        {
            model.isReturningToOriginalX = false;
        }
    }

}
