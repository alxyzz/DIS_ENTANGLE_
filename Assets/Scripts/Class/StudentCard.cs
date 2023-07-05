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

    public void Initialize(StudentSerializableObject SO)
    {
        studentObject = SO;
        UnpackSO();
        student.chosenName = PickRandomName();
        Refresh();
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
        b.Names = studentObject.Names;
        student = b;
        b.prereq = studentObject.prereq;

        b.PREREQ_ARGUMENT = studentObject.PREREQ_ARGUMENT;

        b.effect = studentObject.effect;

        b.EFFECT_ARG_ONE = studentObject.EFFECT_ARG_ONE;
        b.EFFECT_ARG_two = studentObject.EFFECT_ARG_two;



    }

    public void Refresh()
    {
        cardPortrait.sprite = student.portrait;
    }

    string PickRandomName()
    {

        if (student.Names.Count == 1)
        {
            return student.Names[0];
        }
        if (student.Names.Count == 0 || student.Names == null)
        {
            return "MISSING#";
        }
        return student.Names[Random.Range(0, student.Names.Count)];
    }

    public void ToggleHighLight(bool b)
    {
        highlight.SetActive(b);
    }

    public void OnClick()
    {
        ClassroomManager.Instance.OnSelectCard(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ClassroomManager.Instance.OnHoverCard(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ClassroomManager.Instance.OnLeaveHover();

    }
}
