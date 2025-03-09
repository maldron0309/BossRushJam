using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameData : MonoBehaviour
{
    public static GameData instance;  // Singleton instance
    public List<string> events = new List<string>();

    private string savePath; // Path to save game data

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        savePath = Application.persistentDataPath + "/gameData.json";
        Load();
    }
    public void AddEvent(string eventName)
    {
        if (!events.Contains(eventName))
        {
            events.Add(eventName);
        }
    }
    public void RemoveEvent(string eventName)
    {
        if (events.Contains(eventName))
        {
            events.Remove(eventName);
        }
    }
    public bool HasEvent(string eventName)
    {
        return events.Contains(eventName);
    }
    public void Save()
    {
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(savePath, json);
    }
    public void Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }
}
