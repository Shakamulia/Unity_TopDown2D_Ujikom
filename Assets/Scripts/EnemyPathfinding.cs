using System.Collections; // Mengimpor pustaka untuk menggunakan koleksi dan IEnumerator (untuk coroutine)
using System.Collections.Generic; // Mengimpor pustaka untuk koleksi generik
using UnityEngine; // Mengimpor pustaka Unity yang digunakan dalam pengembangan game

public class EnemyPathfinding : MonoBehaviour // Mendeklarasikan kelas EnemyPathfinding yang mewarisi MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; // Kecepatan gerakan musuh, dapat diset melalui inspector di Unity

    private Rigidbody2D rb; // Variabel untuk menyimpan referensi ke komponen Rigidbody2D (digunakan untuk pergerakan fisika)
    private Vector2 moveDir; // Variabel untuk menyimpan arah pergerakan musuh (target posisi)

    private void Awake() {
        // Fungsi ini dipanggil saat objek pertama kali diinisialisasi
        rb = GetComponent<Rigidbody2D>(); // Mengambil referensi ke komponen Rigidbody2D pada objek ini
    }

    private void FixedUpdate() {
        // Fungsi ini dipanggil secara teratur pada interval tetap (lebih tepat untuk manipulasi fisika)
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime)); 
        // Menggerakkan posisi Rigidbody2D dengan kecepatan yang disesuaikan dengan waktu fixedDeltaTime
    }

    public void MoveTo(Vector2 targetPosition) {
        // Fungsi ini dipanggil untuk mengatur tujuan gerakan musuh
        moveDir = (targetPosition - rb.position).normalized; 
        // Menghitung arah gerakan dari posisi saat ini ke targetPosition, lalu menormalisasi vektor untuk memastikan gerakan konsisten
    }
}
