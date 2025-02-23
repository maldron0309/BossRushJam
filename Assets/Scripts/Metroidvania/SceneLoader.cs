using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;  // Singleton

    [Header("Player Prefab and Default Scene")]
    public GameObject playerPrefab;
    public string defaultScene = "Stage1";
    public string defaultCheckpoint = "Start";
    public WeaponSlot[] items;

    public GameObject player;

    private void Awake()
    {
        DontDestroyOnLoad(GameObject.Find("Canvas"));

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }
    private void Start()
    {
        LoadSceneFromSave();
    }

    private void LoadSceneFromSave()
    {
        // Load saved data
        SaveData data = SaveSystem.LoadGame();
        string sceneToLoad = data != null ? data.currentScene : defaultScene;
        string checkpointID = data != null ? data.lastCheckpoint : defaultCheckpoint;


        if(data != null && data.equippedWeaponIDs != null)
        {
            // Clear current storage before loading
            ItemStorage.instance.weapons.Clear();
            WeaponsInventory.instance.slots = new InventorySlot[data.equippedWeaponIDs.Count];

            // Restore stored weapons
            foreach (var storedWeapon in data.storedWeapons)
            {
                WeaponSlot weaponData = FindWeaponByName(storedWeapon.weaponName);
                if (weaponData != null)
                {
                    ItemStorage.instance.weapons.Add(new InventorySlot(weaponData, storedWeapon.id));
                }
            }

            // Restore equipped weapons using IDs
            for (int i = 0; i < data.equippedWeaponIDs.Count; i++)
            {
                int id = data.equippedWeaponIDs[i];
                InventorySlot foundSlot = ItemStorage.instance.weapons.Find(slot => slot.id == id);
                if (foundSlot != null)
                {
                    WeaponsInventory.instance.slots[i] = foundSlot;
                }
            }
            WeaponWheelController.instance.updateWheel();
        }

        StartCoroutine(LoadSceneAndPlacePlayer(sceneToLoad, checkpointID));
    }

    private System.Collections.IEnumerator LoadSceneAndPlacePlayer(string sceneName, string checkpointID)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);

        // Find the checkpoint in the loaded scene and place the player there
        Checkpoint checkpoint = FindCheckpointByID(checkpointID);
        Vector3 spawnPosition = checkpoint != null ? checkpoint.transform.position : Vector3.zero;

        // Spawn or move the player to the spawn position
        if (player == null)
        {
            player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            player.transform.position = spawnPosition;
        }
        RoomCamera.instance.player = player.transform;
    }

    private Checkpoint FindCheckpointByID(string checkpointID)
    {
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (checkpoint.checkpointID == checkpointID)
                return checkpoint;
        }
        Debug.LogWarning($"Checkpoint {checkpointID} not found. Using default spawn position.");
        return null;
    }

    public void TransitionToScene(string sceneName, string checkpointID)
    {
        StartCoroutine(LoadSceneAndPlacePlayer(sceneName, checkpointID));
    }
    private WeaponSlot FindWeaponByName(string weaponName)
    {
        foreach (var slot in items)
        {
            if (slot.weaponName == weaponName)
                return slot;
        }
        return null;
    }
}
