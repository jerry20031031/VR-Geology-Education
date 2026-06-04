using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; // 引入 TextMeshPro
using System.Collections; // 引入協程命名空間
using UnityEngine.Events;
using System;
using System.IO;

public class Scene3Session : MonoBehaviour
{
    int convergent = 0;
    float convergent_time = 0;
    int divergent  = 0;
    float divergent_time = 0;
    int transform = 0;
    float transform_time = 0;
    int talk = 0;
    float talk_time = 0;
    int right,left,mid;

    string predit = "錯誤";
    string preditResult;

    void Start()
    {
    }

    public void SessionAction(string str)
    {
        SessionLogger.LogAction(str);
    } 
    
    public void SessionConvergent() //聚合型板塊邊界
    {
        convergent++;
        if(convergent_time == 0) convergent_time = elapsedTime - interactTime;
    }
    public void SessionDivergent() //張裂型板塊邊界
    {
        divergent++;
        if(divergent_time == 0) divergent_time = elapsedTime - interactTime;
    }
    public void SessionTransform() //錯動型板塊邊界
    {
        transform++;
        if(transform_time == 0) transform_time = elapsedTime - interactTime;
    }

    public void SessionTalk() //對話
    {
        talk = 1;
        if(talk_time == 0) talk_time = elapsedTime - interactTime;
    }
    public void SessionTalkMid() //對話
    {
        mid = 1;
    }
    public void SessionTalkR() //對話
    {
        right = 1;
    }
    public void SessionTalkL() //對話
    {
        left = 1;
    }
    public void SessionPredit(string S, string T)
    {
        predit = T;
        preditResult = S;
    }
    
    public void TaskOutput()
    {
        SessionLogger.LogTask("任務三：板塊運動實作","100%",TimeSpan.FromSeconds(elapsedTime),$"'{talk+mid+right+left+convergent+divergent+transform}/12",predit,0,0);
        SessionLogger.LogTask("└ 與教授對話獲取知識",$"{(talk+mid+right+left)*100/4}%",TimeSpan.FromSeconds(talk_time),$"'{talk+mid+right+left}/4","-",0,0);
        SessionLogger.LogTask("└ 聚合型板塊邊界",$"{convergent*100/3}%",TimeSpan.FromSeconds(convergent_time),$"'{convergent}/3","-",0,0);
        SessionLogger.LogTask("└ 張裂型板塊邊界",$"{divergent*100/2}%",TimeSpan.FromSeconds(divergent_time),$"'{divergent}/2","-",0,0);
        SessionLogger.LogTask("└ 錯動型板塊邊界",$"{transform*100/3}%",TimeSpan.FromSeconds(transform_time),$"'{transform}/3","-",0,0);

        
        SessionLogger.LogAction(preditResult);
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
        SessionLogger.LogAction("學習者開始任務(任務三：板塊運動實作)");
    }

    public void StartInteractTime()
    {
        interactTime = elapsedTime;
    }

    public void StopTimer()
    {
        isRunning = false;
        SessionLogger.LogAction($"學習者結束任務(板塊運動實作完成時間{FormatTime(elapsedTime)})");

    }

    public static string FormatTime(float seconds)
    {
        TimeSpan t = TimeSpan.FromSeconds(seconds);
        return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }

    #endregion
}
