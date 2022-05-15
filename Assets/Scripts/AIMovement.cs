using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIMovement : MonoBehaviour
{
    public Transform player; //to find the location of player
    public float chaseDistance = 3; //how far the player needs to be for the AI to start a chase

    public Transform square;
    public Transform drop;

    //public GameObject[] position;
    public List<GameObject> position; //The waypoint the AI is heading to
    public int positionIndex = 0; //Controls which waypoint the AI should be chasing
    public GameObject wayPointPrefab; //To be included with other waypoints
    public int pickedBerry;
    public static int berryPicked;
        
    public float speed = 1.5f;  //Speed of the AI
    public float minGoalDistance = 0.05f;  //How close the Ai needs to be with the 

    /*private void Update()
    {
        //are we within the player chase distance?
        if (Vector2.Distance(transform.position, player.position) < chaseDistance)
        {
            //move towards player
            AIMoveTowards(player);
        }
        else
        {
            WaypointUpdate();
            //move towards our waypoints
            AIMoveTowards(position[positionIndex].transform);
        }
    }*/

    private void Start() //Ran at the first start of the game
    {
        NewWayPoint();
        NewWayPoint();
        NewWayPoint();
        NewWayPoint();
        berryPicked = 0;


       // RemoveCurrentWayPoint();
      //  RemoveCurrentWayPoint();
    }

    public void RemoveCurrentWayPoint()
    {
        //Two methods of removing current waypoint

        /* method 1 give list.remove a gameobject to remove from the list
         GameObject current = position[positionIndex];
         position.Remove( current);                     
         Destroy(current);

         //method 2 give list.removeat an index to remove from the list*/
        GameObject current = position[positionIndex];
        position.RemoveAt(positionIndex); //Move on to the next waypoint
        Debug.Log("Destroy this please");
        Destroy(current); //Destroy the current waypoint

        NewWayPoint(); //Generate a new waypoint


    }

    public void NewWayPoint() //Make a new waypoint
    {

        //Generate a random waypoint in the bellow range (X is left to right and Y is up and down)
        float x = Random.Range(-6f, 6f); 
        float y = Random.Range(-4f, 4f);
        
        GameObject newPoint = Instantiate(wayPointPrefab, new Vector2(x,y), Quaternion.identity);
        
        position.Add(newPoint);
        

    }

    public void FindClosestWaypoint() //To pinpoint the closest waypoint for the AI
    {
        float nearest = float.PositiveInfinity;
        int nearestIndex = 0;

        for (int i = 0; i < position.Count; i++)
        {
            float distance = Vector2.Distance(transform.position, position[i].transform.position);
            if (distance < nearest) //checks the waypoints one by one to see which waypoint is closests
            {
                //if this is the current nearest waypoint, set it at as the nearest distance  

                nearest = distance; 
                nearestIndex = i;
            }
        }

        positionIndex = nearestIndex;
    }
    

    public bool IsPlayerInRange() //is the bool to check if the player is within the chase distance
    {
        return Vector2.Distance(transform.position, player.position) < chaseDistance;
    }
    

    public void WaypointUpdate() // Updates the AI to move to the next position if the AI is within the mingoaldistance of the waypoint
    {
        if (Vector2.Distance(transform.position, position[positionIndex].transform.position) < minGoalDistance)
        {
            //  RemoveCurrentWayPoint();
            Debug.Log("Way point has been updated");
            positionIndex++;
            pickedBerry++;
            berryPicked = pickedBerry;

            if (positionIndex >= position.Count)
            {
                positionIndex = 0;
            }
        }
    }
    
    public void AIMoveTowards(Transform goal)
    {
        //if we the AI is not in the mingoaldistance then move towards the current waypoint,
        if (Vector2.Distance(transform.position, goal.position) > minGoalDistance)
        {
            //direction from A to B
            // is B - A
            // X = B - A
            Vector2 directionToGoal = (goal.position - transform.position);
            directionToGoal.Normalize();
            transform.position += (Vector3) directionToGoal * speed * Time.deltaTime;
        }
    }

  public void ResetDrop()
    {
        pickedBerry = 0;
        berryPicked = pickedBerry;
    }




}