using UnityEngine;

public class ResetManager : MonoBehaviour
{
    public Material originalMaterials;
    public LavaFlowController lavaFlowController;
    
    private GameObject[][] targetObjectGroups;
    private bool[] groupErrors;
    private MaterialChecker1 materialChecker; // 讓 ResetManager 可以修改 MaterialChecker1 的變數
    
    public Scene2Session s2;
    int times;

    public void Initialize(GameObject[][] groups, bool[] errors, MaterialChecker1 checker)
    {
        targetObjectGroups = groups;
        groupErrors = errors;
        materialChecker = checker;
    }

    public void ResetModels()
    {
        if (targetObjectGroups == null) return; // 防止未初始化時執行

        // 重置物件的材質
        for (int i = 0; i < targetObjectGroups.Length; i++)
        {
            for (int j = 0; j < targetObjectGroups[i].Length; j++)
            {
                targetObjectGroups[i][j].GetComponent<Renderer>().sharedMaterial = originalMaterials;
            }
        }

        // **修正點：恢復 MaterialChecker1 的變數狀態**
        for (int i = 0; i < 6; i++)
        {
            groupErrors[i] = false;
        }

        materialChecker.hasError = false;
        materialChecker.stopcheck = false;
        materialChecker.isdown = false;

        lavaFlowController.StopLavaFlow();
        times++;
        s2.SessionAction($"模型重製:{times}次");
    }
}
