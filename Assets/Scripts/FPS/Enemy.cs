using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    //private AIController _ai;
    public Transform player;             // Reference to the player's transform
    PlayerScript _pscript;
    public float detectionRange = 5f;    // Range at which the enemy detects the player
    public float attackRange = 2f;        // Range at which the enemy initiates a hit
    public float attackDelay = 1.5f;        // Range at which the enemy initiates a hit
    private NavMeshAgent navMeshAgent;    // Reference to the NavMeshAgent component

    public float knockbackForce = 5f;

    private float timeSinceLastAttack;
    bool attackCooldown;
    bool ragdolling;
    private Rigidbody rbody;
    Quaternion initialRotation;
    // Start is called before the first frame update
    void Start()
    {
        //if (_ai == null)
        //{
        //    _ai = new AIController();
        //}
        initialRotation = transform.rotation;
        navMeshAgent = GetComponent<NavMeshAgent>();
        _pscript = player.GetComponent<PlayerScript>();
    }

    public void ReceiveHit(Vector3 playerPos)
    {
        if (rbody == null)
        {
            StartCoroutine(gotHit(playerPos));

        }
    }

    IEnumerator gotHit(Vector3 playpos)
    {
        //initializes physics
        rbody = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        navMeshAgent.isStopped = true;
        Vector3 direction = (rbody.transform.position - playpos).normalized;
        rbody.AddForce(direction * knockbackForce, ForceMode.Force);
        rbody.AddTorque(transform.up * 5f * 1f);
        yield return new WaitForSecondsRealtime(5f);
        //disables physics and enables navigation
        Destroy(rbody);
        navMeshAgent.isStopped = false;
        transform.rotation = initialRotation;
       


    }

   

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (!navMeshAgent.enabled)
        {
            return;
        }

        if (rbody != null)
        {
            return;
        }

        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (navMeshAgent.speed != 5)
        {
            navMeshAgent.speed = 5;
            navMeshAgent.angularSpeed = 300;
            navMeshAgent.isStopped= true;
            navMeshAgent.isStopped= false;
        }

        navMeshAgent.SetDestination(player.position);
        if (distanceToPlayer <= attackRange && timeSinceLastAttack > attackDelay)
        {
           
            timeSinceLastAttack = 0;
            // Initiate a hit or attack
            AttackPlayer();
        }
        else if (distanceToPlayer > 20)
        {
            navMeshAgent.speed = int.MaxValue;
        }
        if (distanceToPlayer < 1.5f)
        {
            navMeshAgent.isStopped = true;
        }
        else
        {
            navMeshAgent.isStopped = false;
            
        }
    }

    private void AttackPlayer()
    {
        // Perform the attack logic here
        Debug.Log("Enemy hits the player!");
        _pscript.GetHit();
    }
}





// Define an interface for the AI states
class EnemyState
{

    //2 states - chasing and hitting
    //hitting passes a timer to next frame after adding to it, if it's long enough it attacks (like 0.5 seconds) if play is still close enough. if still close, goes back to hitting, if not, goes to chasing



    public virtual void Enter(PlayerScript uPlayer) { }

    public virtual void Execute(PlayerScript uPlayer, double dt) { }

    public virtual void Exit(PlayerScript uPlayer) { }

};


class Chasing : EnemyState
{

    //2 states - chasing and hitting
    //hitting passes a timer to next frame after adding to it, if it's long enough it attacks (like 0.5 seconds) if play is still close enough. if still close, goes back to hitting, if not, goes to chasing



    public override void Enter(PlayerScript uPlayer) { }

    public override void Execute(PlayerScript uPlayer, double dt) { }

    public override void Exit(PlayerScript uPlayer) { }

};


class Hitting : EnemyState
{

    //2 states - chasing and hitting
    //hitting passes a timer to next frame after adding to it, if it's long enough it attacks (like 0.5 seconds) if play is still close enough. if still close, goes back to hitting, if not, goes to chasing



    public override void Enter(PlayerScript uPlayer) { }

    public override void Execute(PlayerScript uPlayer, double dt) { }

    public override void Exit(PlayerScript uPlayer) { }

};




// Implement another concrete state class


// Implement the AI controller
//public class AIController
//{
//    private EnemyState currentState;

//    public Transform target; // Reference to the target the AI is interacting with

//    private int TimeElapsed;

//    private void Start()
//    {
//        // Initialize the AI with the starting state
//        //currentState = new PatrolState(target);
//        //currentState.EnterState();
//    }

//    private void Update(float timedeltatime) //call this on update
//    {
//        //TimeElapsed += timedeltatime;
//        //// Update the current state
//        //currentState.UpdateState();

//        //// Transition to a new state if necessary
//        //if (/* Some condition to transition to chase state */)
//        //{
//        //    TransitionToState(new ChaseState(target));
//        //}
//        //else if (/* Some condition to transition to patrol state */)
//        //{
//        //    TransitionToState(new PatrolState(target));
//        //}
//    }

//    //private void TransitionToState(IAIState newState)
//    //{
//    //    TimeElapsed = 0;
//    //    currentState.ExitState();
//    //    currentState = newState;
//    //    currentState.EnterState();
//    //}
//}

