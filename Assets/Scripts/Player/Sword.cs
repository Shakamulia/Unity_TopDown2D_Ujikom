using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimPrefab; // Prefab animasi serangan
    [SerializeField] private Transform slashAnimSpawnPoint; // Titik spawn animasi
    [SerializeField] private Transform weaponCollider; // Collider senjata
    [SerializeField] private float swordAttackCD = .5f;

    private PlayerControls playerControls; // Input kontrol dari player
    private Animator myAnimator; // Animator untuk animasi senjata
    private PlayerController playerController; // Kontroler player
    private ActiveWeapon activeWeapon; // Referensi ke senjata aktif
    private bool attackButtonDown, isAttacking = false;

    private GameObject slashAnim; // Objek animasi serangan yang sedang berlangsung

    private void Awake()
    {
        // Menyiapkan referensi komponen seperti PlayerController, ActiveWeapon, Animator, dan PlayerControls
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }

    private void OnEnable() => playerControls.Enable(); // Mengaktifkan input kontrol saat senjata diaktifkan

    void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    } // Mendaftarkan event untuk tombol serangan

    private void Update() {
        MouseFollowWithOffset();
        Attack();
    } // Memperbarui rotasi senjata mengikuti posisi mouse setiap frame

    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    private void Attack()
    {
        if (attackButtonDown && !isAttacking)
        {
            isAttacking = true;
            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
            slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent;
            StartCoroutine(AttackCDRoutine());
        }
    }

    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(swordAttackCD);
        isAttacking = false;
    }



    public void DoneAttackingAnimEvent() => weaponCollider.gameObject.SetActive(false); // Menonaktifkan collider setelah serangan selesai

    public void SwingUpFlipAnimEvent() => FlipAnim(-180); // Menangani rotasi animasi serangan untuk gerakan ke atas

    public void SwingDownFlipAnimEvent() => FlipAnim(0); // Menangani rotasi animasi serangan untuk gerakan ke bawah

    private void FlipAnim(float rotation)
    {
        // Fungsi untuk mengatur rotasi dan pembalikan sprite animasi
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(rotation, 0, 0);
        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true; // Membalik sprite jika player menghadap kiri
        }
    }

    private void MouseFollowWithOffset()
    {
        // Mendapatkan posisi mouse dan posisi player di layar
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        // Menghitung sudut rotasi antara mouse dan player
        float angle = Mathf.Atan2(mousePos.y - playerScreenPoint.y, mousePos.x - playerScreenPoint.x) * Mathf.Rad2Deg;

        // Menyesuaikan rotasi senjata berdasarkan posisi mouse
        bool isFacingLeft = mousePos.x < playerScreenPoint.x; // Menentukan apakah player menghadap kiri
        activeWeapon.transform.rotation = Quaternion.Euler(0, isFacingLeft ? -180 : 0, angle); // Menyesuaikan rotasi senjata
        weaponCollider.transform.rotation = Quaternion.Euler(0, isFacingLeft ? -180 : 0, 0); // Menyesuaikan rotasi collider senjata
    }
}
