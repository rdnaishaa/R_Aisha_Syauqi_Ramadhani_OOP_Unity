using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent;
    private InvincibilityComponent invincibilityComponent;

    private void Start()
    {
        // Fetch the HealthComponent attached to the same GameObject or parent
        healthComponent = GetComponent<HealthComponent>();
        invincibilityComponent = GetComponent<InvincibilityComponent>(); // Referensi class yang benar
        if (healthComponent == null)
        {
            Debug.LogError("HealthComponent not found on " + gameObject.name);
        }
    }

    // Method to damage using Bullet
    public void Damage(Bullet bullet)
    {
        if (invincibilityComponent != null && !invincibilityComponent.isInvincible) // Cek apakah invincible
        {
            if (healthComponent != null)
            {
                Debug.Log("Applying bullet damage.");
                healthComponent.Subtract(bullet.damage);
                invincibilityComponent.StartInvincibility();  // Kurangi health berdasarkan damage dari bullet
            }
        }
    }

    // Method to damage using an integer value
    public void Damage(int damage)
    {
        if (invincibilityComponent != null && !invincibilityComponent.isInvincible) // Cek apakah invincible
        {
            Debug.Log("Applying integer damage.");
            healthComponent.Subtract(damage);
            invincibilityComponent.StartInvincibility();  // Kurangi health berdasarkan damage integer
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the "Bullet" tag
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();

            if (bullet != null)
            {
                Debug.Log("Hit by bullet: " + bullet.name);
                Damage(bullet); // Apply damage from the bullet

                // Optionally, destroy the bullet after it hits
                Destroy(other.gameObject);
            }
        }
    }
}
