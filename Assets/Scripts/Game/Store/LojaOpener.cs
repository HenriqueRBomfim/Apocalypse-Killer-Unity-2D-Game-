using UnityEngine;
using UnityEngine.UI;

public class LojaOpener : MonoBehaviour
{
    public GameObject lojaUI; // Arraste aqui o GameObject do Canvas "Loja"
    public Button openLojaButton; // Arraste aqui o botão de abrir loja

    private void Awake()
    {
        openLojaButton.onClick.AddListener(ToggleLoja);
    }

    private void ToggleLoja()
    {
        bool isActive = !lojaUI.activeSelf;
        lojaUI.SetActive(isActive);
        GamePauseManager.SetPaused(isActive);
    }
}