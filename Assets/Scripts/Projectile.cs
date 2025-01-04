using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            BossHealth bossHealth = collision.gameObject.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
                Debug.Log($"Hit boss for {damage} damage!");
            }

            // Destroy the projectile
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}