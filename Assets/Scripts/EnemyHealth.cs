using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3; // Jumlah HP awal musuh

    private int currentHealth; // HP saat ini
    private KnockBack knockback; // Referensi ke skrip KnockBack

    private void Awake()
    {
        knockback = GetComponent<KnockBack>(); // Mengambil komponen KnockBack
    }

    private void Start()
    {
        currentHealth = startingHealth; // Set HP awal saat musuh muncul
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Mengurangi HP musuh
        knockback.GetKnockedBack(PlayerController.Instance.transform, 15f); // Memberikan efek knockback
        DetectDeath(); // Mengecek apakah musuh sudah mati
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // Menghapus musuh dari game jika HP habis
        }
    }
}
