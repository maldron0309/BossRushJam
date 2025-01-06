using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

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
    public void ApplyForce(Vector2 force)
    {
        if (rb != null)
        {
            rb.velocity += force;
        }
    }
}

