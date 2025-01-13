using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseBossController : MonoBehaviour
{
    public bool facingRight = true;
    public bool isBattleStarted = false;
    [SerializeField] private GameObject door;

    public void StartBossBattle()
    {
        isBattleStarted = true;
    }
    
    public void ActivateDoor()
    {
        if (door != null)
        {
            door.SetActive(true);
        }
    }
}