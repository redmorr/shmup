using TMPro;
using UnityEngine;

public class HealthPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Health health;

    private void Awake()
    {
        healthText.SetText("");
        health.OnHealthChanged += UpdateDisplayedHealth;
    }

    private void UpdateDisplayedHealth(int newHealthValue)
    {
        healthText.SetText(newHealthValue.ToString());
    }

    private void OnDisable()
    {
        health.OnHealthChanged -= UpdateDisplayedHealth;
    }
}
