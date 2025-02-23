using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public InventorySlot item;
    public Image itemImage;
    public Image slotFrame;
    public Sprite imageNormal;
    public Sprite imageSelected;
    public AudioClip soundEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnValidate()
    {
        if(item != null && item.weapon != null)
        {
            itemImage.sprite = item.weapon.weaponImage;
        }
    }
    public void SetSlot(InventorySlot s)
    {
        item = s;
        if (item != null)
        {
            itemImage.sprite = item.weapon.weaponImage;
        }
    }
    public void Reset()
    {
        slotFrame.sprite = imageNormal;
        GetComponent<Button>().interactable = true;
    }
    public void OnPress()
    {
        NewWeaponSelectController.instance.SelectEquipSlot(this);
        GetComponent<Button>().interactable = false;
        slotFrame.sprite = imageSelected;
        SoundEffectsManager.Instance.PlaySound(soundEffect);
    }
}
