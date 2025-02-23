using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StorageManager : MonoBehaviour
{
    public GameObject rowPrefab;
    public GameObject itemPrefab;
    public GameObject itemsContainer;
    private List<NewInventorySlotUI> buttons = new List<NewInventorySlotUI>();
    void Start()
    {
        
    }
    void Update()
    {

    }
    public void ResetButtons()
    {
        foreach (var item in buttons)
        {
            item.Reset();
        }
    }
    public void ResetButton(InventorySlot slot)
    {
        foreach (var item in buttons)
        {
            if(item.slot.id == slot.id)
                item.Reset();
        }
    }
    public void Refresh()
    {
        foreach (Transform child in itemsContainer.transform)
        {
            Destroy(child.gameObject);
        }

        List<InventorySlot> equipedItems = WeaponsInventory.instance.slots.ToList();
        List<InventorySlot> weaponSlots = ItemStorage.instance.weapons;
        buttons.Clear();
        for (int i = 0; i <= (weaponSlots.Count - 1) / 4; i++)
        {
            GameObject rowObj = Instantiate(rowPrefab, itemsContainer.transform);
            for (int j = 0; j < Mathf.Min(4, (weaponSlots.Count) - i * 4); j++)
            {
                NewInventorySlotUI slotUI = Instantiate(itemPrefab, rowObj.transform).GetComponent<NewInventorySlotUI>();
                slotUI.slot = weaponSlots[i * 4 + j];
                if (equipedItems.Any(slot => slot.id == slotUI.slot.id))
                    slotUI.SetAsInUse();
                buttons.Add(slotUI);
            }
        }
    }
}
