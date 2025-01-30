using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Credits credits;
    [SerializeField] private Settings settings;

    [SerializeField] GameObject FadeOut;
    private void Start()
    {
        // didn't play music in web build. might need delayed start?
        StartCoroutine(DelayedPlay());
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
    private IEnumerator DelayedPlay()
    {
        yield return new WaitForSeconds(1);
        //BackgroundMusicManager.Instance.PlayMenuTrack();
        Debug.Log($" music volume ={BackgroundMusicManager.Instance.adSrc.volume}");
    }
}
