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

    Seat lastSelectedSeat;
    StudentCard lastSelectedCard;
    List<StudentSerializableObject> studentTypes = new();

    List<StudentCard> cards = new();


    #region UI
    public GameObject UI_PauseMenu, UI_Options, cardInfo;

    public TextMeshProUGUI currentlySelectedCardName;
    public TextMeshProUGUI currentlySelectedCardDesc;
    public TextMeshProUGUI currentlySelectedCardModifier;


    #endregion


    void HideCardInfo()
    {
        cardInfo.SetActive(false); 
    }

    void DisplayCardInfo()
    {
        cardInfo.SetActive(true);

        currentlySelectedCardName.text = lastSelectedCard.student.chosenName;
        currentlySelectedCardDesc.text = lastSelectedCard.student.DESC;
        currentlySelectedCardModifier.text = lastSelectedCard.student.LANE_MODIFIER.ToString();
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


    public void OnSelectCard(StudentCard b)
    {
        if (lastSelectedCard == b)
        {
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

    }


    #endregion


    void InitializeSeats()
    {

    }
    public void OnClickSeat(Seat s)
    {
        //gets values of clicked stuff
        if (lastSelectedSeat == null)
        {
            lastSelectedSeat = s;
            Debug.Log("Clicked first student with name " + lastSelectedSeat.gameObject.name);


        }
        else
        {
            Debug.Log("Clicked second student with name " + lastSelectedSeat.gameObject.name);
            StudentSerializableObject seatcurrentlyclicked = null;
            StudentSerializableObject studentFromlastSelectedSeat = null;

            if (s.student != null)
            {
                seatcurrentlyclicked = s.student;

            }
            if (lastSelectedSeat.student != null)
            {
                studentFromlastSelectedSeat = lastSelectedSeat.student;

            }

            lastSelectedSeat.student = seatcurrentlyclicked;
            s.student = studentFromlastSelectedSeat;


            s.PostMove();
            lastSelectedSeat.PostMove();

            lastSelectedSeat = null;

        }
    }





}
