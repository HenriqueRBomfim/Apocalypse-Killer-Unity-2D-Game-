using UnityEngine;
using UnityEngine.Events;

public class RubyController : MonoBehaviour
{
    public UnityEvent OnRubiesChanged;
    public int rubies = 0; // Número de rubis coletados

    void Start()
    {
        rubies = 0;
    }

    // Método para obter a quantidade atual de rubis
    public int GetRubies()
    {
        return rubies;
    }

    // Método para verificar se o jogador tem rubis suficientes
    public bool HasEnoughRubies(int amount)
    {
        return rubies >= amount;
    }

    // Método para gastar rubis
    public void SpendRubies(int amount)
    {
        if (HasEnoughRubies(amount))
        {
            rubies -= amount;
            OnRubiesChanged.Invoke();
        }
        else
        {
            Debug.Log("Not enough rubies!");
        }
    }

    public void AddRubies(int amount)
    {
        rubies += amount;
        OnRubiesChanged.Invoke();
    }
}