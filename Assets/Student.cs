using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Student : MonoBehaviour
{

    NavMeshAgent _agent;
    Vector3 goal;
    public float minDistance;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }



    public void ChangeSeat(Seat target)
    {

        
        goal = target.transform.position;
        _agent.destination = goal;
        _agent.isStopped = false;



    }


    //if we want to move away
    public void MoveToLocation(Vector3 target)
    {
        goal = target;
        _agent.destination = target;
        _agent.isStopped = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (goal != null)
        {
            if (Vector3.Distance(transform.position, goal) < minDistance)
            {
                _agent.isStopped = true;
            }
        }
       
    }
}
