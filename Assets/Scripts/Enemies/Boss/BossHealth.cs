using System.Collections;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 500f;
    [SerializeField] private GameObject door;
    [SerializeField] private Animator bossAnim;
    [SerializeField] private float deathTime;
    [SerializeField] private GuardBossSpinningShield shields;
    [SerializeField] private Collider2D bossCol;
    [SerializeField] private Rigidbody2D bossRb;
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
            Debug.Log("Boss is dead!");
            shields.DestroyAllShields();
            bossAnim.SetTrigger("Death");
            ActivateDoor();
            bossRb.constraints = RigidbodyConstraints2D.FreezePositionY;
            bossCol.enabled = false;
            //Destroy(gameObject, deathTime);

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