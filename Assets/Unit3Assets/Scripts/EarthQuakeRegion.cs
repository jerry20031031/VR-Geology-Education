using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuakeRegion : MonoBehaviour
{
    // Start is called before the first frame update
   public void Active()
{
    this.gameObject.SetActive(!gameObject.activeSelf); // 切換啟用狀態
}
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
