using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TimeController : MonoBehaviour
{
    public float timeElapsed = 0f;
    public float timeScale = 1.0f;
    public TextMeshProUGUI timeText;

    public List<GameObject> roomEntrances; // Entrada Sala 2 = index 0, Entrada Sala 3 = index 1, etc.

    private int nextRoomIndex = 0; // Índice da próxima porta a ser destruída
    private float nextUnlockTime = 120f; // Tempo (em segundos) para a próxima porta abrir

    void Update()
    {
        timeElapsed += Time.deltaTime * timeScale;
        UpdateTimeUI();
        CheckForRoomUnlocks();
    }

    public float GetElapsedTime()
    {
        return timeElapsed;
    }

    private void UpdateTimeUI()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        timeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    private void CheckForRoomUnlocks()
    {
        if (nextRoomIndex < roomEntrances.Count && timeElapsed >= nextUnlockTime)
        {
            Debug.Log($"Abrindo Sala {nextRoomIndex + 2}!");
            Destroy(roomEntrances[nextRoomIndex]);
            nextRoomIndex++;
            nextUnlockTime += 120f; // Próxima porta em mais 2 minutos
        }
    }
}
