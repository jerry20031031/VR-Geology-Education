using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit3Scene1MissionDetect : MonoBehaviour
{
    public GameObject mountain;
    public GameObject island1;
    public GameObject island2;
    public GameObject Trench1;
    public GameObject Trench2;
    public Material green;
    public Material blue;
    public Talk GirlTalk;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Scene1MissionDetect()
    {
        Renderer mountainRenderer = mountain.GetComponent<Renderer>();
        Renderer island1Renderer = island1.GetComponent<Renderer>();
        Renderer island2Renderer = island2.GetComponent<Renderer>();
        Renderer trench1Renderer = Trench1.GetComponent<Renderer>();
        Renderer trench2Renderer = Trench2.GetComponent<Renderer>();

        bool isSameColor = mountainRenderer.sharedMaterial.color == green.color &&
                        island1Renderer.sharedMaterial.color == blue.color &&
                        island2Renderer.sharedMaterial.color == blue.color &&
                        trench1Renderer.sharedMaterial.color == blue.color &&
                        trench2Renderer.sharedMaterial.color == blue.color;

        if (isSameColor)
        {
            GirlTalk.Conversationindex(5);
            Debug.Log("correct");
        }
        else
        {
            GirlTalk.Conversationindex(4);
            Debug.Log("no");
        }
    }
}
