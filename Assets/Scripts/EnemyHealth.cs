using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
