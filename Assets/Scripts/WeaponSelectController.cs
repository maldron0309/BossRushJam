using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class WeaponSelectController : MonoBehaviour
{
    public static WeaponSelectController instance;
    int currentslot;
    WeaponSlot currentweapon;
    bool[] filled;
    public Image[] slots;
    public WeaponsInventory inv;
    public TMP_Text description;
    public InventorySlotUI[] weaponbuttons;
    public WeaponWheelController weaponWheel;
    public PlayerController player;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        filled = new bool[8];
        currentslot = 0;
        for (int i = 0; i < filled.Length; i++)
        {
            filled[i] = true;
        }
        updateWheel();
        gameObject.SetActive(false);
    }
    void Update()
    {
        
    }
    public void finish()
    {
        bool good = true;
        for (int i = 0; i < 8; i++)
        {
            if(filled[i] == false)
            {
                good = false;
                break;
            }
        }
        if (good)
        {
            gameObject.SetActive(false);
            weaponWheel.gameObject.SetActive(true);
            weaponWheel.updateWheel();
            inv.GiveWeapon(player);
        }
        
    }

    public void setActive(int num)
    {
        transform.Find("Buttons").transform.GetChild(currentslot).GetComponent<Button>().interactable = true;
        currentslot = num;
        Debug.Log("Set active to slot " + num.ToString());
        transform.Find("Buttons").transform.GetChild(num).GetComponent<Button>().interactable = false;
    }
    public void setWeapon(WeaponSlot weapon)
    {
        if (currentweapon != null)
            weaponbuttons.Where(x => x.weapon == currentweapon).FirstOrDefault().GetComponent<Button>().interactable = true; 

        currentweapon = weapon;
        description.text = weapon.weaponDescription;
        Debug.Log("Set active to slot " + weapon.weaponName);
        //weaponbuttons.transform.Find(weapon.weaponName).GetComponent<Button>().interactable = false;
    }

    void updateWheel()
    {
        
        for (int i = 0; i < inv.weapons.Length; i++)
        {
            slots[i].sprite = inv.weapons[i].weaponImage;
            slots[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < 8; i++)
        {
            if (filled[i] == false)
            {
                slots[i].gameObject.SetActive(false);
            }

        }
    }

    public void clear()
    {
        for(int i = 0; i<8; i++)
        {
            filled[i] = false;
        }
        updateWheel();
        foreach (Button child in transform.Find("Buttons").transform.GetComponentsInChildren<Button>()) { child.interactable = true; currentslot = 0; }
        transform.Find("Buttons").transform.GetChild(0).GetChild(0).GetComponent<Button>().interactable = false;
    }

    public void refreshMenu()
    {
        clear();
        //Write stuff to determine what items the player currntly has unlocked and put onto the shop thing
    }

    public void swap()
    {
        inv.weapons[currentslot] = currentweapon;
        filled[currentslot] = true;
        updateWheel();
    }
}
