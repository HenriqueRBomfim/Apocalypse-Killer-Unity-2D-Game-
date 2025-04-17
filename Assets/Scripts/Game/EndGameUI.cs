using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;

[System.Serializable]
public class ScoreData
{
    [JsonProperty("name")]
    public string name;

    [JsonProperty("score")]
    public int score;

    [JsonProperty("time_survived")]
    public int time_survived;
}


public class EndGameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text finalTimeText;
    [SerializeField] private TMP_InputField nameInputField;

    private int score;
    private int timeInSeconds;

    private void Start()
    {
        score = GameManager.Instance.FinalScore;
        float timeElapsed = GameManager.Instance.FinalTime;

        // Calcular minutos e segundos para exibição
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Exibir o score final e o tempo na tela
        finalScoreText.text = $"Score Final: {score}";
        finalTimeText.text = $"Tempo Sobrevivido: {formattedTime}";

        timeInSeconds = Mathf.FloorToInt(timeElapsed);
    }

    // Essa função é chamada por um botão "Enviar Score"
    public void OnSubmitScore()
    {
        string playerName = nameInputField.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Nome do jogador está vazio.");
            return;
        }

        StartCoroutine(SubmitScore(playerName, score, timeInSeconds));
    }

    private IEnumerator SubmitScore(string name, int score, int timeInSeconds)
    {
        Debug.Log("Enviando score para o servidor...");
        Debug.Log($"Nome: {name}, Score: {score}, Tempo: {timeInSeconds} segundos");

        ScoreData scoreData = new ScoreData
        {
            name = name,
            score = score,
            time_survived = timeInSeconds
        };

        string json = JsonConvert.SerializeObject(scoreData);
        Debug.Log("JSON gerado: " + json);

        UnityWebRequest request = new UnityWebRequest("https://www.henriquerochabomfim.com.br/api/score", "POST")
        {
            uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json)),
            downloadHandler = new DownloadHandlerBuffer()
        };
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Score enviado com sucesso!");
        }
        else
        {
            Debug.LogError("Erro ao enviar o score: " + request.error);
            Debug.LogError("Resposta do servidor: " + request.downloadHandler.text); // Mostra a mensagem detalhada
        }
    }
}
