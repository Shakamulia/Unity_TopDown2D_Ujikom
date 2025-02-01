using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;

    private int CurrentHealth;

    private void Start()
    {
        CurrentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log(CurrentHealth);
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
