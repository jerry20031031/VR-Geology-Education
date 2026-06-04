using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class minimap_cam_moving : MonoBehaviour
{
    private GameObject MainCamera; // ��J�A�Q�l�� Y �b���ਤ�ת����� (MainCamera)
    public GameObject PlayerIcon; // ��J�n���H Y �b���ਤ�ת����� (PlayerIcon)
    public GameObject MapUI;
    public InputActionReference trigger;

    private bool isCentered = false; // �Ω�l�� MapUI �O�_�b�����󥿤���
    // Start is called before the first frame update

    void Start()
    {
        MainCamera = Camera.main.gameObject;
    }

    // Update is called once per frame

    void Update()
    {
        // ��� PlayerIcon ���e�����ਤ��
        Vector3 rotationB = PlayerIcon.transform.rotation.eulerAngles;

        // ��� MainCamera �� Y �b���ਤ��
        float rotationAY = MainCamera.transform.rotation.eulerAngles.y;

        // ��s PlayerIcon �� Y �b���ਤ�סA�O����L�b����
        PlayerIcon.transform.rotation = Quaternion.Euler(rotationB.x, rotationAY, rotationB.z);

        if (Input.GetKeyDown(KeyCode.M) || trigger.action.triggered)
        {
            if (isCentered)
            {
                // �N MapUI ���^��۹������� (650, 350)
                MapUI.transform.localPosition = new Vector3(650, 350, 0);

                // �N MapUI ���Y��վ㬰 1 ��
                MapUI.transform.localScale = Vector3.one;
            }
            else
            {
                // �N MapUI ���ʨ������󪺥�����
                MapUI.transform.localPosition = Vector3.zero;

                // �N MapUI ���Y��վ㬰 2 ��
                MapUI.transform.localScale = new Vector3(2, 2, 2);
            }

            // �������A
            isCentered = !isCentered;
        }
    }
}