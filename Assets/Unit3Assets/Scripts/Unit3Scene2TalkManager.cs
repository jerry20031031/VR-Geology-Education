using System.Collections;
using System.Collections.Generic;
using cherrydev;
using UnityEngine;

public class Unit3Scene2TalkManager : MonoBehaviour
{
    public DialogBehaviour TeacherTalk;    // Start is called before the first frame update
    public AudioSource Audio;
    public List<AudioClip> Clips = new List<AudioClip>();
    void Start()
    {
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher1_1", Teacher1_1);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher1_2", Teacher1_2);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher1_3", Teacher1_3);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher1_4", Teacher1_4);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher1_5", Teacher1_5);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher2_1", Teacher2_1);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher2_2", Teacher2_2);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher2_3", Teacher2_3);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher2_4", Teacher2_4);

    }
    public void Teacher1_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[0]);


    }
    public void Teacher1_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[1]);


    }
    public void Teacher1_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[2]);


    }
    public void Teacher1_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[3]);


    }

    public void Teacher1_5()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[4]);


    }

    // -----------------------------------------
    public void Teacher2_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[5]);


    }
    public void Teacher2_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[6]);


    }
    public void Teacher2_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[7]);


    }
    public void Teacher2_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[8]);


    }
    // Update is called once per frame
    void Update()
    {

    }
}
