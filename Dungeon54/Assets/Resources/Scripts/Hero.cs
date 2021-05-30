using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hero : Character
// Inherit from Character
{
    public int money;

    private void Awake()
    {
        health = 3;
        attack = 1;
    }

    override protected void Start()
    {
        base.Start();
        handManager.CardRemovedFromHand.AddListener(TilePlaced);
    }

    void Update()
    {
        // Check if any movement inputs are detected
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // If so call the appropriate function in the parent Character class
            MoveAdjacent(direction.up);
            //Then, get the tile I'm on, and explore it. If it hasn't been explored, draw a card, and gain the explore benefit of the tile
            if(gridManager.GetTile(new Vector2(gridPosition.x, gridPosition.y)).GetComponent<TileScript>().Explore() == true)
            {
                handManager.DrawTile();
            }
            // Also, call the appropriate movement functions in all the enemies through an enemy manager
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveAdjacent(direction.right);

            if (gridManager.GetTile(new Vector2(gridPosition.x, gridPosition.y)).GetComponent<TileScript>().Explore() == true)
            {
                handManager.DrawTile();
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveAdjacent(direction.down);

            if (gridManager.GetTile(new Vector2(gridPosition.x, gridPosition.y)).GetComponent<TileScript>().Explore() == true)
            {
                handManager.DrawTile();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveAdjacent(direction.left);

            if (gridManager.GetTile(new Vector2(gridPosition.x, gridPosition.y)).GetComponent<TileScript>().Explore() == true)
            {
                handManager.DrawTile();
            }
        }

        // Check if that movement has caused me to share a space with an enemy or pickup
        // If enemy, deal damage to the enemy equal to my attack. If it's still alive, take damage equal to the enemy's attack
        // If pickup, do whatever the pickup does with an appropriate function in the pickup

        // Check if I'm dead
        // If so, present game over screen through the game manager

    }

    public void TilePlaced(GameObject tile)
    {
        int suit = tile.GetComponent<TileScript>().GetSuit();
        switch (suit)
        {
            case 0:
                attack += 1;
                break;

            case 1:
                money += 1;
                break;

            case 2:
                health += 1;
                break;
            default:
                break;
        }
    }
}