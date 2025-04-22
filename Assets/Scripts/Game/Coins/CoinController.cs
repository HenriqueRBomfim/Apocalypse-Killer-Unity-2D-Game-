using UnityEngine;
using UnityEngine.Events;

public class CoinController : MonoBehaviour
{
    public UnityEvent OnCoinsChanged;
    public int coins = 0; // Number of coins collected
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize coins to 0 at the start of the game
        coins = 0;
    }

    // Method to add coins
    public void AddCoins(int amount)
    {
        coins += amount; // Increase the coin count by the specified amount
        OnCoinsChanged.Invoke();
    }

    // Method to get the current coin count
    public int GetCoins()
    {
        return coins; // Return the current coin count
    }

    // Method to check if the player has enough coins
    public bool HasEnoughCoins(int amount)
    {
        return coins >= amount; // Return true if the player has enough coins, false otherwise
    }

    // Method to spend coins
    public void SpendCoins(int amount)
    {
        if (HasEnoughCoins(amount)) // Check if the player has enough coins
        {
            coins -= amount; // Deduct the specified amount from the coin count
            OnCoinsChanged.Invoke();
        }
        else
        {
            Debug.Log("Not enough coins!"); // Log a message if there are not enough coins
        }
    }
}
