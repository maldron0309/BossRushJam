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
        //currentRotation = rb.rotation.;
        rotationStep = Vector3.forward * rotationSpeed * Time.deltaTime;
        //rb.isKinematic = true;
    }
    public void FixedUpdate()
    {
        //currentRotation += rotationStep;

        //var rotation = Quaternion.Euler(currentRotation);
        //rb.AddTorque(rotationSpeed);// (rotation);
        rb.angularVelocity = rotationSpeed;// (rotation);
    }

    private void Update()
    {
        // Rotate the parent object around its own Z-axis
        //transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.collider.CompareTag("Player"))
        //{
        //    Rigidbody2D playerRigidbody = collision.collider.GetComponent<Rigidbody2D>();
        //    if (playerRigidbody != null)
        //    {
        //        // Calculate the tangential velocity of the platform at the player's position
        //        Vector3 platformVelocity = transform.right * rotationSpeed * Mathf.Deg2Rad * Vector3.Distance(transform.position, collision.transform.position);
        //        playerRigidbody.velocity += new Vector2(platformVelocity.x, platformVelocity.y);
        //    }
        //}
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.CompareTag("Player"))
        //{
        //    Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();
        //    if (playerRigidbody != null)
        //    {
        //        // Calculate the tangential velocity of the platform at the player's position
        //        Vector3 platformVelocity = transform.right * rotationSpeed * Mathf.Deg2Rad * Vector3.Distance(transform.position, collision.transform.position);
        //        playerRigidbody.velocity += new Vector2(platformVelocity.x, platformVelocity.y);
        //    }
        //}
    }
}
