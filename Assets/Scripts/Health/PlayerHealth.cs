using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private HealthUI healthUI;

    void Start()
    {
        currentHealth = maxHealth;
        healthUI = FindObjectOfType<HealthUI>();
        healthUI.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player is dead!");
        }

        healthUI.SetHealth(currentHealth);
    }
    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);

        healthUI.SetHealth(currentHealth);
    }
}