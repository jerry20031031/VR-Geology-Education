using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneToTwo : MonoBehaviour
{
    public GameObject[] objectsToCheck; // �s��ݭn�ˬd�� 7 �Ӫ���
    public GameObject objectToDisable; // �ݭn����������
    public GameObject[] objectsToEnable; // �ݭn�ҥΪ� 3 �Ӫ���

    void Update()
    {
        // �T�{�}�C���Ҧ�����O�_���w�ҥ�
        if (AreAllObjectsActive(objectsToCheck))
        {
            // �������w����
            if (objectToDisable != null)
            {
                objectToDisable.SetActive(false);
            }

            // �ҥΨ�L���w����
            foreach (GameObject obj in objectsToEnable)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                    
                }
            }
            //LogSystem.LogAction("學習者觀察完各大洲");
            // �קK���ư���A�������}��
            this.enabled = false;
        }
    }

    // �ˬd�O�_�Ҧ����󳣱ҥ�
    private bool AreAllObjectsActive(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            if (obj == null || !obj.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
}
