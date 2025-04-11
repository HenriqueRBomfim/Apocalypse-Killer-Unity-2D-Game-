using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemySpawner spawner;

    public void SetSpawner(EnemySpawner enemySpawner)
    {
        spawner = enemySpawner;
    }

    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.EnemyDied();
        }
    }
}
