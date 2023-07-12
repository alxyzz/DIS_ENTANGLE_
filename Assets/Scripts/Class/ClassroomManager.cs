using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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




    #region settings
    float happinessThreshold = 8; //this is used wether the variable 'happy' returns it or higher



    #endregion

    #region Variables
    string happy
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
                if (hp > happinessThreshold)
                {
                    StartCoroutine(delayedWin());
                }
            }
            return hp.ToString() + "/8";
        }
    }

    ///goal is to get a percentage of students happy

    Seat lastSelectedSeat;
    StudentCard HOVERED_CARD;
    StudentCard CLICKED_CARD;
    Student HOVERED_STUDENT;

    [SerializeReference] List<StudentSerializableObject> Students_ACT1 = new();
    [SerializeReference] List<StudentSerializableObject> Students_ACT2 = new();


    List<StudentCard> CARDS = new(); //this is populated on start


    #endregion
    #region references
    //intro cinematic - wip until game actually works
    public List<TextMeshProUGUI> introLines = new();
    public int introIndex = 0;
    public GameObject introProf;
    public GameObject bg;

    //


    public GameObject UI_CardInfo;
    public AudioSource asource;
    public AudioClip bell;
    public TextMeshProUGUI currentlySelectedCardName;
    public TextMeshProUGUI currentlySelectedCardDesc;
    public TextMeshProUGUI currentlySelectedCardLearning;
    [SerializeReference] GameObject StudentCardPrefab;
    [SerializeReference] GameObject CardParent;
    [SerializeReference] GameObject WinPanel;
    [SerializeReference] List<SeatRow> rows = new();
    [HideInInspector] public List<Seat> LIST_SEATS = new();
    [HideInInspector] public List<SeatRow> LIST_ROWS = new();
    #endregion




    void DisplayStudentInfo()
    {
        UI_CardInfo.SetActive(true);

        if (HOVERED_STUDENT != null)
        {
            currentlySelectedCardName.text = HOVERED_STUDENT.chosenName;
            currentlySelectedCardDesc.text = HOVERED_STUDENT.DESC;
            currentlySelectedCardLearning.text = "SHOWN ON LABEL";
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
                    currentlySelectedCardLearning.text = HOVERED_CARD.student.STAT_LEARNING.ToString();

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
                    currentlySelectedCardLearning.text = CLICKED_CARD.student.STAT_LEARNING.ToString();
                }
            }
        }

        //currentlySelectedCardModifier.text = lastSelectedCard.student.LANE_MODIFIER.ToString();
    }
    public void OnWinClickProceed()
    {

        GameManager.Instance.ChangeLevel();
    }


   


    IEnumerator delayedWin()
    {
        asource.PlayOneShot(bell);
        yield return new WaitForSecondsRealtime(bell.length + 1f);
        GameManager.Instance.ChangeLevel();

    }

    void OnPlaceCard()
    {
        HideCardInfo();
        Destroy(CLICKED_CARD.gameObject);
        HOVERED_CARD = null;


    }
    [SerializeReference] TextMeshProUGUI txt_HappinessFeedback;




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
    public TextMeshProUGUI seatdisplay;
    void RefreshHappinessFeedback()
    {
        if (lastSelectedSeat == null)
        {
            seatdisplay.text = "NOTHING";
        }
        else
        {
            if (lastSelectedSeat.student != null)
            {
                seatdisplay.text = lastSelectedSeat.student.chosenName;

            }
            else
            {
                seatdisplay.text = lastSelectedSeat.gameObject.name;
            }
        }
        txt_HappinessFeedback.text = happy;
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
        if (GameManager.Instance == null)
        {
            SceneManager.LoadScene("MAIN_MENU");
        }
        RefreshHappinessFeedback();
    }



    //private GameObject lastHoveredObject;

    #endregion
    public void CustomStart()
    {
        UI_CardInfo.SetActive(false);

        // InitializeSeats();  initialized in the seatRow script
        foreach (var item in rows)
        {
            Debug.Log(item.name);
        }
        InitializeCards();
    }


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
            while (cardsMade < Students_ACT1.Count)
            {
                StudentCard b = Instantiate(StudentCardPrefab, CardParent.transform).GetComponent<StudentCard>();
                cardsMade++;
                cds.Add(b);
                CARDS.Add(b);

            }
            if (GameManager.Instance.MrMistaInstance == 1)
            {
                while (cardsMade < Students_ACT1.Count)
                {
                    StudentCard b = Instantiate(StudentCardPrefab, CardParent.transform).GetComponent<StudentCard>();
                    cardsMade++;
                    cds.Add(b);
                    CARDS.Add(b);

                }

                for (int i = 0; i < Students_ACT1.Count; i++)
                {//preset choice
                    cds[i].Initialize(Students_ACT1[i]);
                }
            }
            else
            { //different cards on act 2
                while (cardsMade < Students_ACT2.Count)
                {
                    StudentCard b = Instantiate(StudentCardPrefab, CardParent.transform).GetComponent<StudentCard>();
                    cardsMade++;
                    cds.Add(b);
                    CARDS.Add(b);

                }
                while (cardsMade < Students_ACT2.Count)
                {
                    StudentCard b = Instantiate(StudentCardPrefab, CardParent.transform).GetComponent<StudentCard>();
                    cardsMade++;
                    cds.Add(b);
                    CARDS.Add(b);

                }

                for (int i = 0; i < Students_ACT2.Count; i++)
                {//preset choice
                    cds[i].Initialize(Students_ACT2[i]);
                }

            }
        }
        else
        {

            while (cardsMade < Students_ACT1.Count)
            {
                StudentCard b = Instantiate(StudentCardPrefab, CardParent.transform).GetComponent<StudentCard>();
                cardsMade++;
                cds.Add(b);
                CARDS.Add(b);

            }

            for (int i = 0; i < Students_ACT1.Count; i++)
            {//preset choice
                cds[i].Initialize(Students_ACT1[i]);
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
    void PlaceStudent(Seat s, Student st, bool placedFromCard)
    {

        if (s.student != null && placedFromCard)
        {
            Debug.Log("Clicked occupied seat, doing nothing.");
            return;
        }

        //RemoveStudentAndEffects(s.student);

        s.student = st;
        s.student.row = s.row;
        if (placedFromCard)
        {
            OnPlaceCard();
            CLICKED_CARD = null;

        }


        RefreshEffects();

        foreach (var item in LIST_SEATS)
        {
            item.RefreshGraphics();
        }
    }

    void RemoveStudent(Seat s)
    {


        //RemoveStudentAndEffects(s.student);
        if (s.student != null)
        {
            s.student = null;
        }
        else
        {
            Debug.Log("Student already gone from this spot. Seat - " + s.name);
        }

        foreach (var item in LIST_SEATS)
        {
            item.RefreshGraphics();
        }
    }


    IEnumerator unselectSeat()
    {


        yield return new WaitForSecondsRealtime(1.5f);
        lastSelectedSeat = null;
    }
    public void OnClickSeat(Seat s)
    {
        //StopAllCoroutines();
        //StartCoroutine(unselectSeat());
        if (s == lastSelectedSeat)
        {
            lastSelectedSeat = null;
            return;
        }

        if (CLICKED_CARD != null && s.student == null)
        { //if this is from clicking a card, we just place it down.
            PlaceStudent(s, CLICKED_CARD.student, true);
            return;
        }
        if (lastSelectedSeat != null)
        {
            if (lastSelectedSeat.student != null)
            {
                if (s.student != null)
                {
                    SwitchStudents(s, lastSelectedSeat);
                }
                else
                {
                    MoveStudent(lastSelectedSeat, s);
                }
                lastSelectedSeat = null;
            }
            return;
        }
        if (lastSelectedSeat == null)
        {
            if (s.student != null)
            {
                lastSelectedSeat = s;
            }


        }
       
           

                    
      


        void SwitchStudents(Seat firstSeat, Seat secondS)
        {
            Student FirstSeatStudent = new Student(firstSeat.student.chosenName,

               firstSeat.student.seatedImage,
                firstSeat.student.portrait,
                firstSeat.student.STAT_LEARNING,
               firstSeat.student.DESC,
                firstSeat.student.ROW_MODIFIER,
               firstSeat.student.prereq,
                firstSeat.student.PREREQ_ARGUMENT,
               firstSeat.student.effect,
              firstSeat.student.EFFECT_ARG_ONE,
              firstSeat.student.EFFECT_ARG_two)
            { };

            Student secondSeatStudent = new Student(secondS.student.chosenName,
               secondS.student.seatedImage,
                secondS.student.portrait,
                secondS.student.STAT_LEARNING,
               secondS.student.DESC,
                secondS.student.ROW_MODIFIER,
               secondS.student.prereq,
                secondS.student.PREREQ_ARGUMENT,
               secondS.student.effect,
              secondS.student.EFFECT_ARG_ONE,
              secondS.student.EFFECT_ARG_two)
            { };


            RemoveStudent(firstSeat);
            RemoveStudent(secondS);

            PlaceStudent(firstSeat, secondSeatStudent, false);
            PlaceStudent(secondS, FirstSeatStudent, false);
            RefreshEffects();

        }

        void MoveStudent(Seat oldplace, Seat newplace)
        {
            if (lastSelectedSeat.student != null)
            {
                //we get the student
                Student stud = new Student(oldplace.student.chosenName,

                   oldplace.student.seatedImage,
                    oldplace.student.portrait,
                    oldplace.student.STAT_LEARNING,
                   oldplace.student.DESC,
                    oldplace.student.ROW_MODIFIER,
                   oldplace.student.prereq,
                    oldplace.student.PREREQ_ARGUMENT,
                   oldplace.student.effect,
                  oldplace.student.EFFECT_ARG_ONE,
                  oldplace.student.EFFECT_ARG_two)
                { };
                //we remove it from the last seat
                RemoveStudent(lastSelectedSeat);
                //we add it to the new one
                PlaceStudent(newplace, stud, false);
                RefreshEffects();

            }
        }

        RefreshHappinessFeedback();
    }



    void RefreshEffects()
    {
        RemoveAllEffects();
        ApplyAllEffects();
        RefreshHappinessFeedback();
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
