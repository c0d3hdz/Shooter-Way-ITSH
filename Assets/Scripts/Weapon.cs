using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    public float damage = 25f;       // Daño que hace esta arma
    public float bulletSpeed = 50f;  // Velocidad a la que sale la bala
    public float fireRate = 0.5f;    // Tiempo entre disparos (Cadencia)

    [Header("References")]
    public GameObject bulletPrefab;  // El prefab de la bala que vas a disparar
    public Transform firePoint;      // Un punto vacío (Empty) en la punta del cañón del arma

    private float nextFireTime = 0f; // Control interno para la cadencia de tiro

    void Update()
    {
        // Detecta el clic izquierdo del mouse (0) mantenido para disparo automático
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("Faltan asignar el prefab de la bala o el punto de disparo (firePoint) en el arma.");
            return;
        }

        // 1. Instanciar la bala en la posición y rotación del cañón (firePoint)
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // 2. Pasarle el daño del arma a la bala
        Projectile projectileScript = bullet.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.damage = this.damage;
        }

        // 3. Aplicar físicas: Darle velocidad a la bala hacia adelante
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("El prefab de la bala necesita un componente Rigidbody para moverse con físicas.");
        }
    }
}