using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    //A.classroom game
    //janitor smoking, wolfblade looking out window
    //B.wolfblade FPS game
    //wolfblade sitting, paper hits her
    //C.paige journal game
    //janitor walks in, picks up paper

    //act 2

    //A.classroom game 2
    //janitor looks at paper
    //B.wolfblade FPS game 2
    //wolfblade runs by janitor, trips, janitor fights bullies
    //C.paige journal game 3D ?
    [Header("SCENE NAMES as a string")]

    [HideInInspector] public string MAIN_MENU = "MAIN_MENU";
    //games
    [HideInInspector] public string MRMISTA_CLASSROOM_SCENE = "MRMISTA_CLASSROOM_SCENE"; //
    [HideInInspector] public string WOLFBLADE_ESCAPE_SCENE = "WOLFBLADE_ESCAPE_SCENE";//
    [HideInInspector] public string PAIGE_JOURNAL_SCENE = "PAIGE_JOURNAL_SCENE";//
    [HideInInspector] public string PAIGE_JOURNAL_3D_SCENE = "PAIGE_JOURNAL_3D_SCENE";

    //cinematics
    [HideInInspector] public string JANITOR_SMOKE_CINEMATIC = "JANITOR_SMOKE_CINEMATIC";
    [HideInInspector] public string WOLFBLADE_PAPER_HIT_CINEMATIC = "WOLFBLADE_PAPER_HIT_CINEMATIC";
    [HideInInspector] public string JANITOR_PICKUP_PAPER_CINEMATIC = "JANITOR_PICKUP_PAPER_CINEMATIC";
    [HideInInspector] public string JANITOR_LOOKS_AT_PAPER_CINEMATIC = "JANITOR_LOOKS_AT_PAPER_CINEMATIC";
    [HideInInspector] public string JANITOR_FIGHT_CINEMATIC = "JANITOR_FIGHT_CINEMATIC";

    [HideInInspector] public string ENDGAME_MENU = "ENDGAME_MENU";

    [HideInInspector] public int act = 1;
    [HideInInspector]public int level = 1;

    #endregion
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ChangeLevel();
        }
    }


    public void ChangeLevel( bool advancePlot = true)
    {
        if (level >= 6)
        {
            act++;
            level = 0;
        }
        if (advancePlot)
        {
            level++;

        }
        //act 1

        //A.classroom game
        //janitor smoking, wolfblade looking out window
        //B.wolfblade FPS game
        //wolfblade sitting, paper hits her
        //C.paige journal game
        //janitor walks in, picks up paper

        //act 2

        //A.classroom game 2
        //janitor looks at paper
        //B.wolfblade FPS game 2
        //wolfblade runs by janitor, trips, janitor fights bullies
        //C.paige journal game 3D ?

        //[HideInInspector] public string MAIN_MENU = "MAIN_MENU";
        ////games
        //[HideInInspector] public string MRMISTA_CLASSROOM_SCENE = "MRMISTA_CLASSROOM_SCENE"; //
        //[HideInInspector] public string WOLFBLADE_ESCAPE_SCENE = "WOLFBLADE_ESCAPE_SCENE";//
        //[HideInInspector] public string PAIGE_JOURNAL_SCENE = "PAIGE_JOURNAL_SCENE";//
        //[HideInInspector] public string PAIGE_JOURNAL_3D_SCENE = "PAIGE_JOURNAL_3D_SCENE";

        ////cinematics
        //[HideInInspector] public string JANITOR_SMOKE_CINEMATIC = "JANITOR_SMOKE_CINEMATIC";
        //[HideInInspector] public string WOLFBLADE_PAPER_HIT_CINEMATIC = "WOLFBLADE_PAPER_HIT_CINEMATIC";
        //[HideInInspector] public string JANITOR_PICKUP_PAPER_CINEMATIC = "JANITOR_PICKUP_PAPER_CINEMATIC";
        //[HideInInspector] public string JANITOR_LOOKS_AT_PAPER_CINEMATIC = "JANITOR_LOOKS_AT_PAPER_CINEMATIC";
        //[HideInInspector] public string JANITOR_FIGHT_CINEMATIC = "JANITOR_FIGHT_CINEMATIC";

        //[HideInInspector] public string ENDGAME_MENU = "ENDGAME_MENU";
        if (act == 1)
        {
            switch (level)
            {
                case 1: //classroom
                    SceneManager.LoadScene(MRMISTA_CLASSROOM_SCENE);

                    break;

                case 2: //janitor smoking cinematic | act 2 looking at paper
                    SceneManager.LoadScene(JANITOR_SMOKE_CINEMATIC);
                    break;

                case 3://wolfblade FPS game
                    SceneManager.LoadScene(WOLFBLADE_ESCAPE_SCENE);

                    break;

                case 4://woflblade sitting, paper hits
                    SceneManager.LoadScene(WOLFBLADE_PAPER_HIT_CINEMATIC);

                    break;
                case 5://paige journal game
                    SceneManager.LoadScene(PAIGE_JOURNAL_SCENE);

                    break;

                case 6://janitor walks in, picks up paper
                    SceneManager.LoadScene(JANITOR_PICKUP_PAPER_CINEMATIC);

                    break;
                case 0://janitor smoking cinematic | act 2 looking at paper
                    throw new System.Exception("Error. Level in GameManager was 0. this means we fucked up somewhere.");
                default:
                   
                    break;
            }
        }
        else
        {


            //A.classroom game 2
            //janitor looks at paper
            //B.wolfblade FPS game 2
            //wolfblade runs by janitor, trips, janitor fights bullies
            //C.paige journal game 3D ?

            switch (level)
            {
                case 1: //classroom GAME 2
                    SceneManager.LoadScene(PAIGE_JOURNAL_SCENE);
                    break;

                case 2: // act 2 looking at paper
                    SceneManager.LoadScene(PAIGE_JOURNAL_SCENE);
                    break;

                case 3://wolfblade FPS GAME 2
                    SceneManager.LoadScene(PAIGE_JOURNAL_SCENE);
                    break;

                case 4://wolfblade runs by janitor, trips, janitor fights bullies
                    SceneManager.LoadScene(PAIGE_JOURNAL_SCENE);
                    break;
                case 5://paige journal GAME 3D
                    SceneManager.LoadScene(PAIGE_JOURNAL_SCENE);

                    break;
                case 6://End Game Screen
                    SceneManager.LoadScene(PAIGE_JOURNAL_SCENE);
                    break;
                case 0://janitor smoking cinematic | act 2 looking at paper
                    throw new System.Exception("Error. Level in GameManager was 0. this means we fucked up somewhere.");
                default:

                    break;
            }
        }

       
       
          
       


    }
   

    public void BadEnding()
    {
        SceneManager.LoadScene(JANITOR_FIGHT_CINEMATIC);
    }

    public void GoodEnding()
    {
        SceneManager.LoadScene(JANITOR_LOOKS_AT_PAPER_CINEMATIC);
    }

    
    
}
