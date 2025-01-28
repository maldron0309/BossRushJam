using System.Collections;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 500f;
    [SerializeField] private GameObject door;
    [SerializeField] private Animator bossAnim;
    [SerializeField] private SpriteRenderer bossSprite;

    private Color originalColor;
    private bool isFlashing;
    float currentHealth;
    BossHealthUI bossHealthUI;

    void Start()
    {
        currentHealth = maxHealth;
        originalColor = bossSprite.color;
    }

    public void Initialize(BossHealthUI ui)
    {
        bossHealthUI = ui;
        bossHealthUI.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (!isFlashing)
            StartCoroutine(FlashRed());
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            ActivateDoor();
            GetComponent<BaseBossController>().OnDefeat();
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
            door.SetActive(false);
            Debug.Log("Door is active");
        }
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