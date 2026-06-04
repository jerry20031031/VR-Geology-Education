using System;
using System.Collections;
using System.Collections.Generic;
using cherrydev;
using UnityEngine;

public class TalkManager1 : MonoBehaviour
{
  // Start is called before the first frame update\
  public unclockUI unlockUI;
  public TaskSystemUnit3 taskSystem;
  public DialogBehaviour Person1Talk;
  public DialogBehaviour Person2Talk;
  public DialogBehaviour Person3Talk;
  public DialogBehaviour SUPTalk1;
  public DialogBehaviour SUPTalk2;
  public DialogBehaviour SUPTalk3;
  public DialogBehaviour TeacherTalk;
  public ResetOBJECTNoUpdate Stone1;
  public ResetOBJECTNoUpdate Stone2;
  public ResetOBJECTNoUpdate Stone3;
  public ResetOBJECTNoUpdate Stone4;
  public ResetOBJECTNoUpdate Stone5;
  public ResetOBJECTNoUpdate Stone6;
  public ResetOBJECTNoUpdate Stone7;
  public ResetOBJECTNoUpdate Stone8;
  public ResetOBJECTNoUpdate Stone9;
  public ResetOBJECTNoUpdate Stone10;
  public ResetOBJECTNoUpdate Stone11;
  public AudioSource Audio;
  public List<AudioClip> Clips = new List<AudioClip>();
  public Backpack backpack;
  

  private bool isRunning = false;
  void Start()
  {
    Stone11.initPosoition();
    Stone6.initPosoition();
    TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher1_1", Teacher1_1);
    TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher1_2", Teacher1_2);
    TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher1_3", Teacher1_3);
    TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher1_4", Teacher1_4);
    TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher1_5", Teacher1_5);
    TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher1_6", Teacher1_6);
    TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher2_1", Teacher2_1);
    TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher2_2", Teacher2_2);
    TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher2_3", Teacher2_3);
    TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher2_4", Teacher2_4);
    TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher3_1", Teacher3_1);
    TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher3_2", Teacher3_2);
    TeacherTalk.ExternalFunctionsHandler.BindExternalFunction("Teacher3_3", Teacher3_3);
    SUPTalk1.ExternalFunctionsHandler.BindExternalFunction("SupTalk1_1", SupTalk1_1);
    SUPTalk1.ExternalFunctionsHandler.BindExternalFunction("SupTalk1_2", SupTalk1_2);
    SUPTalk1.ExternalFunctionsHandler.BindExternalFunction("SupTalk1_3", SupTalk1_3);
    SUPTalk2.ExternalFunctionsHandler.BindExternalFunction("SupTalk2_1", SupTalk2_1);
    SUPTalk2.ExternalFunctionsHandler.BindExternalFunction("SupTalk2_2", SupTalk2_2);
    SUPTalk2.ExternalFunctionsHandler.BindExternalFunction("SupTalk2_3", SupTalk2_3);
    SUPTalk3.ExternalFunctionsHandler.BindExternalFunction("SupTalk3_1", SupTalk3_1);
    SUPTalk3.ExternalFunctionsHandler.BindExternalFunction("SupTalk3_2", SupTalk3_2);
    SUPTalk3.ExternalFunctionsHandler.BindExternalFunction("SupTalk3_3", SupTalk3_3);
    SUPTalk3.ExternalFunctionsHandler.BindExternalFunction("SupTalk3_4", SupTalk3_4);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1TalkWronAnswer", Person1TalkWronAnswer);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk1_1", Person1Talk1_1);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk1_2", Person1Talk1_2);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk1_3", Person1Talk1_3);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk1_3_1", Person1Talk1_3_1);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk1_3_2", Person1Talk1_3_2);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk1_4", Person1Talk1_4);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk1_5", Person1Talk1_5);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk1_6", Person1Talk1_6);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk2_1", Person1Talk2_1);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk2_2", Person1Talk2_2);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk2_3", Person1Talk2_3);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk2_4", Person1Talk2_4);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk3_1", Person1Talk3_1);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk3_2", Person1Talk3_2);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk3_3", Person1Talk3_3);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk3_4", Person1Talk3_4);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk3_5", Person1Talk3_5);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk3_6", Person1Talk3_6);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk3_7", Person1Talk3_7);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk3_8", Person1Talk3_8);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk3_9", Person1Talk3_9);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk3_10", Person1Talk3_10);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk3_11", Person1Talk3_11);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk3_12", Person1Talk3_12);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk3_13", Person1Talk3_13);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk3_14", Person1Talk3_14);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk3_15", Person1Talk3_15);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk3_16", Person1Talk3_16);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk3_17", Person1Talk3_17);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk3_18", Person1Talk3_18);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("Person1Talk3_19", Person1Talk3_19);
    //------------------------------------------
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2TalkWronAnswer", Person2TalkWronAnswer);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk1_1", Person2Talk1_1);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk1_2", Person2Talk1_2);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk1_3", Person2Talk1_3);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk1_3_1", Person2Talk1_3_1);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk1_4", Person2Talk1_4);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk1_5", Person2Talk1_5);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk1_6", Person2Talk1_6);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk2_1", Person2Talk2_1);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk2_2", Person2Talk2_2);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk2_3", Person2Talk2_3);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk2_4", Person2Talk2_4);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk3_1", Person2Talk3_1);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk3_2", Person2Talk3_2);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk3_3", Person2Talk3_3);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk3_4", Person2Talk3_4);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk3_5", Person2Talk3_5);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk3_6", Person2Talk3_6);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk3_7", Person2Talk3_7);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk3_8", Person2Talk3_8);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk3_9", Person2Talk3_9);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk3_10", Person2Talk3_10);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk3_11", Person2Talk3_11);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk3_12", Person2Talk3_12);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk3_13", Person2Talk3_13);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk3_14", Person2Talk3_14);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk3_15", Person2Talk3_15);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk3_16", Person2Talk3_16);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk3_17", Person2Talk3_17);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk3_18", Person2Talk3_18);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("Person2Talk3_19", Person2Talk3_19);
    //------------------------------------------
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3TalkWronAnswer", Person3TalkWronAnswer);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk1_1", Person3Talk1_1);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk1_2", Person3Talk1_2);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk1_3", Person3Talk1_3);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk1_3_1", Person3Talk1_3_1);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk1_4", Person3Talk1_4);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk1_5", Person3Talk1_5);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk1_6", Person3Talk1_6);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk2_1", Person3Talk2_1);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk2_2", Person3Talk2_2);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk2_3", Person3Talk2_3);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk2_4", Person3Talk2_4);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk3_1", Person3Talk3_1);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk3_2", Person3Talk3_2);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk3_3", Person3Talk3_3);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk3_4", Person3Talk3_4);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk3_5", Person3Talk3_5);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk3_6", Person3Talk3_6);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk3_7", Person3Talk3_7);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk3_8", Person3Talk3_8);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk3_9", Person3Talk3_9);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk3_10", Person3Talk3_10);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk3_11", Person3Talk3_11);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk3_12", Person3Talk3_12);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk3_13", Person3Talk3_13);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk3_14", Person3Talk3_14);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk3_15", Person3Talk3_15);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk3_16", Person3Talk3_16);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk3_17", Person3Talk3_17);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk3_18", Person3Talk3_18);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("Person3Talk3_19", Person3Talk3_19);
    // ------------------------------------------
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("PlainTaskComplete", PlainTaskComplete);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("igneousTaskComplete", igneousTaskComplete);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("MetamorphicTaskComplete", MetamorphicTaskComplete);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("igneousTaskStart", igneousTaskStart);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("MetamorphicTaskStart", MetamorphicTaskStart);
    Person1Talk.ExternalFunctionsHandler.BindExternalFunction("PlainTaskFailed", PlainTaskFailed);
    Person2Talk.ExternalFunctionsHandler.BindExternalFunction("igneousTaskFailed", igneousTaskFailed);
    Person3Talk.ExternalFunctionsHandler.BindExternalFunction("MetamorphicTaskFailed", MetamorphicTaskFailed);
  }

  // Update is called once per frame
  void Update()
  {


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
  public void Teacher1_6()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[109]);
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
    ResetObject();


  }

  // -----------------------------------------
  public void Teacher3_1()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[9]);


  }

  public void Teacher3_2()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[10]);


  }
  public void Teacher3_3()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[11]);


  }
  public void SupTalk1_1()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[12]);


  }
  public void SupTalk1_2()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[13]);


  }
  public void SupTalk1_3()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[14]);


  }
  public void SupTalk2_1()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[15]);


  }
  public void SupTalk2_2()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[16]);


  }
  public void SupTalk2_3()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[17]);


  }
  public void SupTalk3_1()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[18]);


  }
  public void SupTalk3_2()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[19]);


  }
  public void SupTalk3_3()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[20]);


  }
  public void SupTalk3_4()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[21]);


  }
  public void Person1Talk1_1()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[22]);


  }
  public void Person1TalkWronAnswer()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[111]);
    PlainTaskFailed();

  }
  public void Person1Talk1_2()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[23]);


  }
  public void Person1Talk1_3()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[24]);
    LogSystemUnit3.instance.PredictionResult = true;

  }
  public void Person1Talk1_3_1()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[24]);
    LogSystemUnit3.instance.PredictionResult = false;

  }
  public void Person1Talk1_3_2()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[24]);
    LogSystemUnit3.instance.PredictionResult = false;
  }

  public void Person1Talk1_4()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[25]);


  }

  public void Person1Talk1_5()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[26]);


  }
  public void Person1Talk1_6()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[27]);
    PlainTaskStart();
  }
  public void Person1Talk2_1()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[28]);
  }
  public void Person1Talk2_2()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[29]);


  }
  public void Person1Talk2_3()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[30]);


  }
  public void Person1Talk2_4()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[31]);


  }
  public void Person1Talk3_1()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[32]);


  }
  public void Person1Talk3_2()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[33]);


  }
  public void Person1Talk3_3()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[34]);


  }
  public void Person1Talk3_4()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[35]);


  }
  public void Person1Talk3_5()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[36]);


  }
  public void Person1Talk3_6()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[37]);



  }
  public void Person1Talk3_7()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[38]);



  }
  public void Person1Talk3_8()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[39]);


  }
  public void Person1Talk3_9()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[40]);



  }
  public void Person1Talk3_10()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[41]);


  }
  public void Person1Talk3_11()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[42]);



  }
  public void Person1Talk3_12()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[43]);


  }
  public void Person1Talk3_13()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[44]);



  }
  public void Person1Talk3_14()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[45]);



  }
  public void Person1Talk3_15()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[46]);


  }
  public void Person1Talk3_16()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[47]);


  }
  public void Person1Talk3_17()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[48]);


  }
  public void Person1Talk3_18()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[49]);


  }
  public void Person1Talk3_19()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[50]);
    PlainTaskComplete();


  }
  public void Person2TalkWronAnswer()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[112]);
    igneousTaskFailed();
  }
  public void Person2Talk1_1()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[51]);
  }
  public void Person2Talk1_2()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[52]);
  }
  public void Person2Talk1_3()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[53]);
    LogSystemUnit3.instance.PredictionResult = true;
  }
  public void Person2Talk1_3_1()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[53]);
    LogSystemUnit3.instance.PredictionResult = false;
  }
  public void Person2Talk1_4()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[54]);
  }
  public void Person2Talk1_5()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[55]);
  }
  public void Person2Talk1_6()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[56]);
    igneousTaskStart();
  }
  public void Person2Talk2_1()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[57]);
  }
  public void Person2Talk2_2()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[58]);
  }
  public void Person2Talk2_3()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[59]);
  }
  public void Person2Talk2_4()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[60]);
  }
  public void Person2Talk3_1()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[61]);
  }
  public void Person2Talk3_2()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[62]);
  }
  public void Person2Talk3_3()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[63]);
  }
  public void Person2Talk3_4()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[64]);
  }
  public void Person2Talk3_5()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[65]);
  }
  public void Person2Talk3_6()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[66]);

  }
  public void Person2Talk3_7()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[67]);

  }
  public void Person2Talk3_8()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[68]);
  }
  public void Person2Talk3_9()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[69]);

  }
  public void Person2Talk3_10()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[70]);
  }
  public void Person2Talk3_11()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[71]);

  }
  public void Person2Talk3_12()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[72]);
  }
  public void Person2Talk3_13()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[73]);

  }
  public void Person2Talk3_14()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[74]);

  }
  public void Person2Talk3_15()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[75]);
  }
  public void Person2Talk3_16()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[76]);
  }
  public void Person2Talk3_17()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[77]);
  }
  public void Person2Talk3_18()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[78]);
  }
  public void Person2Talk3_19()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[79]);
    igneousTaskComplete();
  }
  public void Person3TalkWronAnswer()
  {
     Audio.Stop();
    Audio.PlayOneShot(Clips[110]);
    MetamorphicTaskFailed();
  }
  public void Person3Talk1_1()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[80]);
  }
  public void Person3Talk1_2()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[81]);
  }
  public void Person3Talk1_3()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[82]);
    LogSystemUnit3.instance.PredictionResult = true;
  }
  public void Person3Talk1_3_1()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[82]);
    LogSystemUnit3.instance.PredictionResult = false;
  }
  public void Person3Talk1_4()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[83]);
  }
  public void Person3Talk1_5()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[84]);
  }
  public void Person3Talk1_6()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[85]);
    MetamorphicTaskStart();
  }
  public void Person3Talk2_1()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[86]);
  }
  public void Person3Talk2_2()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[87]);
  }
  public void Person3Talk2_3()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[88]);
  }
  public void Person3Talk2_4()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[89]);
  }
  public void Person3Talk3_1()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[90]);
  }
  public void Person3Talk3_2()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[91]);
  }
  public void Person3Talk3_3()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[92]);
  }
  public void Person3Talk3_4()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[93]);
  }
  public void Person3Talk3_5()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[94]);

  }
  public void Person3Talk3_6()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[95]);
  }
  public void Person3Talk3_7()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[96]);

  }
  public void Person3Talk3_8()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[97]);
  }
  public void Person3Talk3_9()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[98]);
  }
  public void Person3Talk3_10()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[99]);

  }

  public void Person3Talk3_11()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[100]);

  }
  public void Person3Talk3_12()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[101]);
  }
  public void Person3Talk3_13()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[102]);

  }
  public void Person3Talk3_14()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[103]);
  }
  public void Person3Talk3_15()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[104]);

  }
  public void Person3Talk3_16()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[105]);
  }
  public void Person3Talk3_17()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[106]);
  }
  public void Person3Talk3_18()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[107]);
  }
  public void Person3Talk3_19()
  {
    Audio.Stop();
    Audio.PlayOneShot(Clips[108]);
    MetamorphicTaskComplete();
  }
  public void WrongPredict()
  {
    LogSystemUnit3.instance.PredictionResult = false;
  }

  private string FormatTime(float time)
  {
    int minutes = Mathf.FloorToInt(time / 60);
    int seconds = Mathf.FloorToInt(time % 60);
    return string.Format("{0:00}:{1:00}", minutes, seconds); // 轉換為 MM:SS 格式
  }
  public void PlainTaskStart()
  {
    LogSystemUnit3.instance.StartTimer();
  }
  public void igneousTaskStart()
  {
    LogSystemUnit3.instance.StartTimer();
  }
  public void MetamorphicTaskStart()
  {
    LogSystemUnit3.instance.StartTimer();
  }
  public void PlainTaskFailed()
  {
    LogSystemUnit3.LogAction("沉積岩岩石判斷錯誤");
    LogSystemUnit3.instance.InteractionFailures++;
  }
  public void igneousTaskFailed()
  {
    LogSystemUnit3.LogAction("火沉岩岩石判斷錯誤");
    LogSystemUnit3.instance.InteractionFailures++;
  }
  public void MetamorphicTaskFailed()
  {
    LogSystemUnit3.LogAction("變質岩岩石判斷錯誤");
    LogSystemUnit3.instance.InteractionFailures++;
  }
  public void PlainTaskComplete()
  {
    unlockUI.unclock(0);
    unlockUI.unclock(3);
    taskSystem.ForceCompleteTask("完成沉積岩區任務");
    LogSystemUnit3.instance.StopTimer();
    LogSystemUnit3.instance.LogActionAndTime("沉績岩區任務完成時間:");
    LogSystemUnit3.instance.TotalKnowledgePoints = 2;
    LogSystemUnit3.instance.UpdateTaskRowByLine(8);

  }
  public void igneousTaskComplete()
  {
    unlockUI.unclock(1);
    taskSystem.ForceCompleteTask("完成火成岩區任務");
    LogSystemUnit3.instance.StopTimer();
    LogSystemUnit3.instance.LogActionAndTime("火成岩區任務完成時間:");
    LogSystemUnit3.instance.TotalKnowledgePoints = 1;
    LogSystemUnit3.instance.UpdateTaskRowByLine(9);
  }
  public void MetamorphicTaskComplete()
  {
    unlockUI.unclock(2);
    unlockUI.unclock(4);
    taskSystem.ForceCompleteTask("完成變質岩區任務");
    LogSystemUnit3.instance.StopTimer();
    LogSystemUnit3.instance.LogActionAndTime("變質岩區任務完成時間:");
    LogSystemUnit3.instance.TotalKnowledgePoints = 2;
    LogSystemUnit3.instance.UpdateTaskRowByLine(10);
  }
  public void ResetObject()
  {
    Rigidbody rb1 = Stone1.gameObject.GetComponent<Rigidbody>();
    Debug.Log("reset");
    backpack.ClearBackpack();
    Stone1.ResetObjectPosition();
    Stone2.ResetObjectPosition();
    Stone3.ResetObjectPosition();
    Stone4.ResetObjectPosition();
    Stone5.ResetObjectPosition();
    Stone6.ResetObjectPosition();
    Stone7.ResetObjectPosition();
    Stone8.ResetObjectPosition();
    Stone9.ResetObjectPosition();
    Stone10.ResetObjectPosition();
    Stone11.ResetObjectPosition();
    Stone1.gameObject.SetActive(true);
    Stone2.gameObject.SetActive(true);
    Stone3.gameObject.SetActive(true);
    Stone4.gameObject.SetActive(true);
    Stone5.gameObject.SetActive(true);
    Stone6.gameObject.SetActive(false);
    Stone6.gameObject.GetComponent<SpacialObject>().ResetDisplay();
    Stone7.gameObject.SetActive(true);
    Stone8.gameObject.SetActive(true);
    Stone9.gameObject.SetActive(true);
    Stone10.gameObject.SetActive(true);
    Stone11.gameObject.SetActive(false);
    Stone11.gameObject.GetComponent<SpacialObject>().ResetDisplay();

    ResetPhysics(Stone1.gameObject);
    ResetPhysics(Stone2.gameObject);
    ResetPhysics(Stone3.gameObject);
    ResetPhysics(Stone4.gameObject);
    ResetPhysics(Stone5.gameObject);
    ResetPhysics(Stone6.gameObject);
    ResetPhysics(Stone7.gameObject);
    ResetPhysics(Stone8.gameObject);
    ResetPhysics(Stone9.gameObject);
    ResetPhysics(Stone10.gameObject);
    ResetPhysics(Stone11.gameObject);




  }
  private void ResetPhysics(GameObject stone)
  {
    if (stone != null)
    {
      Rigidbody rb = stone.GetComponent<Rigidbody>();
      if (rb != null)
      {
        rb.velocity = Vector3.zero;      // 設置速度為0
        rb.angularVelocity = Vector3.zero;  // 設置角速度為0
        rb.isKinematic = true;  // 取消物理影響
        rb.isKinematic = false; // 重新啟用物理影響，確保物理重置
      }
    }
  }
}
