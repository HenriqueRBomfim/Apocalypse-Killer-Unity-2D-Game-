using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private string enemyType; // Nome do tipo de inimigo para o pool

    [SerializeField]
    private EnemyPool enemyPool; // Referência ao EnemyPool

    [SerializeField]
    private float minimumSpawnTime = 2f;

    [SerializeField]
    private float maximumSpawnTime = 5f;

    [SerializeField]
    private float spawnAccelerationRate = 0.98f;

    [SerializeField]
    private float minimumLimit = 0.5f;

    [SerializeField]
    private float startSpawningAt; // Tempo global para começar a spawnar (em segundos)

    [SerializeField]
    private TimeController timeController; // Referência ao controlador de tempo

    private float timeUntilSpawn;
    private float spawnRateTimer;

    private int currentEnemyCount = 0;
    public int maxEnemies = 5;

    void Awake()
    {
        SetTimeUntilSpawn();
    }

    void Update()
    {
        float globalTime = timeController.GetElapsedTime();

        if (globalTime < startSpawningAt)
            return; // Ainda não é hora de spawnar zumbis nesse spawner

        spawnRateTimer += Time.deltaTime;
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0 && currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
            SetTimeUntilSpawn();
        }

        if (spawnRateTimer >= 5f)
        {
            AccelerateSpawnRate();
            spawnRateTimer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPool == null)
        {
            Debug.LogWarning("EnemyPool não atribuído ao EnemySpawner!");
            return;
        }

        GameObject enemy = enemyPool.GetEnemy(enemyType, transform.position, Quaternion.identity);

        if (enemy != null)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.SetPool(enemyPool, enemyType);
                enemyScript.SetSpawner(this); // Se você tiver esse método para notificar o spawner
            }
            currentEnemyCount++;
        }
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }

    private void AccelerateSpawnRate()
    {
        minimumSpawnTime = Mathf.Max(minimumSpawnTime * spawnAccelerationRate, minimumLimit);
        maximumSpawnTime = Mathf.Max(maximumSpawnTime * spawnAccelerationRate, minimumLimit);
    }

    public void EnemyDied()
    {
        currentEnemyCount = Mathf.Max(0, currentEnemyCount - 1);
    }
}