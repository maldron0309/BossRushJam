using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 500f;
    float currentHealth;
    BossHealthUI bossHealthUI;
    private BaseBossController bossController;

    void Start()
    {
        currentHealth = maxHealth;
        bossController = GetComponent<BaseBossController>();
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

            bossController.ActivateDoor();
            Destroy(gameObject);
        }

        bossHealthUI.SetHealth(currentHealth);
    }

    public float PercentageHealth()
    {
        return currentHealth / maxHealth;
    }
}