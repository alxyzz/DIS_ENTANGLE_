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
    StudentCard selectedCard;
    [SerializeReference] List<StudentSerializableObject> StudentSequence = new();
    [SerializeReference] List<SeatRow> rows = new();

    int SETTING_AMT_STARTING_CARDS = 10;
    int SETTING_AESTHETIC_AMT_CARDS_PER_SIDE = 5;
    List<StudentCard> leftCards = new(); //this is populated on start
    List<StudentCard> rightCards = new(); //this is populated on start
    [SerializeReference] GameObject StudentCardPrefab;
    [SerializeReference] GameObject LeftCardParent;
    [SerializeReference] GameObject RightCardParent;
    [SerializeReference] GameObject WinPanel;



    #region UI
    public GameObject UI_PauseMenu, UI_Options, UI_CardInfo;

    public TextMeshProUGUI currentlySelectedCardName;
    public TextMeshProUGUI currentlySelectedCardDesc;
    public TextMeshProUGUI currentlySelectedCardModifier;

    public TextMeshProUGUI completion;


    #endregion


    void HideCardInfo()
    {
        if (selectedCard != null)
        {
            OnHoverCard(selectedCard);
        }
        else
        {
            UI_CardInfo.SetActive(false);
        }

    }

    void DisplayCardInfo()
    {
        UI_CardInfo.SetActive(true);
        if (selectedCard != null)
        {
            if (selectedCard.student != null)
            {


                currentlySelectedCardName.text = selectedCard.student.chosenName;
                currentlySelectedCardDesc.text = selectedCard.student.DESC;
            }
        }

        //currentlySelectedCardModifier.text = lastSelectedCard.student.LANE_MODIFIER.ToString();
    }
    public void OnWinClickProceed()
    {

        GameManager.Instance.ChangeLevel();
    }
    public void OnHoverCard(StudentCard b)
    {
        //if (lastSelectedCard != null)
        //{
        //    return;
        //}

        selectedCard = b;
        DisplayCardInfo();


    }

    public void OnLeaveHover()
    {
        HideCardInfo();

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
        selectedCard.ToggleHighLight(false);
        HideCardInfo();
        Destroy(selectedCard.gameObject);
        selectedCard = null;


        CheckWinCondition();
    }
    [SerializeReference] TextMeshProUGUI txt_HappinessFeedback;
    float happinessThreshold = 12;
    int happy
    {
        get
        {
            int hp = 0;
            foreach (var item in LIST_SEATS)
            {fix this shit
                if (item.student.EFFECTIVE_HAPPINESS > 0)
                {
                    hp += 1;
                }
            }
            return hp;
        }
    }

    int students = 12;
    void RefreshHappinessFeedback()
    {
        txt_HappinessFeedback.text = happinessThreshold.ToString();
    }
    public void OnSelectCard(StudentCard b)
    {
        if (selectedCard == b)
        {
            selectedCard.ToggleHighLight(false);

            selectedCard = null;

            HideCardInfo();
            return;
        }
        if (selectedCard != null)
        {
            selectedCard.ToggleHighLight(false);
        }

        selectedCard = b;
        selectedCard.ToggleHighLight(true);
        DisplayCardInfo();

    }
    #region Settings


    #endregion


    #region UnityMethods



    void Start()
    {
        UI_CardInfo.SetActive(false);

        // InitializeSeats();  initialized in the seatRow script
        InitializeCards();
    }
    //private GameObject lastHoveredObject;
    void FixedUpdate()
    {

    }
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

        while (cardsMade < SETTING_AMT_STARTING_CARDS)
        {
            StudentCard b = Instantiate(StudentCardPrefab, RightCardParent.transform).GetComponent<StudentCard>();
            cardsMade++;
            cds.Add(b);
            rightCards.Add(b);

        }


        for (int i = 0; i < StudentSequence.Count; i++)
        {//preset choice
            cds[i].Initialize(StudentSequence[i]);
        }
    }

    /// <summary>
    /// removes the modifiers caused by a certain student from all students
    /// </summary>
    /// <param name="source"></param>
    public void RemoveEffects(Student source)
    {
        //if (source.modifiedByThis.Count < 1)
        //{
        //    return;
        //}
        also make this work 
        List<(Seat, (Student, float, StudentEffectType))> SeatsToClean = new();
        foreach (var seaty in LIST_SEATS)
        {
            foreach (var item in seaty.modifiers)
            {

                if (item.Item1 == source)
                {
                    try
                    {
                        seaty.modifiers.Remove(item);
                    }
                    catch (System.Exception)
                    {
                        Debug.LogError("ClassroomManager@RemoveStudentEffects - could not remove the effects because list was modified during removal.");
                        throw;
                    }
                    
                    //(Seat, (Student, float, StudentEffectType)) b = new(seaty, (item.Item1, item.Item2, item.Item3));
                    //SeatsToClean.Add(b);
                }


            }
        }


        //for (int i = SeatsToClean.Count; i < 0; i--)
        //{

        //}
        //foreach (var item in SeatsToClean)
        //{
        //    item.Item1.modifiers.Remove(item.Item2);

        //}
    }


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

        s.student = selectedCard.student;
        s.student.row = s.row;
        OnPlaceCard();
        s.Refresh();
        s.row.Refresh();
        selectedCard = null;
        stuffplaced++;




        foreach (var item in LIST_SEATS)
        {
            item.Refresh();
        }
        CheckWinCondition();
    }
    public void OnClickSeat(Seat s)
    {

        if (selectedCard != null)
        {
            PlaceStudent(s);
        }
    }





}
