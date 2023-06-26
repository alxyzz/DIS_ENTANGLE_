using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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

    Seat lastSelectedSeat;
    StudentCard lastSelectedCard;
   [SerializeReference] List<StudentSerializableObject> studentTypes = new();
    List<SeatRow> rows = new();

    int SETTING_AMT_STARTING_CARDS = 10;
    int SETTING_AESTHETIC_AMT_CARDS_PER_SIDE = 5;
    List<StudentCard> leftCards = new(); //this is populated on start
    List<StudentCard> rightCards = new(); //this is populated on start
    [SerializeReference] GameObject StudentCardPrefab;
    [SerializeReference] GameObject LeftCardParent;
    [SerializeReference] GameObject RightCardParent;
    List<Seat> seats = new();


    #region UI
    public GameObject UI_PauseMenu, UI_Options, UI_CardInfo;

    public TextMeshProUGUI currentlySelectedCardName;
    public TextMeshProUGUI currentlySelectedCardDesc;
    public TextMeshProUGUI currentlySelectedCardModifier;


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

    void OnPlaceCard()
    {
        lastSelectedCard.ToggleHighLight(false);
        lastSelectedCard = null;
        HideCardInfo();
        Destroy(lastSelectedCard.gameObject);
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
        InitializeSeats();
        InitializeCards();
    }
    //private GameObject lastHoveredObject;
    void FixedUpdate()
    {
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    GameObject hoveredObject = EventSystem.current.currentSelectedGameObject;

        //    // Check if the currently hovered object is different from the last one
        //    if (hoveredObject != lastHoveredObject)
        //    {
        //        Debug.Log("Mouse over: " + hoveredObject.name);
        //        lastHoveredObject = hoveredObject;
        //    }
        //}
    }
    #endregion


    void InitializeSeats()
    {
        foreach (var item in rows)
        {
            seats.AddRange(item.seats);
        }
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

    public void OnClickSeat(Seat s)
    {

        if (lastSelectedCard != null)
        {
            if (s.student != null)
            {
                return;
            }
            s.student = lastSelectedCard.student;
            lastSelectedCard = null;
            OnPlaceCard();
        }


        foreach (var item in seats)
        {
            item.Refresh();
        }

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
