using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public WeaponSlot weapon;
    public int id;

    public InventorySlot(WeaponSlot item, int count)
    {
        this.weapon = item;
        this.id = count;
    }

    internal object Where(Func<object, bool> p)
    {
        throw new NotImplementedException();
    }
}
public class ItemStorage : MonoBehaviour
{
    public static ItemStorage instance;
    public List<InventorySlot> weapons = new List<InventorySlot>();
    private int currentIdx;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddWeapon(WeaponSlot weapon)
    {
        InventorySlot slot = new InventorySlot(weapon, weapons.Count);
        weapons.Add(slot);
    }
}
