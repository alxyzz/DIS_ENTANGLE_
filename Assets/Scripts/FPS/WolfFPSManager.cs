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

    public TMPro.TextMeshProUGUI countdownText;

    private float timeRemaining = 90.0f; // 1 minute 30 seconds

    public List<UnityEngine.AI.NavMeshAgent> ai = new();

    private bool timerIsRunning = false;

    public void CustomStart()
    {
        timerIsRunning = true;
        foreach (var item in ai)
        {
            item.enabled = true;
        }
    }

   
    void Start()
    {
        foreach (var item in ai)
        {
            item.enabled = false;
        }
    }
  


   

    // Update is called once per frame
    void Update()
    {

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateCountdownText();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                // Call your method here
                WinWolfblade();
            }
        }

    
    }

    private void UpdateCountdownText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    void WinWolfblade()
    {
        WinScreenScript.Instance.PopUp();
    }


    public void Lose()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    }

}
