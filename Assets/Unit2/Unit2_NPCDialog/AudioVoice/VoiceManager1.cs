using UnityEngine;
using System.Collections.Generic;
using cherrydev;

public class VoiceManager1 : MonoBehaviour
{
    public Animator animator1;
    public Animator animator2;
    public AudioSource audioSource;
    private Dictionary<string, AudioClip> voiceMap = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        LoadAllVoices();
    }

    private void LoadAllVoices()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio");
        foreach (var clip in clips)
        {
            if (!voiceMap.ContainsKey(clip.name))
            {
                voiceMap.Add(clip.name, clip);
            }
        }

        Debug.Log($"Loaded {voiceMap.Count} voice clips.");
    }

    public void PlayVoice(string name)
    {
        if (voiceMap.TryGetValue(name, out AudioClip clip))
        {
            audioSource.Stop();
            audioSource.PlayOneShot(clip);
            animator1.SetTrigger("isTalk");
            animator2.SetTrigger("isTalk");
        }
        else
        {
            Debug.LogWarning($"Voice '{name}' not found in voiceMap.");
        }
    }
    
    public void StopTalk()
    {
        audioSource.Stop();
        animator1.SetBool("isTalk", false);
        animator2.SetBool("isTalk", false);
    }
}
