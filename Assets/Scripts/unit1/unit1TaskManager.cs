using cherrydev;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class unit1TaskManager : MonoBehaviour
{
    public DialogBehaviour Person1Talk;  //學姊
    public DialogBehaviour Person1Talk2; //教授

    public AudioClip[] audioClips;  // 存儲音檔的陣列

    public AudioSource audioSource1;  // 第一個AudioSource組件，用於播放音檔
    public AudioSource audioSource2;  // 第二個AudioSource組件，用於播放音檔
    public AudioSource audioSource3;  // 第三個AudioSource組件，用於播放音檔
    public Animator GirlMove1;
    public Animator TeacherMove1;

    //時間計時
    private float elapsedTime = 0f;
    private bool isRunning = false;

    private bool hasWegnerPointGiven1 = false; // ← 加在 class 最上面當旗標
    private bool hasWegnerPointGiven2 = false; // ← 加在 class 最上面當旗標
    void Awake()
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length >= 3)
        {
            audioSource1 = sources[0];  //學姊
            audioSource2 = sources[1];  //教授
            audioSource3 = sources[2];  //系統
        }

    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime; // 計算經過的時間
            Debug.Log("Elapsed Time: " + elapsedTime);
        }
    }
    public void StartTimer()
    {
        isRunning = true;
        elapsedTime = 0f; // 重新開始
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds); // 轉換為 MM:SS 格式
    }


    //-----------------------------------------------------------------------

    void Start()
    {
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talk1", talk1);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talk2", talk2);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talk3", talk3);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talk6", talk6);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk7", talk7);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk8", talk8);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk9", talk9);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk10", talk10);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk11", talk11);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk12", talk12);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talk13", talk13);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talk14", talk14);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talk15", talk15);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk16", talk16);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk17", talk17);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talk18", talk18);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talk19", talk19);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talk20", talk20);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talk21", talk21);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talk22", talk22);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talk23", talk23);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk24", talk24);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk25", talk25);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk26", talk26);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk27", talk27);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk28", talk28);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk29", talk29);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk200", talk200);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk30", talk30);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk31", talk31);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk32", talk32);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk33", talk33);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talk34", talk34);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("talk201", talk201);


    }
    public void GirlTalking()
    {
        GirlMove1.SetBool("Thinking", false);
        GirlMove1.SetBool("Talking", true);

    }
    public void TeacherTalking()
    {

        TeacherMove1.SetBool("Talking", true);

    }
    public void GirlIdle()
    {
        GirlMove1.SetBool("Thinking", false);
        GirlMove1.SetBool("Talking", false);

    }
    public void TeacherIdle()
    {
        TeacherMove1.SetBool("Talking", false);

    }
    //audioSource1是學姊
    public void talk1()
    {
        PlayAudio(audioSource1, 0);
    }

    public void talk2()
    {
        PlayAudio(audioSource1, 1);
    }

    public void talk3()
    {
        PlayAudio(audioSource1, 2);
    }

    public void talk6()
    {
        PlayAudio(audioSource1, 3);
    }
    public void talk7()
    {
        PlayAudio(audioSource2, 4);
    }
    public void talk8()
    {
        PlayAudio(audioSource2, 5);
    }
    public void talk9()
    {
        PlayAudio(audioSource2, 6);
        LogSystem.LogAction("與教授對話獲取知識");
    }
    public void talk10()
    {
        PlayAudio(audioSource2, 7);
    }
    public void talk11()
    {
        PlayAudio(audioSource2, 8);
    }
    public void talk12()
    {
        PlayAudio(audioSource2, 9);
    }
    public void talk13()
    {
        PlayAudio(audioSource1, 10);
    }
    public void talk14()
    {
        PlayAudio(audioSource1, 11);
    }
    public void talk15()
    {
        PlayAudio(audioSource1, 12);
    }
    public void talk16()
    {
        PlayAudio(audioSource2, 13);
    }
    public void talk17()
    {
        PlayAudio(audioSource2, 14);
    }
    public void talk18()
    {
        PlayAudio(audioSource1, 15);
    }
    public void talk19()
    {
        PlayAudio(audioSource1, 16);
    }
    public void talk20()
    {
        PlayAudio(audioSource1, 17);
    }
    public void talk21()
    {
        PlayAudio(audioSource1, 18);
    }
    public void talk22()
    {
        PlayAudio(audioSource1, 19);
    }
    public void talk23()
    {
        PlayAudio(audioSource1, 20);
    }
    public void talk24()
    {
        PlayAudio(audioSource2, 21);
    }
    public void talk25()
    {
        PlayAudio(audioSource2, 22);
    }
    public void talk26()
    {
        PlayAudio(audioSource2, 23);
    }
    public void talk27()
    {
        PlayAudio(audioSource2, 24);
    }
    public void talk28()
    {
        PlayAudio(audioSource2, 25);
    }
    public void talk29()
    {
        PlayAudio(audioSource2, 26);
    }
    public void talk200()
    {
        PlayAudio(audioSource2, 27);
    }
    public void talk30()
    {
        PlayAudio(audioSource2, 28);
    }
    public void talk31()
    {
        PlayAudio(audioSource2, 29);
    }
    public void talk32()
    {
        PlayAudio(audioSource2, 30);
    }
    public void talk33()
    {
        LogSystem.LogAction("與教授對話獲取知識");
        PlayAudio(audioSource2, 31);
    }
    public void talk34()
    {
        PlayAudio(audioSource1, 32);
    }
    public void talk201()
    {
        PlayAudio(audioSource1, 33);
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
    //------------------------------------------------完成時間--------------------
    public void playEnd1()
    {
        StopTimer();
        LogSystem.LogAction("盤古大陸拼圖拼湊完成時間" + FormatTime(elapsedTime));
        LogSystem.instance.CompletionTime = FormatTime(elapsedTime);
    }


    //-----------------------------------------------提示次數

    public void Hint1()
    {
        LogSystem.instance.HintCount++;
        LogSystem.LogAction("盤古大陸拼圖學習者使用提示");
    }
    public void playHint1()
    {
        LogSystem.LogAction("盤古大陸拼圖學習者使用提示次數" + LogSystem.instance.HintCount);
    }

    public void Hint2()
    {
        LogSystem.instance.HintCount++;
        LogSystem.LogAction("大陸漂移互動學習者使用提示");
    }
    //-------------------------------------------整體完成
    public void playAllEnd1()
    {
        LogSystem.instance.UpdateCompletionRate();
        LogSystem.instance.UpdateTaskRowByLine(1);

    }

    //---------------------------------------------------學習者互動失敗次數
    public void error1()
    {
        LogSystem.LogAction("學習者選擇錯誤漂移選項");
        LogSystem.instance.InteractionFailures++;
    }

    //--------------------------------------------------第二分鏡維格納知識點
    public void playWegner()
    {
        if (!hasWegnerPointGiven1)
        {
            LogSystem.LogAction("獲取額外知識(與韋格納對話獲取知識)");
            LogSystem.instance.GainedKnowledgePoints++;
            hasWegnerPointGiven1 = true; // 確保只加一次
        }
    }
    //-------------------------------------------------第二分鏡漂移知識點
    public void playDrift()
    {
        if (!hasWegnerPointGiven2)
        {
            LogSystem.LogAction("知識點解鎖(大陸漂移過程)");
            LogSystem.instance.GainedKnowledgePoints++;
            hasWegnerPointGiven2 = true; // 確保只加一次
        }
    }
}
