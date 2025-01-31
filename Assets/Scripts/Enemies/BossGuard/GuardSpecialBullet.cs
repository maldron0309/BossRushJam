using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSpecialBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float damage = 10f;
    public Transform destenation;
    public float detonationTime;
    public GameObject clusterPrefab;
    public int numberOfProjectiles = 8;
    public float initialRadius = 1f;
    public AudioClip soundEffect;
    public AudioClip spawnSound;

    private float detomationCounter;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector3 direction = (destenation.position - transform.position).normalized;
        rb.velocity = direction * speed;

        detomationCounter = detonationTime;
        SoundEffectsManager.Instance.PlaySound(spawnSound);
    }
    private void Update()
    {
        if (detomationCounter > 0)
        {
            detomationCounter -= Time.deltaTime;
        }
        else
        {
            Detonate();
        }
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
        }
    }
    public void Detonate()
    {
        destenation.position = transform.position;

        float angleStep = 360f / numberOfProjectiles;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = i * angleStep;
            Vector2 offset = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad) * initialRadius,
                Mathf.Sin(angle * Mathf.Deg2Rad) * initialRadius
            );

            GameObject projectile = Instantiate(clusterPrefab, destenation.position + (Vector3)offset, Quaternion.identity);

            // Assign the center point and initial offset to the projectile
            GuardSpecialBulletCluster spinningProjectile = projectile.GetComponent<GuardSpecialBulletCluster>();
            if (spinningProjectile != null)
            {
                spinningProjectile.centerPoint = destenation;
                spinningProjectile.transform.position = (Vector2)destenation.position + offset;
            }
        }
        SoundEffectsManager.Instance.PlaySound(soundEffect);

        Destroy(gameObject);
    }
}
