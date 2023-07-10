using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void OnClickPlay()
    {
        GameManager.Instance.ChangeLevel(false);
    }


    public void OnClickExit()
    {
        Application.Quit();

    }
}
