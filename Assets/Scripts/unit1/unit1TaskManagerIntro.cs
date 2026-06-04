using cherrydev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit1TaskManagerIntro : MonoBehaviour
{
    public DialogBehaviour Person1Talk;  //學姊

    public AudioClip[] audioClips;  // 存儲音檔的陣列
    public AudioSource audioSource1;  // 第一個AudioSource組件，用於播放音檔

    public Animator GirlMove1;

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
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talk1000", talk1000);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talk2000", talk2000);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talk3000", talk3000);
    }
    public void GirlTalking()
    {
        GirlMove1.SetBool("Thinking", false);
        GirlMove1.SetBool("Talking", true);

    }

    public void GirlIdle()
    {
        GirlMove1.SetBool("Thinking", false);
        GirlMove1.SetBool("Talking", false);

    }
    //audioSource1是學姊
    public void talk1000()
    {
        PlayAudio(audioSource1, 0);
    }

    public void talk2000()
    {
        PlayAudio(audioSource1, 1);
    }

    public void talk3000()
    {
        PlayAudio(audioSource1, 2);
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
