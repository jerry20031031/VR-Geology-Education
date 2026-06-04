using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class checkTwoSocket : MonoBehaviour
{
    // �N���� XR Socket Interactor ���w�b Inspector ��
    public List<XRSocketInteractor> socketInteractors;

    // ��������ⰼ Transform�]�Ҧp���������M�k���^
    public GameObject rocktitle;
    public GameObject rockPlaneText;
    public AudioSource audioSource;
    public GameObject playmode;
    public AudioSource audioWrite2;

    private bool allObjectsInserted = false;
    private bool hasWegnerPointGiven = false; // ← 加在 class 最上面當旗標

    void Start()
    {
        // �q�\�C�� Socket �� SelectEntered �ƥ�
        foreach (var socket in socketInteractors)
        {
            socket.selectEntered.AddListener(CheckAllSocketsStatus);
        }
    }

    private void CheckAllSocketsStatus(SelectEnterEventArgs args)
    {
        // �ˬd�O�_�Ҧ��� Socket �������󴡤J
        allObjectsInserted = true;
        foreach (var socket in socketInteractors)
        {
            if (!socket.hasSelection)
            {
                allObjectsInserted = false;
                break;
            }
        }

        // �p�G�Ҧ����ѳ����J�F����}�l�}��
        if (allObjectsInserted)
        {
            OpenDoors();
            audioSource.Play();
            if (!hasWegnerPointGiven)
            {
                LogSystem.LogAction("知識點解鎖(學習者解鎖大陸漂移海岸線相似&地層構造連續證據知識點)");
                LogSystem.instance.GainedKnowledgePoints++;
                hasWegnerPointGiven = true; // 設定為已給予
            }
            
            Invoke("PlayAudioWrite2", 0.5f);
            //audioWrite2.Play();
        }
    }
    void PlayAudioWrite2()
    {
        audioWrite2.Play();
    }
    private void OpenDoors()
    {
        rocktitle.SetActive(false);
        rockPlaneText.SetActive(true);
        playmode.SetActive(false);
    }



    private void OnDestroy()
    {
        // �Ѱ��ƥ�q�\
        foreach (var socket in socketInteractors)
        {
            socket.selectEntered.RemoveListener(CheckAllSocketsStatus);
        }
    }
}
