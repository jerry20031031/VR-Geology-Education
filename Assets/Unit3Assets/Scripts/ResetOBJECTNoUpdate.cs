using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOBJECTNoUpdate : MonoBehaviour
{

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

    }

    public void ResetObjectPosition()
    {
        // �N�����m�M���୫�m����l��
        transform.position = initialPosition;
        transform.rotation = initialRotation;


    }
public void initPosoition()
    {
        // ���N�����m�M���୫�m����l��
        initialPosition = transform.position;
        initialRotation = transform.rotation;   
    }
    
}
