using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPointScript : MonoBehaviour
{
    protected bool attachDetected;
    // Make a protected variable to hold the outcome of a detected collision

    void Start()
    {
        
    }

    void Update()
    {
        // Check if I'm colliding with another attach point
            // If so, set the attachCollision variable to the outcome of the collision, so that the parent Tile can access the collision, and set attachDetected to true
            // If not, set attachDetected to false
    }
}
