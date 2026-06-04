using System.Collections;
using System.Collections.Generic;
using cherrydev;
using UnityEngine;

public class Unit3Scene3TalkManager : MonoBehaviour
{
    public DialogBehaviour GirlTalk;
    public List<AudioClip> Clips = new List<AudioClip>();
    public AudioSource Audio;
    void Start()
    {
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("GirlTalk1", GirlTalk1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("GirlTalk2", GirlTalk2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("GirlTalk3", GirlTalk3);

    }
    public void GirlTalk1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[0]);

    }
    public void GirlTalk2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[1]);
    }
    public void GirlTalk3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[2]);

    }
}
