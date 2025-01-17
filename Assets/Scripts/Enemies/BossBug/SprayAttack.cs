using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayAttack : MonoBehaviour
{
    public float jumpForce = 10f; // Force applied for jumping
    public float maxHeight = 5f; // Maximum height the object can reach before stopping its upward motion
    //public float minX, maxX;

    [SerializeField] GameObject projectile;
    

    private Rigidbody2D rb;
    private Collider2D cl;
    private bool attacking;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        cl = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Check if the object has reached the maximum height
        if (attacking && transform.position.y >= maxHeight)
        {
            rb.velocity = new Vector2(0, 0);
            rb.simulated = false;
            cl.enabled = false;
        }
    }


    public IEnumerator BeginAttack()
    {
        attacking = true;

        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        

        yield return new WaitForSeconds(0.5f);


        for(int i = 0; i < 10; i++)
        {
            GameObject b = Instantiate(projectile, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }

        rb.simulated = true;
        cl.enabled = true;
        attacking = false;
    }
}
