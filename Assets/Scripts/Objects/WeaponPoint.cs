using UnityEngine;
using TMPro;

public class WeaponPoint : MonoBehaviour
{
    [SerializeField] private GameObject weaponsInventory;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private PlayerController playerController;

    private bool isPlayerInRange = false;

    private void Start()
    {
        text.gameObject.SetActive(false);
        weaponsInventory.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            OpenInventory();
        }

        if (weaponsInventory.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseInventory();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            text.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            text.gameObject.SetActive(false);
        }
    }

    private void OpenInventory()
    {
        weaponsInventory.SetActive(true);
        playerController.DisableInput();
    }

    private void CloseInventory()
    {
        weaponsInventory.SetActive(false);
        playerController.EnableInput();
    }
}