using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThePaiger : MonoBehaviour
{

    public int order = 1;
    public int pageIndex = 0;
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
        StopAllCoroutines();
        StartCoroutine(SlideNExtPage());

    }
    IEnumerator SlideNExtPage()
    {
        yield return new WaitForSecondsRealtime(3f);
        pageIndex++;
        if (pages != null)
        {
            if (pages[pageIndex] == null)
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
            }
        }
        else
        {

            Debug.LogError("no pages? do something about it.");
        }
       
    }
}


