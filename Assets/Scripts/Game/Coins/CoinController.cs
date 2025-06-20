using UnityEngine;
using UnityEngine.Events;

public class CoinController : MonoBehaviour
{
    public UnityEvent OnCoinsChanged;
    public int coins = 0; // Número de moedas coletadas

    void Start()
    {
        coins = 0;
    }

    // Método para obter a quantidade atual de moedas
    public int GetCoins()
    {
        return coins;
    }

    // Método para verificar se o jogador tem moedas suficientes
    public bool HasEnoughCoins(int amount)
    {
        return coins >= amount;
    }

    // Método para gastar moedas
    public void SpendCoins(int amount)
    {
        if (HasEnoughCoins(amount))
        {
            coins -= amount;
            OnCoinsChanged.Invoke();
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        OnCoinsChanged.Invoke();
    }
}
