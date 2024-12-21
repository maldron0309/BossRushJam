using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private Rigidbody2D rb;
    private Vector2 moveInput;
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
    public bool isWallStickAllowed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateGroundAndWallChecks();
        UpdateCoyoteAndJumpBuffer();
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
                float snapOffset = hit.distance - (wallSnapDistance / 2); // Adjust based on wallSnapDistance
                transform.position = new Vector3(transform.position.x + snapOffset * wallDirection, transform.position.y, transform.position.z);
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else if (context.canceled)
        {
            OnJumpReleased();
        }

        jumpHeld = context.performed;
    }

    public void OnAttack1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Attack action called!");
        }
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
