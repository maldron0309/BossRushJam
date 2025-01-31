using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGirlTornado : MonoBehaviour
{
    public float moveSpeed;
    public bool isStarted = false;
    private bool isFInishing = false;
    public Transform targetPost;
    public float idleSpinTime;
    public float bigJump = 50;
    public GameObject bulletPrefab;
    public int numberOfJumps = 2;
    public float minTimeBetweenAttacks;
    public float maxTimeBetweenAttacks;

    private bool isFalling;
    private int jumpCounter = 0;
    private float spinTimer = 0;
    private float attackTimer = 0;
    private Rigidbody2D rb;
    private BossRedGirlController boss;
    private Animator anim;
    private int movedir;
    private int stage = 0;
    public AudioClip soundEffect;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boss = GetComponent<BossRedGirlController>();
        anim = boss.anim;
    }
    private void Update()
    {
        if (!isStarted)
            return;
        if (boss.IsGrounded())
        {
            if (stage == 0)
                anim.Play("Move");
        }
    }
    void FixedUpdate()
    {
        if (!isStarted)
            return;

        float dist = targetPost.position.x - transform.position.x;
        if (dist > 0)
            movedir = 1;
        else
            movedir = -1;

        // walk to middle
        if(stage == 0)
        {
            if (Mathf.Abs(dist) > 0.5f)
            {
                Move();
            }
            else
            {
                boss.FacePlayer();
                stage = 1;
                spinTimer = idleSpinTime;
                anim.Play("SpinHorizontal");
            }
        }

        // spin
        if (stage == 1)
        {
            if (spinTimer > 0)
                spinTimer -= Time.deltaTime;
            else
            {
                stage = 2;
                Jump(bigJump);
                attackTimer = Random.Range(minTimeBetweenAttacks, maxTimeBetweenAttacks);
            }
        }

        // jump
        if (stage == 2)
        {
            if (!isFalling)
            {
                isFalling = rb.velocity.y < 0;
            }
            if (boss.IsGrounded() && isFalling)
            {
                if (jumpCounter < numberOfJumps)
                {
                    Jump(bigJump);
                    isFalling = false;
                }
                else
                {
                    stage = 3;
                }
            }
            if (attackTimer > 0)
                attackTimer -= Time.deltaTime;
            else
            {
                Fire();
                attackTimer = Random.Range(minTimeBetweenAttacks, maxTimeBetweenAttacks);
            }
        }

        if(stage == 3)
        {
            boss.isPerformingAction = false;
            isStarted = false;
        }
    }
    public void Fire()
    {
        Rigidbody2D bullet;
        bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Rigidbody2D>();
        bullet.velocity = new(10, 0);

        bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Rigidbody2D>();
        bullet.transform.localScale = new Vector3(-1, 1, 1);
        bullet.velocity = new(-10, 0);
        SoundEffectsManager.Instance.PlaySound(soundEffect);
    }
    public void BeginAttack()
    {
        isFInishing = false;
        isStarted = true;
        boss.isPerformingAction = true;
        stage = 0;
        jumpCounter = 0;
    }
    public void Jump(float jumpForce)
    {
        jumpCounter++;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Fire();
    }
    public void Move()
    {
        rb.velocity = new Vector2(movedir * moveSpeed, rb.velocity.y);
    }
}
