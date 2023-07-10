using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClassroomManager : MonoBehaviour
{
    #region singleton
    private static ClassroomManager _instance;
    public static ClassroomManager Instance
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



    #region provisory goal
    int stuffplaced;

    #endregion


    [HideInInspector] public List<Seat> LIST_SEATS = new();
    [HideInInspector] public List<SeatRow> LIST_ROWS = new();
    float averageHappiness
    {
        get
        {
            float b = 0;
            foreach (var item in LIST_SEATS)
            {
                b += item.LEARNING_FACTOR;
            }
            return b;
        }
    }
    float goalhappiness = 15;
    ///goal is to get a percentage of students happy

    Seat lastSelectedSeat;
    StudentCard HOVERED_CARD;
    StudentCard CLICKED_CARD;
    [SerializeReference] List<StudentSerializableObject> StudentSequence = new();
    [SerializeReference] List<StudentSerializableObject> Act2Sequence = new();
    [SerializeReference] List<SeatRow> rows = new();


    int SETTING_AESTHETIC_AMT_CARDS_PER_SIDE = 5;
    List<StudentCard> leftCards = new(); //this is populated on start
    List<StudentCard> rightCards = new(); //this is populated on start
    [SerializeReference] GameObject StudentCardPrefab;
    [SerializeReference] GameObject CardParent;
    [SerializeReference] GameObject WinPanel;

    bool hoveringOverCard = false;

    #region UI
    public GameObject UI_CardInfo;

    public TextMeshProUGUI currentlySelectedCardName;
    public TextMeshProUGUI currentlySelectedCardDesc;

    public TextMeshProUGUI completion;


    #endregion

    public Student HOVERED_STUDENT;



    void DisplayStudentInfo()
    {
        UI_CardInfo.SetActive(true);

        if (HOVERED_STUDENT != null)
        {
            currentlySelectedCardName.text = HOVERED_STUDENT.chosenName;
            currentlySelectedCardDesc.text = HOVERED_STUDENT.DESC;
        }

    }

    void HideCardInfo(bool isStudent = false)
    {
        if (isStudent)
        {
            UI_CardInfo.SetActive(false);
        }
        if (HOVERED_CARD != null)
        {
            OnHoverCardEnter(HOVERED_CARD);
        }
        else
        {
            UI_CardInfo.SetActive(false);
        }

    }

    void DisplayCardInfo(bool hover)
    {
        UI_CardInfo.SetActive(true);
        if (hover)
        {
            if (HOVERED_CARD != null)
            {
                if (HOVERED_CARD.student != null)
                {


                    currentlySelectedCardName.text = HOVERED_CARD.student.chosenName;
                    currentlySelectedCardDesc.text = HOVERED_CARD.student.DESC;
                }
            }
        }
        else
        {
            if (CLICKED_CARD != null)
            {
                if (CLICKED_CARD.student != null)
                {


                    currentlySelectedCardName.text = CLICKED_CARD.student.chosenName;
                    currentlySelectedCardDesc.text = CLICKED_CARD.student.DESC;
                }
            }
        }

        //currentlySelectedCardModifier.text = lastSelectedCard.student.LANE_MODIFIER.ToString();
    }
    public void OnWinClickProceed()
    {

        GameManager.Instance.ChangeLevel();
    }


    void CheckWinCondition()
    {


        if (happy >= happinessThreshold)
        {
            Win();
        }

        void Win()
        {
            //WinPanel.SetActive(true);
            WinScreenScript.Instance.PopUp();
        }
    }
    void OnPlaceCard()
    {
        HideCardInfo();
        Destroy(CLICKED_CARD.gameObject);
        HOVERED_CARD = null;


        CheckWinCondition();
    }
    [SerializeReference] TextMeshProUGUI txt_HappinessFeedback;
    float happinessThreshold = 8;
    int happy
    {
        get
        {
            int hp = 0;
            foreach (var item in LIST_SEATS)
            {
                if (item.EFFECTIVE_HAPPINESS > 0)
                {
                    hp += 1;
                }
            }
            return hp;
        }
    }




    public static void LogIfNull(object variable, string variableName)
    {
        if (variable == null)
        {
            Debug.LogError("Variable '" + variableName + "' is null!");
        }
    }


    public Seat GetRightNeighbor(Seat b)
    {
        return b.right;

    }

    int students = 12;
    void RefreshHappinessFeedback()
    {
        txt_HappinessFeedback.text = happinessThreshold.ToString();
    }
    public void OnSelectCard(StudentCard b)
    {
        if (CLICKED_CARD == b)
        { //when deselecting
            CLICKED_CARD.ToggleHighLight(false);

            HOVERED_CARD = null;
            CLICKED_CARD = null;
            HideCardInfo();
            return;
        }
        //when selecting but already have one selected
        if (CLICKED_CARD != null)
        {
            CLICKED_CARD.ToggleHighLight(false);

            HOVERED_CARD = null;

            HideCardInfo();
        }

        CLICKED_CARD = b;
        CLICKED_CARD.ToggleHighLight(true);
        DisplayCardInfo(false);

    }
    #region Settings


    #endregion


    #region UnityMethods



    void Start()
    {
        UI_CardInfo.SetActive(false);

        // InitializeSeats();  initialized in the seatRow script
        foreach (var item in rows)
        {
            Debug.Log(item.name);
        }
        InitializeCards();

    }
    //private GameObject lastHoveredObject;

    #endregion



    public void Restart()
    {
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex);
    }
    void InitializeCards()
    {
        //initialize card objects on both sides up to the maximum
        List<StudentCard> cds = new();
        int cardsMade = 0;

      
        if (GameManager.Instance != null)
        {
            while (cardsMade < StudentSequence.Count)
            {
                StudentCard b = Instantiate(StudentCardPrefab, CardParent.transform).GetComponent<StudentCard>();
                cardsMade++;
                cds.Add(b);
                rightCards.Add(b);

            }
            if (GameManager.Instance.act == 1)
            {
                while (cardsMade < StudentSequence.Count)
                {
                    StudentCard b = Instantiate(StudentCardPrefab, CardParent.transform).GetComponent<StudentCard>();
                    cardsMade++;
                    cds.Add(b);
                    rightCards.Add(b);

                }

                for (int i = 0; i < StudentSequence.Count; i++)
                {//preset choice
                    cds[i].Initialize(StudentSequence[i]);
                }
            }
            else
            { //different cards on act 2
                while (cardsMade < Act2Sequence.Count)
                {
                    StudentCard b = Instantiate(StudentCardPrefab, CardParent.transform).GetComponent<StudentCard>();
                    cardsMade++;
                    cds.Add(b);
                    rightCards.Add(b);

                }
                while (cardsMade < Act2Sequence.Count)
                {
                    StudentCard b = Instantiate(StudentCardPrefab, CardParent.transform).GetComponent<StudentCard>();
                    cardsMade++;
                    cds.Add(b);
                    rightCards.Add(b);

                }

                for (int i = 0; i < Act2Sequence.Count; i++)
                {//preset choice
                    cds[i].Initialize(Act2Sequence[i]);
                }

            }
        }
        else
        {

            while (cardsMade < StudentSequence.Count)
            {
                StudentCard b = Instantiate(StudentCardPrefab, CardParent.transform).GetComponent<StudentCard>();
                cardsMade++;
                cds.Add(b);
                rightCards.Add(b);

            }

            for (int i = 0; i < StudentSequence.Count; i++)
            {//preset choice
                cds[i].Initialize(StudentSequence[i]);
            }
        }
       

    }

    /// <summary>
    /// removes the modifiers caused by a certain student from all students
    /// </summary>
    /// <param name="source"></param>
    

    

    public void OnClickRestart()
    {
        Debug.Log("Clicked restart.");
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("The GameManager script is not loaded, because you probably didn't start from the menu. This disables using Restart, but no worries, you can still play.");
            return;
        }
        GameManager.Instance.ChangeLevel(false, true);
    }

    public void OnClickGotoMenu()
    {
        Debug.Log("Clicked goto menu.");


        UnityEngine.SceneManagement.SceneManager.LoadScene("MAIN_MENU");


    }
    void PlaceStudent(Seat s)
    {

        if (s.student != null)
        {
            Debug.Log("Clicked occupied seat, doing nothing.");
            return;
        }

        //RemoveStudentAndEffects(s.student);

        s.student = CLICKED_CARD.student;
        s.student.row = s.row;
        OnPlaceCard();

        CLICKED_CARD = null;
        stuffplaced++;



        RefreshEffects();
        foreach (var item in LIST_SEATS)
        {
            item.RefreshGraphics();
        }
        CheckWinCondition();
    }
    public void OnClickSeat(Seat s)
    {
        Debug.Log("Clicked a seat.");
        if (CLICKED_CARD != null)
        {
            PlaceStudent(s);
        }
        RefreshHappinessFeedback();
    }



    void RefreshEffects()
    {
        RemoveAllEffects();
        ApplyAllEffects();
    }

    public void RemoveAllEffects()
    {
        foreach (var item in LIST_SEATS)
        {
            item.modifiers.Clear();
        }
    }

    public void ApplyAllEffects()
    {
        foreach (var item in LIST_SEATS)
        {
            if (item.student != null)
            {
                item.CheckPrerequisiteAndDoEffect();
            }
        }
    }
    public void OnHoverSeatEnter(Student s)
    {


        HOVERED_STUDENT = s;
        DisplayStudentInfo();
    }

    public void OnHoverSeatExit() { HideCardInfo(true); }


    public void OnHoverCardEnter(StudentCard b)
    {
        HOVERED_CARD = b;
        DisplayCardInfo(true);


    }

    public void OnHoverCardExit()
    {
        HideCardInfo();

    }


}
