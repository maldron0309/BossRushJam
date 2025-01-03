using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Logic for hitting enemies
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Deal damage to the enemy (you'll need an enemy health script)
            Debug.Log($"Hit enemy for {damage} damage!");
        }

        // Destroy the projectile
        Destroy(gameObject);
    }
}
