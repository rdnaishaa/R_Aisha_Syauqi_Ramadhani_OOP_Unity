using UnityEngine;
using UnityEngine.UIElements;

public class GameStats : MonoBehaviour
{
    public VisualElement rootVisualElement;
    private Label healthLabel;
    private Label enemiesLeftLabel;
    private Label waveLabel;
    private Label pointsLabel;
    private HealthComponent playerHealthComponent;

    private void OnEnable()
    {
        // Mengambil referensi UIDocument dan root visual element
        var uiDocument = GetComponent<UIDocument>();
        rootVisualElement = uiDocument.rootVisualElement;

        // Mengambil referensi ke setiap label dengan metode caching lebih efisien
        healthLabel = rootVisualElement.Q("health") as Label;
        enemiesLeftLabel = rootVisualElement.Q("enemiesleft") as Label;
        waveLabel = rootVisualElement.Q("wave") as Label;
        pointsLabel = rootVisualElement.Q("point") as Label;

        // Mencari objek player dengan tag "Player" dan mengambil komponen kesehatan
        playerHealthComponent = FindPlayerHealthComponent();
    }

    // Mencari objek player dan mendapatkan komponen HealthComponent
    private HealthComponent FindPlayerHealthComponent()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player ? player.GetComponent<HealthComponent>() : null;
    }

    // Memperbarui label kesehatan pemain
    public void UpdateHealth(float health)
    {
        if (healthLabel != null)
        {
            healthLabel.text = $"Health: {health}";
        }
    }

    // Memperbarui label jumlah musuh yang tersisa
    public void UpdateEnemiesLeft(int enemiesLeft)
    {
        if (enemiesLeftLabel != null)
        {
            enemiesLeftLabel.text = $"Enemies Left: {enemiesLeft}";
        }
    }

    // Memperbarui label gelombang permainan
    public void UpdateWave(int wave)
    {
        if (waveLabel != null)
        {
            waveLabel.text = $"Wave: {wave}";
        }
    }

    // Memperbarui label jumlah poin
    public void UpdatePoints(int points)
    {
        if (pointsLabel != null)
        {
            pointsLabel.text = $"Points: {points}";
        }
    }

    private void Update()
    {
        // Jika komponen kesehatan ada, perbarui nilai kesehatan
        if (playerHealthComponent != null)
        {
            UpdateHealth(playerHealthComponent.Health);
        }
    }
}
