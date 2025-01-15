using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Boss 1");
    }

    public void Setting()
    {
        SceneManager.LoadScene("Setting");
    }

    public void Credit()
    {
        SceneManager.LoadScene("Credits");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
