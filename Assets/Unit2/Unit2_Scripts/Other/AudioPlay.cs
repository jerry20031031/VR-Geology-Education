using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cherrydev;

public class AudioPlay : MonoBehaviour
{

    public DialogBehaviour TeacherTalk_1;
    public DialogBehaviour TeacherTalk_2;


    public AudioSource Audio;
    public List<AudioClip> Clips = new List<AudioClip>();
    // Start is called before the first frame update
    void Start()
    {
       //TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher2_2", Teacher2_2);
    }
    public void Teacher3_7()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[81]);
    }
}
