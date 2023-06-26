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
    public StudentSerializableObject student;

    void Start()
    {
        student.chosenName = PickRandomName();
        Refresh();
    }

    public void Refresh()
    {
        cardPortrait.sprite = student.portrait;
    }

    string PickRandomName()
    {
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
