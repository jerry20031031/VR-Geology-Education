using UnityEngine;

public class EnableModel : MonoBehaviour
{
    [Header("檢測的 Cube 組")]
    public GameObject[] cubes1 = new GameObject[2]; // 第一組 Cube
    public GameObject[] cubes2 = new GameObject[2]; // 第二組 Cube
    public GameObject[] cubes3 = new GameObject[2]; // 第三組 Cube

    [Header("模型組 (A, B, C 位置的 4 種模型)")]
    public GameObject[] modelsA = new GameObject[4]; // A 位置的 4 種模型
    public GameObject[] modelsB = new GameObject[4]; // B 位置的 4 種模型
    public GameObject[] modelsC = new GameObject[4]; // C 位置的 4 種模型

    [Header("材質判斷")]
    public Material oceanMaterial; // 用來判斷的海洋材質

    public void UpdateModel()
    {
        // 確保模型陣列大小正確
        if (modelsA.Length < 4 || modelsB.Length < 4 || modelsC.Length < 4)
        {
            Debug.LogError("模型陣列大小不足，請確保每個位置都有 4 種模型");
            return;
        }

        // 取得三組 Cube 的判斷結果
        int indexA = GetModelIndex(cubes1);
        int indexB = GetModelIndex(cubes2);
        int indexC = GetModelIndex(cubes3);

        // 啟用對應的模型
        ActivateModel(modelsA, indexA);
        ActivateModel(modelsB, indexB);
        ActivateModel(modelsC, indexC);
    }

    private int GetModelIndex(GameObject[] cubePair)
    {
        if (cubePair.Length != 2)
        {
            Debug.LogError("Cube 組大小錯誤，應該是 2 個物件！");
            return 0;
        }

        // 確保物件存在
        if (cubePair[0] == null || cubePair[1] == null)
        {
            Debug.LogError("有 Cube 物件未設定！");
            return 0;
        }

        // 取得 Cube 材質
        bool isOcean1 = IsOcean(cubePair[0]);
        bool isOcean2 = IsOcean(cubePair[1]);
        // 判斷組合類型
        if (isOcean1 && isOcean2) return 0; // 海洋 + 海洋
        if (!isOcean1 && !isOcean2) return 1; // 大陸 + 大陸
        if (isOcean1 && !isOcean2) return 2; // 海洋在左，大陸在右
        if (!isOcean1 && isOcean2) return 3; // 大陸在左，海洋在右 (水平翻轉)

        return 0; // 預設值
    }

    private bool IsOcean(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError($"物件 {obj.name} 沒有 Renderer，無法判斷材質！");
            return false;
        }

        return renderer.sharedMaterial == oceanMaterial;
    }

    private void ActivateModel(GameObject[] models, int activeIndex)
    { 
        models[activeIndex].SetActive(true);
    }
}
