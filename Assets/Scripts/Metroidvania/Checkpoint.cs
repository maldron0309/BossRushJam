using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public string checkpointID;
    public bool setOnEnter = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (setOnEnter && other.CompareTag("Player") && SaveSystem.lastCheckPoint != $"{SceneManager.GetActiveScene().name}.{checkpointID}")
        {
            SaveSystem.SaveGame(SceneManager.GetActiveScene().name, checkpointID);
        }
    }
    public void Save()
    {
        SaveSystem.SaveGame(SceneManager.GetActiveScene().name, checkpointID);
    }
}
