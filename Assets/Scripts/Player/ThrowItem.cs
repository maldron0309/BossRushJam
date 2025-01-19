using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowItem : BaseAttack
{
    public GameObject itemPrefab;
    public float verticalForce;
    public float HorizontalForce;
    void Start()
    {
        currentCharges = maxCharges;
    }
    override public void OnRelease()
    {

    }
    override public void OnPressed()
    {

    }
    override public void Fire()
    {
        PlayerController player = GetComponentInParent<PlayerController>();
        if (currentCharges > 0 && canShoot)
        {
            GameObject go = Instantiate(itemPrefab, transform.root.position, Quaternion.identity);

            Vector2 direction = new Vector2(player.facingRight ? 1 : -1, 0);
            go.GetComponent<Rigidbody2D>().AddForce(Vector2.up * verticalForce + Vector2.right * HorizontalForce * direction);
            currentCharges--;
            if (currentCharges <= 0)
                WeaponsInventory.instance.GiveNextWeapon(player);
        }
    }
}
