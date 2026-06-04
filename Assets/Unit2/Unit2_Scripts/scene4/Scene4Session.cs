using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; // 引入 TextMeshPro
using System.Collections; // 引入協程命名空間
using UnityEngine.Events;
using System;
using System.IO;

public class Scene4Session : MonoBehaviour
{
    int convergent;
    float convergent_time = 0;
    int divergent;
    float divergent_time = 0;
    
    int k1 = 0;
    int k2_1 = 0;
    int k2_2 = 0;
    int k3 = 0;

    int talk1;
    int talk2;
    int talk3;
    int talk4;
    float talk_time;
    
    private SessionLogger sessionLogger;

    void Start()
    {
        sessionLogger = FindObjectOfType<SessionLogger>();
    }

    public void SessionAction(string str)
    {
        SessionLogger.LogAction(str);
    } 

    public void SessionConvergent() //聚合型板塊邊界
    {
        convergent = 1;
        if(convergent_time == 0) convergent_time = elapsedTime - interactTime;
    }
    public void SessionDivergent() //張裂型板塊邊界
    {
        divergent = 1;
        if(divergent_time == 0) divergent_time = elapsedTime - interactTime;
    }

    public void SessionKnowledge1()
    {
        k1 = 1;
    }

    public void SessionKnowledge2_1()
    {
        k2_1 = 1;
    }
    
    public void SessionKnowledge2_2()
    {
        k2_2 = 1;
    }

    public void SessionKnowledge3()
    {
        k3 = 1;
    }
    public void SessionTalk1() //對話1
    {
        talk1 = 1;
        if(talk_time == 0) talk_time = elapsedTime - interactTime;
    }
    public void SessionTalk2() //對話2
    {
        talk2 = 1;
        if(talk_time == 0) talk_time = elapsedTime - interactTime;
    }
    public void SessionTalk3() //對話3
    {
        talk3 = 1;
        if(talk_time == 0) talk_time = elapsedTime - interactTime;
    }
    public void SessionTalk4() //對話4
    {
        talk4 = 1;
        if(talk_time == 0) talk_time = elapsedTime - interactTime;
    }
    
    public void TaskOutput()
    {
        SessionLogger.LogTask("任務四：岩漿觀察","100%",TimeSpan.FromSeconds(elapsedTime),$"'{k1+talk1+talk2+talk3+talk4+k2_1+k2_2+k3}/8","-",0,0);
        SessionLogger.LogTask("└ 與教授對話獲取知識",$"{(talk1+talk2+talk3+talk4)*100/4}%",TimeSpan.FromSeconds(talk_time),$"'{talk1+talk2+talk3+talk4}/4","-",0,0);
        SessionLogger.LogTask("└ 張裂型板塊邊界岩漿觀察",$"{divergent*100}%",TimeSpan.FromSeconds(divergent_time),$"'{k2_1+k2_2}/2","-",0,0);
        SessionLogger.LogTask("└ 聚合型板塊邊界岩漿觀察",$"{convergent*100}%",TimeSpan.FromSeconds(convergent_time),$"'{k3}/1","-",0,0);
    }

    #region 計時器
    static private float elapsedTime = 0f;
    static private float interactTime = 0f;
    private bool isRunning = false;

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime; // 計算經過的時間
        }
    }
    public void StartTimer()
    {
        isRunning = true;
        elapsedTime = 0f; // 重新開始
        SessionLogger.LogAction("學習者開始任務(任務四：岩漿觀察)");
    }

    public void StopTimer()
    {
        isRunning = false;
        SessionLogger.LogAction($"學習者結束任務(岩漿觀察完成時間{FormatTime(elapsedTime)})");
    }
    public void StartInteractTime()
    {
        interactTime = elapsedTime;
    }
    public static string FormatTime(float seconds)
    {
        TimeSpan t = TimeSpan.FromSeconds(seconds);
        return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }

    #endregion

    public void Save()
    {
        sessionLogger.SaveCSV();
        sessionLogger.ExportAllToExcel();
    }
}
