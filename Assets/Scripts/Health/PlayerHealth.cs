using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public bool isInvincible = false;
    private float currentHealth;
    private HealthUI healthUI;
    private Vector3 initialPosition;
    [SerializeField] private GameOver gameOver;

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

        if (currentHealth <= 0)
        {
            if(gameOver != null)
            {
                gameOver.IsGameOver();
            }
            currentHealth = 0;
            Debug.Log("Player is dead!");
            
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
        //currentHealth = maxHealth;
        //healthUI.SetHealth(currentHealth);

        //transform.position = initialPosition;

        //Debug.Log("Player has respawned");
    }
}