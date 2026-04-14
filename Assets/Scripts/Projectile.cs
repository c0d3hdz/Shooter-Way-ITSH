using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float damage; 
    public float lifetime = 3f; 

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Revisamos si el objeto con el que chocamos tiene el script EnemyHealth
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        Debug.Log("Bala colisionó con: " + collision.gameObject.name);
        // Si es un enemigo, le aplicamos el daño
        if (enemy != null)
        {   
            Debug.Log("Enemigo tiene EnemyHealth. Aplicando daño: " + damage);                      
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}