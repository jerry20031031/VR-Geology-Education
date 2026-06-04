using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlHand : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void OnSceneLoad()
    {
        GameObject targetPoint = GameObject.Find("SpawnPoint");
        if (targetPoint != null)
        {
            this.gameObject.transform.position = targetPoint.transform.position;
        }
    }
    
    void Start()
    {
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            OnSceneLoad();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
