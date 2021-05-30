using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private GameObject[,] tileArray = new GameObject[109, 109];
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddToArrayFromPosition(GameObject tile, Vector2 tilePos, bool nearTileOverride)
    {
        int arrayX = 55 + (int)tilePos.x;
        int arrayY = 55 + (int)tilePos.y;

        // If a tile is not in that space
        if(tileArray[arrayX,arrayY] == null)
        {
            tileArray[arrayX, arrayY] = tile;
            Debug.Log(arrayX + "," + arrayY + " placement success", gameObject);
            return true;
        }
        else
        {
            return false;
        }
    }

    public GameObject GetTile(Vector2 tilePos)
    {
        return tileArray[(int)tilePos.x, (int)tilePos.y];
    }
}
