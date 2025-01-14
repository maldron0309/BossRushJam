using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGirlAirDash : MonoBehaviour
{
    public float bigJump = 50;
    public GameObject bulletPrefab;

    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public int numberOfAattacks;
    private bool isDashing = false;
    private bool canDash = true;
    private float dashTimer = 0f;
    private bool dashFinished;
    private bool isFInishing = false;

    private bool isStarted = false;
    private Rigidbody2D rb;
    private BossRedGirlController boss;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boss = GetComponent<BossRedGirlController>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isStarted)
            return;

        if(rb.velocity.y < 0 && !boss.IsGrounded() && !isFInishing)
        {
            if(!isDashing)
                StartCoroutine(Dash());
        }

        if (boss.IsGrounded() && dashFinished && isFInishing)
        {
            isStarted = false;
            boss.isPerformingAction = false;
        }
    }
    public void BeginAttack()
    {
        boss.FacePlayer();
        Jump(bigJump);
        dashFinished = false;
        isFInishing = false;
        isStarted = true;
        boss.isPerformingAction = true;
    }
    public void Jump(float jumpForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    private IEnumerator Dash()
    {
        isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0; // Temporarily disable gravity during dash

        Vector2 dashDirection = boss.FacinRight() ? Vector2.right : Vector2.left;
        rb.velocity = dashDirection * dashSpeed;

        Rigidbody2D bullet;

        for (int i = 0; i < numberOfAattacks; i++)
        {
            bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            bullet.velocity = new(0, -dashSpeed);
            yield return new WaitForSeconds(dashDuration / numberOfAattacks);
        }

        rb.gravityScale = originalGravity; // Restore gravity
        rb.velocity = Vector2.zero;
        isDashing = false;

        isFInishing = true;
        dashFinished = true;
    }
}
