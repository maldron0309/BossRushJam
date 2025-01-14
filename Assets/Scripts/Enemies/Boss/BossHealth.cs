using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 500f;
    [SerializeField] private GameObject door;
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
            Debug.Log("Boss is dead!");

            ActivateDoor();
            Destroy(gameObject);
            
        }

        bossHealthUI.SetHealth(currentHealth);
    }

    public float PercentageHealth()
    {
        return currentHealth / maxHealth;
    }

    private void ActivateDoor()
    {
        if (door != null)
        {
            door.SetActive(true);
            Debug.Log("Door is active");
        }
    }
}