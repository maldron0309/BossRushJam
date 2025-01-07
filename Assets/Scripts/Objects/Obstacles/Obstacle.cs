using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float damagePerSecond = 20f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                StartCoroutine(DealDamage(playerHealth));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                StopAllCoroutines();
            }
        }
    }

    private IEnumerator DealDamage(PlayerHealth playerHealth)
    {
        while (true)
        {
            int damage = Mathf.RoundToInt(damagePerSecond * Time.deltaTime); 
            playerHealth.TakeDamage(damage);
            yield return null;
        }
    }
}