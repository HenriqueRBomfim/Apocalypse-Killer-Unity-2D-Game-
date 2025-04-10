using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maxHealth;

    public float RemainingHealthPercentage
    {
        get { return currentHealth / maxHealth; }
    }

    public void TakeDamage(float damageAmount)
    {
        if (currentHealth <= 0)
        {
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
        }
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
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
            currentHealth = healAmount;
        }

    }

}
