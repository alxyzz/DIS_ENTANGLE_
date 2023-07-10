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

    //cinematics
    [HideInInspector] public string JANITOR_SMOKE_CINEMATIC = "JANITOR_SMOKE_CINEMATIC";
    [HideInInspector] public string WOLFBLADE_PAPER_HIT_CINEMATIC = "WOLFBLADE_PAPER_HIT_CINEMATIC";
    [HideInInspector] public string JANITOR_PICKUP_PAPER_CINEMATIC = "JANITOR_PICKUP_PAPER_CINEMATIC";
    [HideInInspector] public string JANITOR_LOOKS_AT_PAPER_CINEMATIC = "JANITOR_LOOKS_AT_PAPER_CINEMATIC";
    [HideInInspector] public string JANITOR_FIGHT_CINEMATIC = "JANITOR_FIGHT_CINEMATIC";

    [HideInInspector] public string ENDGAME_MENU = "ENDGAME_MENU";

    [HideInInspector] public int level = 1; //1 to 12

    #endregion
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ChangeLevel();
        }
    }
    [HideInInspector] public int PaigeLevelInstance = 1;
    [HideInInspector] public int MrMistaInstance = 1;
    [HideInInspector] public int WolfbladeInstance = 1;
    [HideInInspector] public bool SkipIntro = false;
    [HideInInspector] public bool? badEnding = null;
    public void ChangeLevel(bool advancePlot = true, bool skipintro = false)
    {
        if (advancePlot)
        {
            level++;

        }
        SkipIntro = skipintro; //skip intro so we can restart stuff


        switch (level)
        {
            case 1: //classroom
                SceneManager.LoadScene(MRMISTA_CLASSROOM_SCENE);
                break;

            case 2: //janitor smoking cinematic 
                SceneManager.LoadScene(JANITOR_SMOKE_CINEMATIC);
                break;

            case 3://wolfblade FPS game
                SceneManager.LoadScene(WOLFBLADE_ESCAPE_SCENE);

                break;

            case 4://paige journal game
                SceneManager.LoadScene(PAIGE_JOURNAL_SCENE);

                break;

            case 5: //classroom
                MrMistaInstance = 2;
                SceneManager.LoadScene(MRMISTA_CLASSROOM_SCENE);

                break;
            case 6: //paige
                MrMistaInstance = 2;
                SceneManager.LoadScene(PAIGE_JOURNAL_SCENE);

                break;
            case 7: //wolfblade classroom survival
                WolfbladeInstance = 2;
                SceneManager.LoadScene(WOLFBLADE_ESCAPE_SCENE);

                break;
            case 8: //paige
                PaigeLevelInstance = 2;
                SceneManager.LoadScene(PAIGE_JOURNAL_SCENE);

                break;
            case 9: //janitor vid leaving dooro pen and looking at paper
                SceneManager.LoadScene(JANITOR_LOOKS_AT_PAPER_CINEMATIC);

                break;
            case 10: //paige
                PaigeLevelInstance = 3;
                SceneManager.LoadScene(PAIGE_JOURNAL_SCENE);

                break;
            case 11: //wolfblade escaping thru door
                WolfbladeInstance = 3;
                SceneManager.LoadScene(WOLFBLADE_ESCAPE_SCENE);

                break;
            case 12: //ending
                if (badEnding == false)
                { //good ending
                    SceneManager.LoadScene(JANITOR_LOOKS_AT_PAPER_CINEMATIC);

                }
                else if (badEnding == true)
                {//bad ending
                    SceneManager.LoadScene(JANITOR_FIGHT_CINEMATIC);

                }
                else
                {
                    throw new System.Exception("@gameManager @ ChangeLevel - bad-ending variable undefined. this should not happen .");
                }
                MrMistaInstance = 2;
                SceneManager.LoadScene(MRMISTA_CLASSROOM_SCENE);

                break;

            default:
                throw new System.Exception("@gameManager @ ChangeLevel - level was something that it shouldnt be .");
          
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
