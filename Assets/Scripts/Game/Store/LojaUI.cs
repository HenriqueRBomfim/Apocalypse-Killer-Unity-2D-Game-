using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LojaUI : MonoBehaviour
{
    public PlayerShoot playerShoot;
    public CoinController coinController;
    public RubyController rubyController;
    public Button addBulletButton;
    public Button buyDragonPetButton;
    public Button closeButton;
    public TMP_Text feedbackText;
    public TMP_Text dragonPetPriceText;
    public int bulletUpgradeCost = 20;
    public int bulletUpgradeRubyCost = 1;
    public GameObject dragonPetPrefab;
    public Transform petSpawnPoint; // Defina onde o pet deve aparecer

    private bool dragonPetPurchased = false;

    private void Awake()
    {
        addBulletButton.onClick.AddListener(OnAddBulletClicked);
        buyDragonPetButton.onClick.AddListener(OnBuyDragonPetClicked);
        closeButton.onClick.AddListener(OnCloseClicked);
        UpdateDragonPetPriceText();
    }

    private void UpdateDragonPetPriceText()
    {
        if (dragonPetPrefab != null && dragonPetPriceText != null)
        {
            Pets dragonPetData = dragonPetPrefab.GetComponent<Pets>();
            if (dragonPetData != null)
            {
                dragonPetPriceText.text = $"{dragonPetData.priceCoins} coins\n{dragonPetData.priceRubies} ruby";
            }
            else
            {
                dragonPetPriceText.text = "Price unavailable";
            }
        }
    }

    private void OnAddBulletClicked()
    {
        if (playerShoot.shotsPerFire >= playerShoot.maxShotsPerFire)
        {
            feedbackText.text = "Maximum shots per fire reached!";
            return;
        }

        // Tenta comprar com coins primeiro
        if (coinController.HasEnoughCoins(bulletUpgradeCost))
        {
            coinController.SpendCoins(bulletUpgradeCost);
            playerShoot.TryUpgradeShotsPerFire();
            feedbackText.text = "Upgrade purchased with coins!";
        }
        // Se não tiver coins, tenta comprar com rubies
        else if (rubyController != null && rubyController.HasEnoughRubies(bulletUpgradeRubyCost))
        {
            rubyController.SpendRubies(bulletUpgradeRubyCost);
            playerShoot.TryUpgradeShotsPerFire();
            feedbackText.text = "Upgrade purchased with ruby!";
        }
        else
        {
            feedbackText.text = "Not enough coins or rubies to buy +1 Bullet!";
        }
    }

    private void OnBuyDragonPetClicked()
    {
        if (dragonPetPurchased)
        {
            feedbackText.text = "Dragon Pet already purchased!";
            return;
        }

        Pets dragonPetData = dragonPetPrefab.GetComponent<Pets>();
        if (dragonPetData == null)
        {
            feedbackText.text = "Dragon Pet data not found!";
            return;
        }

        int coinCost = dragonPetData.priceCoins;
        int rubyCost = dragonPetData.priceRubies;

        if (coinController.HasEnoughCoins(coinCost))
        {
            coinController.SpendCoins(coinCost);
            SpawnDragonPet();
            feedbackText.text = "Dragon Pet purchased with coins!";
            dragonPetPurchased = true;
        }
        else if (rubyController != null && rubyController.HasEnoughRubies(rubyCost))
        {
            rubyController.SpendRubies(rubyCost);
            SpawnDragonPet();
            feedbackText.text = "Dragon Pet purchased with ruby!";
            dragonPetPurchased = true;
        }
        else
        {
            feedbackText.text = "Not enough coins or rubies for Dragon Pet!";
        }
    }

    private void SpawnDragonPet()
    {
        if (dragonPetPrefab != null)
        {
            Vector3 spawnPos = petSpawnPoint != null ? petSpawnPoint.position : playerShoot.transform.position + Vector3.right * 2f;
            Instantiate(dragonPetPrefab, spawnPos, Quaternion.identity);
        }
    }

    private void OnCloseClicked()
    {
        gameObject.SetActive(false);
        GamePauseManager.SetPaused(false);
    }
}