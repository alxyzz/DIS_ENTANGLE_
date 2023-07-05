using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalAnswerButton : MonoBehaviour
{
    [HideInInspector]public TextMeshProUGUI text;
    [HideInInspector] public Button btn;



    public void OnClickButton()
    {
        Debug.Log("Clicked button" + gameObject + "!");
        JournalManager.Instance.OnClickButton(text.text);
    }


    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(delegate { OnClickButton(); });
    }

}
