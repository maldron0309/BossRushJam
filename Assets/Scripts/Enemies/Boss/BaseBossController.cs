using UnityEngine;

public class BaseBossController : MonoBehaviour
{
    public bool facingRight = true;
    public bool isBattleStarted = false;
    [SerializeField] private GameObject door;

    private Vector3 initialPosition;
    private BossHealth bossHealth;


    void Start()
    {
        initialPosition = transform.position;
        bossHealth = GetComponent<BossHealth>();
    }

    public void StartBossBattle()
    {
        isBattleStarted = true;
    }

    public void ResetBoss()
    {
        isBattleStarted = false;

        transform.position = initialPosition;

        if (bossHealth != null)
        {
            bossHealth.ResetHealth();
        }

        if (door != null)
        {
            door.SetActive(false);
        }
    }
    public void StopBossActions()
    {
        isBattleStarted = false;
    }

    public void ActivateDoor()
    {
        if (door != null)
        {
            door.SetActive(true);
        }
    }
}