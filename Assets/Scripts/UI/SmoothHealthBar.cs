using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthBar : HeartListener
{
    [SerializeField] private Slider _mainSlider;
    [SerializeField] private Slider _subSlider;
    [SerializeField, Min(0f)] private float _rateDecrease;
    [SerializeField, Min(0f)] private float _timeBeforeDecrease;

    private Heart _heart;
    private float _maxHealth;
    private Coroutine _decrease;
    private WaitForSeconds _delayBeforeDecrease;

    private void Awake()
    {
        _delayBeforeDecrease = new WaitForSeconds(_timeBeforeDecrease);
    }

    private void OnDisable()
    {
        _subSlider.value = _mainSlider.value;
    }

    private void OnDestroy()
    {
        _heart.HealthChanged -= DisplayHealth;
    }

    public override void Initilization(Heart heart, float maxHealth, float currentHealth)
    {
        _heart = heart;
        _heart.HealthChanged += DisplayHealth;
        _maxHealth = maxHealth;
        _subSlider.value = GetNormalizedHealth(currentHealth);
        _mainSlider.value = GetNormalizedHealth(currentHealth);
    }

    private void DisplayHealth(float newHealthValue)
    {
        float finalNormalizedHealthValue = GetNormalizedHealth(newHealthValue);

        if (_decrease != null)
            StopCoroutine(_decrease);

        if (_mainSlider.value > finalNormalizedHealthValue)
            _decrease = StartCoroutine(Decrease(newHealthValue));
        else
            _subSlider.value = finalNormalizedHealthValue;

        _mainSlider.value = finalNormalizedHealthValue;
    }

    private IEnumerator Decrease(float newHealthValue)
    {
        float currentHealth = Mathf.Lerp(0f, _maxHealth, _subSlider.value);
        float delta = currentHealth - newHealthValue;

        if (Mathf.Approximately(_subSlider.value, _mainSlider.value))
            yield return _delayBeforeDecrease;

        while (delta > 0)
        {
            _subSlider.value = Mathf.InverseLerp(0f, _maxHealth, newHealthValue + delta);
            delta -= Time.deltaTime * _rateDecrease;
            yield return null;
        }

        _subSlider.value = _mainSlider.value;
    }

    private float GetNormalizedHealth(float currentHealth)
    {
        return Mathf.InverseLerp(0f, _maxHealth, currentHealth);
    }
}
