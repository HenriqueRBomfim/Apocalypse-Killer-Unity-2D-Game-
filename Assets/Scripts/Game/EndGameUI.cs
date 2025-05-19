using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
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

    [JsonProperty("email")]
    public string email;
}

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text finalTimeText;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_Text aviso;

    private int score;
    private int timeInSeconds;

    private void Start()
    {
        score = GameManager.Instance.FinalScore;
        float timeElapsed = GameManager.Instance.FinalTime;

        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        finalScoreText.text = $"Score Final: {score}";
        finalTimeText.text = $"Tempo Sobrevivido: {formattedTime}";

        timeInSeconds = Mathf.FloorToInt(timeElapsed);
    }

    public void OnSubmitScore()
    {
        string playerName = nameInputField.text.Trim();
        string playerEmail = emailInputField.text.Trim();

        aviso.gameObject.SetActive(false);

        if (string.IsNullOrEmpty(playerName))
        {
            aviso.text = "Digite seu nome.";
            aviso.gameObject.SetActive(true);
            Debug.LogWarning("Nome do jogador está vazio.");
            return;
        }

        if (string.IsNullOrEmpty(playerEmail))
        {
            aviso.text = "Digite seu email.";
            aviso.gameObject.SetActive(true);
            Debug.LogWarning("Email do jogador está vazio.");
            return;
        }

        StartCoroutine(GetTokenAndSubmitScore(playerName, playerEmail, score, timeInSeconds));
    }

    private IEnumerator GetTokenAndSubmitScore(string name, string email, int score, int timeInSeconds)
    {
        Debug.Log("Solicitando token do servidor...");

        ScoreData scoreData = new ScoreData
        {
            name = name,
            email = email,
            score = score,
            time_survived = timeInSeconds
        };

        string json = JsonConvert.SerializeObject(scoreData);

        // Primeiro: solicitar o token
        UnityWebRequest tokenRequest = new UnityWebRequest("https://unity-score-api.vercel.app/api/token", "POST")
        {
            uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json)),
            downloadHandler = new DownloadHandlerBuffer()
        };
        tokenRequest.SetRequestHeader("Content-Type", "application/json");

        yield return tokenRequest.SendWebRequest();

        if (tokenRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Erro ao obter token: " + tokenRequest.error);
            Debug.LogError("Resposta: " + tokenRequest.downloadHandler.text);
            aviso.text = "Erro ao enviar o score.";
            aviso.gameObject.SetActive(true);
            yield break;
        }

        string tokenJson = tokenRequest.downloadHandler.text;
        var tokenResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(tokenJson);
        if (!tokenResponse.TryGetValue("token", out string token))
        {
            Debug.LogError("Token não recebido.");
            aviso.text = "Erro ao autenticar envio.";
            aviso.gameObject.SetActive(true);
            yield break;
        }

        Debug.Log("Token recebido com sucesso.");

        // Segundo: enviar score com o token
        UnityWebRequest scoreRequest = new UnityWebRequest("https://unity-score-api.vercel.app/api/score", "POST")
        {
            uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json)),
            downloadHandler = new DownloadHandlerBuffer()
        };
        scoreRequest.SetRequestHeader("Content-Type", "application/json");
        scoreRequest.SetRequestHeader("Authorization", $"Bearer {token}");

        yield return scoreRequest.SendWebRequest();

        if (scoreRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Score enviado com sucesso!");
        }
        else
        {
            Debug.LogError("Erro ao enviar score: " + scoreRequest.error);
            Debug.LogError("Resposta: " + scoreRequest.downloadHandler.text);
            aviso.text = "Erro ao enviar o score.";
            aviso.gameObject.SetActive(true);
        }
    }
}
