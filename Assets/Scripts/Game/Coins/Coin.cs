// Coin.cs
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    private CoinPool pool;

    public void SetPool(CoinPool coinPool)
    {
        pool = coinPool;
    }

    public void SetValue(int value)
    {
        coinValue = value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CoinController coinController = other.GetComponent<CoinController>();
            if (coinController != null)
            {
                coinController.AddCoins(coinValue);
            }
            if (pool != null)
                pool.ReturnCoin(gameObject);
            else
                Destroy(gameObject);
        }
    }
}