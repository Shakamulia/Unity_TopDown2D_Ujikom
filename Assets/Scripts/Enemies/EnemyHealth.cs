using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3; // Jumlah HP awal musuh
    [SerializeField] private GameObject deathVFXPrefab;

    private int currentHealth; // HP saat ini
    private KnockBack knockback; // Referensi ke skrip KnockBack
    private Flash flash;

    private void Awake()
    {
        flash = GetComponent<Flash>();
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
        StartCoroutine(flash.FlashRoutine());
    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject); // Menghapus musuh dari game jika HP habis
        }
    }
}
