using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{

    public static AudioSystem instance;
    public AudioSource sfxSource;
     private AudioSource loopingSfxSource;

    private void Awake()
    {
          if (instance == null)
        {
            instance = this;
           

            // 初始化一个新的 AudioSource 用于循环音效
            loopingSfxSource = gameObject.AddComponent<AudioSource>();
            loopingSfxSource.loop = true; // 设置循环播放
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlayLoopingSFX(AudioClip clip)
    {
        if (loopingSfxSource.isPlaying) loopingSfxSource.Stop(); // 停止当前播放的循环音效
        loopingSfxSource.clip = clip;
        loopingSfxSource.Play();
    }
    public void StopLoopingSFX()
    {
        if (loopingSfxSource.isPlaying)
        {
            loopingSfxSource.Stop();
        }
    }


}
