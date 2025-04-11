using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float minimumSpawnTime = 2f;

    [SerializeField]
    private float maximumSpawnTime = 5f;

    [SerializeField]
    private float spawnAccelerationRate = 0.98f;

    [SerializeField]
    private float minimumLimit = 0.5f;

    private float timeUntilSpawn;
    private float elapsedTime;

    private int currentEnemyCount = 0;
    private const int maxEnemies = 10;

    void Awake()
    {
        SetTimeUntilSpawn();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0 && currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
            SetTimeUntilSpawn();
        }

        if (elapsedTime >= 1f)
        {
            AccelerateSpawnRate();
            elapsedTime = 0f;
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        
        enemy.AddComponent<Enemy>().SetSpawner(this);

        currentEnemyCount++;
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
