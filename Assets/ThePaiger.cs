using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThePaiger : MonoBehaviour
{

    public int order = 1;
    public int pageIndex = 0;
    bool pressed = false;
    [SerializeReference] List<Page> pages = new();
    TextMeshProUGUI texty
    {
        get
        {
            return pages[pageIndex].texty;
        }
    }


    public void ChangeText(string texter)
    {
        texty.text = texter;
        if (order >= pages[pageIndex].words)
        {
            NextPage();
        }
        order++;
    }

    public void NextPage()
    {
        if (pressed)
        {
            return;

        }
        pressed = true;
        StopAllCoroutines();
        dirtysecondpagebooleanyeah = true;
        pageIndex++;
        StartCoroutine(SlideNExtPage());

    }

    bool dirtysecondpagebooleanyeah = false;
    IEnumerator SlideNExtPage()
    {
        yield return new WaitForSecondsRealtime(1f);
        if (pages != null)
        {
            if (dirtysecondpagebooleanyeah)
            {
                if (GameManager.Instance == null)
                {
                    SceneManager.LoadScene("JANITOR_PICKUP_PAPER_CINEMATIC");
                }
                else
                {
                    GameManager.Instance.ChangeLevel();
                }
            }
            else
            {
                foreach (var item in pages)
                {
                    item.gameObject.SetActive(false);
                }
                pages[pageIndex].gameObject.SetActive(true);
                order = 1;
                pressed = false;
            }
        }
        else
        {

            Debug.LogError("no pages? do something about it.");
        }

    }
}


