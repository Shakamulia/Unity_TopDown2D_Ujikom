using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; // Kecepatan gerak musuh

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private KnockBack knockback;

    private void Awake()
    {
        // Mengambil referensi ke Rigidbody2D dan komponen KnockBack
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<KnockBack>();
    }

    private void FixedUpdate()
    {
        // Jika sedang terkena knockback, hentikan eksekusi fungsi
        if (knockback.gettingKnockedBack) return;

        // Memindahkan musuh ke arah moveDir dengan kecepatan tertentu
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition)
    {
        // Menghitung arah menuju target dan menyimpannya dalam moveDir
        moveDir = (targetPosition - rb.position).normalized;
    }
}
