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
        student.chosenName = SO.chosenName;
        RefreshPortrait();
    }

    void UnpackSO()
    {
        Student b = new Student();
        b.DESC = studentObject.DESC;
        //b.LANE_MODIFIER = studentObject.LANE_MODIFIER;
        b.portrait = studentObject.portrait;
        b.ROW_MODIFIER = studentObject.ROW_MODIFIER;
        b.STAT_LEARNING = studentObject.STAT_LEARNING;
        b.seatedImage = studentObject.seatedImage;
        student = b;
        b.prereq = studentObject.prereq;

        b.PREREQ_ARGUMENT = studentObject.PREREQ_ARGUMENT;

        b.effect = studentObject.effect;

        b.EFFECT_ARG_ONE = studentObject.EFFECT_ARG_ONE;
        b.EFFECT_ARG_two = studentObject.EFFECT_ARG_two;



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
