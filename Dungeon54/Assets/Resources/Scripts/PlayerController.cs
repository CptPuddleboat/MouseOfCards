using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool placeTileMode;
    private GridManager gridManager;
    private HandManagerScript handManager;
    private GameObject selectedTile;
    public Camera cam;

    private GameObject tileCursor;
    private SpriteRenderer[] spriteRenderers;

    void Awake()
    {
        // Find the Grid Manager in the scene
        gridManager = FindObjectOfType<GridManager>();
        // Find the hand manager script on this object
        handManager = GetComponent<HandManagerScript>();
    }

    void Update()
    {
        if(placeTileMode == true)
        {
            // Check where the cursor is
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            // Get a grid location from this position
            mousePos.x = Mathf.Round((mousePos.x));
            mousePos.y = Mathf.Round((mousePos.y));

            // Spawn a cursor version of the tile if there isn't one and add its sprite renderers to the array
            if (tileCursor == null)
            {
                UpdateTile(new Vector2(mousePos.x, mousePos.y));
            }
            // Otherwise, move the existing tile to the mousePos
            else
            {
                tileCursor.transform.position = mousePos;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                RotateTile(false);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                RotateTile(true);
            }

            if (Input.GetMouseButtonDown(0))
            {
                // Check the grid array. If there is no tile there, allow placement, otherwise, don't
                if (gridManager.GetTile(new Vector2(55 + mousePos.x, 55 + mousePos.y)) == null)
                {
                    // Create a new tile exactly the same as the cursor, changing the alpha and colour
                    GameObject newTile = Instantiate(tileCursor, mousePos, tileCursor.transform.rotation);
                    Color newTileAlpha = new Color(.5f, .5f, .5f, 1f);
                    spriteRenderers = newTile.GetComponentsInChildren<SpriteRenderer>();
                    foreach (SpriteRenderer renderer in spriteRenderers)
                    {
                        renderer.color = newTileAlpha;
                    }

                    Debug.Log("Place tile at : " + mousePos);

                    // Deactivate the cursor temporarily so it doesn't interfere with collision detection
                    tileCursor.SetActive(false);

                    // Attempt to connect the new tile
                    if (newTile.GetComponent<TileScript>().AttemptConnect(false) == true)
                    {
                        Debug.Log("Connect success!");
                        SetTileMode(false);
                        GameObject.Find("Deck&HandManager").GetComponent<HandManagerScript>().RemoveFromHand(selectedTile);
                        GameObject.Find("Deck&HandManager").GetComponent<HandManagerScript>().RefreshHandUI();
                        selectedTile = null;
                    }
                    else
                    {
                        Destroy(newTile);
                        Debug.Log("Connect failed!");
                        tileCursor.SetActive(true);
                    }
                }
                else
                {
                    Debug.Log("Tile is already there!");
                    tileCursor.SetActive(true);
                }
            }
        }
    }
    private void RotateTile(bool clockwiseRotate)
    {
        if (clockwiseRotate == true)
        {
            tileCursor.transform.Find("AttachPoints").Rotate(Vector3.forward * -90);
            tileCursor.transform.Find("Path").Rotate(Vector3.forward * -90);
            tileCursor.transform.GetChild(tileCursor.transform.childCount - 1).SetAsFirstSibling();
            
        }
        else
        {
            tileCursor.transform.Find("AttachPoints").Rotate(Vector3.forward * 90);
            tileCursor.transform.Find("Path").Rotate(Vector3.forward * 90);
            tileCursor.transform.GetChild(0).SetAsLastSibling();
        }
    }

    private void UpdateTile(Vector2 mousePos)
    {
        if (tileCursor != null)
        {
            Destroy(tileCursor);
            tileCursor = null;
        }

        tileCursor = Instantiate(selectedTile, mousePos, Quaternion.identity);
        tileCursor.SetActive(true);
        spriteRenderers = tileCursor.GetComponentsInChildren<SpriteRenderer>();
        // Make the cursor tile translucent
        Color tileCursorAlpha = new Color(255, 255, 255, 0.5f);
        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            renderer.color = tileCursorAlpha;
        }
    }
    
    public void SetSelectedTile(GameObject newTile)
    {
        selectedTile = newTile;
    }

    public void SetTileMode(bool newTileMode)
    {
        placeTileMode = newTileMode;
        if (tileCursor != null)
        {
            Destroy(tileCursor);
            tileCursor = null;
        }
        Debug.Log("Tile mode : " + placeTileMode);
    }
}