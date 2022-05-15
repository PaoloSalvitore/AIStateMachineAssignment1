using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class StateMachine : MonoBehaviour
{

    public GameObject drop;
    public GameObject square;
    

    //comma separated list of identifiers
    public enum State
    {
        //Four different states
        Attack,
        Defence,
      //  RunAway,
        Goal,
        BerryPicking
    }

    public State currentState; //Changes which state the AI is in

    public AIMovement aiMovement; //Controls the Ai's Movement

    private void Start()  //Runs at the start of the game
    {
        aiMovement = GetComponent<AIMovement>(); //Get the info of the AIMovement Script
        
        NextState(); //Goes to the next state
    }
    
    private void NextState()
    {
        switch (currentState) //switchstatement that changes which state the AI is in
        {
            case State.Attack:
                StartCoroutine(AttackState());
                break;
            case State.Defence:
                StartCoroutine(CombatState());
                break;
            //case State.RunAway:
            //    StartCoroutine(RunAwayState());
            //    break;

            case State.Goal:

                StartCoroutine(GoalState());
                break;

            case State.BerryPicking:

                StartCoroutine(BerryPickingState());
                break;
        }
    }
    private IEnumerator AttackState()
    {
        Debug.Log("Attack: Enter");

     //   aiMovement.AIMoveTowards(squ.transform);

        while (currentState == State.Attack)
        {
            aiMovement.AIMoveTowards(aiMovement.player);
            if (!aiMovement.IsPlayerInRange())
            {
                currentState = State.BerryPicking;
            }
            
            yield return null;
        }
        Debug.Log("Attack: Exit");
        NextState();
    }
    
    private IEnumerator CombatState()
    {
        Debug.Log("CombatState: Enter");


        while (currentState == State.Defence)
        {
            aiMovement.AIMoveTowards(aiMovement.square);



            if (Vector2.Distance(transform.position, aiMovement.square.position) < 0.1f)
            {
                currentState = State.BerryPicking;
            }

            yield return null;

        }
        


            
          
        Debug.Log("CombatState: Exit");
        NextState();
    }
 
    private IEnumerator GoalState()
    {

       // aiMovement.FindClosestWaypoint();

        Debug.Log("Hello Goal State");
        while (currentState == State.Goal)
        {
           
            aiMovement.AIMoveTowards(aiMovement.drop);

            if (Vector2.Distance(transform.position, aiMovement.drop.position) < 0.1f)
            {
                aiMovement.ResetDrop();
                currentState = State.Defence;
            }

            //if (!aiMovement.IsPlayerInRange())
            //{
            //    currentState = State.BerryPicking;
            //}

            yield return null;
        }

        
        NextState();
    }
  
    private IEnumerator BerryPickingState()
    {
        Debug.Log("BerryPicking: Enter");
        
        aiMovement.FindClosestWaypoint();
        
        while (currentState == State.BerryPicking)
        {
            aiMovement.WaypointUpdate();
            aiMovement.AIMoveTowards(aiMovement.position[aiMovement.positionIndex].transform);
            if (aiMovement.IsPlayerInRange())//what goes in here?
            {
                currentState = State.Attack;
            }

            if (AIMovement.berryPicked==5)
            {
                currentState = State.Goal;
            }
     

            
            yield return null;
        }
        Debug.Log("BerryPicking: Exit");
        NextState();
    } 
    
}
