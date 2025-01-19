using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public WeaponSlot weapon;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SelectWeapon()
    {
        WeaponSelectController.instance.setWeapon(weapon);
        GetComponent<Button>().interactable = false;

    }
}
