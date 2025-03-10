using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsInventory : MonoBehaviour
{
    public static WeaponsInventory instance;
    public WeaponSlot[] weapons;
    public InventorySlot[] slots;
    public WeaponWheelController WeaponWheel;
    public int currentIdx;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
        }
        currentIdx = 0;
    }
    void Start()
    {
        if(instance != this)
        {
            Destroy(gameObject);
            WeaponSelectController.instance.inv = instance;
            instance.WeaponWheel = WeaponWheel;
            WeaponWheel.inv = instance;
            WeaponWheel.updateWheel();
            instance.GiveWeapon(PlayerController.instance);
            WeaponSelectController.instance.updateWheel();
        }
    }
    public void GiveNextWeapon(PlayerController player)
    {
        if(weapons.Length > 0)
            currentIdx = (currentIdx + 1) % weapons.Length;
        else
            currentIdx = (currentIdx + 1) % slots.Length;
        StartCoroutine(GiveWeaponWithDelay(player));

    }
    public void GiveWeapon(PlayerController player)
    {
        GameObject oldWeapon = player.weapon.gameObject;

        GameObject newWeapon;
        if (weapons.Length > 0)
            newWeapon = Instantiate(weapons[currentIdx].weaponPrefab);
        else
            newWeapon = Instantiate(slots[currentIdx].weapon.weaponPrefab);
        newWeapon.transform.position = player.weaponPlacement.transform.position;
        newWeapon.transform.SetParent(player.weaponPlacement.transform);
        if (!player.facingRight)
            newWeapon.transform.localScale = new Vector3(-newWeapon.transform.localScale.x, newWeapon.transform.localScale.y, newWeapon.transform.localScale.z);

        player.weapon = newWeapon.GetComponent<BaseAttack>();
        Destroy(oldWeapon);
        StartCoroutine(WeaponWheelController.instance.RotateWheel((currentIdx) * 45f));
    }
    public IEnumerator GiveWeaponWithDelay(PlayerController player)
    {
        yield return new WaitForSeconds(1);
        GiveWeapon(player);
        
    }
}
