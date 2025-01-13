using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBugController : BaseBossController
{
    private Vector3 basePos;
    [SerializeField] float timeBetweenActions;
    public BossHealth health;
    public float jumpForce = 50;
    private int normalAttacksPerformed = 0;
    private float nextActionCounter;

    public BugJumpAttack jumpAttack;
    public ThrowAcid acidAttack;
    
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        nextActionCounter = timeBetweenActions;
        basePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBattleStarted) return;

        if (nextActionCounter > 0)
        {
            nextActionCounter -= Time.deltaTime;
        }
        else
        {
            MakeRandomMove();
        }

        if (health.PercentageHealth() == 0)
            isBattleStarted = false;
    }
    public void MakeRandomMove()
    {
        
        int randomNumber = Random.Range(0, 100);
        {
            if(randomNumber <= 70)
            {
                acidAttack.BeginAttack();
                nextActionCounter = 3;
            }
            if (randomNumber > 70)
            {
                StartCoroutine(jumpAttack.BeginAttack());
                nextActionCounter = 5;
            }


            //StartCoroutine(jumpAttack.BeginAttack());


            //nextActionCounter = timeBetweenActions;
        }
    }
    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
