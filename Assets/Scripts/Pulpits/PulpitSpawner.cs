using UnityEngine;
public class PulpitSpawner : MonoBehaviour
{
    public GameObject pulpitPrefab;
    public float pulpitSize_1 = 6.7f;
    public float pulpitSize_2 = 6.51f;
    private Vector3 currentPulpitPosition;

    void Start()
    {
        currentPulpitPosition = Vector3.zero;
        SpawnPulpit(currentPulpitPosition);
    }

    public void SpawnNextPulpit()
    {
        Vector3 direction = GetRandomDirection();
        Vector3 newPosition = currentPulpitPosition;

        if (direction == Vector3.forward) newPosition += new Vector3(0f, 0f, pulpitSize_1);
        else if (direction == Vector3.back) newPosition += new Vector3(0f, 0f, -pulpitSize_1);
        else if (direction == Vector3.right) newPosition += new Vector3(pulpitSize_2, 0f, 0f);
        else if (direction == Vector3.left) newPosition += new Vector3(-pulpitSize_2, 0f, 0f);

        SpawnPulpit(newPosition);
        currentPulpitPosition = newPosition; // spawner just tracks "where's the frontier", nothing else
    }

    Vector3 GetRandomDirection()
    {
        switch (Random.Range(0, 4))
        {
            case 0: return Vector3.forward;
            case 1: return Vector3.back;
            case 2: return Vector3.right;
            default: return Vector3.left;
        }
    }

    GameObject SpawnPulpit(Vector3 position)
    {
        return Instantiate(pulpitPrefab, position, Quaternion.identity);
    }
}