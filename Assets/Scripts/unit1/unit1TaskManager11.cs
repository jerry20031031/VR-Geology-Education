using cherrydev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit1TaskManager11 : MonoBehaviour
{
    public DialogBehaviour Person1Talk;  //�ǩn
    public DialogBehaviour Person1Talk2; //�б�
    public DialogBehaviour Person1Talk3;

    public AudioSource audioSource1;  // �Ĥ@��AudioSource�ե�A�Ω󼽩���
    public AudioSource audioSource2;  // �ĤG��AudioSource�ե�A�Ω󼽩���
    public AudioSource audioSource3;  // �ĤT��AudioSource�ե�A�Ω󼽩���
    public AudioClip[] audioClips;  // �s�x���ɪ��}�C
    public Animator GirlMove1;
    public Animator TeacherMove1;
    public Animator TeacherMove2;
    void Awake()
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length >= 3)
        {
            audioSource1 = sources[0];  //�ǩn
            audioSource2 = sources[1];  //�б�
            audioSource3 = sources[2];
        }

    }
    void Start()
    {
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal135", tal135);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal136", tal136);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal137", tal137);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal138", tal138);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal139", tal139);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal140", tal140);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal141", tal141);

        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("tal142", tal142);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("tal143", tal143);

        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal144", tal144);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal145", tal145);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal146", tal146);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal147", tal147);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal148", tal148);

        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("tal149", tal149);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("tal150", tal150);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("tal151", tal151);

        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal152", tal152);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal153", tal153);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal154", tal154);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal155", tal155);

        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("tal156", tal156);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("tal157", tal157);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("tal158", tal158);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("tal159", tal159);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("tal160", tal160);
        Person1Talk.ExternalFunctionsHandler.BindExternalFunction("tal161", tal161);

        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal162", tal162);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal163", tal163);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal164", tal164);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal165", tal165);


        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal190", tal190);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal191", tal191);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal192", tal192);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal193", tal193);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal194", tal194);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal195", tal195);

        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal1400", tal1400);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal1401", tal1401);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal1402", tal1402);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal1403", tal1403);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal1404", tal1404);
        Person1Talk2.ExternalFunctionsHandler.BindExternalFunction("tal1405", tal1405);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void tal11()
    {
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal166", tal166);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal167", tal167);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal168", tal168);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal169", tal169);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal170", tal170);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal171", tal171);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal172", tal172);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal173", tal173);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal174", tal174);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal175", tal175);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal176", tal176);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal177", tal177);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal178", tal178);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal179", tal179);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal180", tal180);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal181", tal181);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal182", tal182);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal183", tal183);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal184", tal184);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal185", tal185);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal186", tal186);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal187", tal187);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal188", tal188);
        Person1Talk3.ExternalFunctionsHandler.BindExternalFunction("tal189", tal189);
    }
    public void GirlTalking()
    {
        GirlMove1.SetBool("Thinking", false);
        GirlMove1.SetBool("Talking", true);

    }
    public void TeacherTalking()
    {

        TeacherMove1.SetBool("Talking", true);
        TeacherMove2.SetBool("Talking", true);

    }
    public void GirlIdle()
    {
        GirlMove1.SetBool("Thinking", false);
        GirlMove1.SetBool("Talking", false);

    }
    public void TeacherIdle()
    {
        TeacherMove1.SetBool("Talking", false);
        TeacherMove2.SetBool("Talking", false);

    }
    public void tal135()
    {
        PlayAudio(audioSource2, 0);
    }
    public void tal136()
    {
        PlayAudio(audioSource2, 1);
    }
    public void tal137()
    {
        PlayAudio(audioSource2, 2);
    }
    public void tal138()
    {
        PlayAudio(audioSource2, 3);
    }
    public void tal139()
    {
        PlayAudio(audioSource2, 4);
    }
    public void tal140()
    {
        PlayAudio(audioSource2, 5);
    }
    public void tal141()
    {
        PlayAudio(audioSource2, 6);
    }
    public void tal142()
    {
        PlayAudio(audioSource1, 7);
    }
    public void tal143()
    {
        PlayAudio(audioSource1, 8);
    }
    public void tal144()
    {
        PlayAudio(audioSource2, 9);
    }
    public void tal145()
    {
        PlayAudio(audioSource2, 10);
    }
    public void tal146()
    {
        PlayAudio(audioSource2, 11);
    }
    public void tal147()
    {
        PlayAudio(audioSource2, 12);
    }
    public void tal148()
    {
        PlayAudio(audioSource2, 13);
    }
    public void tal149()
    {
        PlayAudio(audioSource1, 14);
    }
    public void tal150()
    {
        PlayAudio(audioSource1, 15);
    }
    public void tal151()
    {
        PlayAudio(audioSource1, 16);
    }
    public void tal152()
    {
        PlayAudio(audioSource2, 17);
    }
    public void tal153()
    {
        PlayAudio(audioSource2, 18);
    }
    public void tal154()
    {
        PlayAudio(audioSource2, 19);
    }
    public void tal155()
    {
        PlayAudio(audioSource2, 20);
    }
    public void tal156()
    {
        PlayAudio(audioSource1, 21);
    }
    public void tal157()
    {
        PlayAudio(audioSource1, 22);
    }
    public void tal158()
    {
        PlayAudio(audioSource1, 23);
    }
    public void tal159()
    {
        PlayAudio(audioSource1, 24);
    }
    public void tal160()
    {
        PlayAudio(audioSource1, 25);
    }
    public void tal161()
    {
        PlayAudio(audioSource1, 26);
    }
    public void tal162()
    {
        PlayAudio(audioSource2, 27);
    }
    public void tal163()
    {
        PlayAudio(audioSource2, 28);
    }
    public void tal164()
    {
        PlayAudio(audioSource2, 29);
    }
    public void tal165()
    {
        PlayAudio(audioSource2, 30);
    }
    public void tal166()
    {
        PlayAudio(audioSource3, 31);
    }
    public void tal167()
    {
        PlayAudio(audioSource3, 32);
    }
    public void tal168()
    {
        PlayAudio(audioSource3, 33);
    }
    public void tal169()
    {
        PlayAudio(audioSource3, 34);
    }
    public void tal170()
    {
        PlayAudio(audioSource3, 35);
    }
    public void tal171()
    {
        PlayAudio(audioSource3, 36);
    }
    public void tal172()
    {
        PlayAudio(audioSource3, 37);
    }
    public void tal173()
    {
        PlayAudio(audioSource3, 38);
    }
    public void tal174()
    {
        PlayAudio(audioSource3, 39);
    }
    public void tal175()
    {
        PlayAudio(audioSource3, 40);
    }
    public void tal176()
    {
        PlayAudio(audioSource3, 41);
    }
    public void tal177()
    {
        PlayAudio(audioSource3, 42);
        LogSystem.LogAction("學習者開始任務(互動五:晶體方向分布)");
        LogSystem.instance.StartTimer();
    }
    public void tal178()
    {
        PlayAudio(audioSource3, 43);
        LogSystem.instance.playEnd5();
        LogSystem.instance.StopTimer();
    }
    public void tal179()
    {
        PlayAudio(audioSource3, 44);
    }
    public void tal180()
    {
        PlayAudio(audioSource3, 45);
    }
    public void tal181()
    {
        PlayAudio(audioSource3, 46);
    }
    public void tal182()
    {
        PlayAudio(audioSource3, 47);
    }
    public void tal183()
    {
        PlayAudio(audioSource3, 48);
    }
    public void tal184()
    {
        PlayAudio(audioSource3, 49);
    }
    public void tal185()
    {
        PlayAudio(audioSource3, 50);
    }
    public void tal186()
    {
        PlayAudio(audioSource3, 51);
    }
    public void tal187()
    {
        PlayAudio(audioSource3, 52);
    }
    public void tal188()
    {
        PlayAudio(audioSource3, 53);
        LogSystem.instance.TotalKnowledgePoints = 1;
        LogSystem.instance.GainedKnowledgePoints++;
        LogSystem.LogAction("知識點解鎖(海底海洋地殼磁場分布)");
        LogSystem.instance.UpdateTaskRowByLine(5);
    }
    public void tal189()
    {
        PlayAudio(audioSource3, 54);
    }
    public void tal190()
    {
        PlayAudio(audioSource2, 55);
    }
    public void tal191()
    {
        PlayAudio(audioSource2, 56);
    }
    public void tal192()
    {
        PlayAudio(audioSource2, 57);
    }
    public void tal193()
    {
        PlayAudio(audioSource2, 58);
    }
    public void tal194()
    {
        PlayAudio(audioSource2, 59);
    }
    public void tal195()
    {
        PlayAudio(audioSource2, 60);
    }
    public void tal1400()
    {
        PlayAudio(audioSource2, 61);
    }
    public void tal1401()
    {
        PlayAudio(audioSource2, 62);
    }
    public void tal1402()
    {
        PlayAudio(audioSource2, 63);
    }
    public void tal1403()
    {
        PlayAudio(audioSource2, 64);
    }
    public void tal1404()
    {
        PlayAudio(audioSource2, 65);
    }
    public void tal1405()
    {
        PlayAudio(audioSource2, 66);
    }

    private void PlayAudio(AudioSource source, int clipIndex)
    {
        if (clipIndex >= 0 && clipIndex < audioClips.Length)
        {
            source.Stop();  // ������e���b���񪺭���
            source.clip = audioClips[clipIndex];  // �]�w�s������
            source.Play();  // ����s������
        }
        else
        {
            Debug.LogError("Audio clip index out of range");
        }
    }
}
