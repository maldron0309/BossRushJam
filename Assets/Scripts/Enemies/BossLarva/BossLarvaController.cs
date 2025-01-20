using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLarvaController : BaseBossController
{
    [SerializeField] float timeBetweenActions;
    public BossHealth health;
    public Animator anim;
    [Header("Attacks")]
    public BossLarvaAttackMeele meeleAttack;
    public BossLarvaSpit spitAttack;
    public PlayerMonitor groundMonitor;
    public BossLarvaRunning running;

    [Header("Other")]
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public float groundCheckRadius = 0.2f;
    public Transform groundCheck;
    public bool isPerformingAction = false;
    public BossFlyerControll phase2;

    private bool hadMoved = false;
    private bool isGrounded;
    private bool wasGrounded;
    private float nextActionCounter;
    private int rage = 0;
    private int normalMoveCounter = 0;

    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        nextActionCounter = timeBetweenActions;
    }
    public bool IsGrounded()
    {
        return isGrounded;
    }
    public bool WasGrounded()
    {
        return wasGrounded;
    }

    void Update()
    {
        wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
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

        HandleAnimation();
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
    public override void OnDefeat()
    {
        phase2.health.Initialize(BossHealthUI.instance);
        phase2.transform.position = transform.position;
        phase2.StartBossBattle();
        BackgroundMusicManager.Instance.PlayBossMusic(5);
        Destroy(gameObject);   
    }
    public void MakeRandomMove()
    {
        if (groundMonitor.playerIsIn)
        {
            meeleAttack.BeginAttack();
        }
        else
        {
            int randomNumber = Random.Range(0, 100);
            if(randomNumber > 50)
                spitAttack.BeginAttack();
            else
                running.BeginAttack();

            nextActionCounter = timeBetweenActions;

        }
        hadMoved = !hadMoved;
    }
}
