using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    public float timeElapsed = 0f;
    public float timeScale = 1.0f;
    public TextMeshProUGUI timeText; 

    void Update()
    {
        timeElapsed += Time.deltaTime * timeScale;
        UpdateTimeUI(); 
    }

    private void UpdateTimeUI()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        timeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }
}
