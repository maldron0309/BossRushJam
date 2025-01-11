using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private string nextScene; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        Debug.Log("Loading next scene: " + nextScene);
        SceneManager.LoadScene(nextScene);
    }
}