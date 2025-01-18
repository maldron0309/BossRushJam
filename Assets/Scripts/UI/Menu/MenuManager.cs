using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Credits credits;
    [SerializeField] private Settings settings;

    private void Start()
    {
        BackgroundMusicManager.Instance.PlayMenuTrack();
    }

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
