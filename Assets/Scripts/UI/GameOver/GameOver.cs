using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;
    [SerializeField] private GameObject gameOverPanel;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }
    void Update()
    {

    }
    public void IsGameOver()
    {
        gameOverPanel.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainHall");
    }
}
