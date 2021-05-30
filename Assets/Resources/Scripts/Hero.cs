using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
// Inherit from Character
{
    void Start()
    {

    }

    void Update()
    {
        // Check if any movement inputs are detected
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // If so call the appropriate function in the parent Character class
            MoveAdjacent(direction.up);
            // Also, call the appropriate movement functions in all the enemies through an enemy manager
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveAdjacent(direction.right);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveAdjacent(direction.down);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveAdjacent(direction.left);
        }

        // Check if that movement has caused me to share a space with an enemy or pickup
        // If enemy, deal damage to the enemy equal to my attack. If it's still alive, take damage equal to the enemy's attack
        // If pickup, do whatever the pickup does with an appropriate function in the pickup

        // Check if I'm dead
        // If so, present game over screen through the game manager

    }
}