using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGirlRunAndJump : MonoBehaviour
{
    public float bigJump = 50;
    public GameObject bulletPrefab;
    public Transform post1;
    public Transform post2;

    public float moveSpeed;
    private bool isFInishing = false;
    private Transform targetPost;

    public bool isStarted = false;
    private Rigidbody2D rb;
    private BossRedGirlController boss;
    private int movedir;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boss = GetComponent<BossRedGirlController>();
        FindAnyObjectByType<PlayerController>().OnPlayerAttack += JumpOver;
    }
    private void Update()
    {
        if (!isStarted)
            return;
        if (boss.IsGrounded())
        {
            boss.anim.Play("Move");
        }
        else
        {
            boss.anim.Play("Jump");
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

        if(Mathf.Abs(dist) > 0.5f)
        {
            Move();
        }
        else
        {
            boss.isPerformingAction = false;
            isStarted = false;
            boss.FacePlayer();
        }
    }
    public void JumpOver(PlayerController player)
    {
        if (player.IsGrounded() && boss.IsGrounded() && isStarted)
        {
            Jump(bigJump);
        }
    }
    public void BeginAttack()
    {
        // pick far corner
        if (Vector2.Distance(post1.position, transform.position) > Vector2.Distance(post2.position, transform.position))
            targetPost = post1;
        else
            targetPost = post2;


        boss.FacePlayer();
        isFInishing = false;
        isStarted = true;
        boss.isPerformingAction = true;
    }
    public void Jump(float jumpForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    public void Move()
    {
        rb.velocity = new Vector2(movedir * moveSpeed, rb.velocity.y);
    }
}
