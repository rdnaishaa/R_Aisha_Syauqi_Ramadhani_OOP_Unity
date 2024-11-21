using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy; // Prefab musuh yang akan di-spawn

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3; // Kills minimum untuk menambah jumlah spawn
    public int totalKill = 0; // Total kill global
    private int totalKillWave = 0; // Total kill dalam satu wave

    [SerializeField] private float spawnInterval = 3f; // Jarak waktu antar spawn

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0; // Jumlah musuh yang di-spawn per wave
    public int defaultSpawnCount = 1; // Default jumlah musuh
    public int spawnCountMultiplier = 1; // Pengali jumlah musuh
    public int multiplierIncreaseCount = 1; // Counter untuk meningkatkan pengali

    public CombatManager combatManager; // Referensi ke CombatManager (opsional)

    public bool isSpawning = false; // Status apakah sedang spawning

    private void Start()
    {
        // Memulai coroutine untuk spawn musuh
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (isSpawning)
            {
                for (int i = 0; i < spawnCount; i++)
                {
                    SpawnEnemy();
                }

                totalKillWave = 0; // Reset jumlah kill dalam wave
                while (totalKillWave < spawnCount)
                {
                    yield return null; // Tunggu hingga semua musuh dalam wave terbunuh
                }

                // Cek apakah sudah cukup kill untuk menambah jumlah spawn
                if (totalKill >= minimumKillsToIncreaseSpawnCount * multiplierIncreaseCount)
                {
                    spawnCountMultiplier++;
                    spawnCount = defaultSpawnCount * spawnCountMultiplier;
                    multiplierIncreaseCount++;
                }
            }

            yield return new WaitForSeconds(spawnInterval); // Tunggu hingga spawn berikutnya
        }
    }

    private void SpawnEnemy()
    {
        if (spawnedEnemy != null)
        {
            // Spawn instance musuh dari prefab
            Enemy newEnemy = Instantiate(spawnedEnemy, transform.position, Quaternion.identity);

            // Aktifkan musuh setelah delay
            StartCoroutine(ActivateEnemyAfterDelay(newEnemy, 0f));
        }
        else
        {
            Debug.LogError("No enemy prefab assigned!");
        }
    }

    private IEnumerator ActivateEnemyAfterDelay(Enemy enemy, float delay)
    {
        yield return new WaitForSeconds(delay); // Tunggu sesuai delay
        enemy.gameObject.SetActive(true); // Aktifkan musuh
    }

    // Method untuk mulai spawning
    public void StartSpawning()
    {
        isSpawning = true;
    }

    // Method untuk menghentikan spawning
    public void StopSpawning()
    {
        isSpawning = false;
    }

    // Method untuk menambah kill global
    public void AddKill()
    {
        totalKill++;
        totalKillWave++;
    }
}
