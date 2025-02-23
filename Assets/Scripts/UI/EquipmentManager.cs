using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public EquipmentSlot slotPrefab;
    public List<EquipmentSlot> slots = new List<EquipmentSlot>();
    public float radius = 100f; // Radius of the circle
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ResetButtons()
    {
        foreach (var item in slots)
        {
            item.Reset();
        }
    }
    public void Refresh()
    {
        foreach (var item in slots)
        {
            Destroy(item);
        }
        slots.Clear();

        List<InventorySlot> weapons = WeaponsInventory.instance.slots.ToList();
        int count = weapons.Count;
        if (count == 0) return;

        // Place slots in a circle
        for (int i = 0; i < count; i++)
        {
            float angle = (360f / count) * i; // Divide full circle by item count
            float radians = angle * Mathf.Deg2Rad; // Convert to radians

            Vector3 position = new Vector3(
                Mathf.Cos(radians) * radius,
                Mathf.Sin(radians) * radius,
                0f
            );

            // Instantiate slot
            EquipmentSlot slot = Instantiate(slotPrefab, transform);
            slot.transform.localPosition = position; // Place relative to parent
            slot.GetComponent<EquipmentSlot>().SetSlot(weapons[i]); // Set weapon

            slots.Add(slot);
        }
    }
}
