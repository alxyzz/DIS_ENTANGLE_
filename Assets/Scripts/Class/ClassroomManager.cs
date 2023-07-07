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

    Seat lastSelectedSeat;
    StudentCard lastSelectedCard;
    [SerializeReference] List<StudentSerializableObject> studentTypes = new();
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
        UI_CardInfo.SetActive(false);
    }

    void DisplayCardInfo()
    {
        UI_CardInfo.SetActive(true);

        currentlySelectedCardName.text = lastSelectedCard.student.chosenName;
        currentlySelectedCardDesc.text = lastSelectedCard.student.DESC;
        //currentlySelectedCardModifier.text = lastSelectedCard.student.LANE_MODIFIER.ToString();
    }
    public void OnWinClickProceed()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene("WOLFBLADE_ESCAPE");
    }
    public void OnHoverCard(StudentCard b)
    {
        if (lastSelectedCard != null)
        {
            return;
        }

        lastSelectedCard = b;
        DisplayCardInfo();


    }

    public void OnLeaveHover()
    {
        if (lastSelectedCard == null)
        {
            HideCardInfo();

        }

    }

    void CheckWinCondition()
    {
        //if (averageHappiness >= goalhappiness)
        //{
        //    Win();
        //}
        //completion.text = averageHappiness.ToString() + "/" + goalhappiness.ToString();



        if (stuffplaced > 7)
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
        lastSelectedCard.ToggleHighLight(false);
        HideCardInfo();
        Destroy(lastSelectedCard.gameObject);
        lastSelectedCard = null;

    }
    public void OnSelectCard(StudentCard b)
    {
        if (lastSelectedCard == b)
        {
            lastSelectedCard.ToggleHighLight(false);

            lastSelectedCard = null;

            HideCardInfo();
            return;
        }
        if (lastSelectedCard != null)
        {
            lastSelectedCard.ToggleHighLight(false);
        }

        lastSelectedCard = b;
        lastSelectedCard.ToggleHighLight(true);
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
        while (leftCards.Count < SETTING_AESTHETIC_AMT_CARDS_PER_SIDE && cardsMade < SETTING_AMT_STARTING_CARDS)
        {
            StudentCard b = Instantiate(StudentCardPrefab, LeftCardParent.transform).GetComponent<StudentCard>();
            cardsMade++;
            cds.Add(b);
            leftCards.Add(b);
        }
        while (rightCards.Count < SETTING_AESTHETIC_AMT_CARDS_PER_SIDE && cardsMade < SETTING_AMT_STARTING_CARDS)
        {
            StudentCard b = Instantiate(StudentCardPrefab, RightCardParent.transform).GetComponent<StudentCard>();
            cardsMade++;
            cds.Add(b);
            rightCards.Add(b);

        }
        //initialize student card data
        foreach (var item in cds)
        {
            item.Initialize(studentTypes[Random.Range(0, studentTypes.Count)]);
        }
    }

    /// <summary>
    /// removes the modifiers caused by a certain student from all students
    /// </summary>
    /// <param name="source"></param>
    public void RemoveStudentAndEffects(Student source)
    {
        //if (source.modifiedByThis.Count < 1)
        //{
        //    return;
        //}

        List<(Seat,(Student, float, StudentEffectType))> toRemove = new();
        foreach (var seaty in LIST_SEATS)
        {
            foreach (var item in seaty.modifiers)
            {

                if (item.Item1 == source)
                {

                    (Seat, (Student, float, StudentEffectType)) b = new(seaty, (item.Item1, item.Item2, item.Item3));
                    toRemove.Add(b);
                }


            }
        }
        foreach (var item in toRemove)
        {
            item.Item1.modifiers.Remove(item.Item2);

        }
    }

    public void OnClickSeat(Seat s)
    {

        if (lastSelectedCard != null)
        {
            if (s.student != null)
            {
                Debug.Log("Clicked occupied seat, doing nothing.");
                return;
            }

            //RemoveStudentAndEffects(s.student);
      
            s.student = lastSelectedCard.student;
            s.student.row = s.row;
            OnPlaceCard();
            s.Refresh();
            s.row.Refresh();
            lastSelectedCard = null;
            stuffplaced++;

        }


        foreach (var item in LIST_SEATS)
        {
            item.Refresh();
        }
        CheckWinCondition();
        ////gets values of clicked stuff
        //if (lastSelectedSeat == null)
        //{
        //    lastSelectedSeat = s;
        //    Debug.Log("Clicked first student with name " + lastSelectedSeat.gameObject.name);


        //}
        //else
        //{
        //    Debug.Log("Clicked second student with name " + lastSelectedSeat.gameObject.name);
        //    StudentSerializableObject seatcurrentlyclicked = null;
        //    StudentSerializableObject studentFromlastSelectedSeat = null;

        //    if (s.student != null)
        //    {
        //        seatcurrentlyclicked = s.student;

        //    }
        //    if (lastSelectedSeat.student != null)
        //    {
        //        studentFromlastSelectedSeat = lastSelectedSeat.student;

        //    }

        //    lastSelectedSeat.student = seatcurrentlyclicked;
        //    s.student = studentFromlastSelectedSeat;


        //    s.PostMove();
        //    lastSelectedSeat.PostMove();

        //    lastSelectedSeat = null;

        //}
    }





}
