using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SeatRow : MonoBehaviour
{[Header("All IDs must be identical and are used to build the grid for calculations. so count up or down normally")]
    public int rowID;
   public List<Seat> seats = new();
    [SerializeReference] TextMeshProUGUI UI_rowmod;

    public float ROW_MODIFIER
    {
        get
        {
            float b = 0;
            foreach (var item in seats)
            {
                if (item.student != null)
                {
                        b += item.student.ROW_MODIFIER;
                }
                else return 0;
            }
            return b;
        }
    }


    public float TotalHappinessInRow
    {
        get
        {
            float b = 0;

            foreach (var item in seats)
            {
                if (item.student != null)
                {
                    b += item.student.EFFECTIVE_HAPPINESS;
                }
            }
            return b;
        }
    }

    private void Start()
    {
        foreach (var item in seats)
        {
            if (item == null)
            {
                Debug.LogError("Seat was null @ SeatRow" + gameObject.name + ", you probably need to reassign a missing seat reference.");

                return;
            }
            if (item.row != null)
            {
                throw new System.Exception("seat at row " + gameObject.name + " already has a row... this must mean we have overlapping stuff. not good.");
            }
            ClassroomManager.Instance.LIST_SEATS.Add(item);
            item.row = this; 
        }

        
    }


    public void Refresh()
    {
        UI_rowmod.text = TotalHappinessInRow.ToString();
    }



    public float GetAverageLearningFactor()
    {
        float b = 0;
        foreach (var item in seats)
        {
            if (item.student != null)
            {
                b += item.student.STAT_LEARNING;
            }

        }

        return b;
    }

}
