using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f; // Kecepatan pergerakan pemain, dapat diset melalui inspector di Unity

    private PlayerControls playerControls; // Variabel untuk menyimpan referensi ke input kontrol pemain
    private Vector2 movement; // Variabel untuk menyimpan arah pergerakan pemain
    private Rigidbody2D rb; // Variabel untuk menyimpan referensi ke komponen Rigidbody2D (untuk pergerakan fisika)
    private Animator myAnimator; // Variabel untuk menyimpan referensi ke komponen Animator (untuk animasi)
    private SpriteRenderer mySpriteRender; // Variabel untuk menyimpan referensi ke komponen SpriteRenderer (untuk pengaturan sprite)

    private void Awake() {
        // Fungsi ini dipanggil saat objek pertama kali diinisialisasi
        playerControls = new PlayerControls(); // Membuat instance dari PlayerControls untuk menangani input
        rb = GetComponent<Rigidbody2D>(); // Mengambil referensi ke komponen Rigidbody2D pada objek ini
        myAnimator = GetComponent<Animator>(); // Mengambil referensi ke komponen Animator pada objek ini
        mySpriteRender = GetComponent<SpriteRenderer>(); // Mengambil referensi ke komponen SpriteRenderer pada objek ini
    }

    private void OnEnable() {
        // Fungsi ini dipanggil saat objek diaktifkan (enable)
        playerControls.Enable(); // Mengaktifkan input kontrol pemain
    }

    private void Update() {
        // Fungsi ini dipanggil setiap frame
        PlayerInput(); // Memproses input dari pemain (misalnya, pergerakan)
    }

    private void FixedUpdate() {
        // Fungsi ini dipanggil setiap frame tetap, digunakan untuk pergerakan fisika
        AdjustPlayerFacingDirection(); // Menyesuaikan arah pemain berdasarkan posisi mouse
        Move(); // Memindahkan pemain sesuai input yang diberikan
    }

    private void PlayerInput() {
        // Fungsi ini memproses input pergerakan dari pemain
        movement = playerControls.Movement.Move.ReadValue<Vector2>(); 
        // Membaca nilai pergerakan (input) dari kontrol, disimpan dalam variabel 'movement'

        // Menyesuaikan parameter animasi berdasarkan input pergerakan
        myAnimator.SetFloat("moveX", movement.x); // Mengatur parameter "moveX" di Animator untuk animasi horizontal
        myAnimator.SetFloat("moveY", movement.y); // Mengatur parameter "moveY" di Animator untuk animasi vertikal
    }

    private void Move() {
        // Fungsi ini digunakan untuk memindahkan pemain berdasarkan input pergerakan
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
        // Menggerakkan posisi Rigidbody2D dengan kecepatan yang disesuaikan dengan input dan waktu fixedDeltaTime
    }

    private void AdjustPlayerFacingDirection() {
        // Fungsi ini menyesuaikan arah wajah pemain berdasarkan posisi mouse
        Vector3 mousePos = Input.mousePosition; // Mendapatkan posisi mouse dalam koordinat layar
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position); 
        // Mendapatkan posisi pemain dalam koordinat layar

        if (mousePos.x < playerScreenPoint.x) {
            mySpriteRender.flipX = true; // Jika posisi mouse di kiri pemain, flip sprite secara horizontal
        } else {
            mySpriteRender.flipX = false; // Jika posisi mouse di kanan pemain, tidak ada flip
        }
    }
}
