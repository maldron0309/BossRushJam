using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugJumpMovement : MonoBehaviour
{
    public float lowerBound, upperBound;
    public float minForce, maxForce;
    public float jumpForce;

    public bool onGround;
    public float groundCheckRadius = 0.2f;
    public Transform groundCheck;

    public float jumpDelayTime;
    float lastJump;

    Rigidbody2D rb;

    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        lastJump = jumpDelayTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastJump > 0)
        {
            lastJump -= Time.deltaTime;
        }
        else if (onGround)
        {
            Jump();
        }
        else if (onGround == false)
        {
            onGround = CheckGround();
        }

        
        
        
    }

    public bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void Jump()
    {
        lastJump = jumpDelayTime;
        onGround = false;
        float force = Random.Range(minForce, maxForce);
        

        if(this.transform.position.x > upperBound) { force = -force; }
        else if(!(this.transform.position.x < lowerBound)) {
            int v = Random.Range(0, 2);
            if(v >= 1)
            {
                force = -force;
            }
        }Debug.Log(force);

        //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        rb.AddForce(new Vector2(force, jumpForce), ForceMode2D.Impulse);
    }

    
}
