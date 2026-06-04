using System;
using System.Collections;
using System.Collections.Generic;
using cherrydev;
using UnityEngine;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using System.Xml.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class TalkManager : MonoBehaviour
{
    public static TalkManager Instances;
    public GameObject Scene2Collider1;
    public GameObject Scene2Collider2;
    public TaskSystemUnit3 TaskSystem;
    public DialogBehaviour GirlTalk;
    public DialogBehaviour TeacherTalk;////
    private Action GielFunction;
    private Action TeacherFunction;
    public GameObject CenterEarth;
    public GameObject Scene3object1;

    public GameObject Wave;
    public Animator GirlMove;
    public Animator TeacherMove;
    public GameObject CenterMapPin;
    public GameObject Scene3AllMapPin;
    public XRSocketInteractor Scene3Socket1;
    public XRSocketInteractor Scene3Socket2;
    public XRSocketInteractor Scene3Socket3;

    //時間計時
    private float elapsedTime = 0f;
    private bool isRunning = false;
    public TextMeshProUGUI TeacherPanel;
    public TextMeshProUGUI GirlPanel;
    public AudioSource Audio;
    public List<AudioClip> Clips = new List<AudioClip>();



    // Start is called before the first frame update
    void Start()
    {
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Scene1MissionFailed", Scene1MissionFailed);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("WrongTalkLine", WrongTalkLine);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("TaskComplete", Scene2TaskComplete);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Scene2MissionHint", Scene2MissionHint);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Scene2MissionFailed", Scene2MissionFailed);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Scene3TaskComplete", Scene3TaskComplete);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Scene3MissionHint", Scene3MissionHint);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Scene3MissionFailed", Scene3MissionFailed);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Scene4TaskComplete", Scene4TaskComplete);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Scene4MissionHint", Scene4MissionHint);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Scene4MissionFailed", Scene4MissionFailed);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("showEarthquake", showEarthquake);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("GirlTalking", GirlTalking);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("GirlThinking", GirlThinking);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("TeacherTalking", TeacherTalking);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("LockScene3AllMapPin", LockScene3AllMapPin);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("LockScene3AllSocket", LockScene3AllSocket);
        //_______________________________暴力寫法小孩子請勿模仿__________________________________________
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher1_1", Teacher1_1);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher1_2", Teacher1_2);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher1_3", Teacher1_3);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher1_4", Teacher1_4);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher1_5", Teacher1_5);
        //__________________________________
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher2_1", Teacher2_1);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher2_2", Teacher2_2);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher2_3", Teacher2_3);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher2_4", Teacher2_4);

        //__________________________________
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl1_1", Girl1_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl1_2", Girl1_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl1_3", Girl1_3);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl1_4", Girl1_4);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl1_5", Girl1_5);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl1_6", Girl1_6);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl1_7", Girl1_7);
        //__________________________________
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl2_1", Girl2_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl2_2", Girl2_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl2_3", Girl2_3);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl2_4", Girl2_4);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl2_5", Girl2_5);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl2_6", Girl2_6);
        //__________________________________
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl3_1", Girl3_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl3_2", Girl3_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl3_3", Girl3_3);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl3_4", Girl3_4);
        //__________________________________
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl4_1", Girl4_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl4_2", Girl4_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl4_3", Girl4_3);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl4_4", Girl4_4);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl4_5", Girl4_5);
        //__________________________________
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl5_1", Girl5_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl5_2", Girl5_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl5_3", Girl5_3);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl5_4", Girl5_4);
        //__________________________________
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl6_1", Girl6_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl6_2", Girl6_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl6_3", Girl6_3);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl6_4", Girl6_4);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl6_5", Girl6_5);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl6_6", Girl6_6);
        //__________________________________
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl7_1", Girl7_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl7_2", Girl7_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl7_3", Girl7_3);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl7_4", Girl7_4);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl7_5", Girl7_5);
        //__________________________________
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl8_1", Girl8_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl8_2", Girl8_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl8_3", Girl8_3);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl8_4", Girl8_4);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl8_5", Girl8_5);
        //__________________________________
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl9_1", Girl9_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl9_2", Girl9_2);
        //__________________________________
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl9_1", Girl9_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl9_2", Girl9_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl10_1", Girl10_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl10_2", Girl10_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl10_3", Girl10_3);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl10_4", Girl10_4);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl10_5", Girl10_5);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl10_6", Girl10_6);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl10_7", Girl10_7);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl11_1", Girl11_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl11_2", Girl11_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("NewGirl11_2", NewGirl11_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl11_3", Girl11_3);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl11_4", Girl11_4);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl11_5", Girl11_5);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl11_6", Girl11_6);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl12_1", Girl12_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("NewGirl12_2_1", NewGirl12_2_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("NewGirl12_2_2", NewGirl12_2_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl12_3", Girl12_3);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl12_4", Girl12_4);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl13_1", Girl13_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl13_2", Girl13_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl13_3", Girl13_3);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl13_4", Girl13_4);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl13_5", Girl13_5);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl14_1", Girl14_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl14_2", Girl14_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl15_1", Girl15_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl15_2", Girl15_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl15_3", Girl15_3);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl16_1", Girl16_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl17_1", Girl17_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl18_1", Girl18_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl19_1", Girl19_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl19_2", Girl19_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl19_3", Girl19_3);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl19_4", Girl19_4);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl19_5", Girl19_5);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl20_1", Girl20_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl20_2", Girl20_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl20_3", Girl20_3);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl20_4", Girl20_4);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl20_5", Girl20_5);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl20_6", Girl20_6);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl20_7", Girl20_7);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl20_8", Girl20_8);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl20_9", Girl20_9);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl21_1", Girl21_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl21_2", Girl21_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl21_3", Girl21_3);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl21_4", Girl21_4);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl22_1", Girl22_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl22_2", Girl22_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl22_3", Girl22_3);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl22_4", Girl22_4);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl22_5", Girl22_5);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl22_6", Girl22_6);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl22_7", Girl22_7);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl22_8", Girl22_8);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl23_1", Girl23_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl23_2", Girl23_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("NewGirl23_2", NewGirl23_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl23_3", Girl23_3);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl23_4", Girl23_4);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl24_1", Girl24_1);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl24_2", Girl24_2);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl24_3", Girl24_3);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl24_4", Girl24_4);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("Girl24_5", Girl24_5);
        GirlTalk.ExternalFunctionsHandler.BindExternalFunction("GirlNew5-2", GirlNew5_2);

        // TeacherTalk 綁定剩餘函式
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher3_1", Teacher3_1);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher3_2", Teacher3_2);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher3_3", Teacher3_3);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher3_4", Teacher3_4);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher3_5", Teacher3_5);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher3_6", Teacher3_6);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher3_7", Teacher3_7);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher3_8", Teacher3_8);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher4_1", Teacher4_1);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher4_2", Teacher4_2);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher4_3", Teacher4_3);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher4_4", Teacher4_4);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher4_5", Teacher4_5);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher5_1", Teacher5_1);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher5_2", Teacher5_2);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher5_3", Teacher5_3);
        TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher5_4", Teacher5_4);

    }

    // Update is called once per frame
    void Update()
    {

        if (isRunning)
        {
            elapsedTime += Time.deltaTime; // 計算經過的時間
        }
    }

    //共用外部函式
    public void GirlThinking()
    {
        GirlMove.SetBool("Talking", false);
        GirlMove.SetBool("Thinking", true);

    }
    public void GirlTalking()
    {
        GirlMove.SetBool("Thinking", false);
        GirlMove.SetBool("Talking", true);

    }
    public void TeacherTalking()
    {

        TeacherMove.SetBool("Talking", true);

    }
    public void GirlIdle()
    {
        GirlMove.SetBool("Thinking", false);
        GirlMove.SetBool("Talking", false);

    }
    public void TeacherIdle()
    {
        TeacherMove.SetBool("Talking", false);

    }


    //分鏡一對話外部函式

    //Teacher1_________________________________________________________________
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
    //Teacher2_________________________________________________________________
    public void Teacher2_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[49]);
    }
    public void Teacher2_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[50]);
    }
    public void Teacher2_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[51]);
    }
    public void Teacher2_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[52]);
    }
    //Teacher3_________________________________________________________________
    public void Teacher3_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[75]);
    }
    public void Teacher3_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[76]);
    }
    public void Teacher3_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[77]);
    }
    public void Teacher3_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[78]);
    }
    public void Teacher3_5()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[79]);
    }
    public void Teacher3_6()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[80]);
    }
    public void Teacher3_7()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[81]);
    }
    public void Teacher3_8()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[82]);
        CenterEarthquake();
    }
    //Teacher4_________________________________________________________________
    public void Teacher4_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[83]);
    }
    public void Teacher4_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[84]);
    }
    public void Teacher4_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[85]);
    }
    public void Teacher4_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[86]);
    }
    public void Teacher4_5()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[87]);
    }
    //Teacher5_________________________________________________________________
    public void Teacher5_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[131]);
    }
    public void Teacher5_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[132]);
    }
    public void Teacher5_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[133]);
    }
    public void Teacher5_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[134]);
    }
    //Girl1_________________________________________________________________
    public void Girl1_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[5]);
    }
    public void Girl1_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[6]);
    }
    public void Girl1_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[7]);
    }
    public void Girl1_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[8]);
    }
    public void Girl1_5()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[9]);
    }
    public void Girl1_6()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[10]);
    }
    public void Girl1_7()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[11]);
    }
    //Girl2_________________________________________________________________

    public void Girl2_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[12]);
    }
    public void Girl2_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[13]);
    }
    public void Girl2_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[14]);
    }
    public void Girl2_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[15]);
    }
    public void Girl2_5()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[16]);
    }
    public void Girl2_6()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[17]);
    }
    //Girl3_________________________________________________________________

    public void Girl3_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[18]);
    }

    public void Girl3_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[19]);
    }

    public void Girl3_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[20]);
    }

    public void Girl3_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[21]);
    }
    //Girl4_________________________________________________________________
    public void Girl4_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[22]);
    }
    public void Girl4_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[23]);
    }
    public void Girl4_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[24]);
    }
    public void Girl4_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[25]);
    }
    public void Girl4_5()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[26]);
    }
    //Girl5_________________________________________________________________
    public void Girl5_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[27]);
    }
    public void Girl5_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[28]);
    }
    public void GirlNew5_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[135]);
        Scene1MissionHint();
    }
    public void Girl5_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[29]);
    }
    public void Girl5_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[30]);
        Scene1MissionFailed();
    }
    //Girl6_________________________________________________________________
    public void Girl6_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[31]);
    }
    public void Girl6_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[32]);

    }
    public void Girl6_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[33]);
    }
    public void Girl6_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[34]);
        Scene1MissionComplete();
    }
    public void Girl6_5()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[35]);
    }
    public void Girl6_6()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[36]);

    }
    //Girl7_________________________________________________________________
    public void Girl7_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[37]);
    }
    public void Girl7_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[38]);
    }
    public void Girl7_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[39]);
    }
    public void Girl7_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[40]);
    }
    public void Girl7_5()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[41]);
    }
    //Girl8_________________________________________________________________
    public void Girl8_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[42]);
    }
    public void Girl8_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[43]);
    }
    public void Girl8_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[44]);
    }
    public void Girl8_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[45]);
    }
    public void Girl8_5()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[46]);
    }
    //Girl9_________________________________________________________________
    public void Girl9_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[47]);
    }
    public void Girl9_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[48]);
    }
    //______________________________________________________________________

    public void Girl10_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[53]);
        LockScene2MapPin();
    }
    public void Girl10_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[54]);
        Scene2MissionHint();
    }
    public void Girl10_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[55]);
    }
    public void Girl10_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[56]);
    }
    public void Girl10_5()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[57]);
    }
    public void Girl10_6()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[58]);
    }
    public void Girl10_7()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[59]);
        Scene2TaskComplete();
    }
    //______________________________________________________________________
    public void Girl11_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[60]); // 原為 Clips[58]
        LockScene2MapPin();
    }
    public void Girl11_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[61]); // 原為 Clips[59]
        Scene2MissionHint();
    }
      public void NewGirl11_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[136]); // 原為 Clips[59]
    }
    public void Girl11_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[62]); // 原為 Clips[60]
    }
    public void Girl11_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[63]); // 原為 Clips[61]
        WrongTalkLine();
    }
    public void Girl11_5()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[64]); // 原為 Clips[62]

    }
    public void Girl11_6()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[65]); // 原為 Clips[62]
        Scene2MissionFailed();
    }
    //______________________________________________________________________
    public void Girl12_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[66]); // 原為 Clips[63]
        LockScene2MapPin();
    }
    public void NewGirl12_2_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[67]); // 原為 Clips[64]
        
    }
    public void NewGirl12_2_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[137]); // 原為 Clips[64]
        Scene2MissionHint();
    }
    public void Girl12_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[68]); // 原為 Clips[65]
    }
    public void Girl12_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[69]); // 原為 Clips[66]
        Scene2MissionFailed();
    }
    //______________________________________________________________________
    public void Girl13_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[70]); // 原為 Clips[67]
        LockScene2MapPin();
    }
    public void Girl13_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[71]); // 原為 Clips[68]
        Scene2MissionHint();
    }
    public void Girl13_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[72]); // 原為 Clips[69]
    }
    public void Girl13_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[73]); // 原為 Clips[70]
    }
    public void Girl13_5()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[74]); // 原為 Clips[71]
        Scene2TaskComplete();
    }
    //______________________________________________________________________
    public void Girl14_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[88]); // 原為 Clips[85]
    }
    public void Girl14_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[89]); // 原為 Clips[86]
    }
    //______________________________________________________________________
    public void Girl15_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[90]); // 原為 Clips[87]
    }
    public void Girl15_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[91]); // 原為 Clips[88]
    }
    public void Girl15_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[92]); // 原為 Clips[89]
    }
    //______________________________________________________________________
    public void Girl16_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[93]); // 原為 Clips[90]
    }
    //______________________________________________________________________
    public void Girl17_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[94]); // 原為 Clips[91]
    }
    //______________________________________________________________________
    public void Girl18_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[95]); // 原為 Clips[92]
    }
    //______________________________________________________________________
    public void Girl19_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[96]); // 原為 Clips[93]
        LockScene3AllMapPin();
    }
    public void Girl19_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[97]); // 原為 Clips[94]
    }
    public void Girl19_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[98]); // 原為 Clips[95]
        Scene3MissionHint();
    }
    public void Girl19_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[99]); // 原為 Clips[96]
    }
    public void Girl19_5()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[100]); // 原為 Clips[97]
        Scene3MissionFailed();
    }
    //______________________________________________________________________
    public void Girl20_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[101]); // 原為 Clips[98]
        LockScene3AllMapPin();
    }
    public void Girl20_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[102]); // 原為 Clips[99]
    }
    public void Girl20_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[103]); // 原為 Clips[100]
        Scene3MissionHint();
    }
    public void Girl20_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[104]); // 原為 Clips[101]
    }
    public void Girl20_5()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[105]); // 原為 Clips[102]
        Scene3TaskComplete();
    }
    public void Girl20_6()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[106]); // 原為 Clips[103]
    }
    public void Girl20_7()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[107]); // 原為 Clips[104]
    }
    public void Girl20_8()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[108]); // 原為 Clips[105]
    }
    public void Girl20_9()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[109]); // 原為 Clips[106]
    }
    //______________________________________________________________________
    public void Girl21_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[110]); // 原為 Clips[107]
    }
    public void Girl21_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[111]); // 原為 Clips[108]
    }
    public void Girl21_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[112]); // 原為 Clips[109]
    }
    public void Girl21_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[113]); // 原為 Clips[110]
    }
    //______________________________________________________________________
    public void Girl22_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[114]); // 原為 Clips[111]
    }
    public void Girl22_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[115]); // 原為 Clips[112]
    }
    public void Girl22_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[116]); // 原為 Clips[113]
    }
    public void Girl22_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[117]); // 原為 Clips[114]
    }
    public void Girl22_5()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[118]); // 原為 Clips[115]
    }
    public void Girl22_6()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[119]); // 原為 Clips[116]
    }
    public void Girl22_7()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[120]); // 原為 Clips[117]
    }
    public void Girl22_8()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[121]); // 原為 Clips[118]
    }
    //______________________________________________________________________
    public void Girl23_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[122]); // 原為 Clips[119]
        LockScene3AllSocket();
    }
    public void Girl23_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[123]); // 原為 Clips[120]
    }
    public void NewGirl23_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[138]); // 原為 Clips[120]
        Scene4MissionHint();
    }
    public void Girl23_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[124]); // 原為 Clips[121]
    }
    public void Girl23_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[125]); // 原為 Clips[122]
        Scene4MissionFailed();
    }
    //______________________________________________________________________
    public void Girl24_1()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[126]); // 原為 Clips[123]
        LockScene3AllSocket();
    }
    public void Girl24_2()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[127]); // 原為 Clips[124]
        Scene4MissionHint();
    }
    public void Girl24_3()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[128]); // 原為 Clips[125]
    }
    public void Girl24_4()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[129]); // 原為 Clips[126]
        Scene4TaskComplete();
    }
    public void Girl24_5()
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[130]); // 原為 Clips[127]
    }
    public void Scene1MissionComplete()
    {
        TaskSystem.ForceCompleteTask("將模型上的白色區域染色後，與學姊對話");
    }
    public void Scene1MissionFailed()
    {
        LogSystemUnit3.LogAction("任務一回答錯誤");
        LogSystemUnit3.instance.InteractionFailures++;

    }
    public void Scene1MissionHint()
    {
        LogSystemUnit3.LogAction("任務一：斷層類型分類提示");
        LogSystemUnit3.instance.HintCount++;
    }

    //分鏡二對話外部函式
    public void WrongTalkLine()
    {
        Scene2Collider1.SetActive(false);
        Scene2Collider2.SetActive(true);
    }
    public void Scene2TaskComplete()
    {

        TaskSystem.ForceCompleteTask("放置物件到震央點上，並與學姊對話確認結果");
        UnLockScene2MapPin();


    }
    public void CenterEarthquake()
    {
        CenterEarth.SetActive(true);
    }
    public void showEarthquake()
    {
        Wave.SetActive(true);
    }
    public void LockScene2MapPin()
    {
        CenterMapPin.GetComponent<XRGrabInteractable>().enabled = false;

    }
    public void UnLockScene2MapPin()
    {
        CenterMapPin.GetComponent<XRGrabInteractable>().enabled = true;

    }
    public void Scene2MissionFailed()
    {
        LogSystemUnit3.LogAction("任務二回答錯誤");
        UnLockScene2MapPin();
        LogSystemUnit3.instance.InteractionFailures++;

    }
    public void Scene2MissionHint()
    {
        LogSystemUnit3.LogAction("任務二提示");
        UnLockScene2MapPin();
        LogSystemUnit3.instance.HintCount++;

    }


    //分鏡三對話外部函式

    public void Scene3TaskComplete()
    {


        TaskSystem.ForceCompleteTask("選出所有包含板塊交界處的圖標，並與學姊確認");
        Scene3object1.SetActive(false);




    }
    public void Scene4TaskComplete()
    {
        TaskSystem.ForceCompleteTask("將特徵物件放置正確位置");

    }
    public void LockScene3AllMapPin()
    {
        Scene3AllMapPin.SetActive(!Scene3AllMapPin.activeSelf);

    }
    public void Scene3MissionFailed()
    {
        LogSystemUnit3.LogAction("任務三回答錯誤");
        LockScene3AllMapPin();
        LogSystemUnit3.instance.InteractionFailures++;
    }
    public void Scene3MissionHint()
    {
        LogSystemUnit3.LogAction("任務三提示");
        LockScene3AllMapPin();
        LogSystemUnit3.instance.HintCount++;

    }
    public void Scene4MissionFailed()
    {
        LogSystemUnit3.LogAction("特徵物件放置錯誤");
        UnLockScene3AllSocket();
        LogSystemUnit3.instance.InteractionFailures++;
    }
    public void Scene4MissionHint()
    {
        LogSystemUnit3.LogAction("特徵物件放置提示");
        UnLockScene3AllSocket();
        LogSystemUnit3.instance.HintCount++;
    }

    public void LockScene3AllSocket()
    {
        XRSocketInteractor[] sockets = new XRSocketInteractor[] { Scene3Socket1, Scene3Socket2, Scene3Socket3 };

        foreach (var socket in sockets)
        {
            if (socket.interactablesSelected.Count > 0 && socket.interactablesSelected[0] != null)
            {
                XRGrabInteractable grab = socket.interactablesSelected[0].transform.GetComponent<XRGrabInteractable>();
                if (grab != null)
                {
                    grab.interactionLayers = 1 << LayerMask.NameToLayer("nograb"); // 或用 LayerMask.GetMask("Locked")
                }
            }
        }
    }

    public void UnLockScene3AllSocket()
    {
        XRSocketInteractor[] sockets = new XRSocketInteractor[] { Scene3Socket1, Scene3Socket2, Scene3Socket3 };

        foreach (var socket in sockets)
        {
            if (socket.interactablesSelected.Count > 0 && socket.interactablesSelected[0] != null)
            {
                XRGrabInteractable grab = socket.interactablesSelected[0].transform.GetComponent<XRGrabInteractable>();
                if (grab != null)
                {
                    grab.interactionLayers = LayerMask.GetMask("Default"); // 解鎖時恢復可抓取層
                }
            }
        }
    }

    //分鏡四對話外部函式



    //分鏡五對話外部函式


    //對話顏色重點
}
