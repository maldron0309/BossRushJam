using UnityEngine;

public class SpinningPlatforms : MonoBehaviour
{
    [Header("Spinning Settings")]
    public float rotationSpeed = 50f; // Speed at which the platforms spin (degrees per second)

    private Rigidbody2D rb;
    private Vector3 currentRotation;
    private Vector3 rotationStep;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rotationStep = Vector3.forward * rotationSpeed * Time.deltaTime;
    }
    public void FixedUpdate()
    {
        rb.angularVelocity = rotationSpeed;// (rotation);
    }

    private void Update()
    {

    }
    private void OnCollisionStay2D(Collision2D collision)
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {

    }
}
