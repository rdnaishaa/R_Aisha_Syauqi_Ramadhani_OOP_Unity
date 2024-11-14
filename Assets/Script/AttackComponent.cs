using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    public float damage = 10f; // Damage yang diberikan
    public Bullet bulletPrefab; // Referensi ke prefab Bullet

    private void OnTriggerEnter2D(Collider2D collision)
{
    Hitbox enemyHitbox = collision.GetComponent<Hitbox>();
    if (enemyHitbox != null)
    {
        enemyHitbox.Damage((int)damage);
        // Hapus atau sesuaikan Destroy(gameObject) jika ingin peluru terus bergerak

        }
    }
}
