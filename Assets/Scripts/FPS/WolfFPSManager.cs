using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfFPSManager : MonoBehaviour
{

    #region singleton
    private static WolfFPSManager _instance;
    public static WolfFPSManager Instance
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
    }

    #endregion



    public float SETTING_TIME_TO_WIN;
     float TIME_REMAINING;
    bool started;

    // Start is called before the first frame update
    void Start()
    {
        
    }



    public void InitiateCountdown()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            TIME_REMAINING -= Time.deltaTime;
            if (TIME_REMAINING == 0)
            {
                WinWolfblade();
            }
        }
    }




    void WinWolfblade()
    {

    }


    public void Lose()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    }

}
