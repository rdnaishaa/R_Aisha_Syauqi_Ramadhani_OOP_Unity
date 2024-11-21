using System.Collections; // Hilangkan modifier 'private' di sini
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy // Hanya gunakan modifier 'public' pada class
{
    public float speed = 2f;
    private Vector2 moveDirection;
    private SpriteRenderer spriteRenderer;

    private float spawnRangeX = 8f;  // Rentang spawn di X
    private float spawnYRange = 4f;  // Rentang spawn di Y

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Mendapatkan komponen SpriteRenderer
        RespawnAtSide();
    }

    private void Update()
    {
        // Logika untuk pergerakan EnemyBoss
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Respawn ketika keluar dari layar
        if (transform.position.x < -spawnRangeX || transform.position.x > spawnRangeX)
        {
            RespawnAtSide();
        }
    }

    private void RespawnAtSide()
    {
        float spawnX = Random.Range(0, 2) == 0 ? -spawnRangeX : spawnRangeX;
        float spawnY = Random.Range(-spawnYRange, spawnYRange);
        transform.position = new Vector2(spawnX, spawnY);
        moveDirection = spawnX < 0 ? Vector2.right : Vector2.left;
        transform.rotation = Quaternion.identity;
    }
}
