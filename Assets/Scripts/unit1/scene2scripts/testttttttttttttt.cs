using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testttttttttttttt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"start x = {transform.position.x}");
        Debug.Log($"start y = {transform.position.y}");
        Debug.Log($"start z = {transform.position.z}");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"x = {transform.position.x}");
        Debug.Log($"y = {transform.position.y}");
        Debug.Log($"z = {transform.position.z}");
    }
}
