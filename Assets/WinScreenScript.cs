using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenScript : MonoBehaviour
{
    [SerializeField] GameObject uiobject;
    #region singleton
    private static WinScreenScript _instance;
    public static WinScreenScript Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            _instance = this;

        }
        uiobject.SetActive(false);
    }

    #endregion


    public void PopUp()
    {
        uiobject.SetActive(true);

        Debug.LogWarning("Popped up win screene.");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void OnClickProceed()
    {
        GameManager.Instance.Transition();
    }

    public void OnClickRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

}
