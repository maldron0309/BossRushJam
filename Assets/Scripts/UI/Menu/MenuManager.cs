using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Credits credits;
    [SerializeField] private Settings settings;
    public void Play()
    {
        SceneManager.LoadScene("Boss 1");
    }

    public void Setting()
    {
        settings.OpenSettings();
    }

    public void Credit()
    {
        credits.OpenCredits();
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
