// CoinPool.cs
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    public GameObject coinPrefab;
    public int poolSize = 20;
    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject coin = Instantiate(coinPrefab, transform);
            coin.SetActive(false);
            pool.Enqueue(coin);
        }
    }

    public GameObject GetCoin(Vector3 position)
    {
        if (pool.Count > 0)
        {
            GameObject coin = pool.Dequeue();
            coin.transform.position = position;
            coin.SetActive(true);
            Debug.Log("Coin retrieved from pool: " + coin.name);
            return coin;
        }
        else
        {
            // Se o pool acabar, instancia uma nova (opcional)
            GameObject coin = Instantiate(coinPrefab, position, Quaternion.identity, transform);
            return coin;
        }
    }

    public void ReturnCoin(GameObject coin)
    {
        coin.SetActive(false);
        pool.Enqueue(coin);
    }
}