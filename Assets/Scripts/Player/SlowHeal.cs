using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowHeal : BaseAttack
{
    public float healInterval = 0.25f;
    private int lotalHealed;
    private bool healingStarted = false;
    private bool isFineshed = false;
    private PlayerHealth playerHealth;
    private float nextHeal;
    public GameObject effectPrefab;
    public AudioClip attackSound;
    void Start()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();
        currentCharges = maxCharges;
    }

    // Update is called once per frame
    void Update()
    {
        if (healingStarted && !isFineshed)
        {
            if(currentCharges > 0 && canShoot)
            {
                if (nextHeal > 0)
                {
                    nextHeal -= Time.deltaTime;
                }
                else
                {
                    nextHeal = healInterval;
                    Instantiate(effectPrefab, transform.root.position, Quaternion.identity);
                    playerHealth.Heal(1);
                    currentCharges--;
                    if(currentCharges <= 0)
                        WeaponsInventory.instance.GiveNextWeapon(GetComponentInParent<PlayerController>());
                    SoundEffectsManager.Instance.PlaySound(attackSound);
                }
            }
        }
    }
    override public void OnRelease()
    {
        if (healingStarted && !isFineshed)
        {
            healingStarted = false;
            isFineshed = true;
            WeaponsInventory.instance.GiveNextWeapon(GetComponentInParent<PlayerController>());
        }
    }
    override public void OnPressed()
    {
        healingStarted = true;
    }
    override public void Fire()
    {
        OnPressed();
    }
}
