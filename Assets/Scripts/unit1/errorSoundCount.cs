using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class errorSoundCount : MonoBehaviour
{
    public AudioSource audioSource; // 將AudioSource組件拖到這個欄位
    [SerializeField] private int activationCount = 0; // 用來記錄active次數
    void OnEnable()
    {
        activationCount++; // 每次物件變為active時，增加計數

        // 檢查是否是第一次設置為active
        if (activationCount == 1)
        {
            StartCoroutine(PlaySoundAfterDelay(0.5f)); // 延遲1秒播放音效
        }
    }

    IEnumerator PlaySoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 等待指定的延遲時間
        audioSource.Play(); // 播放音效
    }
}
