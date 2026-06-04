using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public InputActionReference ControlUi;
    public InputActionReference FocusControlUi;
    public GameObject Informationui;

    void Update()
    {
            if (ControlUi.action.triggered||FocusControlUi.action.triggered)
            {
                Informationui.SetActive(!Informationui.activeSelf);
            }
            
    }

    public void RestartScene()
    {
        // 重新載入當前場景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToHall()
    {
        SceneManager.LoadScene(0);
    }
}
