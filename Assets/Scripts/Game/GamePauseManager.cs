using UnityEngine;

public class GamePauseManager : MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    public static void SetPaused(bool paused)
    {
        IsPaused = paused;
        Time.timeScale = paused ? 0f : 1f; // Opcional: pausa física do jogo
    }
}