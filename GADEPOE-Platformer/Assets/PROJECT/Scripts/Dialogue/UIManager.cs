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

    private void OnEnable()
    {
        // Subscribe to the events when the UI is active
        PlayerStats.Instance.OnHealthChanged += UpdateHealthUI;
        PlayerStats.Instance.OnGoldChanged += UpdateGoldUI;
    }

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