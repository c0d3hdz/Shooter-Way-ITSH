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

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (canSpawn)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

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
