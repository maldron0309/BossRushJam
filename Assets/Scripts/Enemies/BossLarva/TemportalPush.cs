using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemportalPush : MonoBehaviour
{
    public Vector2 pushVector;
    Rigidbody2D rb;
    private float life;
    void Start()
    {
        Destroy(this, 0.25f);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, 0);
        life = 0.25f;
        GetComponent<PlayerController>().jumpEnable = false;
    }
    private void OnDestroy()
    {
        GetComponent<PlayerController>().jumpEnable = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        life -= Time.deltaTime;
        rb.velocity += life * 4 * pushVector;
    }
}
