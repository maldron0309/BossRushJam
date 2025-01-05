using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon Slot", menuName = "Weapon Slot")]
public class WeaponSlot : ScriptableObject
{
    public string weaponName;
    public string weaponDescription;
    public GameObject weaponPrefab;
}
