using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(HitboxComponent))]
public class InvincibilityComponent : MonoBehaviour
{
    [SerializeField] private int blinkingCount = 7; // Jumlah kedipan
    [SerializeField] private float blinkInterval = 0.1f; // Waktu antar kedipan
    private SpriteRenderer spriteRenderer; // Referensi ke SpriteRenderer
    public bool isInvincible = false; // Menandai status invincibility

    void Awake()
    {
        // Mendapatkan komponen SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer tidak ditemukan pada " + gameObject.name);
        }
    }

    private IEnumerator FlashRoutine()
    {
        // Melakukan efek blinking sesuai jumlah yang ditentukan
        for (int i = 0; i < blinkingCount; i++)
        {
            // Matikan tampilan sprite
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkInterval);

            // Hidupkan kembali tampilan sprite
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkInterval);
        }

        // Setelah blinking selesai, set invincibility ke false
        isInvincible = false;
        Debug.Log("Invincibility selesai.");
    }

    public void StartInvincibility()
    {
        // Memulai blinking hanya jika entitas belum invincible
        if (!isInvincible)
        {
            isInvincible = true;
            StartCoroutine(FlashRoutine());
            Debug.Log("Invincibility dimulai.");
        }
    }

    void Update()
    {
        // Tambahkan logika tambahan jika diperlukan
    }
}
