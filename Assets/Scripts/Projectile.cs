using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public GameObject hitEffect;
    public AudioClip hitClip;
    public float timeToLive = 5f;
    private float lifeCounter;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        lifeCounter = timeToLive; 
    }
    private void FixedUpdate()
    {
        if(lifeCounter > 0)
        {
            lifeCounter -= Time.fixedDeltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss") && collision.gameObject.GetComponent<BaseBossController>().isBattleStarted)
        {
            BossHealth bossHealth = collision.gameObject.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
            }
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            if (hitClip)
                SoundEffectsManager.Instance.PlaySound(hitClip);
            Destroy(gameObject);
        }
        else
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            if (hitClip)
                SoundEffectsManager.Instance.PlaySound(hitClip);
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
