using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onesound : MonoBehaviour
{
    public AudioSource audioSource; // 指向AudioSource組件的參考
    private bool hasPlayed = false; // 用於追蹤音頻是否已經播放過

    // 這個方法應該在按鈕的OnClick()事件中被調用
    public void PlayAudio()
    {
        // 檢查音頻是否已經播放過
        if (!hasPlayed)
        {
            audioSource.Play(); // 播放音頻
            hasPlayed = true; // 標記音頻已經播放過，防止再次播放
        }
    }
}
