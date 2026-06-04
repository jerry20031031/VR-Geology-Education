using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// using cherrydev;//如果需要在Dialog Node中呼叫Function需要此行(共三行)
public class MissionControlver2 : MonoBehaviour
{
    public static MissionControlver2 Instance;

    public GameObject[] MissionPanel;
    public TMP_Text[] missionText;
    public string[] missionText_ForHistory_V5;
    public int CheckTimes = 0; //確保不會先做任務2在做任務1導致任務介面出問題
    // boolean
    public bool mOneCheck;
    public bool mOneComplete;
    public bool mTwoCheck;
    public bool mTwoComplete;


    public bool mThreeCheck;
    public bool mThreeComplete;

    public bool missionManagerTalk;

    public bool mFourCheck;
    public bool mFourComplete;

    public bool mFiveCheck;
    public bool mFiveComplete;
    public int SetHistoryV5_missionText = 0; //設置當前任務對話之陣列空間的文字

    // public bool mFiveCheck;

    public int timeDuration = 3;
    // public cherrydev.DialogBehaviour dialogBehaviourOne;//如果需要在Dialog Node中呼叫Function需要此行(共三行)

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        // 初始化MissionPanel陣列
        MissionPanel = new GameObject[5];  // 假設有五個任務面板
        MissionPanel[0] = GameObject.Find("M1Panel");
        MissionPanel[1] = GameObject.Find("M2Panel");
        MissionPanel[2] = GameObject.Find("M3Panel");
        MissionPanel[3] = GameObject.Find("M4Panel");
        MissionPanel[4] = GameObject.Find("M5Panel");

        missionText = new TMP_Text[MissionPanel.Length]; // 設定文本陣列長度
        missionText_ForHistory_V5 = new string[5];//初始設定陣列長度

        for (int i = 0; i < MissionPanel.Length; i++)//根據需求調整陣列數量
        {
            MissionPanel[i].SetActive(false);
            missionText[i] = MissionPanel[i].GetComponentInChildren<TMP_Text>(true);
            missionText_ForHistory_V5[i] = missionText[i].text;//設置給History歷程記錄的乾淨任務(沒有"完成"兩字)

        }
        MissionPanel[0].SetActive(true);
        // dialogBehaviourOne.BindExternalFunction("mOneCheck_dia", M1Check); //如果需要在Dialog Node中呼叫Function需要此行(共三行)
    }

    void Update()
    {
        // CheckMissionCompelete();
    }

    void FixedUpdate()
    {
        CheckMissionCompelete();
    }

    public void CheckMissionCompelete() //有可能會隱藏前一個Scene讓某個check變為true，compelete變為false的情況，導致下一個Scene無法正常運作，前提是換Scene的腳本不會重製
    {
        //M1
        if (mOneCheck)//mOneCompelete是預防同個動作重複做而導致任務流程介面亂掉
        {
            // SetHistoryV5_missionText = 0;//確保歷程記錄抓的到當前為甚麼任務
            if (CheckTimes == 0)//確定跑過一次任務UI任務引導更新不重複
            {
                missionText[0].text = "<color=green>" + missionText[0].text + "(完成)</color=green>";
                CheckTimes++;
                mOneCheck = false;
                StartCoroutine(WaitOneMission(timeDuration));
            }
            else
            {
                mOneCheck = false;
            }
        }
        //M2
        if (mTwoCheck)
        {
            Debug.Log("mTwoCheck" + mTwoCheck);
            Debug.Log("SetHistoryV5_missionText為" + SetHistoryV5_missionText);

            if (CheckTimes == 1)
            {
                missionText[1].text = "<color=green>" + missionText[1].text + "(完成)</color=green>";
                CheckTimes++;
                mTwoCheck = false;
                StartCoroutine(WaitTwoMission(timeDuration));
            }
            else
            {
                mTwoCheck = false;
            }

        }
        //M3
        if (mThreeCheck)
        {
            if (CheckTimes == 2)
            {
                missionText[2].text = "<color=green>" + missionText[2].text + "(完成)</color=green>";
                CheckTimes++;
                mThreeCheck = false;
                StartCoroutine(WaitThreeMission(timeDuration));
            }
        }
        else
        {
            mThreeCheck = false;
        }
        //M4
        if (mFourCheck)
        {
            if (CheckTimes == 3)
            {
                missionText[3].text = "<color=green>" + missionText[3].text + "(完成)</color=green>";
                CheckTimes++;
                mFourCheck = false;
                StartCoroutine(WaitFourMission(timeDuration));
            }
            else
            {
                mFourCheck = false;
            }
        }

        //M5
        if (mFiveCheck)
        {
            if (CheckTimes == 4)
            {
                missionText[4].text = "<color=green>" + missionText[4].text + "(完成)</color=green>";
                CheckTimes++;
                mFiveCheck = false;
                StartCoroutine(WaitFiveMission(timeDuration));
            }
            else
            {
                mFiveCheck = false;
            }

        }
    }

    public void CheckManagerTalk()
    {
        missionManagerTalk = true;
    }

    public void CheckQuestionExam()
    {
        mFiveCheck = true;
    }

    IEnumerator WaitOneMission(int _timeDuration)
    {
        _timeDuration = timeDuration;
        yield return new WaitForSeconds(_timeDuration);
        MissionPanel[0].SetActive(false);
        MissionPanel[1].SetActive(true);
    }
    IEnumerator WaitTwoMission(int _timeDuration)
    {
        _timeDuration = timeDuration;
        yield return new WaitForSeconds(_timeDuration);
        MissionPanel[1].SetActive(false);
        MissionPanel[2].SetActive(true);
    }

    IEnumerator WaitThreeMission(int _timeDuration)
    {
        _timeDuration = timeDuration;
        yield return new WaitForSeconds(_timeDuration);
        MissionPanel[2].SetActive(false);
        MissionPanel[3].SetActive(true);
    }

    IEnumerator WaitFourMission(int _timeDuration)
    {
        _timeDuration = timeDuration;
        yield return new WaitForSeconds(_timeDuration);
        MissionPanel[3].SetActive(false);
        MissionPanel[4].SetActive(true);
    }

    IEnumerator WaitFiveMission(int _timeDuration)
    {
        _timeDuration = timeDuration;
        yield return new WaitForSeconds(_timeDuration);
        MissionPanel[4].SetActive(false);
        MissionPanel[5].SetActive(true);
    }
    public void M1Check()
    {
        SetHistoryV5_missionText = 0;//確保當前為甚麼任務
        HistoryV6.Instance.JustNeedOneTalk();
        mOneCheck = true;
        Debug.Log("M1卻可拉");
        Debug.Log("設置米tion當前int_M1" + SetHistoryV5_missionText);

    }
    public void M2Check()
    {
        SetHistoryV5_missionText = 1;
        HistoryV6.Instance.JustNeedOneTalk();
        mTwoCheck = true;
        Debug.Log("M2卻可拉");
        Debug.Log("設置米tion當前int_M2" + SetHistoryV5_missionText);

    }
    public void M3Check()
    {
        SetHistoryV5_missionText = 2;//一定要在JustNeedOneTalk之前呼叫
        HistoryV6.Instance.JustNeedOneTalk();
        mThreeCheck = true;
        Debug.Log("M3卻可拉");
        Debug.Log("設置米tion當前int_M3" + SetHistoryV5_missionText);
    }
    public void M4Check()
    {
        SetHistoryV5_missionText = 3;
        HistoryV6.Instance.JustNeedOneTalk();
        mFourCheck = true;
        Debug.Log("M4卻可拉");
        Debug.Log("設置米tion當前int_M4" + SetHistoryV5_missionText);
    }
    public void M5Check()
    {
        SetHistoryV5_missionText = 4;
        HistoryV6.Instance.JustNeedOneTalk();
        mFiveCheck = true;
        Debug.Log("M5卻可拉");
        Debug.Log("設置米tion當前int_M5" + SetHistoryV5_missionText);
    }
}
