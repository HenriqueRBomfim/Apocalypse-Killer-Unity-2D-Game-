using UnityEngine;

public class Medkit : MonoBehaviour
{
    private MedkitSpawner spawner;

    public void SetSpawner(MedkitSpawner medkitSpawner)
    {
        spawner = medkitSpawner;
    }

    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.MedkitCollected();
        }
    }
}
