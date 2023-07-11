using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalAnswerButton : MonoBehaviour
{

    [SerializeReference]ThePaiger paiger;
    public int order; //starts from 1
    public string text;

    void Start()
    {
     
        Button b = GetComponent<Button>();

        b.onClick.AddListener(delegate () { OnClickButton(); });
    }
    public void OnClickButton()
    {
        Debug.Log("Clicked paige button with name " + gameObject.name);
        if (paiger.order == order)
        {
            paiger.ChangeText(text);
        }
    }

}
