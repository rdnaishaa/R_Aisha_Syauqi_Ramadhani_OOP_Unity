using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("Enemy Spawners")]
    public EnemySpawner[] enemySpawners; // Array of spawners
    public float timer = 0; // Timer untuk menghitung jeda antar gelombang
    [SerializeField] private float waveInterval = 5f; // Jeda antar gelombang (dalam detik)
    public int waveNumber = 1; // Nomor gelombang saat ini
    public int totalEnemies = 0; // Total musuh yang telah muncul

    private void Start()
    {
        StartWave(); // Memulai gelombang pertama
    }

    private void Update()
    {
        // Mengecek apakah semua spawner telah selesai memunculkan musuh
        if (AllSpawnersFinished())
        {
            timer += Time.deltaTime; // Menambah waktu

            // Jika timer melebihi interval antar gelombang, mulai gelombang berikutnya
            if (timer >= waveInterval)
            {
                StartNextWave();
                timer = 0; // Reset timer
            }
        }
    }

    private void StartWave()
    {
        foreach (var spawner in enemySpawners)
        {
            if (spawner != null)
            {
                spawner.defaultSpawnCount = waveNumber; // Jumlah musuh tergantung gelombang
                spawner.spawnCountMultiplier = 1; // Reset multiplier
                spawner.isSpawning = true; // Aktifkan spawner
            }
        }
    }

    private void StartNextWave()
    {
        waveNumber++; // Naikkan nomor gelombang
        totalEnemies = 0; // Reset total musuh

        foreach (var spawner in enemySpawners)
        {
            if (spawner != null)
            {
                spawner.defaultSpawnCount = waveNumber; // Sesuaikan jumlah musuh dengan gelombang
                spawner.multiplierIncreaseCount = waveNumber; // Naikkan multiplier
                spawner.isSpawning = true; // Aktifkan spawner
            }
        }
    }

    private bool AllSpawnersFinished()
    {
        foreach (var spawner in enemySpawners)
        {
            if (spawner != null && spawner.isSpawning)
            {
                return false; // Jika ada spawner yang masih aktif, gelombang belum selesai
            }
        }
        return true; // Semua spawner telah selesai
    }

    public void RegisterKill()
    {
        totalEnemies++; // Tambahkan jumlah musuh yang mati
    }
}
