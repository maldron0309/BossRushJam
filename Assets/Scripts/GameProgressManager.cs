using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager instance;
    public bool[] bossDefeated;
    public BackgroundDoor[] bossDoor;
    public int spawnPosition;
    public Transform[] spawnLocations;
    public PlayerController player;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            UpdateMainHall();
            DontDestroyOnLoad(instance);
        }
        else
        {
            instance.spawnLocations = spawnLocations;
            instance.bossDoor = bossDoor;
            instance.player = player;
            Destroy(gameObject);
            instance.UpdateMainHall();
        }
    }
    public void UpdateMainHall()
    {
        player.transform.position = spawnLocations[spawnPosition].position;
        for (int i = 0; i < bossDefeated.Length; i++)
        {
            if (bossDefeated[i])
            {
                bossDoor[i].Close();
            }
        }
    }
}
