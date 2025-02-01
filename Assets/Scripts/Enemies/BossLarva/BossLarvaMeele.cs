using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLarvaMeele : MonoBehaviour
{
    public int damage;
    public float upForce;
    public AudioClip attackSound;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                TemportalPush tp = other.gameObject.AddComponent<TemportalPush>();
                tp.pushVector = Vector2.up * upForce;
                SoundEffectsManager.Instance.PlaySound(attackSound);
                //other.GetComponent<Rigidbody2D>().velocity = Vector2.up * upForce;
            }
        }
    }
}
