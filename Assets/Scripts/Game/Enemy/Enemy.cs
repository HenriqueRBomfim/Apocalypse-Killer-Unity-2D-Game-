using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyPool enemyPool;
    private string enemyType;
    private EnemySpawner spawner; // Adicionado

    // Chame este método ao ativar o inimigo pelo pool
    public void SetPool(EnemyPool pool, string type)
    {
        enemyPool = pool;
        enemyType = type;
    }

    // Adicione este método:
    public void SetSpawner(EnemySpawner enemySpawner)
    {
        spawner = enemySpawner;
    }

    // Quando o inimigo "morrer", devolva ao pool e avise o spawner
    public void Die()
    {
        Debug.Log("Enemy died, returning to pool: " + enemyType);
        if (spawner != null)
        {
            spawner.EnemyDied();
        }

        if (enemyPool != null && !string.IsNullOrEmpty(enemyType))
        {
            enemyPool.ReturnEnemy(enemyType, gameObject);
        }
        else
        {
            Debug.LogWarning("EnemyPool or enemyType not set, destroying enemy directly.");
            Debug.LogWarning("Enemy type: " + enemyType);
            Debug.LogWarning("EnemyPool: " + enemyPool);
            Destroy(gameObject);
        }
    }
}