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
        //gets values of clicked stuff
        
       



        if (selection == null)
        {
            selection = s;
            firstI = selection.studentImage;
            Debug.Log("Clicked first student with name " + selection.gameObject.name);
            if (firstI != null)
            {
                first = firstI.sprite;

            }

        }
        else
        {
            Debug.Log("Clicked second student with name " + selection.gameObject.name);
            
            secondI = s.studentImage;
            if (secondI != null)
            {
                second = secondI.sprite;

            }
            Debug.Log("Replacing student sprites.");

           

            ReplaceIfNotNull(firstI, second);
            ReplaceIfNotNull(secondI, first);
            firstI = null;
            secondI = null;
            second = null;
            first = null;

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
