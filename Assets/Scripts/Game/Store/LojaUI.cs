using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LojaUI : MonoBehaviour
{
    public PlayerShoot playerShoot;
    public CoinController coinController;
    public Button addBulletButton;
    public Button closeButton;
    public TMP_Text feedbackText;
    public int bulletUpgradeCost = 20;

    private void Awake()
    {
        addBulletButton.onClick.AddListener(OnAddBulletClicked);
        closeButton.onClick.AddListener(OnCloseClicked);
    }

    private void OnAddBulletClicked()
    {
        if (coinController.HasEnoughCoins(bulletUpgradeCost) && playerShoot.TryUpgradeShotsPerFire())
        {
            coinController.SpendCoins(bulletUpgradeCost);
            feedbackText.text = "Upgrade purchased!";
        }
        else if (playerShoot.shotsPerFire >= playerShoot.maxShotsPerFire)
        {
            feedbackText.text = "Maximum shots per fire reached!";
        }
        else
        {
            feedbackText.text = "Not enough coins to buy +1 Bullet!";
        }
    }

    private void OnCloseClicked()
    {
        gameObject.SetActive(false);
        GamePauseManager.SetPaused(false);
    }
}