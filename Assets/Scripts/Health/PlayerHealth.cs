using UnityEngine;

class PlayerHealth : MonoBehaviour, IHealable
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] EstusFlask estusFlask;
    [SerializeField] float estusFlaskHealAmount = 50f;

    float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UseEstusFlask();
        }
    }

    void UseEstusFlask()
    {
        estusFlask.UseFlask(this, estusFlaskHealAmount);
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log($"Healed! Current Health: {currentHealth}");
    }

    public void IncreaseEstusFlask(int amount)
    {
        estusFlask.IncreaseFlask(amount);
        Debug.Log($"Increased Estus Flasks by {amount}");
    }

    public void IncreaseEstusFlaskHeal(float amount)
    {
        estusFlaskHealAmount += amount;
        Debug.Log($"Increased Estus Flask Heal Amount by {amount}");
    }
}