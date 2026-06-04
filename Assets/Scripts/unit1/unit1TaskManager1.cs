using cherrydev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit1TaskManager1 : MonoBehaviour
{
    //public DialogBehaviour Person1Talk;//學姊
    //public TextMeshProUGUI TeacherPanel;

    public DialogBehaviour Person1Talk2;//教授
    //public TextMeshProUGUI TeacherPanel2;

    public AudioClip[] audioClips;  // 存儲音檔的陣列
    private AudioSource audioSource;  // AudioSource組件，用於播放音檔
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();  // 獲取附加到同一物件上的AudioSource組件
    }
    void Start()
    {
        /*Person1Talk.ExternalFunctionsHandler.BindExternalFunction("PlainTaskComplete111", PlainTaskComplete111);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("PlainTaskComplete22", PlainTaskComplete22);*/
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk1", talk1);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk2", talk2);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("talk3", talk3);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlainTaskComplete111()
    {
        //TeacherPanel.text = "韋格納認為在兩億年前原始大陸只有一塊名為<color=orange>盤古大陸</color>";
    }
    public void PlainTaskComplete22()
    {
        //TeacherPanel2.text = "剛剛的磁場表就是冷卻後的磁鐵礦在海洋地殼記錄<color=orange>地磁反轉</color>現象。";
    }
    public void talk1()
    {
        audioSource.Stop();  // 停止當前正在播放的音檔
        audioSource.clip = audioClips[0];  // 設定新的音檔
        audioSource.Play();  // 播放新的音檔
    }
    public void talk2()
    {
        audioSource.Stop();  // 停止當前正在播放的音檔
        audioSource.clip = audioClips[1];  // 設定新的音檔
        audioSource.Play();  // 播放新的音檔
    }
    public void talk3()
    {
        audioSource.Stop();  // 停止當前正在播放的音檔
        audioSource.clip = audioClips[2];  // 設定新的音檔
        audioSource.Play();  // 播放新的音檔
    }
    public void talk6()
    {
        audioSource.Stop();  // 停止當前正在播放的音檔
        audioSource.clip = audioClips[3];  // 設定新的音檔
        audioSource.Play();  // 播放新的音檔
    }
}
