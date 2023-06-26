using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SeatRow : MonoBehaviour
{

   public List<Seat> seats = new();
    [SerializeReference] TextMeshProUGUI UI_rowmod;

    public float ROW_MODIFIER
    {
        get
        {
            float b = 0;
            foreach (var item in seats)
            {
                b += item.student.ROW_MODIFIER;
            }
            return b;
        }
    }

    private void Start()
    {
        foreach (var item in seats)
        {
            if (item.row != null)
            {
                throw new System.Exception("seat at row " + gameObject.name + " already has a row... this must mean we have overlapping stuff. not good.");
            }
            item.row = this; 
        }
    }


    public void Refresh()
    {

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