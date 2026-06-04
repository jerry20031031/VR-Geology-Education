using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RAR_Left : MonoBehaviour
{
    [System.Serializable]
    public class TargetModel_1
    {
        public Transform targetObject;
        public SkinnedMeshRenderer skinnedMeshRenderer;
        public XRGrabInteractable grabInteractable;
        public int closeKey, upKey;
        public float currentClose, currentUp;
    }
    [System.Serializable]
    public class TargetModel_2
    {
        public Transform targetObject;
        public SkinnedMeshRenderer skinnedMeshRenderer;
        public XRGrabInteractable grabInteractable;
        public int blowKey, upKey;
        public float currentBlow, currentUp;
    }
    [System.Serializable]
    public class TargetModel_3
    {
        public Transform targetObject;
        public SkinnedMeshRenderer skinnedMeshRenderer;
        public XRGrabInteractable grabInteractable;
        public int blow_1Key, up_1Key;
        public float currentBlow_1, currentUp_1;
    }

    public XRRayInteractor rayInteractor;
    public TargetModel_1 Rock;
    public TargetModel_1 Land_1;
    public TargetModel_2 Land_2;
    public TargetModel_3 Land_3;
    public float blendSpeed = 20f;
    public RAR_SharedState sharedState; // 共享狀態
    public Transform mainCamera;

    float blendFactor;
    float newX,newX1;

    void Start()
    {
            Rock.closeKey = Rock.skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("close");
            Rock.upKey = Rock.skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("up");
            
            Land_1.closeKey = Land_1.skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("close");
            Land_1.upKey = Land_1.skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("up");

            Land_2.upKey = Land_2.skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("up");
            Land_2.blowKey = Land_2.skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("blow");

            Land_3.up_1Key = Land_3.skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("up1");
            Land_3.blow_1Key = Land_3.skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("blow1");

            //------------------------------------
                
            Rock.currentClose = Rock.skinnedMeshRenderer.GetBlendShapeWeight(Rock.closeKey);
            Rock.currentUp = Rock.skinnedMeshRenderer.GetBlendShapeWeight(Rock.upKey);
            
            Land_1.currentClose = Land_1.skinnedMeshRenderer.GetBlendShapeWeight(Land_1.closeKey);
            Land_1.currentUp = Land_1.skinnedMeshRenderer.GetBlendShapeWeight(Land_1.upKey);
            
            Land_2.currentUp = Land_2.skinnedMeshRenderer.GetBlendShapeWeight(Land_2.upKey);
            Land_2.currentBlow = Land_2.skinnedMeshRenderer.GetBlendShapeWeight(Land_2.blowKey);

            Land_3.currentUp_1 = Land_3.skinnedMeshRenderer.GetBlendShapeWeight(Land_3.up_1Key);
            Land_3.currentBlow_1 = Land_3.skinnedMeshRenderer.GetBlendShapeWeight(Land_3.blow_1Key);
        
    }

    void Update()
    {

        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            Vector3 rayDirection = rayInteractor.transform.forward; // **使用 XR Controller 的前方向**
            Vector3 localDirection = mainCamera.InverseTransformDirection(rayDirection); // **轉換為相機的本地座標**
            bool isLeft = localDirection.x < -0.1f;
            bool isRight = localDirection.x > 0.1f;

            
                if (Rock.grabInteractable.isSelected)
                {
                    if (isLeft) AdjustBlendshape(Rock,Land_1,Land_2,Land_3, false);
                    if (isRight) AdjustBlendshape(Rock,Land_1,Land_2,Land_3, true);
                }
        }

        
    }

    private void AdjustBlendshape(TargetModel_1 Rock,TargetModel_1 Land_1,TargetModel_2 Land_2,TargetModel_3 Land_3, bool isRight)
    {
        if (isRight) // **向右調整 (聚合)**
        {
            if (Land_3.currentUp_1 > 0 || Land_3.currentBlow_1 > 0) // **改變 Land_3** 
            {
                Land_3.currentUp_1 = Mathf.Max(Land_3.currentUp_1 - blendSpeed * Time.deltaTime, 0);
                Land_3.currentBlow_1 = Mathf.Max(Land_3.currentBlow_1 - blendSpeed * Time.deltaTime, 0);

                blendFactor = Land_3.currentUp_1 / 540f; // 轉換為 0~1 的比例
                newX = Mathf.Lerp( Land_1.targetObject.localPosition.x, Land_1.targetObject.localPosition.x-1f, blendFactor/10f );

                // **更新 Land_1 位置**
                Land_1.targetObject.localPosition = new Vector3(newX, Land_1.targetObject.localPosition.y, Land_1.targetObject.localPosition.z);

                newX1 = Mathf.Lerp( Land_2.targetObject.localPosition.x, Land_2.targetObject.localPosition.x-1f, blendFactor/10f );

                // **更新 Land_2 位置**
                Land_2.targetObject.localPosition = new Vector3(newX1, Land_2.targetObject.localPosition.y, Land_2.targetObject.localPosition.z);
                
            }
            else if (!sharedState.RightBlow_L3Active && !sharedState.RightUp_L3Active 
                && Land_3.currentBlow_1 == 0 && Land_3.currentUp_1 == 0
                && Land_2.currentBlow > 0 && Land_2.currentUp > 0
                && Land_1.currentUp > 0 && Rock.currentUp > 0)// **允許改變 Land_2 和Land_1位置**
            {
                Land_2.currentUp = Mathf.Max(Land_2.currentUp - blendSpeed * Time.deltaTime, 0);
                Land_2.currentBlow = Mathf.Max(Land_2.currentBlow - blendSpeed * Time.deltaTime, 0);
                
                Land_1.currentUp = Mathf.Max(Land_1.currentUp - blendSpeed * Time.deltaTime, 0);
                Rock.currentUp = Mathf.Max(Rock.currentUp - blendSpeed * Time.deltaTime, 0);

                blendFactor = Land_1.currentUp / 540f; // 轉換為 0~1 的比例
                newX = Mathf.Lerp( Land_1.targetObject.localPosition.x, Land_1.targetObject.localPosition.x-1f, blendFactor/10f );

                // **更新 Land_1 位置**
                Land_1.targetObject.localPosition = new Vector3(newX, Land_1.targetObject.localPosition.y, Land_1.targetObject.localPosition.z);
            }
            else if(!sharedState.RightBlow_L2Active && !sharedState.RightUp_L2Active && !sharedState.RightUp_L1Active&& !
            sharedState.RightUp_RockActive && Land_2.currentBlow == 0 && Land_2.currentUp == 0 && Land_1.currentUp == 0
            && Rock.currentUp == 0)// **允許改變 Land_1 下降**
            {
                Land_1.currentClose = Mathf.Min(Land_1.currentClose + blendSpeed * Time.deltaTime, 100);
                Rock.currentClose = Mathf.Min(Rock.currentClose + blendSpeed * Time.deltaTime, 100);
            }
        }
        else // **向右調整 (張裂)**
        {
            if (Land_1.currentClose > 0 || Rock.currentClose > 0 ) // **改變 Land_1, rock** 
            {
                Land_1.currentClose = Mathf.Max(Land_1.currentClose - blendSpeed * Time.deltaTime, 0);
                Rock.currentClose = Mathf.Max(Rock.currentClose - blendSpeed * Time.deltaTime, 0);
            }
            else if (!sharedState.RightClose_L1Active && !sharedState.RightClose_RockActive 
                && Land_1.currentClose == 0 && Rock.currentClose == 0
                && Land_2.currentUp < 100 && Land_2.currentBlow < 100)// **允許改變 Land_2 和 Land_1位置** 
            {
                Land_1.currentUp = Mathf.Min(Land_1.currentUp + blendSpeed * Time.deltaTime, 100);
                Rock.currentUp = Mathf.Min(Rock.currentUp + blendSpeed * Time.deltaTime, 100);
                
                Land_2.currentUp = Mathf.Min(Land_2.currentUp + blendSpeed * Time.deltaTime, 100);
                Land_2.currentBlow = Mathf.Min(Land_2.currentBlow + blendSpeed * Time.deltaTime, 100);

                blendFactor = Land_1.currentUp / 540f; // 轉換為 0~1 的比例
                newX = Mathf.Lerp( Land_1.targetObject.localPosition.x, Land_1.targetObject.localPosition.x+1f, blendFactor/10f );

                // **更新 Land_1 位置**
                Land_1.targetObject.localPosition = new Vector3(newX, Land_1.targetObject.localPosition.y, Land_1.targetObject.localPosition.z);
            }
            else if(sharedState.RightBlow_L2isActive && sharedState.RightUp_L2isActive && sharedState.RightUp_L1isActive&& sharedState.RightUp_RockisActive && Land_2.currentBlow == 100
            && Land_2.currentUp == 100 && Land_1.currentUp == 100 && Rock.currentUp == 100
            && Land_3.currentUp_1 < 100 && Land_3.currentBlow_1 < 100)
            // **允許改變 Land_3 和Land2位置**
            {
                Land_3.currentUp_1 = Mathf.Min(Land_3.currentUp_1 + blendSpeed * Time.deltaTime, 100);
                Land_3.currentBlow_1 = Mathf.Min(Land_3.currentBlow_1 + blendSpeed * Time.deltaTime, 100);

                blendFactor = Land_3.currentUp_1 / 540f; // 轉換為 0~1 的比例
                newX = Mathf.Lerp( Land_1.targetObject.localPosition.x, Land_1.targetObject.localPosition.x+1f, blendFactor/10f );

                // **更新 Land_1 位置**
                Land_1.targetObject.localPosition = new Vector3(newX, Land_1.targetObject.localPosition.y, Land_1.targetObject.localPosition.z);

                newX1 = Mathf.Lerp( Land_2.targetObject.localPosition.x, Land_2.targetObject.localPosition.x+1f, blendFactor/10f );

                // **更新 Land_2 位置**
                Land_2.targetObject.localPosition = new Vector3(newX1, Land_2.targetObject.localPosition.y, Land_2.targetObject.localPosition.z);
            }
        }
        //Rock
        Rock.skinnedMeshRenderer.SetBlendShapeWeight(Rock.closeKey, Rock.currentClose);
        Rock.skinnedMeshRenderer.SetBlendShapeWeight(Rock.upKey, Rock.currentUp);
        //Land_1
        Land_1.skinnedMeshRenderer.SetBlendShapeWeight(Land_1.closeKey, Land_1.currentClose);
        Land_1.skinnedMeshRenderer.SetBlendShapeWeight(Land_1.upKey, Land_1.currentUp);
        //Land_2
        Land_2.skinnedMeshRenderer.SetBlendShapeWeight(Land_2.upKey, Land_2.currentUp);
        Land_2.skinnedMeshRenderer.SetBlendShapeWeight(Land_2.blowKey, Land_2.currentBlow);
        //Land_3
        Land_3.skinnedMeshRenderer.SetBlendShapeWeight(Land_3.up_1Key, Land_3.currentUp_1);
        Land_3.skinnedMeshRenderer.SetBlendShapeWeight(Land_3.blow_1Key, Land_3.currentBlow_1);

        // **更新共享狀態**
        //Rock
        sharedState.LeftClose_RockActive = Rock.currentClose > 0;  // RAR_Left 的 close 是否未歸零
        sharedState.LeftUp_RockActive = Rock.currentUp > 0; // RAR_Left 的 up 是否未歸零
        //Land_1
        sharedState.LeftClose_L1Active = Land_1.currentClose > 0;  // RAR_Left 的 close 是否未歸零
        sharedState.LeftUp_L1Active = Land_1.currentUp > 0; // RAR_Left 的 up 是否未歸零
        //Land_2
        sharedState.LeftUp_L2Active = Land_2.currentUp > 0; // RAR_Left 的 up 是否未歸零
        sharedState.LeftBlow_L2Active = Land_2.currentBlow > 0; // RAR_Left 的 blow 是否未歸零
        //Land_3
        sharedState.LeftUp_L3Active = Land_3.currentUp_1 > 0; // RAR_Left 的 up_1 是否未歸零
        sharedState.LeftBlow_L3Active = Land_3.currentBlow_1 > 0; // RAR_Left 的 blow_1 是否未歸零
        //張裂
        sharedState.LeftUp_RockisActive = Rock.currentUp == 100; // RAR_Left 的 up 是否100
        sharedState.LeftUp_L1isActive = Land_1.currentUp == 100; // RAR_Left 的 up 是否100
        sharedState.LeftUp_L2isActive = Land_2.currentUp == 100; // RAR_Left 的 up 是否100
        sharedState.LeftBlow_L2isActive = Land_2.currentBlow == 100; // RAR_Left 的 blow 是否100


        if(Land_3.currentUp_1 == 100 && Land_3.currentBlow_1 == 100)
            sharedState.LeftUp = true;
        else if(Land_3.currentUp_1 == 99 && Land_3.currentBlow_1 == 99)
            sharedState.LeftUp = false;

        if(Rock.currentClose == 100 && Land_1.currentClose == 100)
            sharedState.LeftClose = true;
        else if(Rock.currentClose == 99 && Land_1.currentClose == 99)
            sharedState.LeftClose = false;
    }
}
