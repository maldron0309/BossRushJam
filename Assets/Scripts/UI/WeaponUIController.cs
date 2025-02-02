using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUIController : MonoBehaviour
{
    public TextMeshProUGUI weaponText; // 무기 정보를 표시할 UI Text
    public WeaponsInventory weaponsInventory; // WeaponsInventory를 참조

    private void Start()
    {
        // weaponsInventory가 할당되지 않은 경우 Player를 통해 가져오기
        //if (weaponsInventory == null)
        //{
        //    PlayerController player = FindObjectOfType<PlayerController>();
        //    if (player != null)
        //    {
        //        weaponsInventory = player.GetComponent<WeaponsInventory>();
        //    }
        //}
        weaponsInventory = WeaponsInventory.instance;
        // 초기 텍스트 업데이트
        UpdateWeaponText();
    }

    private void Update()
    {
        // 현재 무기가 바뀌는 즉시 UI 업데이트
        UpdateWeaponText();
    }

    private void UpdateWeaponText()
    {
        if (weaponsInventory != null && weaponText != null)
        {
            // WeaponsInventory에서 현재 무기 이름 가져오기
            string currentWeaponName = weaponsInventory.weapons[weaponsInventory.currentIdx].weaponName;
            weaponText.text = $"CURRENT WEAPON: {currentWeaponName.ToUpper()}";
        }
    }
}