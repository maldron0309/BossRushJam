using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public delegate void PlayerAttacAction(PlayerController player);

    public event PlayerAttacAction OnPlayerAttack;
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Animator anim;
    //private AnimatorStateInfo stateInfo;
    public bool isInputEnabled = true;

    [Header("Jumping")]
    public float minJumpHeight = 2f;
    public float maxJumpHeight = 4f;
    public float jumpBufferTime = 0.2f;
    public float coyoteTime = 0.2f;
    public bool wallJumpEnabled = false;
    public float wallJumpForce = 7f;
    public float wallJumpUpwardForce = 10f;
    public float wallSnapDistance = 0.5f;
    public bool jumpEnable = true;

    [Header("Roll and Dash")]

    public float dashSpeed = 15f;
    public float rollSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public bool DashEnabled = false;
    public ProgressBar evadeProgress;
    private bool isDashing = false;
    private bool canDash = true;
    private float dashTimer = 0f;

    [Header("Physics")]
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public float groundCheckRadius = 0.2f;
    public Transform groundCheck;
    public Transform wallCheckTopLeft;
    public Transform wallCheckTopRight;
    public Transform wallCheckBottomLeft;
    public Transform wallCheckBottomRight;
    public float wallSlideSpeed = 2f;
    public GameObject model;

    [Header("Attack Settings")]
    public BaseAttack weapon;
    public GameObject weaponPlacement;
    public TextMeshPro ammoText;

    [Header("Sound Effects")]
    public AudioClip jumpSound;
    public AudioClip doubleJumpSound;
    public AudioClip rollSound;
    public AudioClip dashSound;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private PlayerHealth health;
    private bool isAlive = true;
    public bool facingRight = true;
    private bool isGrounded;
    private bool isTouchingWallLeft;
    private bool isTouchingWallRight;
    private bool isTouchingWallLeftPartial;
    private bool isTouchingWallRightPartial;
    private bool canDoubleJump;
    private float jumpBufferCounter;
    private float coyoteTimeCounter;
    private bool jumpHeld;
    private bool isWallSliding;
    private bool isWallStickAllowed;
    private float originalSpeed;
    public Vector2 externalVelocity;
    public Rigidbody2D exRb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalSpeed = moveSpeed;
        health = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        UpdateGroundAndWallChecks();
        UpdateCoyoteAndJumpBuffer();

        if (weapon)
            ammoText.text = $"{weapon.currentCharges}/{weapon.maxCharges}";
    }

    private void FixedUpdate()
    {
        if (!isDashing)
            Move();

        if (wallJumpEnabled && !isDashing)
        {
            if (isWallSliding)
            {
                rb.gravityScale = 0;
                WallSlide();
            }
            else
                rb.gravityScale = 1;
        }
        
        HandleJump();

        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
            evadeProgress.UpdateProgress(1 - (dashTimer / dashCooldown));
            if(dashTimer <= 0 && evadeProgress.gameObject.activeInHierarchy)
                evadeProgress.gameObject.SetActive(false);
        }
    }
    private void WallSlide()
    {
        SnapToWall();
        rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
    }
    private void SnapToWall()
    {
        // Determine the wall direction (-1 for left, 1 for right)
        int wallDirection = isTouchingWallLeft ? -1 : isTouchingWallRight ? 1 : 0;

        if (wallDirection != 0)
        {
            // Cast a ray from the player's position toward the wall
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * wallDirection, wallSnapDistance, wallLayer);

            if (hit.collider != null)
            {
                transform.position = new Vector3(hit.point.x - (wallSnapDistance * wallDirection), transform.position.y, transform.position.z);
            }
        }
    }

    public void OnMove(float noveDir)
    {
        moveInput = new Vector2(noveDir, 0);
    }

    public void OnJump(bool isPressed)
    {
        if (isPressed)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else if (!isPressed)
        {
            OnJumpReleased();
        }

        jumpHeld = isPressed;
    }

    public void OnAttack1(bool isPressed)
    {
        if (isPressed)
        {
            if (weapon)
            {
                weapon.Fire();
                OnPlayerAttack?.Invoke(this);
            }
        }
        else if (!isPressed)
        {
            OnAttackReleased();
        }
    }
    public void PerformDodge()
    {
        if(dashTimer <= 0)
        {
            if (DashEnabled)
            {
                StartCoroutine(Dash());
            }
            else
            {
                if (isGrounded)
                    StartCoroutine(Roll());
            }
        }
    }
    public void OnAttackPressed()
    {
        if(weapon)
            weapon.OnPressed();
    }

    public void OnAttackReleased()
    {
        if(weapon)
            weapon.OnRelease();
    }
    public void PreventMovementFortime(float cooldown)
    {
        StartCoroutine(TemporaryStop(cooldown));
    }
    public IEnumerator TemporaryStop(float cooldown)
    {
        moveSpeed = 0f;
        yield return new WaitForSeconds(cooldown);
        moveSpeed = originalSpeed;
    }

    private void UpdateGroundAndWallChecks()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        bool isTouchingWallTopLeft = Physics2D.OverlapCircle(wallCheckTopLeft.position, groundCheckRadius, wallLayer);
        bool isTouchingWallBottomLeft = Physics2D.OverlapCircle(wallCheckBottomLeft.position, groundCheckRadius, wallLayer);
        isTouchingWallLeftPartial = isTouchingWallTopLeft != isTouchingWallBottomLeft;
        isTouchingWallLeft = isTouchingWallTopLeft && isTouchingWallBottomLeft;

        bool isTouchingWallTopRight = Physics2D.OverlapCircle(wallCheckTopRight.position, groundCheckRadius, wallLayer);
        bool isTouchingWallBottomRight = Physics2D.OverlapCircle(wallCheckBottomRight.position, groundCheckRadius, wallLayer);
        isTouchingWallRightPartial = isTouchingWallTopRight != isTouchingWallBottomRight;
        isTouchingWallRight = isTouchingWallTopRight && isTouchingWallBottomRight;

        if (isGrounded)
        {
            canDoubleJump = true;
            isWallSliding = false;
            isWallStickAllowed = true;
        }
        else if ((wallJumpEnabled && isTouchingWallLeft && moveInput.x < 0 && isWallStickAllowed) ||
                 (wallJumpEnabled && isTouchingWallRight && moveInput.x > 0 && isWallStickAllowed))
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }
    public bool IsGrounded()
    {
        return isGrounded;
    }

    private void UpdateCoyoteAndJumpBuffer()
    {
        if (isGrounded || isWallSliding)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0)
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void Move()
    {
        if (rb.bodyType == RigidbodyType2D.Kinematic)
            return;
        // Start with the external velocity (e.g., platform influence)
        if (exRb)
            externalVelocity = exRb.velocity;
        Vector2 totalVelocity = externalVelocity;

        if ((isTouchingWallLeft || isTouchingWallLeftPartial) && moveInput.x < 0 && !isGrounded)
        {
            // Prevent moving left when touching the left wall and airborne
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if ((isTouchingWallRight || isTouchingWallRightPartial) && moveInput.x > 0 && !isGrounded)
        {
            // Prevent moving right when touching the right wall and airborne
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            // Normal movement
            rb.velocity = externalVelocity +  new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        }

        if ((moveInput.x > 0 && !facingRight) || (moveInput.x < 0 && facingRight))
        {
            Flip();
        }

        if (!isAlive)
            return;

        if (isGrounded)
        {
            if ((rb.velocity.x > 0 || rb.velocity.x < 0) && Mathf.Abs(moveInput.x) > 0.01f)
            {
                anim.Play("Move");
            }
            else
            {
                anim.Play("Idle");
            }
        }
        else
        {
            if ((rb.velocity.y > 0) )
            {
                if(!anim.GetCurrentAnimatorStateInfo(0).IsName("DoubleJump"))
                    anim.Play("Jump");
            }
            else
            {
                anim.Play("Fall");
            }
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;

        // Flip the sprite by inverting the local scale's X value
        Vector3 scale = model.transform.localScale;
        scale.x *= -1;
        model.transform.localScale = scale;
    }

    private void HandleJump()
    {
        if (isDashing || !jumpEnable || !isAlive)
            return;

        if (jumpBufferCounter > 0 && (coyoteTimeCounter > 0 || canDoubleJump || isTouchingWallLeft || isTouchingWallRight))
        {
            if (coyoteTimeCounter > 0)
            {
                isWallStickAllowed = !(isTouchingWallLeft || isTouchingWallRight);
                Jump(Vector2.up);
                coyoteTimeCounter = 0;
                SoundEffectsManager.Instance.PlaySound(jumpSound);
            }
            else if (canDoubleJump)
            {
                Jump(Vector2.up);
                canDoubleJump = false;
                anim.Play("DoubleJump");
                SoundEffectsManager.Instance.PlaySound(doubleJumpSound);
            }

            jumpBufferCounter = 0;
        }

        if (!jumpHeld && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        if (!isWallStickAllowed && rb.velocity.y <= 0)
            isWallStickAllowed = true;
    }
    public void OnJumpReleased()
    {
        isWallStickAllowed = true;
    }

    private void Jump(Vector2 direction)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0); // Reset vertical velocity for consistent jump height
        rb.AddForce(direction * maxJumpHeight, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        if (wallCheckTopLeft != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(wallCheckTopLeft.position, groundCheckRadius);
        }

        if (wallCheckTopRight != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(wallCheckTopRight.position, groundCheckRadius);
        }
        if (wallCheckBottomLeft != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(wallCheckBottomLeft.position, groundCheckRadius);
        }

        if (wallCheckBottomRight != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(wallCheckBottomRight.position, groundCheckRadius);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Patform") && isGrounded)
        {
            exRb = collision.collider.GetComponent<Rigidbody2D>();
            if (exRb)
                externalVelocity = exRb.velocity;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Patform") && exRb == collision.collider.GetComponent<Rigidbody2D>())
        {
            externalVelocity = Vector2.zero;
            exRb = null;
        }
    }

    private IEnumerator Dash()
    {
        SoundEffectsManager.Instance.PlaySound(dashSound);
        isDashing = true;
        canDash = false;
        anim.Play("Dash");
        health.isInvincible = true;
        rb.gravityScale = 0; // Temporarily disable gravity during dash

        Vector2 dashDirection = facingRight ? Vector2.right : Vector2.left;
        rb.velocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);
        anim.Play("Idle");

        rb.gravityScale = 1; // Restore gravity
        isDashing = false;
        health.isInvincible = false;
        canDash = true;
        dashTimer = dashCooldown;
        evadeProgress.gameObject.SetActive(true);
    }

    private IEnumerator Roll()
    {
        SoundEffectsManager.Instance.PlaySound(rollSound);
        isDashing = true;
        canDash = false;

        anim.Play("Roll");
        health.isInvincible = true;
        Vector2 dashDirection = facingRight ? Vector2.right : Vector2.left;
        rb.velocity = dashDirection * rollSpeed;

        yield return new WaitForSeconds(dashDuration);
        anim.Play("Idle");
        // fix weapon placement if wepaon switch happened mid roll
        GetComponentInChildren<BaseAttack>().gameObject.transform.rotation = Quaternion.identity;

        isDashing = false;
        health.isInvincible = false;
        canDash = true;
        dashTimer = dashCooldown;
        evadeProgress.gameObject.SetActive(true);
    }

    public bool IsDashing()
    {
        return isDashing;
    }
    
    public void DisableInput()
    {
        isInputEnabled = false;
    }

    public void EnableInput()
    {
        isInputEnabled = true;
    }
    public void Stop()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
    }
    public void Resume()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        isInputEnabled = true;
    }
    public IEnumerator PlayeDeath()
    {
        isInputEnabled = false;
        isAlive = false;
        rb.velocity = Vector2.zero;
        moveInput = Vector2.zero;
        weaponPlacement.SetActive(false);

        anim.Play("DeathFall");
        health.isInvincible = true;

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
