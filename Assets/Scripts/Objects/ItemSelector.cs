using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelector : MonoBehaviour, IInteractable
{
    public Transform markLocation;
    public Transform MarkLocation => markLocation;
    public AudioClip soundEffect;

    public void Interact(PlayerController player)
    {
        WeaponSelectController.instance.gameObject.SetActive(true);
        player.Stop();
        SoundEffectsManager.Instance.PlaySound(soundEffect);    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
