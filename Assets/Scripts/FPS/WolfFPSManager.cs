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
    // [SerializeReference] GameObject YouDied;
    [SerializeReference] GameObject PlayerPos_Act1;
    [SerializeReference] GameObject PlayerPos_Act2;
    [SerializeReference] GameObject PlayerPos_Act3;
    [SerializeReference] GameObject PlayerObject;

    [SerializeReference] GameObject DoorBlocker;




    private float timeRemaining = 90.0f; // 1 minute 30 seconds



    [SerializeReference] List<UnityEngine.AI.NavMeshAgent> ai_act1 = new();
    [SerializeReference] List<UnityEngine.AI.NavMeshAgent> ai_act2 = new();
    [SerializeReference] List<UnityEngine.AI.NavMeshAgent> ai_act3 = new();

    private bool timerIsRunning = false;

    public void CustomStart()
    {
        if (GameManager.Instance == null)
        {

            // PlayerObject.transform.position = PlayerPos_Act1.transform.position;
            DoorBlocker.SetActive(true);
            foreach (var item in ai_act1)
            {
                item.enabled = true;
            }

        }
        else
        {

            foreach (var item in ai_act1)
            {
                item.enabled = true;
            }

            DoorBlocker.SetActive(true);

        }


        //switch (GameManager.Instance.WolfbladeInstance)
        //{

        //    case 1:

        //        PlayerObject.transform.position = PlayerPos_Act1.transform.position;
        //        foreach (var item in ai_act1)
        //        {
        //            item.enabled = true;
        //        }
        //        break;
        //    case 2:
        //        PlayerObject.transform.position = PlayerPos_Act2.transform.position;
        //        foreach (var item in ai_act2)
        //        {
        //            item.enabled = true;
        //        }
        //        break;
        //    case 3:
        //        PlayerObject.transform.position = PlayerPos_Act3.transform.position;
        //        DoorBlocker.SetActive(false);
        //        foreach (var item in ai_act3)
        //        {
        //            item.enabled = true;
        //        }
        //        break;

        //    default:
        //
        //        break;}
        timerIsRunning = true;

    }


    void Start()
    {
        Debug.LogWarning("If you start this directly from the scene, it will not have the act 2 or 3 features, only the act 1. ");
        CustomStart();
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
