using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShooter : BaseAttack
{
    public GameObject regularProjectilePrefab; // Regular attack projectile prefab
    public Transform projectileSpawnPoint; // Where projectiles spawn
    public float attackCooldown = 0.25f; // Cooldown between regular attacks
    public float chargeTime = 1.0f; // Time required to charge an attack
    public float projectileSpeed = 10f;
    public float angleStep = 7.5f; // Angle difference between each projectile
    public int numProjectiles = 5; // Total number of projectiles to fire

    private float nextAttackCounter; // Time of the last attack
    private bool isCharging; // Whether the player is charging an attack
    private bool isChargeComplete; // Whether the charge attack is ready
    private float chargeStartTime; // When the charge started
    private bool isAttackBuffered; // Whether an attack input is buffered
    private PlayerController player;
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        if (nextAttackCounter > 0)
            nextAttackCounter -= Time.deltaTime;
        else
        {
            if (isAttackBuffered)
            {
                isAttackBuffered = false;
                FireProjectiles(regularProjectilePrefab);
            }
        }
    }
    private void FireProjectiles(GameObject projectilePrefab)
    {
        StartCoroutine(player.TemporaryStop(attackCooldown));

        float baseAngle = player.facingRight ? 0f : 180f; // Base angle based on facing direction

        for (int i = 0; i < numProjectiles; i++)
        {
            float angle = baseAngle + (-(numProjectiles - 1) / 2f + i) * angleStep;

            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        nextAttackCounter = attackCooldown;

        OnPressed();
    }
    override public void Fire()
    {
        if (nextAttackCounter <= 0)
        {
            Debug.Log("Attack action called!");
            FireProjectiles(regularProjectilePrefab);
        }
        else
            isAttackBuffered = true;
    }
}
