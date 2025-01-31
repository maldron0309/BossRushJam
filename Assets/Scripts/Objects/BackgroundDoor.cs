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
    public bool isOpen = true;
    public int mainHallPosition = 0;
    public SpriteRenderer doorSprite;
    public AudioClip soundEffect;

    public Transform MarkLocation => markLocation;

    void Start()
    {
        if(isOpen)
            anim.Play("Opening");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Close()
    {
        anim.Play("Close");
        doorSprite.sortingOrder = 1;
        isOpen = false;
    }
    public void Interact(PlayerController player)
    {
        if (isOpen)
        {
            anim.Play("Closing");
            player.enabled = false;
            if (mainHallPosition > 0)
                GameProgressManager.instance.spawnPosition = mainHallPosition;
            StartCoroutine(LoadLevel());
            SoundEffectsManager.Instance.PlaySound(soundEffect);
        }
    }
    private IEnumerator LoadLevel()
    {
        BlackScreen.instance.Hide();
        yield return new WaitForSeconds(.75f);
        SceneManager.LoadScene(sceneName);
    }
}
