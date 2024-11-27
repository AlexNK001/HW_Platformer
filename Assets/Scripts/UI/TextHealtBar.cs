using TMPro;
using UnityEngine;

public class TextHealtBar : HeartListener
{
    [SerializeField] private TMP_Text _health;

    private Heart _heart;
    private float _maxHealth;

    private void OnDestroy()
    {
        _heart.HealthChanged -= DisplayHealth;
    }

    public override void Initilization(Heart heart, float maxHealth, float currentHealth)
    {
        _heart = heart;
        _maxHealth = maxHealth;
        DisplayHealth(currentHealth);
        _heart.HealthChanged += DisplayHealth;
    }

    private void DisplayHealth(float health)
    {
        _health.text = $"{Round(health)}/{Round(_maxHealth)}";
    }

    private string Round(float value)
    {
        return $"{value:F2}";
    }
}
