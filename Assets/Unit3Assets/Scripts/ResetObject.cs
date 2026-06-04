using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObject : MonoBehaviour
{
    public float resetThresholdY = -1f; // �����󱼸���o��Y�ȥH�U��Ĳ�o���m
    private Vector3 initialPosition; // �x�s���󪺪�l��m
    private Quaternion initialRotation; // �x�s���󪺪�l����

    void Start()
    {
        // �b�C���}�l�ɰO�����󪺪�l��m�M����
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
   
         Debug.Log(transform.localPosition.y);
        // �ˬd����Y�b��m�O�_�C���H��
        if (transform.localPosition.y < resetThresholdY)
        {
            ResetObjectPosition();
        }
    }
    public void ResetObjectPosition()
    {
        // �N�����m�M���୫�m����l��
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        
    }
}
