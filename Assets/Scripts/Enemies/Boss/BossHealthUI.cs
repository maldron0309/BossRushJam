using UnityEngine;
using UnityEngine.UI;

class BossHealthUI : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider easeHealthSlider;
    [SerializeField] float lerpSpeed = 0.01f;

    float maxHealth;
    float currentHealth;
    
    void Update()
    {
        if (healthSlider.value != currentHealth)
        {
            healthSlider.value = currentHealth;
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, currentHealth, lerpSpeed);
        }
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
        currentHealth = health;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        easeHealthSlider.maxValue = maxHealth;
        easeHealthSlider.value = currentHealth;
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
    }
}