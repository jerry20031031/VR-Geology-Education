using cherrydev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit1TaskManager222 : MonoBehaviour
{
    public DialogBehaviour Person1Talk2; //教授
    public AudioSource audioSource1;  // 第一個AudioSource組件，用於播放音檔
    public AudioClip[] audioClips;  // 存儲音檔的陣列
    // Start is called before the first frame update
    void Awake()
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length >= 2)
        {
            audioSource1 = sources[0];  //學姊
        }
    }
  
    void Start()
    {
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk320", talk320);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk330", talk330);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk340", talk340);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk350", talk350);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void talk320()
    {
        PlayAudio(audioSource1, 0);
    }
    public void talk330()
    {
        PlayAudio(audioSource1, 1);
    }
    public void talk340()
    {
        PlayAudio(audioSource1, 2);
    }
    public void talk350()
    {
        PlayAudio(audioSource1, 3);
    }
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
