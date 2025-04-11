using UnityEngine;
using TMPro;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text finalTimeText;

    private void Start()
    {
        int score = GameManager.Instance.FinalScore;
        float timeElapsed = GameManager.Instance.FinalTime;

        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        finalScoreText.text = $"Score Final: {score}";
        finalTimeText.text = $"Tempo Sobrevivido: {formattedTime}";
    }
}
