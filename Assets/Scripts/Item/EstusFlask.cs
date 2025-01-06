using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EstusFlask : MonoBehaviour
{
    [SerializeField] private int maxFlask;
    [SerializeField] private TextMeshProUGUI flaskText;
    
    private int currentFlask;

    private void Start()
    {
        currentFlask = maxFlask;
        UpdateFlaskUI();
    }

    private void UpdateFlaskUI()
    {
        flaskText.text = currentFlask.ToString();
    }

    public bool UseFlask(IHealable target, float amount)
    {
        if (currentFlask > 0)
        {
            currentFlask--;
            UpdateFlaskUI();
            target.Heal(amount);
            return true;
        }
        return false;
    }

    public void RefillFlask()
    {
        currentFlask = maxFlask;
        UpdateFlaskUI();
    }

    public void IncreaseFlask(int amount)
    {
        maxFlask += amount;
        currentFlask += amount;
        UpdateFlaskUI();
    }
}
