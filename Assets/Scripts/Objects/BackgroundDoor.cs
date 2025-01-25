using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IInteractable
{
    Transform MarkLocation { get; }
    void Interact(PlayerController player);
}
public class BackgroundDoor : MonoBehaviour, IInteractable
{
    public string sceneName;
    public Animator anim;
    public Transform markLocation;

    public Transform MarkLocation => markLocation;

    void Start()
    {
        anim.Play("Opening");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact(PlayerController player)
    {
        anim.Play("Closing");
        player.enabled = false;
        StartCoroutine(LoadLevel());
    }
    private IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(sceneName);
    }
}
