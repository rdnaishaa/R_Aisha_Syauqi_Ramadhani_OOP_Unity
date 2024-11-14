using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float maxHealth = 100f; // Health maksimal
    private float currentHealth;   // Health saat ini

    void Start()
    {
        currentHealth = maxHealth; // Inisialisasi health awal
    }

    public float GetHealth()
    {
        return currentHealth; // Getter untuk health saat ini
    }

    public void Subtract(float damage)
    {
        currentHealth -= damage; // Mengurangi health sesuai damage yang diterima
        Debug.Log(gameObject.name + " menerima damage: " + damage + ", sisa health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); // Jika health <= 0, objek hancur
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " hancur.");
        Destroy(gameObject); // Hancurkan objek
    }
}
