using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene4 : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene("Scene4"); // 使用場景名稱
    }
}
