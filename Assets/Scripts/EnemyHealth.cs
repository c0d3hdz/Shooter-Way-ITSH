using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemigo recibió daño. Vida actual: " + currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("Enemigo muerto.");
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
