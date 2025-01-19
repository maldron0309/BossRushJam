using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShooter : BaseAttack
{
    public GameObject regularProjectilePrefab;
    public Transform projectileSpawnPoint;
    public float attackCooldown = 0.25f;
    public float chargeTime = 1.0f;
    public float projectileSpeed = 10f;
    public float angleStep = 7.5f;
    public int numProjectiles = 5;
    public int damage = 4;

    private float nextAttackCounter;
    private bool isAttackBuffered;
    private PlayerController player;
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        currentCharges = maxCharges;
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
        if (currentCharges > 0 && canShoot)
        {
            player.PreventMovementFortime(attackCooldown);

            float baseAngle = player.facingRight ? 0f : 180f;

            for (int i = 0; i < numProjectiles; i++)
            {
                float angle = baseAngle + (-(numProjectiles - 1) / 2f + i) * angleStep;

                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
                projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
                projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
                projectile.GetComponent<Projectile>().damage = damage;
            }
            nextAttackCounter = attackCooldown;

            currentCharges--;
            if (currentCharges <= 0)
                WeaponsInventory.instance.GiveNextWeapon(player);

            OnPressed();
        }
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
