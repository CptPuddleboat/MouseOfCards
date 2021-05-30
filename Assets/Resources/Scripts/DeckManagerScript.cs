using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManagerScript : MonoBehaviour
{
    private GameObject[] deckArray = new GameObject[54];
    private int deckTop = 53;
    public GameObject tileTemplate;

    // Start is called before the first frame update
    void Start()
    {
        // First, populate the array and instantiate the tiles in the deck
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 13; j++)
            {
                deckArray[(i * 13) + j] = Instantiate(tileTemplate);
                deckArray[(i * 13) + j].GetComponent<TileScript>().SetSuitAndValue(i, j + 1);
                deckArray[(i * 13) + j].name = (i + 1) + "," + (j + 1);
                deckArray[(i * 13) + j].SetActive(false);
            }
        }
        // Add the jokers
        deckArray[52] = Instantiate(tileTemplate);
        deckArray[52].GetComponent<TileScript>().SetSuitAndValue(4, 0);
        deckArray[52].name = "Joker1";
        deckArray[52].SetActive(false);
        deckArray[53] = Instantiate(tileTemplate);
        deckArray[53].GetComponent<TileScript>().SetSuitAndValue(5, 0);
        deckArray[53].name = "Joker2";
        deckArray[53].SetActive(false);
        // Then, randomise the array
        int deckSize = deckArray.Length;
        while (deckSize > 1)
        {
            deckSize--;
            int i = Random.Range(0, deckSize + 1); ;
            GameObject temp = deckArray[i];
            deckArray[i] = deckArray[deckSize];
            deckArray[deckSize] = temp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetDeckTop()
    {
        return deckTop;
    }

    public GameObject DrawDeckTop()
    {
        GameObject drawnCard = deckArray[deckTop];
        deckArray[deckTop] = null;
        deckTop -= 1;
        return drawnCard;
    }
}
