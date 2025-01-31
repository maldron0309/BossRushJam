using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBossAnimationEffects : MonoBehaviour
{
    public AudioClip explosionEffect;
    public void StepShake()
    {
        RoomCamera.instance.Shake(.35f, .3f);
    }
    public void PlayExplosion()
    {
        SoundEffectsManager.Instance.PlaySound(explosionEffect);
    }
}
