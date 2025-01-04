using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider easeHealthSlider;
    
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    [SerializeField] private float learpSpeed = 0.01f;

    private void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        
        easeHealthSlider.maxValue = maxHealth;
    }

    private void Update()
    {
        if (healthSlider.value != currentHealth)
        {
            healthSlider.value = currentHealth;
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, currentHealth, learpSpeed);
        }
    }
}
