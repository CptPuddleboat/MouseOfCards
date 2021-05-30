using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is a parent class for all things that need to be moving around in the dungeon by its rules

public class Character : MonoBehaviour
{
    public int health;
    public int attack;
    public GridManager gridManager;
    public HandManagerScript handManager;
    public DeckManagerScript deckManager;
    protected Vector2 gridPosition;
    protected enum direction { up,right,down,left }
    protected GameObject location;

    protected virtual void Start()
    {
        // Set grid position
        gridPosition = new Vector2(55 + transform.position.x, 55 + transform.position.y);
        Debug.Log(gridPosition);

        location = transform.Find("Location").gameObject;
    }


    // Code functions for moving in a given direction. Should check if the target location contains a tile that is connected to the tile the character is on
    protected bool MoveAdjacent(direction Direction)
    {
        GameObject targetTile = null;

        switch(Direction)
        {
            case direction.up:
                targetTile = gridManager.GetTile(new Vector2(gridPosition.x, gridPosition.y + 1));
                Debug.Log("Move up");
                break;

            case direction.right:
                targetTile = gridManager.GetTile(new Vector2(gridPosition.x + 1, gridPosition.y));
                Debug.Log("Move right");
                break;

            case direction.down:
                targetTile = gridManager.GetTile(new Vector2(gridPosition.x, gridPosition.y - 1));
                Debug.Log("Move down");
                break;

            case direction.left:
                targetTile = gridManager.GetTile(new Vector2(gridPosition.x - 1, gridPosition.y));
                Debug.Log("move left");
                break;

            default:
                targetTile = null;
                Debug.Log("Invalid MoveAdjacent case");
                return false;
        }

        if(targetTile != null)
        {
            if (targetTile.GetComponent<TileScript>().IsConnected(gridManager.GetTile(new Vector2(gridPosition.x, gridPosition.y))))
            {
                gridPosition = targetTile.GetComponent<TileScript>().GetGridPosition();
                location.transform.position = targetTile.transform.position;
                Debug.Log("Moving to Tile");
                return true;
            }
            else
            {
                Debug.Log("targetTile is not connected");
                return false;
            }
        }
        else
        {
            Debug.Log("No tile in array at location");
            return false;
        }
    }

    protected bool MoveAny(Vector2 newGridPosition)
    {
        return true;
    }
}
