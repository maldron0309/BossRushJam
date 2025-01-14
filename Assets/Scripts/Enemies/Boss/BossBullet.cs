using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifeTime = 5f; 
    [SerializeField] private float damage = 10f; 
    [SerializeField] public bool isLocking = true; 

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (isLocking)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

            if (player != null)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                rb.velocity = direction * speed;
            }

        }
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject); 
        }
    }
}