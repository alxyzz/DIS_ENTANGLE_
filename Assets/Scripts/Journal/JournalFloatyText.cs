using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalFloatyText : MonoBehaviour
{

    bool moving;
    Vector3 d;
    Vector3 start;
    float move = 0;
    public void SetTarget(Vector3 dest)
    {
        start = transform.position;
        d = dest;
        moving = true;
    }


    void Update()
    {
        if (!moving)
        {
            return;
        }

        if (Vector3.Distance(d, transform.position) < 0.2f)
        {
            Destroy(gameObject);
        }
        move += 0.01f;
        transform.position = Vector3.Lerp(start, d, move);
    }
}
