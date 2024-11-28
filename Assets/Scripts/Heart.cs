using System;
using UnityEngine;

public class Heart : MonoBehaviour
{
    protected float MaxHealth;
    protected float CurrentHealth;

    public Action<float> HealthChanged;
    public Action Died;

    public bool IsAlive => CurrentHealth > 0f;

    public virtual void Initilization(float maxHealt, float currentHealt)
    {
        MaxHealth = maxHealt;
        CurrentHealth = currentHealt;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        HealthChanged?.Invoke(CurrentHealth);

        if (CurrentHealth < 0)
        {
            Died.Invoke();
        }
    }
}
