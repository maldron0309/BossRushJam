using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultStorageLoader : MonoBehaviour
{
    public List<WeaponSlot> weapons = new List<WeaponSlot>();
    void Start()
    {
        if(ItemStorage.instance.weapons.Count == 0)
        {
            LoadDefaultItems();
        }
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadDefaultItems()
    {
        foreach (var item in weapons)
        {
            ItemStorage.instance.AddWeapon(item);
        }
    }
}
