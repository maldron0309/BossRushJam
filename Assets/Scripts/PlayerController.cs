using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Jumping")]
    public float minJumpHeight = 2f;
    public float maxJumpHeight = 4f;
    public float jumpBufferTime = 0.2f;
    public float coyoteTime = 0.2f;
    public float wallJumpForce = 7f;
    public float wallJumpUpwardForce = 10f;
    public float wallSnapDistance = 0.5f;

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
    public GameObject regularProjectilePrefab; // Regular attack projectile prefab
    public GameObject chargedProjectilePrefab; // Charged attack projectile prefab
    public Transform projectileSpawnPoint; // Where projectiles spawn

    public float attackCooldown = 0.1f; // Cooldown between regular attacks
    public float chargeTime = 1.0f; // Time required to charge an attack

    private float nextAttackCounter; // Time of the last attack
    private bool isCharging; // Whether the player is charging an attack
    private bool isChargeComplete; // Whether the charge attack is ready
    private float chargeStartTime; // When the charge started
    private bool isAttackBuffered; // Whether an attack input is buffered

    private Rigidbody2D rb;
    private Vector2 moveInput; 
    private bool facingRight = true;
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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalSpeed = moveSpeed;
    }

    private void Update()
    {
        UpdateGroundAndWallChecks();
        UpdateCoyoteAndJumpBuffer();

        if (isCharging && !isChargeComplete && Time.time - chargeStartTime >= chargeTime)
        {
            isChargeComplete = true;
            Debug.Log("Charge attack ready!");
        }
        if (nextAttackCounter > 0)
            nextAttackCounter -= Time.deltaTime;
        else
        {
            if (isAttackBuffered)
            {
                isAttackBuffered = false;
                FireProjectile(regularProjectilePrefab);
            }
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (isWallSliding)
        {
            rb.gravityScale = 0;
            WallSlide();
        }
        else
            rb.gravityScale = 1;
        HandleJump();
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
                // Snap the player to the wall by adjusting their position
                //float snapOffset = hit.distance - (wallSnapDistance / 2); // Adjust based on wallSnapDistance
                //transform.position = new Vector3(transform.position.x + snapOffset * wallDirection, transform.position.y, transform.position.z);


                //float snapOffset = hit.point; // Adjust based on wallSnapDistance
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
            if (nextAttackCounter <= 0)
            {
                Debug.Log("Attack action called!");
                FireProjectile(regularProjectilePrefab);
            }
            else
                isAttackBuffered = true;
        }
        else if (!isPressed)
        {
            OnAttackReleased();
        }
    }
    public void OnAttackPressed()
    {
        // Start charging the attack
        isCharging = true;
        chargeStartTime = Time.time;
        isChargeComplete = false;
    }

    public void OnAttackReleased()
    {
        if (isCharging)
        {
            isCharging = false;

            // Check if the charge was completed
            if (isChargeComplete)
            {
                FireProjectile(chargedProjectilePrefab);
                isChargeComplete = false;
            }
        }
    }

    private void FireProjectile(GameObject projectilePrefab)
    {
        // Stop movement briefly
        StartCoroutine(TemporaryStop());

        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        // Set its direction based on the player's facing direction
        Vector2 direction = new Vector2(facingRight ? 1 : -1, 0);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * 20f; // Adjust speed as needed

        // Record the time of this attack
        nextAttackCounter = attackCooldown;

        OnAttackPressed();
        // Check for buffered attack
        //if (isAttackBuffered)
        //{
        //    isAttackBuffered = false;
        //    OnAttackPressed();
        //}
    }
    private IEnumerator TemporaryStop()
    {
        moveSpeed = 0f;
        yield return new WaitForSeconds(attackCooldown);
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
        else if ((isTouchingWallLeft && moveInput.x < 0 && isWallStickAllowed) ||
                 (isTouchingWallRight && moveInput.x > 0 && isWallStickAllowed))
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
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
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        }

        if ((moveInput.x > 0 && !facingRight) || (moveInput.x < 0 && facingRight))
        {
            Flip();
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
        if (jumpBufferCounter > 0 && (coyoteTimeCounter > 0 || canDoubleJump || isTouchingWallLeft || isTouchingWallRight))
        {
            if (coyoteTimeCounter > 0)
            {
                isWallStickAllowed = !(isTouchingWallLeft || isTouchingWallRight);
                Jump(Vector2.up);
                coyoteTimeCounter = 0;
            }
            else if (canDoubleJump)
            {
                Jump(Vector2.up);
                canDoubleJump = false;
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
}
