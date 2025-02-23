using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewItemSelector : MonoBehaviour, IInteractable
{
    public Transform markLocation;
    public Transform MarkLocation => markLocation;
    public AudioClip soundEffect;
    public Checkpoint checkpoint;

    public void Interact(PlayerController player)
    {
        if (checkpoint)
        {
            checkpoint.Save();
        }
        NewWeaponSelectController.instance.Open();
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
