using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLarvaAttackMeele : MonoBehaviour
{
    public float minMoveSpeed;
    public float maxMoveSpeed;
    public float moveAcceleration;
    private float moveSpeed;

    public float waitTime;
    private float waitCounter;

    public bool isStarted = false;
    private Rigidbody2D rb;
    private int movedir;
    private BossLarvaController boss;
    private PlayerController player;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindAnyObjectByType<PlayerController>();
        boss = GetComponent<BossLarvaController>();
    }
    void Update()
    {
        if (!isStarted)
            return;

        if (boss.groundMonitor.playerIsIn)
        {
            float dist = player.transform.position.x - transform.position.x;
            if (dist > 0)
                movedir = 1;
            else
                movedir = -1;
            if (moveSpeed < maxMoveSpeed)
                moveSpeed += moveAcceleration * Time.deltaTime;
            Move();
        }
        else
        {
            if (waitCounter > 0)
                waitCounter -= Time.deltaTime;
            else
            {
                isStarted = false;
                boss.isPerformingAction = false;
            }
        }
    }
    public void BeginAttack()
    {
        isStarted = true;
        boss.isPerformingAction = true;
        moveSpeed = minMoveSpeed;
        waitCounter = waitTime;
    }
    public void Move()
    {
        rb.velocity = new Vector2(movedir * moveSpeed, rb.velocity.y);
    }
}
