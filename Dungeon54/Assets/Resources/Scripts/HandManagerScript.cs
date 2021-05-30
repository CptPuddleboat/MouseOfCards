using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class CardPlaceEvent : UnityEvent<GameObject>
{
}

public class HandManagerScript : MonoBehaviour
{
    public GameObject TileButtonBase;
    private GameObject newTileButton;

    private GameObject[] handArray = new GameObject[10];
    private int handTop = 0;
    private DeckManagerScript deckManager;

    public CardPlaceEvent CardRemovedFromHand;

    void Awake()
    {
        deckManager = GetComponent<DeckManagerScript>();

        if (CardRemovedFromHand == null)
            CardRemovedFromHand = new CardPlaceEvent();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RefreshHandUI()
    {
        foreach (Transform child in transform.Find("Canvas/TilesHand").transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < 10; i++)
        {
            if(handArray[i] != null)
            {
                newTileButton = Instantiate(TileButtonBase);
                newTileButton.GetComponent<HandTileScript>().SetActualTile(handArray[i]);
                newTileButton.transform.SetParent(transform.Find("Canvas/TilesHand"),false);

                newTileButton.transform.Find("Path").GetComponent<Image>().sprite = handArray[i].transform.Find("Path").GetComponent<SpriteRenderer>().sprite;
                newTileButton.transform.Find("Suit").GetComponent<Image>().sprite = handArray[i].transform.Find("Suit").GetComponent<SpriteRenderer>().sprite;
            }
        }
    }
    
    public void RemoveFromHand(GameObject tileToRemove)
    {
        int i = 0;
        bool tileRemoved = false;
        foreach (GameObject tile in handArray)
        {
            if(tile == tileToRemove)
            {
                handArray[i] = null;
                tileRemoved = true;
                CardRemovedFromHand.Invoke(tileToRemove);
            }
            if(tileRemoved == true)
            {
                if(i < 9)
                {
                    handArray[i] = handArray[i + 1];
                }
                else
                {
                    handArray[i] = null;
                }
            }
            i += 1;
        }
        handTop -= 1;
    }

    public void DrawTile()
    {
        handArray[handTop] = deckManager.DrawDeckTop();
        handTop += 1;
        RefreshHandUI();
    }
}
