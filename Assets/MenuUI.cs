using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void OnClickClassroom()
    {
        SceneManager.LoadScene("ClassroomScene");
    }
    public void OnClickWolfblade()
    {
        SceneManager.LoadScene("FPS_Test");

    }
    public void OnClickTextGame()
    {
        SceneManager.LoadScene("TextGameScene");

    }
    public void OnClickTextVideo()
    {   
        SceneManager.LoadScene("VideoTest");

    }

    public void OnClickExit()
    {
        Application.Quit();

    }
}
