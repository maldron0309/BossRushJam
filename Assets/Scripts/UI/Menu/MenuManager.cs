using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Credits credits;
    [SerializeField] private Settings settings;

    [SerializeField] GameObject FadeOut;
    private void Start()
    {
        BackgroundMusicManager.Instance.PlayMenuTrack();
    }

    public void Play()
    {
        Instantiate(FadeOut, this.transform.parent.transform);
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
