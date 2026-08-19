using UnityEngine;

public class PulpitSpawner : MonoBehaviour
{
    public GameObject pulpitPrefab;

    public float pulpitSize = 1f;

    private Vector3 currentPulpitPosition;

    void Start()
    {
        currentPulpitPosition = Vector3.zero;

        SpawnPulpit(currentPulpitPosition);
    }

    public void SpawnNextPulpit()
    {
        Vector3 newPosition = currentPulpitPosition;

        // For now, always spawn forward
        newPosition += Vector3.forward * pulpitSize;

        SpawnPulpit(newPosition);

        currentPulpitPosition = newPosition;
    }

    void SpawnPulpit(Vector3 position)
    {
        Instantiate(pulpitPrefab, position, Quaternion.identity);
    }
}