using UnityEngine;
using UnityEngine.UI; // For Slider
using TMPro;         // For Gold Text

public class UIManager : MonoBehaviour
{
    [Header("Health UI")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Economy UI")]
    [SerializeField] private TextMeshProUGUI goldText;

    /* This script manages the player's health and gold UI elements. 
    It listens for changes in the player's health and gold through events in the PlayerStats class, and updates the UI accordingly.
     The health slider and text are updated to reflect the player's current health, 
     while the gold text displays the total amount of gold the player has.*/
    private void OnEnable()
    {
        // Subscribe to the events when the UI is active
        //PlayerStats.Instance.OnHealthChanged += UpdateHealthUI;
       // PlayerStats.Instance.OnGoldChanged += UpdateGoldUI;
    }

    /*
        Unsubscribe from events when the UI is disabled to prevent memory leaks and errors.
        This ensures that the UIManager does not try to update the UI when it is not active, 
        which could lead to null reference exceptions 
        if the PlayerStats instance is destroyed or if the UIManager is disabled.
     */
    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks/errors
        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.OnHealthChanged -= UpdateHealthUI;
            PlayerStats.Instance.OnGoldChanged -= UpdateGoldUI;
        }
    }

    private void UpdateHealthUI(int current, int max)
    {
        healthSlider.maxValue = max;
        healthSlider.value = current;
        healthText.text = $"{current} / {max}";
    }

    private void UpdateGoldUI(int totalGold)
    {
        goldText.text = "Gold: " + totalGold.ToString();
    }
}