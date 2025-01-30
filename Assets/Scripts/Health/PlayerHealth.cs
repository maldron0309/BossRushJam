using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public bool isInvincible = false;
    private float currentHealth;
    private HealthUI healthUI;
    private Vector3 initialPosition;
    private Color originalColor;
    private bool isFlashing;
    public SpriteRenderer bossSprite;

    void Start()
    {
        currentHealth = maxHealth;
        healthUI = FindObjectOfType<HealthUI>();
        healthUI.SetMaxHealth(maxHealth);
        originalColor = bossSprite.color;

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

        if (!isFlashing)
            StartCoroutine(FlashRed());

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
    private IEnumerator FlashRed()
    {
        isFlashing = true;

        bossSprite.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        // Restaurar a cor original
        bossSprite.color = originalColor;

        isFlashing = false;
    }
}