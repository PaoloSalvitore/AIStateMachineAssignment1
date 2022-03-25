using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f; //Players speed

    private void Update()
    {
        Movement(); //on every update run Movement()
    }

    private void Movement()
    {
        Vector2 moveDirection = Vector2.zero; //

        if (Input.GetKey(KeyCode.W)) //If w is pressed move up
        {
            moveDirection.y++;
        }
        if (Input.GetKey(KeyCode.S)) //If S is pressed move down
        {
            moveDirection.y--; 
        }
        if (Input.GetKey(KeyCode.D)) //if D is pressed move right
        {
            moveDirection.x++;
        }
        if (Input.GetKey(KeyCode.A)) //If A is pressed move left
        {
            moveDirection.x--;
        }
        moveDirection.Normalize();
        moveDirection *= (speed * Time.deltaTime); //sets how fast the player is moving, Delta time is useds as to not create inconsistencies with Frame rate speeds
        
        transform.position += (Vector3) moveDirection; //Controls the actual player movement (This movement is kind of like teleporting)
    }
}
