using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1; // Besar damage yang diberikan

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Mengecek apakah objek yang terkena adalah musuh yang memiliki EnemyHealth
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damageAmount); // Memberikan damage ke musuh
        }
    }
}
