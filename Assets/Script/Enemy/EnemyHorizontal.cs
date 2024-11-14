using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : Enemy
{
    public float speed = 2f;
    private Vector2 moveDirection;
    public GameObject enemyPrefab;

    private void Start()
    {
        // Posisikan musuh secara acak di sisi kiri atau kanan layar
        RespawnAtSide();
        EnemyHorizontal.SpawnMultipleEnemies(enemyPrefab, Random.Range(3, 7));
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
        float spawnX = Random.Range(0, 2) == 0 ? -Screen.width / 110f : Screen.width / 120f;
        float spawnY = Random.Range(-Screen.height / 80f, Screen.height / 80f);

        // Set posisi musuh di sisi kiri atau kanan dengan posisi Y acak
        transform.position = new Vector2(spawnX, spawnY);

        // Tentukan arah pergerakan horizontal berdasarkan sisi spawn
        moveDirection = spawnX < 0 ? Vector2.right : Vector2.left;

        // Pastikan rotasi tetap pada keadaan awal (menghadap arah horizontal)
        transform.rotation = Quaternion.identity;
    }

    public static void SpawnMultipleEnemies(GameObject enemyPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Buat instance baru dari musuh
            GameObject newEnemy = Instantiate(enemyPrefab);
            EnemyHorizontal enemyScript = newEnemy.GetComponent<EnemyHorizontal>();
            if (enemyScript != null)
            {
                enemyScript.RespawnAtSide(); // Spawn dengan posisi acak di sisi kiri atau kanan
            }
        }
    }
}
