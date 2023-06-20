using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    Seat selection;


    Sprite first, second; 
    
    Image firstI, secondI;


    public void OnClickSeat(Seat s)
    {
        if (selection != null)
        {
            selection = s;
            firstI = s.GetComponent<Image>();
            first = firstI.sprite;

        }
        else
        {


            secondI = s.GetComponent<Image>();
            second = firstI.sprite;

            ReplaceIfNotNull(firstI, second);
            ReplaceIfNotNull(secondI, first);

            secondI.sprite = first;

            selection = null;

        }
    }

    void ReplaceIfNotNull(Image b, Sprite s)
    {
        if (s == null)
        {
            b.sprite = null;
        }
        else
        {
            b.sprite = s;
        }
    }



}
