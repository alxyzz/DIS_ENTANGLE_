using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Act
{
    One,
    Two,

}

public enum Level
{
    Menu,
    Classroom,
    Wolfblade,
    Paige,
    Janitor
}
public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager _instance;
    public static GameManager Instance
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
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion


    #region settings
    [Header("Time spent in the Transition scene, not including loading in and out.")]
    public float sceneTransitionDelay = 6;


    #endregion

    #region refs


    [Header("SCENE NAMES as a string")]

    public string MENU;
    public string MRMISTA_CLASSROOM;
    public string WOLFBLADE_ESCAPE;
    public string PAIGE_JOURNAL;
    public string PAIGE_JOURNAL_3D;
    public string JANITOR_CINEMATIC_1;
    public string JANITOR_CINEMATIC_2;
    public string JANITOR_CINEMATIC_3;
    public string ENDGAME;


    public Act CurrentAct = Act.One;
    public Level CurrentLevel = Level.Menu;
    #endregion
    //public void ChangeLevel()
    //{
    //    switch (switch_on)
    //    {
    //        default:
    //    }
    //}


    public void AdvanceLevel()
    {
        if (CurrentLevel == Level.Janitor)
        {
            if (CurrentAct == Act.Two)
            {
                SceneManager.LoadScene(ENDGAME);
                return;
            }
            CurrentAct = Act.Two;
            CurrentLevel = Level.Classroom;
            return;
        }
        if (CurrentLevel == Level.Menu)
        {
            CurrentLevel = Level.Classroom;
        }
        else if (CurrentLevel == Level.Classroom)
        {
            CurrentLevel = Level.Wolfblade;
        }
        else if (CurrentLevel == Level.Wolfblade)
        {
            CurrentLevel = Level.Paige;
        }
        else if (CurrentLevel == Level.Paige)
        {
            CurrentLevel = Level.Janitor;
        }
       


    }
    public void Transition()
    {
        switch (CurrentAct)
        {
            case Act.One:
                break;
            case Act.Two:
                //load paige's scene in 3d
                break;
            default:
                break;
        }

        switch (CurrentLevel)
        {
            case Level.Menu:
                SceneManager.LoadScene(MENU);
                break;
            case Level.Classroom:
                SceneManager.LoadScene(MRMISTA_CLASSROOM);
                break;
            case Level.Wolfblade:
                SceneManager.LoadScene(WOLFBLADE_ESCAPE);
                break;
            case Level.Paige:
                if (CurrentAct == Act.One)
                {
                    SceneManager.LoadScene(PAIGE_JOURNAL);

                }
                else
                {
                    SceneManager.LoadScene(PAIGE_JOURNAL_3D);

                }
                SceneManager.LoadScene(PAIGE_JOURNAL);
                break;
            case Level.Janitor:
                if (CurrentAct == Act.One)
                {
                    SceneManager.LoadScene(JANITOR_CINEMATIC_1);

                }
                else
                {
                    SceneManager.LoadScene(JANITOR_CINEMATIC_2);

                }
                break;
            default:
                break;
        }
    }

    public string GetTransitionText()
    {
        string a = "";
        switch (CurrentAct)
        {
            case Act.One:
                a += "Act 1\n";
                break;
            case Act.Two:
                a += "Act 2\n";
                break;
            default:
                break;
        }
        switch (CurrentLevel)
        {
            
            case Level.Classroom:
                a += "The Classroom\nStarring Mr. Mista";
                break;
            case Level.Wolfblade:
                a += "The Escape\nStarring Wolfblade";

                break;
            case Level.Paige:
                a += "The Daydream\nStarring Paige";

                break;
            case Level.Janitor:
                a += "The Classroom\nStarring the Janitor";

                break;
            default:
                break;
        }


        return a;
    }
    
}
