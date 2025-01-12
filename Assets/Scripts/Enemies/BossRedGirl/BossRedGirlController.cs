using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRedGirlController : BaseBossController
{
    [SerializeField] float timeBetweenActions;
    public RedGirlAirSlash jumpAttack;
    public RedGirlAirSlash SmallJump;
    public RedGirlAirDash airDash;
    public RedGirlRunAndJump runAndJump;
    public BossHealth health;

    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public float groundCheckRadius = 0.2f;
    public Transform groundCheck;
    public bool isPerformingAction = false;

    private bool hadMoved = false;
    private bool isGrounded;
    private float nextActionCounter;
    private int rage = 0;

    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        FindAnyObjectByType<PlayerController>().OnPlayerAttack += JumpOver;
    }
    void Start()
    {
        nextActionCounter = timeBetweenActions;
    }
    public bool IsGrounded()
    {
        return isGrounded;
    }
    public void JumpOver(PlayerController player)
    {
        if(player.IsGrounded() && isGrounded)
        {
            //airDash.BeginAttack();
        }
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (!isBattleStarted || isPerformingAction) return;

        if(nextActionCounter > 0)
        {
            nextActionCounter -= Time.deltaTime;
        }
        else
        {
            MakeRandomMove();
        }

        if (rage == 0 && health.PercentageHealth() < 0.75f)
        {
            rage = 1;
            airDash.numberOfAattacks = 5;
            runAndJump.moveSpeed = 12;
        }
        if (rage == 1 && health.PercentageHealth() < 0.50f)
        {
            rage = 2;
            jumpAttack.airBounces = 4;
            airDash.numberOfAattacks = 6;
        }
        if (rage == 2 && health.PercentageHealth() < 0.25f)
        {
            rage = 3;
            jumpAttack.airBounces = 5;
            airDash.numberOfAattacks = 7;
            runAndJump.moveSpeed = 15;
        }
        if (rage == 3 && health.PercentageHealth() < 0.15f)
        {
            rage = 4;
            jumpAttack.airBounces = 6;
            airDash.numberOfAattacks = 8;
            runAndJump.moveSpeed = 18;
        }
        if (health.PercentageHealth() == 0)
            isBattleStarted = false;
    }
    public void MakeRandomMove()
    {
        if (!hadMoved)
        {
            runAndJump.BeginAttack();
        }
        else
        {
            int randomNumber = Random.Range(0, 100);
            {
                if (randomNumber > 50)
                {
                    jumpAttack.BeginAttack();
                }
                else
                {
                    airDash.BeginAttack();
                }
                nextActionCounter = timeBetweenActions;
            }
        }
        hadMoved = !hadMoved;
    }
}
