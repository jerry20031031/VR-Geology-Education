using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RestrictMovementToZAxis4 : XRGrabInteractable
{
    private Vector3 initialPosition;
    public float moveSpeed = 0.01f; // 控制移動速度
    public float maxSpeed = 0.1f; // 限制最大移動速度
    private Vector3 previousPosition;
    private float maxZ = 639.93f; // Z 軸最大值

    public float cooldownTime = 2.0f; // 冷卻時間（秒）
    private float currentCooldown = 0f; // 當前冷卻計時器
    private bool isCooldown = false; // 是否處於冷卻中

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (isCooldown)
            return;

        base.OnSelectEntered(args);
        initialPosition = transform.position;
        previousPosition = initialPosition;

        if (attachTransform != null)
            attachTransform.position = transform.position;
    }


    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // 強制將物件位置設定回上一個正確的位置
        transform.position = previousPosition;

        // 如果有 Rigidbody，清除其速度，避免後續物理效果
        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        isCooldown = true;
        currentCooldown = cooldownTime;
    }

    private void Update()
    {
        // 冷卻計時邏輯
        if (isCooldown)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0)
            {
                // 冷卻結束
                isCooldown = false;
                currentCooldown = 0;
            }
        }
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (isCooldown) return; // 冷卻期間禁止交互

        base.ProcessInteractable(updatePhase);

        if (isSelected)
        {
            // 獲取當前位置
            Vector3 currentPosition = transform.position;

            // 限制 X 和 Y 軸，讓物體只移動 Z 軸
            currentPosition.x = initialPosition.x;
            currentPosition.y = initialPosition.y;

            // 計算移動量（Z 軸）
            float movementDelta = currentPosition.z - previousPosition.z;

            // 如果使用者試圖往左拉（減少 Z 值），則不變更 Z 軸
            if (movementDelta < 0)
            {
                movementDelta = 0;
            }

            // 限制最大移動速度
            movementDelta = Mathf.Clamp(movementDelta, 0, maxSpeed);

            // 只讓 Z 軸增加，並確保不超過 `maxZ`
            currentPosition.z = Mathf.Min(previousPosition.z + movementDelta * moveSpeed, maxZ);

            // 更新物體位置
            transform.position = currentPosition;
            previousPosition = currentPosition;
        }
    }
}
