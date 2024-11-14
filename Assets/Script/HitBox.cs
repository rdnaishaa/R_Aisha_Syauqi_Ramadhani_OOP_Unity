using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hitbox : MonoBehaviour
{
    public HealthComponent healthComponent; // Referensi ke HealthComponent

    void Awake()
    {
        if (healthComponent == null)
        {
            healthComponent = GetComponent<HealthComponent>();
        }
    }

    public void Damage(int damage)
    {
        if (healthComponent != null)
        {
            healthComponent.Subtract(damage); // Memberikan damage ke HealthComponent
        }
    }

    public void Damage(Bullet bullet)
    {
        if (healthComponent != null)
        {
            healthComponent.Subtract(bullet.damage); // Memberikan damage sesuai nilai damage peluru
            Destroy(bullet.gameObject); // Hancurkan bullet setelah memberikan damage
        }
    }
}
