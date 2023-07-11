using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToPlayer : MonoBehaviour
{
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            transform.LookAt(playerTransform, Vector3.up);
        }
    }
}
