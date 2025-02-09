using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBossShieldOrb : MonoBehaviour
{
    public AudioClip soundEffect;
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            SoundEffectsManager.Instance.PlaySound(soundEffect);
            Destroy(collision.gameObject);
        }
    }
}
