using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private int potionCount = 3;
    [SerializeField] private int maxPotions = 5; 
    [SerializeField] private int healAmount = 20; 

    [SerializeField] TextMeshProUGUI potionCountText; 

    private bool potionSystemActive = false; 

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (potionSystemActive && Input.GetKeyDown(KeyCode.R) && potionCount > 0 && currentHealth < maxHealth)
        {
            UsePotion();
            Debug.Log("Upd");
        }
    }

    public void ActivatePotionSystem()
    {
        potionSystemActive = true;
        UpdatePotionUI();
    }

    public void AddPotion()
    {
        if (potionCount < maxPotions)
        {
            potionCount++;
            UpdatePotionUI();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 

        FindObjectOfType<HealthUI>().SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UsePotion()
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        potionCount--;
        UpdatePotionUI();

        FindObjectOfType<HealthUI>().SetHealth(currentHealth);
    }

    void UpdatePotionUI()
    {
        potionCountText.text = $"x{potionCount}";
    }

    void Die()
    {
        Debug.Log("Player Died!");
    }
}