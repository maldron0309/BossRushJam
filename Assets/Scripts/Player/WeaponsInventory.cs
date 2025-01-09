using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsInventory : MonoBehaviour
{
    public static WeaponsInventory instance;
    public WeaponSlot[] weapons;
    public WeaponWheelController WeaponWheel;
    private int currentIdx;
    private void Awake()
    {
        instance = this;
        currentIdx = 0;
    }
    void Start()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        if(player)
            GiveWeapon(player);
    }
    public void GiveNextWeapon(PlayerController player)
    {
        currentIdx = (currentIdx + 1) % weapons.Length;
        StartCoroutine(GiveWeaponWithDelay(player));

    }
    public void GiveWeapon(PlayerController player)
    {
        GameObject oldWeapon = player.weapon.gameObject;

        GameObject newWeapon = Instantiate(weapons[currentIdx].weaponPrefab);
        newWeapon.transform.position = player.weaponPlacement.transform.position;
        newWeapon.transform.SetParent(player.weaponPlacement.transform);

        player.weapon = newWeapon.GetComponent<BaseAttack>();
        Destroy(oldWeapon);
    }
    public IEnumerator GiveWeaponWithDelay(PlayerController player)
    {
        yield return new WaitForSeconds(1);
        GiveWeapon(player);
        StartCoroutine(WeaponWheel.RotateWheel((currentIdx) * 45f));
        
    }
}
