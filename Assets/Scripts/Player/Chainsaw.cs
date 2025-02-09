using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chainsaw : BaseAttack
{
    private bool holdingButton = false;
    private bool isAttacking = false;
    public SpriteRenderer attackEffect;
    public float attackRate;
    private float attackCounter;
    public int damagePerHit;
    private List<BaseBossController> entities = new List<BaseBossController>();
    void Start()
    {
        currentCharges = maxCharges;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking && currentCharges > 0)
        {
            if (attackCounter > 0)
                attackCounter -= Time.deltaTime;
            else
            {
                foreach (var item in entities)
                {
                    item.GetComponent<BossHealth>().TakeDamage(damagePerHit);
                }
                attackCounter = attackRate;
                isAttacking = holdingButton;
                if (!isAttacking)
                    attackEffect.color = Color.white;
                currentCharges--;
                if (currentCharges <= 0)
                    WeaponsInventory.instance.GiveNextWeapon(PlayerController.instance);
            }
        }
    }
    override public void OnRelease()
    {
        if (holdingButton)
        {
            holdingButton = false;
        }
    }
    override public void OnPressed()
    {
        holdingButton = true;
        isAttacking = true;
        attackCounter = attackRate;
    }
    override public void Fire()
    {
        holdingButton = true;
        isAttacking = true;
        attackEffect.color = Color.red;
        attackCounter = attackRate;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            BaseBossController boss = collision.GetComponentInParent<BaseBossController>();
            if (!entities.Contains(boss))
            {
                entities.Add(boss);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            BaseBossController boss = collision.GetComponentInParent<BaseBossController>();
            if (entities.Contains(boss))
            {
                entities.Remove(boss);
            }
        }
    }
}
