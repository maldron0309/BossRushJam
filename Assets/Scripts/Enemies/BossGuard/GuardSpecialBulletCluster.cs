using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSpecialBulletCluster : MonoBehaviour
{
    public Transform centerPoint;
    public float rotationSpeed = 180f;
    public float outwardSpeed = 2f;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private float damage = 10f;
    private Vector2 currentOffset;

    void Start()
    {

        if (centerPoint == null)
        {
            Debug.LogError("Center point is not assigned for SpinningProjectile.");
            Destroy(gameObject);
            return;
        }

        currentOffset = transform.position - centerPoint.position;
        Destroy(gameObject, lifeTime);
    }
    void Update()
    {
        if (centerPoint == null)
            return;

        float angle = rotationSpeed * Time.deltaTime; // Rotate based on speed and deltaTime
        currentOffset = RotateVector(currentOffset, angle);

        currentOffset += currentOffset.normalized * outwardSpeed * Time.deltaTime;

        transform.position = (Vector2)centerPoint.position + currentOffset;
    }
    private Vector2 RotateVector(Vector2 vector, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        return new Vector2(
            cos * vector.x - sin * vector.y,
            sin * vector.x + cos * vector.y
        );
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Bullet hit the player!");
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
