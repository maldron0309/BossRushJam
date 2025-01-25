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
    public Animator anim;

    public BugJumpAttack jumpAttack;
    public ThrowAcid acidAttack;
    public SprayAttack sprayAttack;
    public BugJumpMovement movement;

    public string state;
    
    private Rigidbody2D rb;
    private bool jumpactivated;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        nextActionCounter = timeBetweenActions;
        basePos = transform.position;
    }

    public override void OnDefeat()
    {
        jumpAttack.enabled = false;
        acidAttack.enabled = false;
        sprayAttack.enabled = false;
        movement.enabled = false;
        rb.gravityScale = 100f;
        state = "death";
        Destroy(gameObject, 2);
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
        if (jumpactivated == false)
        {
            jumpactivated = true;
            movement.enabled = true;
        }
        int randomNumber = Random.Range(0, 100);
        {
            if(randomNumber <= 20)
            {
                StartCoroutine(DisableMovement(5f));
                StartCoroutine(sprayAttack.BeginAttack());
                nextActionCounter = 6.5f;
                
            }
            else if(randomNumber <= 80)
            {
                //StartCoroutine(DisableMovement(0.1f));
                acidAttack.BeginAttack();
                nextActionCounter = 2;

                
            }
            else if (randomNumber > 80)
            {
                StartCoroutine(DisableMovement(3.5f));
                StartCoroutine(delayedJumpAttack());
                nextActionCounter = 5f;
                
            }


            //StartCoroutine(jumpAttack.BeginAttack());


            //nextActionCounter = timeBetweenActions;
        }
    }
    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public IEnumerator DisableMovement(float time)
    {
        movement.enabled = false;
        yield return new WaitForSeconds(time);
        movement.enabled = true;
    }

    IEnumerator delayedJumpAttack()
    {
        yield return new WaitForSeconds(0.75f);
        rb.velocity = Vector2.zero;
        StartCoroutine(jumpAttack.BeginAttack());
    }
}
