using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomManager : MonoBehaviour
{
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


    //call this from clicked seat
    public void OnClickSeat(Seat s)
    {
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
        if (delete == true )
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
