using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeGun : BaseAttack
{
    public GameObject weakProjectile;
    public GameObject strongProjectile;
    public Transform projectileSpawnPoint;
    public Animator anim;
    public float projectileSpeed;
    public AudioClip attackSoundWeak;
    public AudioClip attackSoundStrong;
    public AudioSource chargeSound;
    public int weakDamage = 14;
    public int strongDamage = 14;
    public float recoilForce;
    private float nextAttackCounter;
    private bool isAttackBuffered;
    public float attackCooldown = 0.25f;
    public float chargeTime;
    private float chargeCounter;
    private bool isCharging; // Whether the player is charging an attack
    private bool isChargeComplete; // Whether the charge attack is ready
    void Start()
    {
        currentCharges = maxCharges;
        anim.gameObject.SetActive(false);
    }
    void Update()
    {
        if (isCharging && !isChargeComplete)
        {
            if (chargeCounter > 0)
                chargeCounter -= Time.deltaTime;
            else
            {
                isChargeComplete = true;
                anim.Play("ChargeBig");
            }
        }
    }
    override public void OnRelease()
    {
        if (isCharging)
        {
            chargeSound.Stop();
            isCharging = false;
            if (isChargeComplete)
            {
                FireProjectile(strongProjectile, strongDamage, recoilForce, attackSoundStrong);
                isChargeComplete = false;
            }
            else
            {
                FireProjectile(weakProjectile, weakDamage, recoilForce * 0.5f, attackSoundWeak);
            }
        }
    }
    private void FireProjectile(GameObject projectilePrefab, int damage, float recoil, AudioClip sound)
    {
        if (currentCharges > 0 && canShoot)
        {
            PlayerController.instance.PreventMovementFortime(attackCooldown);

            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

            Vector2 direction = new Vector2(PlayerController.instance.facingRight ? 1 : -1, 0);
            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            projectile.GetComponent<Projectile>().damage = damage;

            nextAttackCounter = attackCooldown;
            currentCharges--;
            if (currentCharges <= 0)
                WeaponsInventory.instance.GiveNextWeapon(PlayerController.instance);

            TemportalPush tp = PlayerController.instance.gameObject.AddComponent<TemportalPush>();
            tp.pushVector = -direction * recoil;

            OnPressed();
            SoundEffectsManager.Instance.PlaySound(sound);
            anim.gameObject.SetActive(false);
        }
    }
    override public void Fire()
    {
        if(currentCharges > 0)
        {
            isCharging = true;
            chargeCounter = chargeTime;
            anim.gameObject.SetActive(true);
            anim.Play("ChargeSmall");
            chargeSound.Play();
        }
    }
}
