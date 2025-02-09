using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaRifle : BaseAttack
{
    public GameObject regularProjectilePrefab; // Regular attack projectile prefab
    public Transform projectileSpawnPoint; // Where projectiles spawn
    public float attackCooldown = 0.1f; // Cooldown between regular attacks
    public float projectileSpeed = 10f;
    public AudioClip attackSound;

    private float nextAttackCounter; // Time of the last attack
    private bool isAttackBuffered; // Whether an attack input is buffered
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
                FireProjectile(regularProjectilePrefab);
            }
        }
    }
    override public void OnRelease()
    {

    }
    private void FireProjectile(GameObject projectilePrefab)
    {
        if(currentCharges > 0 && canShoot)
        {
            //player.PreventMovementFortime(attackCooldown);

            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

            Vector2 direction = new Vector2(player.facingRight ? 1 : -1, 0);
            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed; // Adjust speed as needed

            nextAttackCounter = attackCooldown;
            currentCharges--;
            if (currentCharges <= 0)
                WeaponsInventory.instance.GiveNextWeapon(player);

            OnPressed();
            SoundEffectsManager.Instance.PlaySound(attackSound);
        }
    }

    override public void OnPressed()
    {

    }
    override public void Fire()
    {
        if (nextAttackCounter <= 0)
        {
            FireProjectile(regularProjectilePrefab);
        }
        else
            isAttackBuffered = true;
    }
}
