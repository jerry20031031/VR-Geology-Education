using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boat : MonoBehaviour
{
    public float speed = 1f; 

    private bool isMovingRight = false;
    private bool isMovingLeft = false;
    private bool isMovingUp = false;
    private bool isMovingDown = false;
    public void MoveRightStart() { isMovingRight = true; }
    public void MoveRightStop() { isMovingRight = false; }

    public void MoveLeftStart() { isMovingLeft = true; }
    public void MoveLeftStop() { isMovingLeft = false; }

    public void MoveUpStart() { isMovingUp = true; }
    public void MoveUpStop() { isMovingUp = false; }

    public void MoveDownStart() { isMovingDown = true; }
    public void MoveDownStop() { isMovingDown = false; }

    private void Update()
    {
        if (isMovingRight)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (isMovingLeft)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (isMovingUp)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if (isMovingDown)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

}

