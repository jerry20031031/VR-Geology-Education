using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Hint : MonoBehaviour
{
    private Camera mainCamera;
    private float distance; // 玩家與NPC的距離
    private Animator hintAnimator;
    public bool communication = false;
    public InputActionReference prev;
    public InputActionReference next;
    public bool PointTo = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        // 設置Canvas的主相機
        GetComponent<Canvas>().worldCamera = mainCamera;
        hintAnimator = GetComponent<Animator>();
        GetComponent<CanvasGroup>().enabled = true;
    }

    void Update()
    {

        distance = Vector3.Distance(mainCamera.transform.position, transform.position);

        if (distance < 20)
        {
            if (PointTo)
            {
                if (!communication)
                {
                    hintAnimator.SetBool("Active", true);
                    transform.LookAt(mainCamera.transform);
                    transform.Rotate(0, 180, 0);
                }

                if ((Input.GetKeyDown(KeyCode.X) || next.action.triggered))
                {

                    hintAnimator.SetBool("Active", false);

                }
            }
            else
            {

                hintAnimator.SetBool("Active", false);
            }


        }
        else
        {
            hintAnimator.SetBool("Active", false);
            communication = false;
        }
    }
    public void Point()
    {
        PointTo = true;
    }
    public void ExitPoint()
    {
        PointTo = false;
    }
    public void DialogFinish()
    {
        communication = false;
    }
    public void DialogStart()
    {
        communication = true;
    }
}
