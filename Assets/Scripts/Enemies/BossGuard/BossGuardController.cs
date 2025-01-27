using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGuardController : BaseBossController
{
    [SerializeField] float timeBetweenActions;
    [SerializeField] GuardBossShockWaveAttack shockWave;
    public GuardBossYellowGun multishot3;
    public GuardBossMultiShot multishot5;
    public GuardSpecialAttack specialAttack;
    public BossHealth health;
    public GuardBossSpinningShield shields;
    public float jumpForce = 50;
    private int normalAttacksPerformed = 0;
    private float nextActionCounter;
    public bool isPerformingAction = false;

    // count repeates of same attack. used to restain long repeats
    private int rapidStrikes = 0;
    private int slowStrikes = 0;
    private Rigidbody2D rb;
    [SerializeField] private bool isGround;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        nextActionCounter = timeBetweenActions;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBattleStarted) return;

        if(nextActionCounter > 0)
        {
            nextActionCounter -= Time.deltaTime;
        }
        else
        {
            MakeRandomMove();
        }

        if(shields.activeStage == 0  && health.PercentageHealth() < 0.75f)
        {
            shields.EnterStage(1);
        }
        if (shields.activeStage == 1 && health.PercentageHealth() < 0.50f)
        {
            shields.EnterStage(2);
        }
        if (shields.activeStage == 2 && health.PercentageHealth() < 0.15f)
        {
            shields.EnterStage(3);
        }
        if (health.PercentageHealth() == 0)
            isBattleStarted = false;
    }
    public void MakeRandomMove()
    {
        int randomNumber = Random.Range(0, 100);
        {
            if ((randomNumber > 90 && normalAttacksPerformed > 3) || normalAttacksPerformed > 7)
            {
                // every 8th attack is guaranteed special attack
                specialAttack.BeginAttack();
                normalAttacksPerformed = 0;
            }
            else if ((randomNumber > 50 && rapidStrikes <= 2) || slowStrikes >= 3)
            {
                multishot5.BeginSequence();
                normalAttacksPerformed++;
                rapidStrikes++;
                slowStrikes = 0;
                
                if(randomNumber % 2 == 0)
                    Jump();
            }
            else if(randomNumber < 30)
            {
                shockWave.Step();
            }
            else
            {
                multishot3.BeginSequence();
                normalAttacksPerformed++;
                slowStrikes++;
                rapidStrikes = 0;

                //if (randomNumber % 2 == 0)
                //    Jump();
            }
            nextActionCounter = timeBetweenActions;
        }
    }
    public void Jump()
    {        
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")) 
        {
            isGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")) 
        {
            isGround = false;
        }
    }


}
