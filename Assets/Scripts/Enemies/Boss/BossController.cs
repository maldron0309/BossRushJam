using UnityEngine;

class BossController : MonoBehaviour
{
    [SerializeField] Transform roomBounds;
    [SerializeField] Vector2 boundsSize;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float fireRate = 2f;
    [SerializeField] float directionChangeInterval = 2f;

    Vector2 direction;
    bool isBattleStarted = false;
    float fireCooldown = 0f;
    float directionChangeTimer = 0f;

    void Start()
    {
        direction = GetRandomDirection();
    }

    void Update()
    {
        if (!isBattleStarted) return;

        MoveRandomly();
        FireAtPlayer();
    }

    public void StartBossBattle()
    {
        isBattleStarted = true;
    }

    void MoveRandomly()
    {
        directionChangeTimer += Time.deltaTime;
        if (directionChangeTimer >= directionChangeInterval)
        {
            direction = GetRandomDirection();
            directionChangeTimer = 0f;
        }

        Vector3 newPosition = transform.position + (Vector3)direction * moveSpeed * Time.deltaTime;
        newPosition = ClampPositionToBounds(newPosition);
        transform.position = newPosition;
    }

    Vector2 GetRandomDirection()
    {
        return Random.insideUnitCircle.normalized;
    }

    Vector3 ClampPositionToBounds(Vector3 position)
    {
        float xMin = roomBounds.position.x - boundsSize.x / 2f;
        float xMax = roomBounds.position.x + boundsSize.x / 2f;
        float yMin = roomBounds.position.y - boundsSize.y / 2f;
        float yMax = roomBounds.position.y + boundsSize.y / 2f;

        position.x = Mathf.Clamp(position.x, xMin, xMax);
        position.y = Mathf.Clamp(position.y, yMin, yMax);

        return position;
    }

    void FireAtPlayer()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            fireCooldown = fireRate;
        }
    }

    void OnDrawGizmos()
    {
        if (roomBounds != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(roomBounds.position, new Vector3(boundsSize.x, boundsSize.y, 1f));
        }
    }
}
