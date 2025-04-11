using UnityEngine;

public class MedkitSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject medkitPrefab;

    [SerializeField]
    private float minimumSpawnTime = 8f;

    [SerializeField]
    private float maximumSpawnTime = 20f;

    [SerializeField]
    private float spawnAccelerationRate = 0.98f;

    [SerializeField]
    private float minimumLimit = 0.5f;

    private float timeUntilSpawn;
    private float elapsedTime;
    private float spawnRadius = 4f;

    private int currentMedkitCount = 0;
    private const int maxMedkits = 2; // Limite de medkits simultâneos

    void Awake()
    {
        SetTimeUntilSpawn();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0 && currentMedkitCount < maxMedkits)
        {
            SpawnMedkit();
            SetTimeUntilSpawn();
        }

        if (elapsedTime >= 4f)
        {
            AccelerateSpawnRate();
            elapsedTime = 0f;
        }
    }

    private void SpawnMedkit()
    {
        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = new Vector3(transform.position.x + randomOffset.x, transform.position.y + randomOffset.y, 0f);

        GameObject medkit = Instantiate(medkitPrefab, spawnPosition, Quaternion.identity);
        
        // Adiciona um script para chamar o método quando o medkit for destruído
        medkit.AddComponent<Medkit>().SetSpawner(this);
        
        currentMedkitCount++;
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }

    private void AccelerateSpawnRate()
    {
        minimumSpawnTime = Mathf.Max(minimumSpawnTime * spawnAccelerationRate, minimumLimit);
        maximumSpawnTime = Mathf.Max(maximumSpawnTime * spawnAccelerationRate, minimumLimit);
    }

    // Método chamado para reduzir o contador de medkits
    public void MedkitCollected()
    {
        currentMedkitCount = Mathf.Max(0, currentMedkitCount - 1);
    }
}
