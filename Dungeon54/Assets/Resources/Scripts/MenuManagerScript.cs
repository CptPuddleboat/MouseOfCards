using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour
{
    public void ChangeScene(int NewScene)
    {
        SceneManager.LoadScene(NewScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
