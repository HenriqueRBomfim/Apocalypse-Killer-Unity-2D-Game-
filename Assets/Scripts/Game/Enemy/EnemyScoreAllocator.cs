using UnityEngine;

public class EnemyScoreAllocator : MonoBehaviour
{
    [SerializeField]
    private int killScore;
    private ScoreController scoreController;
    private EnemyDrop enemyDrop;
    private void Awake()
    {
        scoreController = FindFirstObjectByType<ScoreController>();
        enemyDrop = GetComponent<EnemyDrop>();
    }

    public void AllocateScore()
    {
        scoreController.AddScore(killScore);

        // Chama o drop do inimigo, se existir
        if (enemyDrop != null)
        {
            enemyDrop.Drop(killScore/10);
        }

    }
}