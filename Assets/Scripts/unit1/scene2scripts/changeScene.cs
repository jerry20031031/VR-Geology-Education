using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    // �o�Ө禡�|�b���s�I���ɩI�s
    public Vector3 newPosition = new Vector3(0, 0, 0); // �ؼЦ�m
    public Vector3 newRotation = new Vector3(0, 0, 0); // �ؼб���
    public GameObject xrOrigin;
    public void ChangeScene()
    {
        SceneManager.LoadScene("Scene3.4");
        
    }
       public void ChangeSceneIndex(int index)
    {
        SceneManager.LoadScene(index);
        
    }
    public void ChangeScene1()
    {
        SceneManager.LoadScene("Scene5");

    }
    public void ChangeScenehobby1()
    {
        SceneManager.LoadScene("Scene1.2");
    }
    public void ChangeScenehobby2()
    {
        SceneManager.LoadScene("Scene1");
    }
    public void ChangeSceneunit3ToBighall()
    {
        SceneManager.LoadScene("bigHall 1");
    }
    public void ChangeSceneIntro()
    {
        SceneManager.LoadScene("intro");
    }
    public void ChangeSceneBigHall2()
    {
        SceneManager.LoadScene("bigHall2");
    }
    public void initial()
    {
        if (xrOrigin != null)
        {
            xrOrigin.transform.position = newPosition;
            xrOrigin.transform.rotation = Quaternion.Euler(newRotation);
        }
    }
}
