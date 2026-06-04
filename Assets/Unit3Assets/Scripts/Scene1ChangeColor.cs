using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1ChangeColor : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeColor(Material color){
        this.gameObject.GetComponent<Renderer>().material=color;

    }
    public void changeColorToOther(Renderer render){
        Material Color=this.gameObject.GetComponent<Renderer>().material;
        render.material=Color;

    }
}
