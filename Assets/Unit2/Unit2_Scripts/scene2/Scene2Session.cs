using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; // 引入 TextMeshPro
using System.Collections; // 引入協程命名空間
using UnityEngine.Events;
using System;
using System.IO;

public class Scene2Session : MonoBehaviour
{
    int land = 0;
    float land_time = 0;
    int land_times;

    int ocean = 0;
    float ocean_time = 0;
    int ocean_times;

    int lava = 0;
    float lava_time = 0;
    int lava_times;

    int talk = 0;
    int talk_True = 0;
    int talk_Error = 0;
    float talk_time = 0;

    int ErrorTimes = 0;
    string isError = "正確";

    void Start()
    {
    }

    public void SessionAction(string str)
    {
        SessionLogger.LogAction(str);
    } 
    
    public void SessionLand()
    {
        land = 1;
        land_times++;
        SessionAction($"放置岩石圈(大陸區域)模型:{land_times}次");
        if(land_time == 0) land_time = elapsedTime;
    }
    public void SessionOcean()
    {
        ocean = 1;
        ocean_times++;
        SessionAction($"放置岩石圈(海洋區域)模型:{ocean_times}次");
        if(ocean_time == 0) ocean_time = elapsedTime;
    }
    public void SessionLava()
    {
        lava = 1;
        lava_times++;
        SessionAction($"放置軟流圈模型:{lava_times}次");
        if(lava_time == 0) lava_time = elapsedTime;
    }

    public void SessionTalk() //對話
    {
        talk = 1;
        if(talk_time == 0) talk_time = elapsedTime - interactTime;
    }
    public void SessionTalkTrue() //對話
    {
        talk_True = 1;
        if(talk_time == 0) talk_time = elapsedTime - interactTime;
    }
    
    public void SessionTalkError() //對話
    {
        talk_Error = 1;
        if(talk_time == 0) talk_time = elapsedTime - interactTime;
    }

    public void SessionError()
    {
        isError = "錯誤";
        ErrorTimes++;
    }
    
    public void TaskOutput()
    {
        SessionLogger.LogTask("任務二：完成板塊模型","100%",TimeSpan.FromSeconds(elapsedTime),$"'{talk+talk_True+talk_Error+lava+ocean+land}/5",isError,ErrorTimes,0);
        SessionLogger.LogTask("└ 與教授對話獲取知識",$"{(talk+talk_True+talk_Error)*100/2}%",TimeSpan.FromSeconds(talk_time),$"'{talk+talk_True+talk_Error}/2","-",0,0);
        SessionLogger.LogTask("└ 放置軟流圈模型",$"{lava*100}%",TimeSpan.FromSeconds(lava_time),$"'{lava}/1","-",0,0);
        SessionLogger.LogTask("└ 放置岩石圈(海洋區域)模型",$"{ocean*100}%",TimeSpan.FromSeconds(ocean_time),$"'{ocean}/1","-",0,0);
        SessionLogger.LogTask("└ 放置岩石圈(大陸區域)模型",$"{land*100}%",TimeSpan.FromSeconds(land_time),$"'{land}/1","-",0,0);
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
        SessionLogger.LogAction("學習者開始任務(任務二：完成板塊模型)");
    }

    public void StopTimer()
    {
        isRunning = false;
        SessionLogger.LogAction($"學習者結束任務(板塊模型完成時間{FormatTime(elapsedTime)})");

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
}
