using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewInventorySlotUI : MonoBehaviour
{
    public InventorySlot slot;
    public AudioClip soundEffect;
    public Image itemImage;
    public Image frameImage;
    public Sprite imageNormal;
    public Sprite imageSelected;
    public Sprite imageInUse;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (slot.weapon)
        {
            itemImage.sprite = slot.weapon.weaponImage;
        }
    }
    public void SelectWeapon()
    {
        NewWeaponSelectController.instance.setWeapon(slot);
        GetComponent<Button>().interactable = false;
        SoundEffectsManager.Instance.PlaySound(soundEffect);
        frameImage.sprite = imageSelected;
    }
    public void Reset()
    {
        GetComponent<Button>().interactable = true;
        frameImage.sprite = imageNormal;
    }
    public void SetAsInUse()
    {
        frameImage.sprite = imageInUse;
        GetComponent<Button>().interactable = false;
    }
    private void OnValidate()
    {
        if (slot.weapon)
        {
            itemImage.sprite = slot.weapon.weaponImage;
        }
    }
}
