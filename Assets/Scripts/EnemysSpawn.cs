using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float health = 100f;
    public GameObject enemyPrefab;
    public Transform[] pointSpawn;
    public Transform positionPlayer;
    [Header("Spawn Settings")]
    public float spawnInterval = 3f; // Tiempo en segundos entre cada aparición
    public bool canSpawn = true;

    [Header("Wave Settings")]
    public int currentWave = 1;
    public int enemiesPerWave = 3; 


    void Start()
    {
       StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (canSpawn)
        {
            Debug.Log("Iniciando Oleada: " + currentWave);

            for (int i = 0; i < enemiesPerWave; i++)
            {
                if (!canSpawn) break;
                SpawnEnemy();
                
                yield return new WaitForSeconds(spawnInterval);
            }

            Debug.Log("Último enemigo de la oleada " + currentWave + " ha sido spawnado. Esperando a que todos mueran...");

            // Esperamos mientras haya enemigos vivos en la escena
            while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            {
                yield return new WaitForSeconds(1f); // Revisamos cada segundo para no sobrecargar el juego
            }

            Debug.Log("Oleada " + currentWave + " completada. Preparando siguiente oleada...");
            
            // Pausa entre oleadas (opcional)
            yield return new WaitForSeconds(3f);

            currentWave++;
            enemiesPerWave += 2; 
        }
    }

    // Eliminamos el Update, ya no lo necesitamos para checar a los enemigos
    private void SpawnEnemy()
    {
        if (enemyPrefab == null || pointSpawn == null || pointSpawn.Length == 0)
        {
            Debug.LogWarning("Faltan asignar el prefab del enemigo o los puntos de aparición.");
            return;
        }

        int randomIndex = Random.Range(0, pointSpawn.Length);
        Transform randomSpawnPoint = pointSpawn[randomIndex];

        GameObject spawnedEnemy = Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
        
        // ¡IMPORTANTE! Asignamos el tag para que la corrutina pueda contarlos usando FindGameObjectsWithTag
        spawnedEnemy.tag = "Enemy";

        EnemyHealth enemyScript = spawnedEnemy.GetComponent<EnemyHealth>();

        if (enemyScript != null)
        {
            enemyScript.currentHealth = this.health;
        }

        EnemyMovement enemyMovement = spawnedEnemy.GetComponent<EnemyMovement>();
        if(enemyMovement != null){
            enemyMovement.target= positionPlayer;
        }
    }
}
