using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maxHealth;

    private PlayerMovement playerMovement;

    public float RemainingHealthPercentage
    {
        get { return currentHealth / maxHealth; }
    }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        
        // Apenas adiciona o listener se o objeto tiver PlayerMovement
        if (playerMovement != null)
        {
            OnDied.AddListener(playerMovement.StopMovement);
        }
    }

    public bool IsInvincible { get; set; }

    public UnityEvent OnDied;
    public UnityEvent OnDamaged;

    public void TakeDamage(float damageAmount)
    {
        if (IsInvincible)
        {
            return;
        }
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
        }
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        if (currentHealth == 0)
        {
            OnDied.Invoke();
        }
        else
        {
            OnDamaged.Invoke();
        }
    }

    public void Heal(float healAmount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += healAmount;
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
