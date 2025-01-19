using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider easeHealthSlider;
    
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [SerializeField] private float lerpSpeed = 0.01f;

    private void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        
        easeHealthSlider.maxValue = maxHealth;
        easeHealthSlider.value = currentHealth;
    }

    private void Update()
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
        healthSlider.maxValue = maxHealth;
        easeHealthSlider.maxValue = maxHealth;
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
    }
}