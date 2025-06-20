using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [Header("Prefabs de Drop")]
    public GameObject coinPrefab;
    public GameObject rareDropPrefab; // Pode ser Ruby, outro item raro, etc.
    public int CoinValue = 1;

    [Header("Chance de Drop Raro (0 a 1)")]
    [Range(0f, 1f)]
    public float rareDropChance = 0.05f; // Defina no Inspector do prefab do inimigo

    [Header("Pool de Moedas")]
    public CoinPool coinPool;

    [Header("Tipo do Inimigo")]
    public string enemyType = "Zombie"; // Defina no Inspector ou via script

    void Awake()
    {
        if (coinPool == null)
        {
            coinPool = FindFirstObjectByType<CoinPool>();
        }
    }

    public void Drop(int value = -1)
    {
        Vector3 dropPosition = transform.position;

        // Drop raro genérico: se rareDropPrefab está definido e sorteio passar, dropa o item raro
        if (rareDropPrefab != null && Random.value <= rareDropChance)
        {
            Instantiate(rareDropPrefab, dropPosition, Quaternion.identity);
            return; // Não dropa mais nada se cair o raro
        }

        // Caso contrário, dropa moeda normalmente
        if (coinPrefab != null)
        {
            if (coinPool != null)
            {
                GameObject coinObj = coinPool.GetCoin(dropPosition);
                Coin coin = coinObj.GetComponent<Coin>();
                if (coin != null)
                {
                    coin.SetPool(coinPool);
                    coin.SetValue(value == -1 ? CoinValue : value);
                }
            }
            else
            {
                GameObject coinObj = Instantiate(coinPrefab, dropPosition, Quaternion.identity);
                Coin coin = coinObj.GetComponent<Coin>();
                if (coin != null)
                {
                    coin.SetValue(value == -1 ? CoinValue : value);
                }
            }
        }
    }
}