using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantHeal : BaseAttack
{
    public int amountHealed = 5;
    private PlayerHealth playerHealth;
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

    }
    override public void OnPressed()
    {

    }
    override public void Fire()
    {
        if(currentCharges > 0 && canShoot)
        {
            Instantiate(effectPrefab, transform.root.position, Quaternion.identity);
            playerHealth.Heal(amountHealed);
            currentCharges--;
            if (currentCharges <= 0)
                WeaponsInventory.instance.GiveNextWeapon(GetComponentInParent<PlayerController>());
            SoundEffectsManager.Instance.PlaySound(attackSound);
        }
    }
}
