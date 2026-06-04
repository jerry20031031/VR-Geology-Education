using UnityEngine;
using System.Collections.Generic;
using cherrydev;

public class VoiceManager : MonoBehaviour
{
    public Animator animator1;
    public Animator animator2;
    public AudioSource audioSource;
    private Dictionary<string, AudioClip> voiceMap = new Dictionary<string, AudioClip>();
    
    public Scene1Session s1;
    public Scene2Session s2;
    public Scene3Session s3;
    public Scene4Session s4;

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
            if(animator2 != null) animator2.SetTrigger("isTalk");
        }
        else
        {
            Debug.LogWarning($"Voice '{name}' not found in voiceMap.");
        }
    #region 預測
        if(name == "3-3-1") 
        {
            SessionLogger.LogAction("預測板塊運動原因：軟流圈的熱對流");
            s3.SessionPredit("學習者預測正確(軟流圈的熱對流)","正確");
        }
        if(name == "3-3-2") 
        {
            SessionLogger.LogAction("預測板塊運動原因：洋流推動");
            s3.SessionPredit("學習者預測錯誤(洋流推動) | 正確為軟流圈的熱對流","錯誤");
        }
        if(name == "3-3-3") 
        {
            SessionLogger.LogAction("預測板塊運動原因：地球自轉");
            s3.SessionPredit("學習者預測錯誤(地球自轉) | 正確為軟流圈的熱對流","錯誤");
        }
    #endregion
    
    #region Scene1
        if(name == "1-1") 
        {
            SessionLogger.LogAction("與教授對話獲取知識");
            s1.StartInteractTime();
        }
        if(name == "1-6") 
        {
            s1.SessionTalk1();
        }
    #endregion

    #region Scene2
        if(name == "1-7") 
        {
            SessionLogger.LogAction("與教授對話獲取知識");
            s2.StartInteractTime();
        }
        if(name == "2-2") 
        {
            s2.SessionTalk();
        }
        if(name == "2-2-False-1") 
        {
            SessionLogger.LogAction("與教授對話獲取知識");
            s2.StartInteractTime();
        }
        if(name == "2-2-False-3") 
        {
            s2.SessionTalkError();
        }

        if(name == "2-2-True-1") 
        {
            SessionLogger.LogAction("與教授對話獲取知識");
            s2.StartInteractTime();
        }
        if(name == "2-2-True-3") 
        {
            s2.SessionTalkTrue();
        }
    #endregion

    #region Scene3
        if(name == "3-1") 
        {
            SessionLogger.LogAction("與教授對話獲取知識");
            s3.StartInteractTime();
        }

        if(name == "3-11") 
        {
            s3.SessionTalk();
        }
    #endregion

    #region Scene4
        if(name == "4-1-1")
        {
            SessionLogger.LogAction("與教授對話獲取知識");
            s4.StartInteractTime();
        }
        if(name == "4-1-3")
        {
            s4.SessionTalk1();
        }

        if(name == "4-2-1") 
        {
            SessionLogger.LogAction("與教授對話獲取知識");
            s4.StartInteractTime();
        }
        if(name == "4-2-4") 
        {
            s4.SessionTalk2();
        }
    
        if(name == "4-3-1") 
        {
            SessionLogger.LogAction("與教授對話獲取知識");
            s4.StartInteractTime();
        }
        if(name == "4-3-3") 
        {
            s4.SessionTalk3();
        }
    
        if(name == "4-5-1") 
        {
            SessionLogger.LogAction("與教授對話獲取知識");
            s4.StartInteractTime();
        }
        if(name == "4-5-3") 
        {
            s4.SessionTalk4();
        }
    #endregion

    }
    
    public void StopTalk()
    {
        audioSource.Stop();
        animator1.SetBool("isTalk", false);
        if(animator2 != null) animator2.SetBool("isTalk", false);
    }
}
