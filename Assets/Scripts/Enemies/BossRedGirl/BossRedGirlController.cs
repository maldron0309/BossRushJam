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
    public RedGirlTornado tornadoAttack;
    public BossHealth health;
    public Animator anim;
    public GameObject afterImagePrefab;
    public float afterImageRate;
    private float afterImageCounter;
    public bool makeAfterImages = false;

    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public float groundCheckRadius = 0.2f;
    public Transform groundCheck;
    public bool isPerformingAction = false;

    private bool hadMoved = false;
    private bool isGrounded;
    private bool wasGrounded;
    private float nextActionCounter;
    private int rage = 0;
    private bool forceTornado = false;
    private int normalMoveCounter = 0;

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
    public void FixedUpdate()
    {
        if (makeAfterImages)
        {
            if (afterImageCounter > 0)
            {
                afterImageCounter -= Time.deltaTime;
            }
            else
            {
                afterImageCounter = afterImageRate;
                SpawnAfterImage();
            }
        }
    }
    public bool IsGrounded()
    {
        return isGrounded;
    }
    public bool WasGrounded()
    {
        return wasGrounded;
    }
    public void JumpOver(PlayerController player)
    {
        if(player.IsGrounded() && isGrounded)
        {
            //airDash.BeginAttack();
        }
    }
    public override void OnDefeat()
    {
        Destroy(gameObject);
        GameProgressManager.instance.bossDefeated[1] = true;
    }
    void Update()
    {
        wasGrounded = isGrounded;
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
            forceTornado = true;
        }
        if (rage == 1 && health.PercentageHealth() < 0.50f)
        {
            rage = 2;
            jumpAttack.airBounces = 4;
            airDash.numberOfAattacks = 6;
            tornadoAttack.numberOfJumps = 4;
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
            tornadoAttack.numberOfJumps = 5;
            forceTornado = true;
        }
        if (health.PercentageHealth() == 0)
            isBattleStarted = false;

        HandleAnimation();
    }
    public void HandleAnimation()
    {
        if (isPerformingAction)
            return;

        if (isGrounded)
        {
            if (runAndJump.isStarted)
                anim.Play("Move");
            else
                anim.Play("Idle");
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
            if (forceTornado || normalMoveCounter >= 5)
            {
                tornadoAttack.BeginAttack();
                normalMoveCounter = 0;
                forceTornado = false;
            }else if (randomNumber > 50)
            {
                jumpAttack.BeginAttack();
                normalMoveCounter++;
            }
            else
            {
                airDash.BeginAttack();
                normalMoveCounter++;
            }
            nextActionCounter = timeBetweenActions;
            
        }
        hadMoved = !hadMoved;
    }
    void SpawnAfterImage()
    {
        GameObject afterImage = Instantiate(afterImagePrefab, transform.position, transform.rotation);
        SpriteRenderer afterImageRenderer = afterImage.GetComponent<SpriteRenderer>();
        SpriteRenderer playerRenderer = anim.GetComponent<SpriteRenderer>();

        // Copy player body sprite
        afterImageRenderer.sprite = playerRenderer.sprite;
        //afterImageRenderer.flipX = playerRenderer.flipX;
        afterImage.transform.localScale = anim.transform.localScale;
        afterImage.transform.rotation = anim.transform.rotation;
        afterImageRenderer.sortingLayerID = playerRenderer.sortingLayerID;
        afterImageRenderer.sortingOrder = playerRenderer.sortingOrder;
    }
}
