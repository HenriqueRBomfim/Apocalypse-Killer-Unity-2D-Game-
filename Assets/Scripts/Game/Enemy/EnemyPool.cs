using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [System.Serializable]
    public class EnemyPrefabEntry
    {
        public string enemyType;
        public GameObject prefab;
    }

    public List<EnemyPrefabEntry> enemyPrefabs;
    public int maxPerType = 16;

    private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();

    void Awake()
    {
        foreach (var entry in enemyPrefabs)
        {
            Queue<GameObject> pool = new Queue<GameObject>();
            for (int i = 0; i < maxPerType; i++)
            {
                GameObject enemy = Instantiate(entry.prefab, transform);
                enemy.SetActive(false);
                pool.Enqueue(enemy);
            }
            pools[entry.enemyType] = pool;
        }
    }

    public GameObject GetEnemy(string enemyType, Vector3 position, Quaternion rotation)
    {
        if (!pools.ContainsKey(enemyType))
        {
            Debug.LogWarning($"Enemy type '{enemyType}' not found in pool!");
            return null;
        }

        Queue<GameObject> pool = pools[enemyType];

        if (pool.Count > 0)
        {
            GameObject enemy = pool.Dequeue();
            enemy.transform.position = position;
            enemy.transform.rotation = rotation;
            enemy.SetActive(true);
            return enemy;
        }
        else
        {
            Debug.LogWarning($"No more '{enemyType}' enemies available in pool!");
            return null;
        }
    }

    public void ReturnEnemy(string enemyType, GameObject enemy)
    {
        Debug.Log("Returning enemy to pool: " + enemyType);
        if (!pools.ContainsKey(enemyType))
        {
            Debug.LogWarning($"Enemy type '{enemyType}' not found in pool!");
            Destroy(enemy);
            return;
        }

        if (pools[enemyType].Count < maxPerType)
        {
            enemy.SetActive(false);
            pools[enemyType].Enqueue(enemy);
        }
        else
        {
            Destroy(enemy);
        }
    }
}