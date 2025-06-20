using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class CoinsUI : MonoBehaviour
{
    private TMP_Text coinText;

    private void Awake()
    {
        coinText = GetComponent<TMP_Text>();
    }

    public void UpdateCoins(CoinController coinController)
    {
        coinText.text = $"Coins: {coinController.GetCoins()}";
    }
}
