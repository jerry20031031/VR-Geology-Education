using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; // 引入 TextMeshPro
using System.Collections; // 引入協程命名空間
using UnityEngine.Events;
using System;
using System.IO;

public class Scene1Session : MonoBehaviour
{
    int land;
    float land_time;
    int ocean;
    float ocean_time;
    int lava;
    float lava_time;
    int talk;
    float talk_time;

    int land_times;
    int ocean_times;
    int lava_times;

    void Start()
    {
    }

    public void SessionAction(string str)
    {
        SessionLogger.LogAction(str);
    } 
    public void SessionLandStart()
    {
        land_times++;
        land = 1;
        SessionAction($"抓取岩石圈(大陸區域)模型:{land_times}次");
    }
    public void SessionLand(float t)
    {
        SessionAction($"放下岩石圈(大陸區域)模型:{land_times}次");
    }
    public void SessionOceanStart()
    {
        ocean_times++;
        ocean = 1;
        SessionAction($"抓取岩石圈(海洋區域)模型:{ocean_times}次");
    }
    public void SessionOcean(float t)
    {
        if(t > ocean_time) ocean_time = t;
        SessionAction($"放下岩石圈(海洋區域)模型:{ocean_times}次");
    }
    public void SessionLavaStart()
    {
        lava_times++;
        lava = 1;
        SessionAction($"抓取軟流圈模型:{lava_times}次");
    }
    public void SessionLava(float t)
    {
        if(t > lava_time) lava_time = t;
        SessionAction($"放下軟流圈模型:{lava_times}次");
    }
    public void SessionTalk1() //對話
    {
        talk = 1;
        if(talk_time == 0) talk_time = elapsedTime - interactTime;
    }
    
    public void TaskOutput()
    {
        SessionLogger.LogTask("任務一：認識板塊構造","100%",TimeSpan.FromSeconds(elapsedTime),$"'{talk+lava+ocean+land}/4","-",0,0); 
        SessionLogger.LogTask("└ 與教授對話獲取知識",$"{(talk)*100}%",TimeSpan.FromSeconds(talk_time),$"'{talk}/1","-",0,0);
        SessionLogger.LogTask("└ 抓取軟流圈模型",$"{lava*100}%",TimeSpan.FromSeconds(lava_time),$"'{lava}/1","-",0,0);
        SessionLogger.LogTask("└ 抓取岩石圈(海洋區域)模型",$"{ocean*100}%",TimeSpan.FromSeconds(ocean_time),$"'{ocean}/1","-",0,0);
        SessionLogger.LogTask("└ 抓取岩石圈(大陸區域)模型",$"{land*100}%",TimeSpan.FromSeconds(land_time),$"'{land}/1","-",0,0);
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
        SessionLogger.LogAction("學習者開始任務(任務一：認識板塊構造)");
    }

    public void StopTimer()
    {
        isRunning = false;
        SessionLogger.LogAction($"學習者結束任務(認識所有板塊構造時間{FormatTime(elapsedTime)})");

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
