using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

[System.Serializable]
public class SerializedWeapon
{
    public string weaponName;
    public int id;

    public SerializedWeapon(WeaponSlot weapon, int id)
    {
        this.weaponName = weapon.weaponName; // Storing name as identifier
        this.id = id;
    }
}

public static class SaveSystem
{
    public static string lastCheckPoint;
    private static string savePath = Application.persistentDataPath + "/save.dat";

    public static void SaveGame(string sceneName, string checkpointID)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(savePath, FileMode.Create);

        SaveData data = new SaveData(sceneName, checkpointID);

        // Save all weapons in storage
        foreach (InventorySlot slot in ItemStorage.instance.weapons)
        {
            data.storedWeapons.Add(new SerializedWeapon(slot.weapon, slot.id));
        }

        // Save equipped weapons by ID
        foreach (InventorySlot slot in WeaponsInventory.instance.slots)
        {
            data.equippedWeaponIDs.Add(slot.id);
        }

        formatter.Serialize(stream, data);
        stream.Close();

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);

        lastCheckPoint = $"{sceneName}.{checkpointID}";
        Debug.Log("Game saved.");
    }

    public static SaveData LoadGame()
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(savePath, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            Debug.Log("Game loaded.");
            return data;
        }
        else
        {
            Debug.LogWarning("No save file found.");
            return null;
        }
    }
}

[System.Serializable]
public class SaveData
{
    public string currentScene;
    public string lastCheckpoint;
    public List<int> equippedWeaponIDs = new List<int>(); // Stores weapon IDs for equipped items
    public List<SerializedWeapon> storedWeapons = new List<SerializedWeapon>(); // Stores all weapons in ItemStorage

    public SaveData(string scene, string checkpoint)
    {
        currentScene = scene;
        lastCheckpoint = checkpoint;
    }
}
