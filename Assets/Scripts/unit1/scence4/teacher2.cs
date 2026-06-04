using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class teacher2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject targetGameObject;
    private void OnEnable()
    {

        Invoke("CallTal11", 0.1f);

    }
    private void CallTal11()
    {
        unit1TaskManager11 target = targetGameObject.GetComponent<unit1TaskManager11>();
        // ÚÌ«O target ¤£?ªÅ
        if (target != null)
        {
            target.tal11();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
