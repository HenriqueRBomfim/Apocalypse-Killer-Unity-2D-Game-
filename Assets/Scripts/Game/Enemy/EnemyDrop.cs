using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [Header("Prefabs de Drop")]
    public GameObject coinPrefab;
    public GameObject rareDropPrefab;
    public int CoinValue = 1;

    [Header("Chance de Drop Raro (0 a 1)")]
    [Range(0f, 1f)]
    public float rareDropChance = 0.1f;

    [Header("Pool de Moedas")]
    public CoinPool coinPool;

    void Awake()
    {
        // Se o coinPool não foi atribuído no Inspector, encontre-o na cena.
        if (coinPool == null)
        {
            // Procura por QUALQUER objeto ativo na cena que tenha o componente CoinPool
            coinPool = FindFirstObjectByType<CoinPool>();
        }
    }

    public void Drop(int value = -1)
    {
        Vector3 dropPosition = transform.position;

        // Sorteia se será drop raro
        if (Random.value < rareDropChance && rareDropPrefab != null)
        {
            Instantiate(rareDropPrefab, dropPosition, Quaternion.identity);
        }
        else if (coinPrefab != null) // Se não for raro, é uma moeda comum
        {
            // É uma moeda comum: Use o Pool se ele existir
            if (coinPool != null)
            {
                GameObject coinObj = coinPool.GetCoin(dropPosition);
                Coin coin = coinObj.GetComponent<Coin>();
                if (coin != null)
                {
                    coin.SetPool(coinPool); // Veja a sugestão 2 sobre isso
                    coin.SetValue(value == -1 ? CoinValue : value);
                }
            }
            else
            {
                // Fallback: Instancie a moeda se não houver pool
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