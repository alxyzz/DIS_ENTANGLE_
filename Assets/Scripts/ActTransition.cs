using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActTransition : MonoBehaviour
{
    TextMeshProUGUI texty;
  


    void Start()
    {
        texty.text = GameManager.Instance.GetTransitionText();
        StartCoroutine(delayedTransition());
    }


    IEnumerator delayedTransition()
    {
        yield return new WaitForSecondsRealtime(GameManager.Instance.sceneTransitionDelay);

        GameManager.Instance.Transition();


    }
}
