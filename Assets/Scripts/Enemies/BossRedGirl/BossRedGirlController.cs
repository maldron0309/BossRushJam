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
