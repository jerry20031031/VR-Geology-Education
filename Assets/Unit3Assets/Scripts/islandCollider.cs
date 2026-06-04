using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class islandCollider : MonoBehaviour
{
    public GameObject HitParticle;
    public Collider island;
    public GameObject detail;
    public GameObject Hiddendetail;
    public GameObject Showdetail;
     public GameObject Hiddeninformation1;
       public GameObject Hiddeninformation2;
    private int HitTime = 0;
    private void OnCollisionEnter(Collision other) {
        
        if (other.collider==island){

  
                HitParticle.SetActive(true); 
            
        }
    }
    private void OnCollisionExit(Collision other)
    {

        if (other.collider == island)
        {

            HitTime++;
            HitParticle.SetActive(false);
            if (HitTime == 3)
            {
                HitTime = 0;
                detail.SetActive(true);
                Showdetail.SetActive(true);
                Hiddendetail.SetActive(false);
                Hiddeninformation1.SetActive(false);
                 Hiddeninformation2.SetActive(false);
            }

        }
    }
    void Start()
    {
        HitParticle.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
