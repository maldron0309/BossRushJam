using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class NewWeaponSelectController : MonoBehaviour
{
    public static NewWeaponSelectController instance;
    public EquipmentManager eqipManager;
    public StorageManager storeManager;
    public GameObject WeaponsContainer;
    public WeaponWheelController WeaponWheel;
    public WeaponsInventory inventory;
    public ItemStorage storage;
    public PlayerController player;
    public GameObject root;
    public InventorySlot currentSlot;
    public EquipmentSlot currentEquipSlot;
    public TextMeshProUGUI description;

    private List<EquipmentSlot> equipmentSlots = new List<EquipmentSlot>();
    private EquipmentSlot selectedEquipmentSlot;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        inventory = WeaponsInventory.instance;
        storage = ItemStorage.instance;
        player = PlayerController.instance;
        Refresh();
        Close();
    }
    void Update()
    {

    }
    public void Close()
    {
        root.SetActive(false);
    }
    public void Open()
    {
        root.SetActive(true);
        Refresh();
    }
    public void Refresh()
    {
        eqipManager.Refresh();
        storeManager.Refresh();
    }
    public void setWeapon(InventorySlot slot)
    {
        if(currentSlot != null)
            storeManager.ResetButton(currentSlot);

        currentSlot = slot;
        description.text = slot.weapon.weaponDescription;
    }
    public void SelectEquipSlot(EquipmentSlot slot)
    {
        eqipManager.ResetButtons();

        currentEquipSlot = slot;
        //description.text = slot.weapon.weaponDescription;
    }
    public void Swap()
    {
        if (currentEquipSlot != null && currentSlot != null)
        {
            if (currentEquipSlot.item != null)
                storeManager.ResetButton(currentEquipSlot.item);
            currentEquipSlot.SetSlot(currentSlot);
            //slotUI.SetAsInUse();
        }
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            inventory.slots[i] = eqipManager.slots[i].item;
        }
        storeManager.Refresh();
    }
    public void Finish()
    {
        PlayerController.instance.Resume();
        inventory.GiveWeapon(PlayerController.instance);
        Close();
        WeaponWheelController.instance.gameObject.SetActive(true);
        WeaponWheelController.instance.updateWheel();
    }
}
