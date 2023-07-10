using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StudentCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Image cardPortrait;
    public GameObject highlight;
    [HideInInspector]public Student student;
    [HideInInspector] StudentSerializableObject studentObject;

    void Start()
    {
        
    }
    
    /// <summary>
    /// unpacks the SerializableObject and prepares the name, refreshes interface
    /// </summary>
    /// <param name="SO"></param>
    public void Initialize(StudentSerializableObject SO)
    {
        studentObject = SO;
        UnpackSO();
        RefreshPortrait();
    }

    void UnpackSO()
    {
       
        Student b = new Student(studentObject.chosenName,
                   studentObject.seatedImage,
                    studentObject.portrait,
                    studentObject.STAT_LEARNING,
                   studentObject.DESC,
                    studentObject.ROW_MODIFIER,
                   studentObject.prereq,
                    studentObject.PREREQ_ARGUMENT,
                   studentObject.effect,
                  studentObject.EFFECT_ARG_ONE,
                  studentObject.EFFECT_ARG_two);
     
        student = b;

    }

    public void RefreshPortrait()
    {
        if (student.portrait != null)
        {
            cardPortrait.sprite = student.portrait;

        }
        else
        {
            Debug.Log("Card " + student.GetType() + " had no portrait.");
        }
    }


    public void ToggleHighLight(bool b)
    {
        highlight.SetActive(b);
    }

    public void OnClick()
    {
        Debug.Log("CLICKED CARD.");
        ClassroomManager.Instance.OnSelectCard(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ClassroomManager.Instance.OnHoverCardEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ClassroomManager.Instance.OnHoverCardExit();

    }
}
