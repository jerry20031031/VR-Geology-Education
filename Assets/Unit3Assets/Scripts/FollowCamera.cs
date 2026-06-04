using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public float yPos = -0.2f;
    private Quaternion targetRotation;
    private Vector3 targetPos;
    private Transform parentCanvas;
    private float step;
    // Start is called before the first frame update
    void Start()
    {
        parentCanvas = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        step = 5.0f * Time.deltaTime;
        targetPos = Camera.main.transform.position;
        targetPos.y += yPos;

        float yRotation = Camera.main.transform.eulerAngles.y;
        targetRotation = Quaternion.Euler(0, yRotation, 0);

        parentCanvas.rotation = Quaternion.Slerp(parentCanvas.rotation, targetRotation, step);
        parentCanvas.position = targetPos;
    }
}
