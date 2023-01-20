using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int initalHealthPoints;

    private int healthPoints;

    public event Action OnHealthDepleted;
    public event Action<int> OnHealthChanged;

    private void OnEnable()
    {
        healthPoints = initalHealthPoints;
        OnHealthChanged?.Invoke(healthPoints);
    }

    public void DealDamage(int amount)
    {
        if (healthPoints > 0)
        {
            healthPoints -= amount;
            OnHealthChanged?.Invoke(healthPoints);

            if (healthPoints <= 0)
            {
                OnHealthDepleted?.Invoke();
            }
        }
    }
}
