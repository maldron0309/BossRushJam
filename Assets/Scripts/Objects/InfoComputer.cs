using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoComputer : MonoBehaviour, IInteractable
{
    [TextArea(4,10)]
    public string[] messages;
    public Transform markLocation;
    public AudioClip soundEffect;
    public Transform MarkLocation => markLocation;

    public void Interact(PlayerController player)
    {
        DialogScreen.instance.Open(messages);
        SoundEffectsManager.Instance.PlaySound(soundEffect);
    }
}
