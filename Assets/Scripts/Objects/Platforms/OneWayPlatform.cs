using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    public Collider2D solidPlatform;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            solidPlatform.enabled = false;
            PlayerController p = FindAnyObjectByType<PlayerController>();
            if (p.exRb == solidPlatform.GetComponent<Rigidbody2D>())
            {
                p.externalVelocity = Vector2.zero;
                p.exRb = null;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            solidPlatform.enabled = true;
        }
    }
}
