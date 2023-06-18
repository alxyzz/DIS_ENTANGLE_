using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private AIController _ai;

    // Start is called before the first frame update
    void Start()
    {
        if (_ai == null)
        {
            _ai = new AIController();
        }
    }


    void HasApproachedPlayer()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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

    public override  void Exit(PlayerScript uPlayer) { }

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
public class AIController
{
    private EnemyState currentState;

    public Transform target; // Reference to the target the AI is interacting with

    private int TimeElapsed;

    private void Start()
    {
        // Initialize the AI with the starting state
        currentState = new PatrolState(target);
        currentState.EnterState();
    }

    private void Update(float timedeltatime) //call this on update
    {
        TimeElapsed += timedeltatime;
        // Update the current state
        currentState.UpdateState();

        // Transition to a new state if necessary
        if (/* Some condition to transition to chase state */)
        {
            TransitionToState(new ChaseState(target));
        }
        else if (/* Some condition to transition to patrol state */)
        {
            TransitionToState(new PatrolState(target));
        }
    }

    private void TransitionToState(IAIState newState)
    {
        TimeElapsed = 0;
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}

