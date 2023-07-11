using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FakeLoadingScreen : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent StartMethod; //method that starts stuff AFTER loading is done
    public VideoPlayer vid;
    public bool skip;
    void Start()
    {
        
        StartCoroutine(delayedTransition());
    }



    float pendulation = 0.5f;
    float timePassed;


    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > pendulation)
        {
            timePassed = 0;
            if (vid.playbackSpeed != 6f)
            {
                vid.playbackSpeed = 6f;
            }
            else
            {
                vid.playbackSpeed = 2f;
            }
        }
    }


    IEnumerator delayedTransition()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.SkipIntro)
            {
                GameManager.Instance.SkipIntro = false;
                if (StartMethod != null)
                {
                    StartMethod.Invoke();
                }
                Destroy(gameObject);
            }
        }


        if (skip)
        {
            yield return new WaitForSecondsRealtime(0f);
        }
        else
        {
            yield return new WaitForSecondsRealtime(6f);

        }

        if (StartMethod != null)
        {
            StartMethod.Invoke();
        }
       
        Destroy(gameObject);
    }
}
