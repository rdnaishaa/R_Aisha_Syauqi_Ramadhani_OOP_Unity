using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    // Menggabungkan parameter X dan Y ke dalam Vector2 agar tampilan di Inspector lebih ringkas
    public Vector2 maxSpeed = new Vector2(7f, 5f);
    public Vector2 timeToFullSpeed = new Vector2(1f, 1f);
    public Vector2 timeToStop = new Vector2(0.5f, 0.5f);
    public Vector2 stopClamp = new Vector2(2.5f, 2.5f);

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 stopFriction;

    private void Start()
    {
        // Mengambil informasi dari Rigidbody2D dan melakukan kalkulasi awal untuk setiap sumbu
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = new Vector2(maxSpeed.x / timeToFullSpeed.x, maxSpeed.y / timeToFullSpeed.y);
        stopFriction = new Vector2(moveVelocity.x / timeToStop.x, moveVelocity.y / timeToStop.y);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        // Mengambil input untuk pergerakan pada sumbu x dan y
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        // Mengatur target kecepatan berdasarkan input dan parameter maxSpeed
        Vector2 targetVelocity = new Vector2(
            moveDirection.x * maxSpeed.x,
            moveDirection.y * maxSpeed.y
        );

        // Mengatur kecepatan pada Rigidbody2D dan menerapkan gesekan
        Vector2 currentVelocity = rb.velocity;
        currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, targetVelocity.x, stopFriction.x * Time.fixedDeltaTime);
        currentVelocity.y = Mathf.MoveTowards(currentVelocity.y, targetVelocity.y, stopFriction.y * Time.fixedDeltaTime);
        rb.velocity = currentVelocity;
    }

    public bool IsMoving()
    {
        // Mengembalikan true jika Player bergerak pada sumbu x atau y melebihi stopClamp
        bool isMoving = Mathf.Abs(rb.velocity.x) > stopClamp.x || Mathf.Abs(rb.velocity.y) > stopClamp.y;
        Debug.Log("IsMoving: " + isMoving); // Log untuk memeriksa status IsMoving
        return isMoving;
    }
}