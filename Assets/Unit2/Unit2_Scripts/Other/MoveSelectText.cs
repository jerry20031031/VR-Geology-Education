using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelectText : MonoBehaviour
{
    public Vector3 MovePosition;
    public Vector3 MoveRotation; 

    public void Move(Vector3 newPosition, Vector3 newRotation)
    {
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(newRotation);
    }

    public void Start()
    {
        Move(MovePosition, MoveRotation);
    }
}
