using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEngine.Android;
using System;
public class HistoryV6 : MonoBehaviour
{

    // 全域變數宣告(時間)
    public static HistoryV6 Instance;
    public string Chapter;


    // 當按輸出位置
    string filePathV5 = "";
    public string JustTalk_Time;
    // 跟第一個NPC講話

    [System.Serializable]
    public class Player
    {
        // 玩家需要儲存的資料
        public string Name;
        public string JustTalk;//任務文字
        public string JustTalk_Time_V5;//任務時間(接收)
        public Player(string _Name, string _JustTalk_Time)
        {
            Name = _Name;

            //JustTalkTime
            JustTalk_Time_V5 = _JustTalk_Time;
        }
    }

    [System.Serializable]
    public class PlayerList
    {
        public List<Player> players;

        public PlayerList()
        {
            players = new List<Player>();
        }

        public void AddPlayer(Player _player)
        {
            players.Add(_player);
        }
    }

    public PlayerList playerList = new PlayerList();

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
        string sceneName = SceneManager.GetActiveScene().name; //抓取Scene場景名稱

        filePathV5 = Path.Combine(Application.persistentDataPath,"CH"+Chapter+"_HistoryData.csv");
        // filePathV5 = Path.Combine("C:\\Users\\a0936\\Desktop\\UnityProjects","CH"+Chapter+"_HistoryData.csv");//Test
        // filePathV5 = Path.Combine("C:\\原本的檔案位置\\Desktop\\Unity_History_storage", "CH"+Chapter+"_HistoryData.csv");//Test


        Player player1 = new Player("Player1", "JustTalkTime");

        playerList.AddPlayer(player1);

        if (Regex.IsMatch(sceneName, @"CH\d_Scene1")) //正則表達式，\d+代表0~9任意數字
        {
            //如果是第一個場景，創建新檔並寫入
            using (StreamWriter sw = new StreamWriter(filePathV5, false, Encoding.UTF8))
            {
                sw.WriteLine("玩家, 任務, 完成時間");
            }
        }
    }

    void Update()
    {

    }
    public void JustNeedOneTalk()
    {
        if (File.Exists(filePathV5))
        {
            using (StreamWriter sw = new StreamWriter(filePathV5, true, Encoding.UTF8))//如果文件已存在，用寫入開啟
            {
                JustTalk_Time = System.DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss");
                Debug.Log("JustTalk_Time" + JustTalk_Time);
                playerList.players[0].JustTalk = MissionControlver2.Instance.missionText_ForHistory_V5[MissionControlver2.Instance.SetHistoryV5_missionText];// 括號裡面要是int
                playerList.players[0].JustTalk_Time_V5 = JustTalk_Time;
                Debug.Log("任務顯示int為" + MissionControlver2.Instance.SetHistoryV5_missionText);
                Debug.Log("當前紀錄任務為" + playerList.players[0].JustTalk);
                Debug.Log("當前紀錄任務的時間為" + playerList.players[0].JustTalk_Time_V5);
                Debug.Log("跑過JustTalkNeed!");
                sw.WriteLine(
                playerList.players[0].Name + "," +
                playerList.players[0].JustTalk + "," +
                playerList.players[0].JustTalk_Time_V5
                );
                Debug.Log("using關檔前");
            }
            Debug.Log("using關檔後");
        }

    }
}
