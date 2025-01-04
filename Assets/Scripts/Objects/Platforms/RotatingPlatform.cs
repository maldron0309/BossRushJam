using UnityEngine;

class RotatingPlatform : MonoBehaviour
{
    [SerializeField] Transform centerPoint;
    [SerializeField] float rotationSpeed = 30.0f;
    [SerializeField] float radius = 5.0f;
    [SerializeField] float initialAngle = 0.0f;

    float angle;

    void Start()
    {
        angle = initialAngle;
        UpdatePlatformPosition();
    }

    void Update()
    {
        angle += rotationSpeed * Time.deltaTime;
        UpdatePlatformPosition();
    }

    void UpdatePlatformPosition()
    {
        float rad = Mathf.Deg2Rad * angle;
        float x = centerPoint.position.x + Mathf.Cos(rad) * radius;
        float y = centerPoint.position.y + Mathf.Sin(rad) * radius;
        transform.position = new Vector3(x, y, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}