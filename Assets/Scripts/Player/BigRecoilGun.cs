using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRecoilGun : BaseAttack
{
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed;
    public AudioClip attackSound;
    public int damage = 14;
    public float recoilForce;
    private float nextAttackCounter;
    private bool isAttackBuffered;
    public float attackCooldown = 0.25f;
    void Start()
    {
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
                FireProjectiles(projectilePrefab);
            }
        }
    }
    private void FireProjectiles(GameObject projectilePrefab)
    {
        if (currentCharges > 0 && canShoot)
        {
            PlayerController.instance.PreventMovementFortime(attackCooldown);

            float baseAngle = PlayerController.instance.facingRight ? 0f : 180f;

            float angle = baseAngle;

            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
            projectile.GetComponent<Projectile>().damage = damage;
            projectile.GetComponent<Projectile>().timeToLive = 2.5f;


            TemportalPush tp = PlayerController.instance.gameObject.AddComponent<TemportalPush>();
            tp.pushVector = (Vector2.up -direction)  * recoilForce;

            nextAttackCounter = attackCooldown;

            currentCharges--;
            if (currentCharges <= 0)
                WeaponsInventory.instance.GiveNextWeapon(PlayerController.instance);

            SoundEffectsManager.Instance.PlaySound(attackSound);
            OnPressed();
        }
    }
    override public void OnRelease()
    {

    }
    override public void Fire()
    {
        if (nextAttackCounter <= 0)
        {
            FireProjectiles(projectilePrefab);
        }
        else
            isAttackBuffered = true;
    }
}
