using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandTileScript : MonoBehaviour
{
    public GameObject player;
    private GameObject actualTile;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => SendClickedTile());
        player = GameObject.Find("Hero");
    }

    public void SendClickedTile()
    {
        Debug.Log(actualTile.name + " selected.");
        player.GetComponent<PlayerController>().SetSelectedTile(actualTile);
        player.GetComponent<PlayerController>().SetTileMode(true);
    }

    public void SetActualTile(GameObject tile)
    {
        actualTile = tile;
    }
}
