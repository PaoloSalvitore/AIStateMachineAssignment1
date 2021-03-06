using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class StateMachine : MonoBehaviour
{
    //comma separated list of identifiers
    public enum State
    {
        //Three different
        Attack,
        Defence,
      //  RunAway,
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
                StartCoroutine(DefenceState());
                break;
           /* case State.RunAway:
                StartCoroutine(RunAwayState());
                break;
            */
            case State.BerryPicking:
                StartCoroutine(BerryPickingState());
                break;
        }
    }
    private IEnumerator AttackState()
    {
        Debug.Log("Attack: Enter");
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
    
    private IEnumerator DefenceState()
    {
        Debug.Log("Defence: Enter");
        //runs every frame
        //sounds like the update
        
        float timeOfLastSpawn = Time.time; 
        while (currentState == State.Defence)
        {
            Debug.Log("Currently Defending");
            
           
                
            if ( timeOfLastSpawn + 3f  < Time.time)
            {
                //spawn new waypoint
                timeOfLastSpawn = Time.time;
            }
            
            yield return null;
            Debug.Log("next frame Currently Defending");
        }
        Debug.Log("Defence: Exit");
        NextState();
    }
    
   /* private IEnumerator RunAwayState()
    {
        Debug.Log("RunAway: Enter");
        while (currentState == State.RunAway)
        {
            Debug.Log("Currently Running Away");
            
            yield return null;
        }
        Debug.Log("RunAway: Exit");
        NextState();
    }
   */ 
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

     

            
            yield return null;
        }
        Debug.Log("BerryPicking: Exit");
        NextState();
    } 
    
}
