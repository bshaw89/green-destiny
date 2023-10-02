using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void Start()
    {
        Screen.SetResolution(640, 480, true);
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // Comment
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
