using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; } // Singleton instance
    public bool FacingLeft { get; private set; } // Menyimpan arah menghadap karakter

    [SerializeField] private float moveSpeed = 1f; // Kecepatan gerak pemain

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // Inisialisasi komponen dan singleton instance
        Instance = this;
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() => playerControls.Enable(); // Mengaktifkan input kontrol

    private void Update() => HandleInput(); // Menangani input pergerakan

    private void FixedUpdate()
    {
        AdjustFacingDirection(); // Menyesuaikan arah karakter
        Move(); // Memindahkan karakter
    }

    private void HandleInput()
    {
        // Membaca input pergerakan dan mengatur parameter animasi
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        // Menggerakkan karakter berdasarkan input
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustFacingDirection()
    {
        // Menyesuaikan arah karakter berdasarkan posisi mouse
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        bool isFacingLeft = mousePos.x < playerScreenPoint.x;

        spriteRenderer.flipX = isFacingLeft;
        FacingLeft = isFacingLeft;
    }
}
