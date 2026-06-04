using UnityEngine;
using Obi;

public class FluidTriggerByZAxis : MonoBehaviour
{
    public ObiEmitter fluidEmitter;  // 連結 ObiEmitter
    public Transform object1;        // 第一個物件
    public Transform object2;        // 第二個物件
    public float object1TriggerZ = 615.5f; // 物件1的觸發 Z 軸座標
    public float object2TriggerZ = 615.3f; // 物件2的觸發 Z 軸座標


    private bool isFluidEmitting = false;   // 記錄流體是否正在噴射

    void Start()
    {
        // 確保初始速度為 0，避免遊戲開始時流體噴射
        fluidEmitter.speed = 0f;
        fluidEmitter.enabled = true;
    }

    void Update()
    {
        // 檢查兩個物件的 Z 軸是否達到各自條件
        if (object1.position.z <= object1TriggerZ && object2.position.z >= object2TriggerZ)
        {
            StartFluidEmission();
        }
        else
        {
            StopFluidEmission();
        }
    }

    void StartFluidEmission()
    {
        if (!isFluidEmitting)  // 避免重複觸發
        {
            fluidEmitter.speed = 4f; // 設定流體噴射速度
            isFluidEmitting = true;
            FluidTriggerMove.phase++;
        }
    }

    void StopFluidEmission()
    {
        if (isFluidEmitting)  // 避免重複關閉
        {
            fluidEmitter.speed = 0f; // 停止噴射
            isFluidEmitting = false;
        }
    }
}
