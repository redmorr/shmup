using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int initalHealthPoints;

    //public int HP { private set; get; }
    public int HealthPoints;

    public event Action OnHealthDepleted;

    private void Start()
    {
        HealthPoints = initalHealthPoints;
    }

    public void DealDamage(int amount)
    {
        HealthPoints -= amount;

        if (HealthPoints <= 0)
        {
            OnHealthDepleted?.Invoke();
        }
    }

}
