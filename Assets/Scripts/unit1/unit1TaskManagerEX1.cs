using cherrydev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit1TaskManagerEX1 : MonoBehaviour
{
    public DialogBehaviour Person1Talk;  //學姊

    public AudioClip[] audioClips;  // 存儲音檔的陣列
    public AudioSource audioSource1;  // 第一個AudioSource組件，用於播放音檔


    void Awake()
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length >= 1)
        {
            audioSource1 = sources[0];  //學姊
        }

    }

    void Update()
    {

    }

    //-----------------------------------------------------------------------

    void Start()
    {
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talkEX1000", talkEX1000);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talkEX2000", talkEX2000);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talkEX3000", talkEX3000);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talkEX4000", talkEX4000);
    }
    //audioSource1是學姊
    public void talkEX1000()
    {
        PlayAudio(audioSource1, 0);
    }

    public void talkEX2000()
    {
        PlayAudio(audioSource1, 1);
    }

    public void talkEX3000()
    {
        PlayAudio(audioSource1, 2);
    }
    public void talkEX4000()
    {
        PlayAudio(audioSource1, 3);
    }

    // 通用方法，用於播放指定 AudioSource 和聲音索引
    private void PlayAudio(AudioSource source, int clipIndex)
    {
        if (clipIndex >= 0 && clipIndex < audioClips.Length)
        {
            source.Stop();  // 停止當前正在播放的音檔
            source.clip = audioClips[clipIndex];  // 設定新的音檔
            source.Play();  // 播放新的音檔
        }
        else
        {
            Debug.LogError("Audio clip index out of range");
        }
    }
}
