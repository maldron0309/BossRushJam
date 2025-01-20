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
