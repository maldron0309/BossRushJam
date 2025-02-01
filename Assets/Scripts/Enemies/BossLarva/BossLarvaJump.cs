using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLarvaJump : MonoBehaviour
{
    public float jumpHeight;
    private Animator anim;

    public float moveSpeed;

    public bool isStarted = false;
    public AudioClip jumpSound;
    private Rigidbody2D rb;
    private BossLarvaController boss;
    private int movedir;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boss = GetComponent<BossLarvaController>();
        anim = boss.anim;
    }
    private void FixedUpdate()
    {
        if ((movedir > 0 && !boss.facingRight) || (movedir < 0 && boss.facingRight))
        {
            boss.Flip();
        }
        if (boss.IsGrounded() && rb.velocity.y < 0)
        {
            boss.isPerformingAction = false;
            isStarted = false;
            anim.Play("Idle");
        } 
    }
    public void BeginAttack()
    {
        anim.Play("Jump");
        boss.FacePlayer();
        isStarted = true;
        boss.isPerformingAction = true;

        float dist = PlayerController.instance.transform.position.x - transform.position.x;
        if (dist > 0)
            movedir = 1;
        else
            movedir = -1;

        Jump();
    }
    public void Move()
    {
        rb.velocity = new Vector2(movedir * moveSpeed, rb.velocity.y);
    }
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0); // Reset vertical velocity for consistent jump height
        rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        SoundEffectsManager.Instance.PlaySound(jumpSound);
    }
}
