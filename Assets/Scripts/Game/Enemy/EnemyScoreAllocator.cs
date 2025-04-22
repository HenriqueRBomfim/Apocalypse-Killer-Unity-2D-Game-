using UnityEngine;

public class EnemyScoreAllocator : MonoBehaviour
{
    [SerializeField]
    private int killScore;
    
    private ScoreController scoreController;
    private CoinController coinController;
    private void Awake()
    {
        scoreController = FindFirstObjectByType<ScoreController>();
        coinController = FindFirstObjectByType<CoinController>();
    }

    public void AllocateScore()
    {
        scoreController.AddScore(killScore);
        coinController.AddCoins(killScore/10);
        Debug.Log($"Allocated {killScore} score and coins to player.");
    }
}