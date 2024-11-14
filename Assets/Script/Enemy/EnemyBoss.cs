using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    public float speed = 2f;
    private Vector2 moveDirection;
    private SpriteRenderer spriteRenderer; 

    private void Start()
    {
        // Posisikan musuh secara acak di sisi kiri atau kanan layar
        RespawnAtSide();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Gerakkan musuh secara horizontal
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Jika musuh keluar dari layar di bagian kiri atau kanan, respawn di sisi berlawanan
        if (transform.position.x < -Screen.width / 80f || transform.position.x > Screen.width / 80f)
        {
            RespawnAtSide();
        }
    }

    // Method untuk memposisikan musuh secara acak di sisi kiri atau kanan layar
    private void RespawnAtSide()
    {
        // Tentukan sisi spawn secara acak (kiri atau kanan)
        float spawnX = Random.Range(0, 2) == 0 ? Screen.width / 120f : -Screen.width / 110f;
        float spawnY = Random.Range(-Screen.height / 80f, Screen.height / 80f);

        // Set posisi musuh di sisi kiri atau kanan dengan posisi Y acak
        transform.position = new Vector2(spawnX, spawnY);

        // Tentukan arah pergerakan horizontal berdasarkan sisi spawn
        moveDirection = spawnX < 0 ? Vector2.right : Vector2.left;

        // Mengubah skala untuk membalikkan gambar (menghadap kiri atau kanan)
        Vector3 scale = transform.localScale;
        scale.x = moveDirection == Vector2.left ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        transform.localScale = scale;

        // Pastikan rotasi tetap pada keadaan awal (menghadap arah horizontal)
        transform.rotation = Quaternion.identity;
    }
}
