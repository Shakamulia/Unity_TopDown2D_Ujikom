using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool gettingKnockedBack { get; private set; }

    [SerializeField] private float knockBackTime = .2f; // Durasi knockback

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Mengambil komponen Rigidbody2D
    }

    public void GetKnockedBack(Transform damageSource, float knockBackThrust)
    {
        Debug.Log("Knockback applied!"); // Debugging log
        gettingKnockedBack = true; // Menandai bahwa objek sedang terkena knockback

        // Menghitung arah knockback berdasarkan posisi damage source
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;

        // Menerapkan gaya knockback dengan mode impulse (langsung memberikan dorongan)
        rb.AddForce(difference, ForceMode2D.Impulse);

        // Memulai coroutine untuk mengatur durasi knockback
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        // Menunggu selama knockBackTime sebelum mengakhiri knockback
        yield return new WaitForSeconds(knockBackTime);

        rb.velocity = Vector2.zero; // Menghentikan pergerakan setelah knockback selesai
        gettingKnockedBack = false; // Menandai bahwa knockback telah berakhir
    }
}
