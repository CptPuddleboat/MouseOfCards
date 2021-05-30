using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public bool addToArrayOnAwake = false;

    private GridManager gridManager;
    private Vector2 gridPosition;
    private GameObject[] attachedTiles = new GameObject[4];
    private int suit;
    private int value;
    private SpriteRenderer suitSprite;
    private SpriteRenderer pathSprite;
    private GameObject[] attachPointArray = new GameObject[4];

    void Awake()
    {
        // Initialise variables
        suitSprite = transform.Find("Suit").GetComponent<SpriteRenderer>();
        pathSprite = transform.Find("Path").GetComponent<SpriteRenderer>();
        // Also, add all the attachpoints to the attach point array
        for (int i = 0; i < 4; i++)
        {
            attachPointArray[i] = transform.Find("AttachPoints").GetChild(i).gameObject;
        }
        // Find the Grid Manager in the scene
        gridManager = FindObjectOfType<GridManager>();
        // Once that's done, if I should be added to the array on awake, do it
        if (addToArrayOnAwake == true)
        {
            gridManager.AddToArrayFromPosition(this.gameObject, this.transform.position, true);
            gridPosition = new Vector2(55 + this.transform.position.x, 55 + this.transform.position.y);
        }
    }

    void Update()
    {

    }

    public bool AttemptConnect(bool update)
    {
        CircleCollider2D[] collisionArray = new CircleCollider2D[1];
        ContactFilter2D filter = new ContactFilter2D();
        filter.NoFilter();

        // Go through the children (being the attach points), see what attach points those points are colliding with, and add the parents of those points (the tile) to the connectedTiles array. Also, add this tile to the other tile's connected tiles array
        for(int i = 0; i < 4; i++)
        {
            collisionArray[0] = null;
            Debug.Log(transform.Find("AttachPoints").GetChild(i).name);
            CircleCollider2D attachPointCollider = attachPointArray[i].GetComponent<CircleCollider2D>();
            attachPointCollider.OverlapCollider(filter, collisionArray);
            if(collisionArray[0] != null)
            {
                attachedTiles[i] = collisionArray[0].transform.parent.parent.gameObject;
                if(update == false)
                {
                    Debug.Log(attachedTiles[i].name + "is updating");
                    attachedTiles[i].GetComponent<TileScript>().AttemptConnect(true);
                }
            }
            else
            {
                attachedTiles[i] = null;
            }
        }

        // Check if the array is empty
        bool empty = true;
        for(int i = 0; i < 4; i++)
        {
            if (attachedTiles[i] != null)
            {
                empty = false;
                break;
            }
        }
        if(empty == false)
        {
            gridManager.AddToArrayFromPosition(this.gameObject, new Vector2(this.transform.position.x, this.transform.position.y), false);
            gridPosition = new Vector2(55 + this.transform.position.x, 55 + this.transform.position.y);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsConnected(GameObject tile)
    {
        for(int i = 0; i < 4; i++)
        {
            if(attachedTiles[i] == tile)
            {
                return true;
            }
        }
        return false;
    }

    public Vector2 GetGridPosition()
    {
        return gridPosition;
    }

    public bool SetSuitAndValue(int newSuit, int newValue)
    {
        suit = newSuit;
        value = newValue;

        switch(suit)
        {
            case 0:
                suitSprite.sprite = Resources.Load<Sprite>("Sprites/Club");
                break;
            case 1:
                suitSprite.sprite = Resources.Load<Sprite>("Sprites/Diamond");
                break;
            case 2:
                suitSprite.sprite = Resources.Load<Sprite>("Sprites/Heart");
                break;
            case 3:
                suitSprite.sprite = Resources.Load<Sprite>("Sprites/Spade");
                break;
            case 4:
                suitSprite.sprite = Resources.Load<Sprite>("Sprites/Joker1");
                pathSprite.sprite = Resources.Load<Sprite>("Sprites/Dead");
                break;
            case 5:
                suitSprite.sprite = Resources.Load<Sprite>("Sprites/Joker2");
                pathSprite.sprite = Resources.Load<Sprite>("Sprites/Dead");
                break;
            default:
                Debug.LogError("Invalid Suit");
                return false;
        }

        switch(value)
        {
            case 0:
                pathSprite.sprite = Resources.Load<Sprite>("Sprites/Dead");
                attachPointArray[2].GetComponent<CircleCollider2D>().enabled = true;
                break;
            case 1:
            case 6:
                pathSprite.sprite = Resources.Load<Sprite>("Sprites/Straight");
                attachPointArray[0].GetComponent<CircleCollider2D>().enabled = true;
                attachPointArray[2].GetComponent<CircleCollider2D>().enabled = true;
                break;
            case 2:
            case 3:
            case 7:
            case 8:
                pathSprite.sprite = Resources.Load<Sprite>("Sprites/Corner");
                attachPointArray[1].GetComponent<CircleCollider2D>().enabled = true;
                attachPointArray[2].GetComponent<CircleCollider2D>().enabled = true;
                break;
            case 4:
            case 9:
                pathSprite.sprite = Resources.Load<Sprite>("Sprites/Tee");
                attachPointArray[1].GetComponent<CircleCollider2D>().enabled = true;
                attachPointArray[2].GetComponent<CircleCollider2D>().enabled = true;
                attachPointArray[3].GetComponent<CircleCollider2D>().enabled = true;
                break;
            case 5:
            case 10:
                pathSprite.sprite = Resources.Load<Sprite>("Sprites/Cross");
                attachPointArray[0].GetComponent<CircleCollider2D>().enabled = true;
                attachPointArray[1].GetComponent<CircleCollider2D>().enabled = true;
                attachPointArray[2].GetComponent<CircleCollider2D>().enabled = true;
                attachPointArray[3].GetComponent<CircleCollider2D>().enabled = true;
                break;
            case 11:
            case 12:
            case 13:
                pathSprite.sprite = Resources.Load<Sprite>("Sprites/Dead");
                attachPointArray[2].GetComponent<CircleCollider2D>().enabled = true;
                break;
            default:
                Debug.LogError("Invalid Value");
                return false;
        }
        return true;
    }
}
