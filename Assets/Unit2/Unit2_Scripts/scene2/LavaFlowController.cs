using UnityEngine;

public class LavaFlowController : MonoBehaviour
{
    // 控制 6 個 Animator 的數組
    public Animator[] animators;
    

    void Start()
    {
         for(int index=0;index<6;index++)
        {
            animators[index].SetBool("LavaFlow", false); // 或使用 SetTrigger 來觸發動畫
        }
    }

    // 控制流動狀態的開關，根據索引來控制不同的流動行為
    public void StartLavaFlow(int index)
    {
        if (index < 0 || index >= animators.Length)
        {
            Debug.LogError("Invalid index for Lava Flow");
            return;
        }

        // 設定對應的 Animator 流動狀態
            animators[index].SetBool("LavaFlow", true); // 或使用 SetTrigger 來觸發動畫
    }

    public void StopLavaFlow()
    {
        for(int index=0;index<6;index++)
        {
            animators[index].SetBool("LavaFlow", false); // 或使用 SetTrigger 來觸發動畫
        }
    }

}
