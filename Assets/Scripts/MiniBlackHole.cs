using UnityEngine;

public class MiniBlackHole : MonoBehaviour
{
    [Header("Black Hole Settings")]
    public float pullRadius = 5f; // Radius within which the black hole affects projectiles
    public float pullStrength = 10f; // Strength of the pull force

    private void FixedUpdate()
    {
        // Find all colliders within the pull radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pullRadius);

        foreach (Collider2D collider in colliders)
        {
            // Check if the collider belongs to a projectile
            Projectile projectile = collider.GetComponent<Projectile>();

            if (projectile != null)
            {
                PullProjectile(projectile);
            }
        }
    }

    private void PullProjectile(Projectile projectile)
    {
        // Calculate direction towards the black hole center
        Vector2 directionToCenter = (Vector2)transform.position - (Vector2)projectile.transform.position;

        // Normalize the direction and scale by pull strength
        Vector2 pullForce = directionToCenter.normalized * pullStrength;

        // Add the pull force to the projectile's velocity
        projectile.ApplyForce(pullForce * Time.fixedDeltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, pullRadius);
    }
}