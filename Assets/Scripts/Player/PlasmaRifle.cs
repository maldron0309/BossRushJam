using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaRifle : BaseAttack
{
    public GameObject regularProjectilePrefab; // Regular attack projectile prefab
    public GameObject chargedProjectilePrefab; // Charged attack projectile prefab
    public Transform projectileSpawnPoint; // Where projectiles spawn
    public float attackCooldown = 0.1f; // Cooldown between regular attacks
    public float chargeTime = 1.0f; // Time required to charge an attack
    public float projectileSpeed = 10f;

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
        if (isCharging && !isChargeComplete && Time.time - chargeStartTime >= chargeTime)
        {
            isChargeComplete = true;
            Debug.Log("Charge attack ready!");
        }
        if (nextAttackCounter > 0)
            nextAttackCounter -= Time.deltaTime;
        else
        {
            if (isAttackBuffered)
            {
                isAttackBuffered = false;
                FireProjectile(regularProjectilePrefab);
            }
        }
    }
    override public void OnRelease()
    {
        if (isCharging)
        {
            isCharging = false;

            // Check if the charge was completed
            if (isChargeComplete)
            {
                FireProjectile(chargedProjectilePrefab);
                isChargeComplete = false;
            }
        }
    }
    private void FireProjectile(GameObject projectilePrefab)
    {
        // Stop movement briefly
        StartCoroutine(player.TemporaryStop(attackCooldown));

        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        // Set its direction based on the player's facing direction
        Vector2 direction = new Vector2(player.facingRight ? 1 : -1, 0);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed; // Adjust speed as needed

        // Record the time of this attack
        nextAttackCounter = attackCooldown;

        OnPressed();
    }

    override public void OnPressed()
    {
        isCharging = true;
        chargeStartTime = Time.time;
        isChargeComplete = false;
    }
    override public void Fire()
    {
        if (nextAttackCounter <= 0)
        {
            Debug.Log("Attack action called!");
            FireProjectile(regularProjectilePrefab);
        }
        else
            isAttackBuffered = true;
    }
}
