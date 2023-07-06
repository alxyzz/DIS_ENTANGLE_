using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(delayeddestroy());
    }


    IEnumerator delayeddestroy()
    {


        yield return new WaitForSecondsRealtime(6f);

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
