using UnityEngine;
using Obi;
using System.Collections;
using System.Collections.Generic;

public class FluidTriggerByZAxis1 : MonoBehaviour
{
    public ObiEmitter fluidEmitter;  // 連結 ObiEmitter
    public Transform object1;        // 第一個物件
    public Transform object2;        // 第二個物件
    public float object1TriggerZ = 615.5f; // 物件1的觸發 Z 軸座標
    public float object2TriggerZ = 615.3f; // 物件2的觸發 Z 軸座標

    public Transform risingObject;   // 需要上升的物件
    public float risingSpeed = 0.5f;   // 每秒上升速度
    public float risingStopY = 5.13f;  // 上升最大 Y 軸限制

    // object2 的 Z 軸會從 638.x 慢慢減少，
    // 此列表中的 Z 值代表斷點：當 object2 剛通過該 Z 值時觸發上升
    private List<float> triggerPoints = new List<float> { 638f, 638.3f, 638.5f, };
    private HashSet<float> triggeredPoints = new HashSet<float>(); // 記錄已觸發的點（避免重複觸發）

    private bool isFluidEmitting = false;   // 記錄流體是否正在噴射
    private bool isRaising = false;         // 記錄是否已經在上升（Coroutine 執行中）
    private bool hasCompletedEmission = false; // 記錄流體噴射是否已完全結束（10秒後）

    // 所有噴射器的引用列表
    private static List<ObiEmitter> allEmitters = new List<ObiEmitter>();

    // 用來記住前一幀的 Z 值
    private float lastObject2Z;

    // 記錄目前上升階段（trigger 的數量），以及物件初始 Y
    private int currentTriggerIndex = 0;
    private float initialRisingY;

    // 新增一個物件，在達到最後一個斷點後顯示
    public GameObject objectToShow;

    public TaskSystemUnit3 taskSystemUnit3;
    public bool isCompleted = false;

    public ParticleSystem smokeEffect; // 煙霧粒子特效（需拖曳進 Inspector）

    void Start()
    {
        // 一開始關閉流體噴射
        fluidEmitter.speed = 0f;
        
        // 將此噴射器加入靜態列表中
        if (fluidEmitter != null && !allEmitters.Contains(fluidEmitter))
        {
            allEmitters.Add(fluidEmitter);
        }

        // 記住一開始 object2 的 Z
        lastObject2Z = object2.position.z;

        // 記錄 risingObject 的初始 Y 值
        if (risingObject != null)
            initialRisingY = risingObject.position.y;

        // 確保目標物件一開始為隱藏狀態
        if (objectToShow != null)
            objectToShow.SetActive(false);

        if (smokeEffect != null)
            smokeEffect.Stop();
    }

    void Update()
    {
        // ------------------------------------------------------------------------
        // 1. 檢查物件 Z 軸是否達到條件，啟動或停止流體噴射
        // ------------------------------------------------------------------------
        if (object2.position.z <= object2TriggerZ && !hasCompletedEmission)
        {
            StartFluidEmission();
        }
        else if (object2.position.z > object2TriggerZ)
        {
            StopFluidEmission();
            // 重置完成狀態，因為物件已經移出觸發區域
            hasCompletedEmission = false;
        }

        // ------------------------------------------------------------------------
        // 2. 檢查 object2 是否剛好穿越 triggerPoints 中的某個斷點
        //    僅當「上一幀大於 point 且這一幀小於等於 point」才算剛通過
        // ------------------------------------------------------------------------
        foreach (float point in triggerPoints)
        {
            if (!triggeredPoints.Contains(point))
            {
                if (lastObject2Z > point && object2.position.z <= point)
                {
                    triggeredPoints.Add(point); // 記錄已觸發，避免重複

                    // 根據當前階段觸發物件上升（使用 Coroutine）
                    StartRaiseCoroutine();
                }
            }
        }

        // 更新 lastObject2Z，供下一幀使用
        lastObject2Z = object2.position.z;
    }

    void OnDestroy()
    {
        // 當物件被銷毀時，從列表中移除噴射器
        if (fluidEmitter != null && allEmitters.Contains(fluidEmitter))
        {
            allEmitters.Remove(fluidEmitter);
        }
    }

    /// 讓流體開始噴射
    void StartFluidEmission()
    {
        if (!isFluidEmitting)
        {
            fluidEmitter.speed = 1f; // 設定流體噴射速度
            if (smokeEffect != null)
                smokeEffect.Play();
            isFluidEmitting = true;
            isCompleted = true;
            taskSystemUnit3.ForceCompleteTask("完成聚合型板塊邊界互動");
            SessionLogger.LogAction("聚合型板塊邊界互動完成");
            // 啟動協程，10 秒後自動停止流體噴射
            StartCoroutine(StopFluidEmissionAfterDelay(20f));
        }
    }

    /// <summary>
    /// 協程：延遲指定秒數後停止流體噴射
    /// </summary>
    /// <param name="delaySeconds">延遲時間（秒）</param>
    IEnumerator StopFluidEmissionAfterDelay(float delaySeconds)
    {
        // 等待指定的秒數
        yield return new WaitForSeconds(delaySeconds);
        
        // 時間到，停止所有流體噴射器
        StopAllFluidEmitters();

        if(smokeEffect != null)
            smokeEffect.Stop();
            
        Debug.Log("流體噴射已在 " + delaySeconds + " 秒後自動停止");
    }

    /// <summary>
    /// 停止所有流體噴射器
    /// </summary>
    private void StopAllFluidEmitters()
    {
        // 停止當前實例的噴射器
        fluidEmitter.speed = 0f;
        isFluidEmitting = false;
        hasCompletedEmission = true; // 標記流體噴射已完全結束
        
        // 停止所有其他噴射器
        foreach (ObiEmitter emitter in allEmitters)
        {
            if (emitter != null)
            {
                emitter.speed = 0f;
            }
        }
    }

    /// 讓流體停止噴射
    void StopFluidEmission()
    {
        if (isFluidEmitting)
        {
            fluidEmitter.speed = 0f; // 停止噴射
            isFluidEmitting = false;
        }
    }

    /// 依照當前斷點階段觸發上升的 Coroutine
    void StartRaiseCoroutine()
    {
        if (!isRaising && risingObject != null && risingObject.position.y < risingStopY)
        {
            float targetY;
            // 計算每次上升的步幅：以 triggerPoints 數量作分割
            float step = (risingStopY - initialRisingY) / triggerPoints.Count;

            // 如果尚未達到最後一個斷點，僅上升一步；否則上升到最終高度
            if (currentTriggerIndex < triggerPoints.Count - 1)
            {
                targetY = risingObject.position.y + step;
            }
            else
            {
                targetY = risingStopY;
            }

            currentTriggerIndex++;
            StartCoroutine(RaiseCoroutine(targetY));
        }
        else
        {
            Debug.Log("目前位置：" + risingObject.position.y);
            Debug.Log("最終位置：" + risingStopY);
        }
    }

    /// 協程：逐幀移動物件至目標 Y 軸（targetY）
    IEnumerator RaiseCoroutine(float targetY)
    {
        isRaising = true;

        // 當物件尚未到達目標高度時，每幀持續上升
        while (risingObject.position.y < targetY)
        {
            // 用 Time.deltaTime 讓移動速度不受 FPS 影響
            float deltaMove = risingSpeed * Time.deltaTime;
            risingObject.position += new Vector3(0, deltaMove, 0);
            yield return null; // 等待下一幀
        }

        // 確保最後位置正好等於 targetY
        risingObject.position = new Vector3(
            risingObject.position.x,
            targetY,
            risingObject.position.z
        );

        isRaising = false;

        // 當達到最後一個斷點時，顯示指定物件
        if (Mathf.Approximately(targetY, risingStopY))
        {
            if (objectToShow != null)
            {
                objectToShow.SetActive(true);
                Debug.Log("最後斷點達成，物件已顯示");
            }
        }
    }
}
