using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPushBack : MonoBehaviour
{
    public int damage;
    public float pushForce;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Vector2 pushDir = other.transform.position - transform.position;
                playerHealth.TakeDamage(damage);
                TemportalPush tp = other.gameObject.AddComponent<TemportalPush>();
                tp.pushVector = pushDir.normalized * pushForce;
                //other.GetComponent<Rigidbody2D>().velocity = Vector2.up * upForce;
            }
        }
    }
}
