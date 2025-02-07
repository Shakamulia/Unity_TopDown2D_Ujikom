using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour 
{
    private enum State { // Enum untuk menyimpan status AI musuh
        Roaming // Status roaming, berarti musuh sedang menjelajah
    }

    private State state; // Variabel untuk menyimpan status terkini musuh
    private EnemyPathfinding enemyPathfinding; // Variabel untuk menyimpan referensi ke komponen EnemyPathfinding

    private void Awake() {
        // Fungsi ini dipanggil sebelum fungsi Start() dan digunakan untuk inisialisasi
        enemyPathfinding = GetComponent<EnemyPathfinding>(); // Mengambil referensi komponen EnemyPathfinding yang terpasang pada objek
        state = State.Roaming; // Menginisialisasi status musuh menjadi Roaming
    }

    private void Start() {
        // Fungsi ini dipanggil setelah Awake() dan digunakan untuk memulai logika permainan
        StartCoroutine(RoamingRoutine()); // Memulai coroutine untuk perulangan roaming musuh
    }

    private IEnumerator RoamingRoutine() {
        // Coroutine untuk mengatur musuh bergerak secara acak selama statusnya Roaming
        while (state == State.Roaming) { // Selama status musuh adalah Roaming
            Vector2 roamPosition = GetRoamingPosition(); // Mendapatkan posisi acak untuk musuh bergerak
            enemyPathfinding.MoveTo(roamPosition); // Meminta musuh bergerak ke posisi yang telah dihitung
            yield return new WaitForSeconds(2f); // Menunggu selama 2 detik sebelum melanjutkan perulangan
        }
    }

    private Vector2 GetRoamingPosition() {
        // Fungsi untuk menghasilkan posisi acak di sekitar area
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized; 
        // Menghasilkan vektor acak antara -1 dan 1 untuk X dan Y, kemudian dinormalisasi untuk menghasilkan arah acak dengan panjang tetap
    }
}
