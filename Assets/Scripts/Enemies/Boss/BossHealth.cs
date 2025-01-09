using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 500f;
    float currentHealth;
    BossHealthUI bossHealthUI;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Initialize(BossHealthUI ui)
    {
        bossHealthUI = ui;
        bossHealthUI.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Boss is dead!");
        }

        bossHealthUI.SetHealth(currentHealth);
    }
    public float PercentageHealth()
    {
        return currentHealth / maxHealth;
    }
}