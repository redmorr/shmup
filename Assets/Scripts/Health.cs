using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int initalHealthPoints;

    //public int HP { private set; get; }
    public int HealthPoints;

    public event Action OnHealthDepleted;
    public event Action<int> OnHealthChanged;

    private void OnEnable()
    {
        HealthPoints = initalHealthPoints;
        OnHealthChanged?.Invoke(HealthPoints);
    }

    public void DealDamage(int amount)
    {
        if (HealthPoints > 0)
        {
            HealthPoints -= amount;
            OnHealthChanged?.Invoke(HealthPoints);

            if (HealthPoints <= 0)
            {
                OnHealthDepleted?.Invoke();
            }
        }
    }
}
