using UnityEngine;

public class CheckScriptsCompleted : MonoBehaviour
{
    // 請在 Inspector 中指定兩個腳本的參考
    public FluidTriggerByZAxis1 fluidTriggerScript;
    public FluidTriggerMove fluidMoveScript;

    // 當上述兩個腳本都完成時，顯示此物件
    public GameObject finalObject;

    // 確保只顯示一次
    private bool finalShown = false;

    void Start()
    {
        if (finalObject != null)
        {
            // 一開始先隱藏最終要顯示的物件
            finalObject.SetActive(false);
        }
    }

    public void Quesion()
    {
        // 確認參考都有設定且尚未顯示最終物件
        if (!finalShown && fluidTriggerScript != null && fluidMoveScript != null)
        {
            // 假設這兩支腳本各自都有 public bool isCompleted 的旗標，
            // 當兩者都完成時，顯示最終物件
            if (fluidTriggerScript.isCompleted && fluidMoveScript.isCompleted)
            {
                finalObject.SetActive(true);
                finalShown = true;
                
            }
        }
    }
}
