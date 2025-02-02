using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlyerControll : BaseBossController
{
    [SerializeField] float timeBetweenActions;
    public float moveSpeed;
    public BossHealth health;
    public Animator anim;
    [Header("Attacks")]
    public BossFlyerMoveAround flyAround;
    public BossFlyerSideAttack sideAttack;
    public BossFlyerCenterAttack centerAttack;
    public BossFlyerCharge flyerCharge;
    public GameObject hitZone;

    [Header("Start Event")]
    private int stage = 0;
    public SpinningPlatforms wheel;
    public SpinningPlatforms[] platforms;

    [Header("Other")]
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public float groundCheckRadius = 0.2f;
    public Transform groundCheck;
    public bool isPerformingAction = false;

    private bool hadMoved = false;
    private float nextActionCounter;
    private int rage = 0;
    private int moveCounter = 0;
    private Rigidbody2D rb;
    private bool isDead = false;
    void Start()
    {
        nextActionCounter = timeBetweenActions;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (isDead)
            return;
        if (!isBattleStarted || isPerformingAction)
            return;

        if (nextActionCounter > 0)
        {
            nextActionCounter -= Time.deltaTime;
        }
        else
        {
            MakeRandomMove();
        }
        ManageStages();
        HandleAnimation();
    }
    public void ManageStages()
    {
        if(stage == 0)
        {
            stage = 1;
            wheel.rotationSpeed = -30;
            foreach (var item in platforms)
            {
                item.rotationSpeed = 60;
            }
        }
    }
    public void HandleAnimation()
    {
        if (isPerformingAction)
            return;

        //if (isGrounded)
        //{
        //    anim.Play("Idle");
        //}
    }
    public void MakeRandomMove()
    {
        if (moveCounter < 4)
        {
            flyAround.BeginAttack();
            moveCounter++;
        }
        else
        {
            int randomNumber = Random.Range(0, 100);
            if (randomNumber > 66)
                sideAttack.BeginAttack();
            else if (randomNumber > 33)
                centerAttack.BeginAttack();
            else
                flyerCharge.BeginAttack();

            moveCounter = 0;
            nextActionCounter = timeBetweenActions;

        }
        hadMoved = !hadMoved;
    }
    public override void OnDefeat()
    {
        //Destroy(gameObject);
        GameProgressManager.instance.bossDefeated[3] = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        isDead = true;
        rb.velocity = Vector2.zero;
        anim.Play("Death");
        hitZone.SetActive(false);
        sideAttack.enabled = false;
        centerAttack.enabled = false;
        flyerCharge.enabled = false;
        flyAround.enabled = false;
    }
    public void Move(Vector2 movedir)
    {
        if (isDead)
            return;
        rb.velocity = movedir * moveSpeed;

        if ((movedir.x > 0 && !facingRight) || (movedir.x < 0 && facingRight))
        {
            Flip();
        }
    }
}
