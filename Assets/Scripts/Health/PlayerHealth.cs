using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public bool isInvincible = false;
    private float currentHealth;
    private HealthUI healthUI;
    private Vector3 initialPosition;

    void Start()
    {
        currentHealth = maxHealth;
        healthUI = FindObjectOfType<HealthUI>();
        healthUI.SetMaxHealth(maxHealth);

        initialPosition = transform.position;
    }

    public void TakeDamage(float damage)
    {
        // for dash and roll
        if (isInvincible) 
            return;

        currentHealth -= damage;
        healthUI.SetHealth(currentHealth);
        RoomCamera.instance.Shake(0.1f, .15f);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            StartCoroutine(PlayerController.instance.PlayeDeath());
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        healthUI.SetHealth(currentHealth);
    }

    void Respawn()
    {
        StartCoroutine(GetComponent<PlayerController>().PlayeDeath());
    }
}