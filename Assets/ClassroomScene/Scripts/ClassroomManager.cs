using System.Collections.Generic;
using UnityEngine;

public class ClassroomManager : MonoBehaviour
{

    #region settings


    public float settings_StudentNavigationMinimumTargetDistance = 0.5f;


    #endregion

    int focusedPeople
    {
        get
        {
            return students.FindAll(x => x.Focused == true).Count;
        }
    }
    int peopleWithoutModifiers
    {
        get
        {
            return students.FindAll(x => x.Focused == false && x.Annoyed == false).Count;
        }
    }
    int disturbedPeople
    {
        get
        {
            return students.FindAll(x => x.Annoyed == true).Count;
        }
    }

    bool classIsReadyToTeach
    {
        get
        {
            if (focusedPeople > disturbedPeople)
            {
                return true;
            }
            else return false;
        }
    }

    List<Student> students = new();
    List<Seat> seats = new();



    public void RegisterStudent(Student s)
    {
        students.Add(s);
    }

    public void RegisterSeat(Seat s)
    {
        seats.Add(s);
    }


    private static ClassroomManager _instance;
    public static ClassroomManager Instance
    {
        get
        {
            return _instance;
        }
    }


    Seat lastClickedSeat;
    GameObject seatGraphic;



    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            _instance = this;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.LogWarning("clicked LMB ");
            DoClick();
        }

    }


    void DoClick()
    {

        // Cast a ray from the mouse position into the scene
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray hits a GameObject
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit GameObject is the one you want to detect clicks on
            if (hit.collider.gameObject == gameObject)
            {
                if (hit.collider.gameObject.tag == "Seat")
                {
                    Seat b = hit.collider.gameObject.GetComponent<Seat>();

                    OnClickSeat(b);

                }

                // Perform any desired actions here
            }
        }
    }


    void Victory()
    {

    }

    /// <summary>
    ///runs post move, checks victory condition
    /// </summary>
    void PostMove()
    {
        if (classIsReadyToTeach)
        {
            Victory();
        }
        else
        {

        }
    }

    //call this from clicked seat
    public void OnClickSeat(Seat s)
    {

        Debug.LogWarning("clicked seat ");

        if (s.occupant == null)
        {
            //throw new System.Exception("ClassroomManager@OnClickSeat() - seat had no occupant.");
        }
        if (lastClickedSeat == null)
        {
            SelectSeat(s);
            //select
        }
        else
        {
            SwitchSeatOccupant(s, lastClickedSeat);
            //switch
        }

        UpdateClickedSeat(true);
    }

    void UpdateClickedSeat(bool delete = false)
    {
        if (delete == true)
        {
            lastClickedSeat = null;
            seatGraphic.SetActive(false);
        }
        else
        {
            if (lastClickedSeat != null)
            {
                seatGraphic.transform.position = lastClickedSeat.transform.position;
            }
            seatGraphic.SetActive(true);
        }


    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"> the newest seat clicked.</param>
    void SwitchSeatOccupant(Seat seatA, Seat seatB)
    {


        if (seatB.occupant != null)
        {

            Student s1 = seatB.occupant;

            s1.ChangeSeat(seatA);

        }
        if (seatA.occupant != null)
        {
            Student s2 = seatA.occupant;

            s2.ChangeSeat(seatB);

        }


        lastClickedSeat.occupant = null;
        UpdateClickedSeat(true);
    }

    void MoveOccupant(Seat std, Vector3 target)
    {

        std.occupant.MoveToLocation(target);

        std.occupant = null;
    }

    void SelectSeat(Seat s)
    {


        UpdateClickedSeat();
    }



}
