using UnityEngine;

public class HealthPotionActivator : MonoBehaviour
{
    [SerializeField] private GameObject potionUI;
    [SerializeField] private PlayerHealth playerHealth;
    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ActivatePotionSystem();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Press E to pick up the potion system!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void ActivatePotionSystem()
    {
        potionUI.SetActive(true);
        playerHealth.ActivatePotionSystem();
        Debug.Log("Potion system activated!");
        Destroy(gameObject);
    }
}