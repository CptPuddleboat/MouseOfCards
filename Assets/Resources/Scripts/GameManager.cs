using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }

        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
