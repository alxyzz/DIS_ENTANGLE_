using UnityEngine;
using UnityEngine.AI;

public enum StudentType
{
    Peeker, //looks at the person in front, annoying them
    Nerd, //is inherently focused. 8-)
    Snacker, //annoys everyone nearby by snacking super loudly during class
    Focuser,//focuses everyone nearby by being so incredibly boring they'd rather pay attention to the class than to them
    Xannoyer, //like Snacker, but annoys everyone diagonally



}

public class Student : MonoBehaviour
{

    NavMeshAgent _agent;
    Vector3 goal;
    float minDistance
    {
        get
        {
            return ClassroomManager.Instance.settings_StudentNavigationMinimumTargetDistance;
        }
    }


    public bool Focused
    {
        get
        {
            return _focused;
        }

    }//wether the annoyance is nullfiied by the positive focusing effect of another student (takes precedence over annoyment)
    public bool Annoyed
    {

        get
        {
            return _annoyed;
        }

    }
    //wether this student is annoyed and disturbed from learning by a student nearby

    bool _focused;//wether the annoyance is nullfiied by the positive focusing effect of another student (takes precedence over annoyment)
    bool _annoyed;//wether this student is annoyed and disturbed from learning by a student nearby


    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        ClassroomManager.Instance.RegisterStudent(this);

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
