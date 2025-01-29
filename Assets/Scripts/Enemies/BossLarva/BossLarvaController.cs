using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLarvaController : BaseBossController
{
    [SerializeField] float timeBetweenActions;
    public BossHealth health;
    public Animator anim;
    [Header("Attacks")]
    public GameObject hitZone;
    public BossLarvaAttackMeele meeleAttack;
    public BossLarvaSpit spitAttack;
    public PlayerMonitor groundMonitor;
    public BossLarvaRunning running;
    public BossLarvaJump bossJump;

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
    private bool isDead = false;

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
        if (isDead)
            return;

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
    }
    public override void OnDefeat()
    {
        isDead = true;
        hitZone.SetActive(false);
        rb.velocity = Vector2.zero;
        isPerformingAction = false;
        spitAttack.enabled = false;
        bossJump.enabled = false;
        running.enabled = false;
        meeleAttack.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Props");
        BackgroundMusicManager.Instance.StopBGM();
        anim.Play("Death");
        StartCoroutine(StartPhase2());
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
            if (randomNumber > 66)
                spitAttack.BeginAttack();
            else if(randomNumber > 33)
                running.BeginAttack();
            else
                bossJump.BeginAttack();

            nextActionCounter = timeBetweenActions;

        }
        hadMoved = !hadMoved;
    }
    private IEnumerator StartPhase2()
    {
        yield return new WaitForSeconds(1);

        phase2.health.Initialize(BossHealthUI.instance);
        phase2.transform.position = transform.position;
        phase2.StartBossBattle();
        BackgroundMusicManager.Instance.PlayBossMusic(5);
    }
}
