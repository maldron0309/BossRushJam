using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGirlAirSlash : MonoBehaviour
{
    public float bigJump = 50;
    public float airJump = 50;
    public int airBounces = 2;
    public float fallTriggerSpeed = 2f;
    public Transform fallHeight;
    public GameObject bulletPrefab;

    private int airBounceCounter;
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

        if(rb.velocity.y < -fallTriggerSpeed && fallHeight.position.y > transform.position.y && !boss.IsGrounded() && airBounceCounter < airBounces)
        {
            Jump(airJump);
            airBounceCounter++;
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }

        if (boss.IsGrounded() && airBounceCounter == airBounces)
        {
            isStarted = false;
            boss.isPerformingAction = false;
            Debug.Log("isPerformingAction = false");
        }
    }
    public void BeginAttack()
    {
        Jump(bigJump);
        airBounceCounter = 0;
        isStarted = true;
        boss.isPerformingAction = true;
        Debug.Log("isPerformingAction = true");
    }
    public void Jump(float jumpForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
