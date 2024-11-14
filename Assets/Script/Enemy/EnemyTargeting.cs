using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : Enemy
{
    private Transform playerTransform; // Posisi Player
    private float speed = 2.0f;        // Kecepatan gerakan enemy
    public GameObject enemyPrefab;     // Prefab EnemyTargeting

    private void Start()
    {
        // Temukan Player di dalam scene
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Pastikan Player ditemukan sebelum menetapkan transform-nya
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player not found in the scene. EnemyTargeting will not move.");
        }

        // Spawn beberapa musuh secara random
        SpawnMultipleEnemies(enemyPrefab, Random.Range(1, 6));
    }

    private void Update()
    {
        // Jika Player ditemukan, bergerak ke arahnya
        if (playerTransform != null)
        {
            // Hitung arah gerakan menuju Player
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Jika enemy bersentuhan dengan Player, maka enemy akan hilang
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject); // Menghancurkan enemy saat menyentuh Player
        }
    }

    // Method untuk spawn beberapa musuh di sisi kiri atau kanan layar
    public void SpawnMultipleEnemies(GameObject enemyPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Tentukan posisi spawn secara acak di sisi kiri atau kanan layar
            float spawnX = Random.Range(0, 2) == 0 ? -Screen.width / 110f : Screen.width / 110f;
            float spawnY = Random.Range(-Screen.height / 80f, Screen.height / 80f);

            // Buat instance baru dari prefab EnemyTargeting
            GameObject newEnemy = Instantiate(enemyPrefab, new Vector2(spawnX, spawnY), Quaternion.identity);
            EnemyTargeting enemyScript = newEnemy.GetComponent<EnemyTargeting>();
            
            if (enemyScript != null)
            {
                enemyScript.playerTransform = playerTransform; // Set target player
            }
        }
    }
}
