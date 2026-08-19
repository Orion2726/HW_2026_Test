using UnityEngine;
using System.Collections;

public class PulpitSpawner : MonoBehaviour
{
    public GameObject pulpitPrefab;

    public float pulpitSize = 6.7f;

    public float spawnDelay = 2f;

    private Vector3 currentPulpitPosition;

    private GameObject currentPulpit;
    private GameObject previousPulpit;

    void Start()
    {
        currentPulpitPosition = Vector3.zero;

        currentPulpit = SpawnPulpit(currentPulpitPosition);

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // Wait before creating the next Pulpit
            yield return new WaitForSeconds(spawnDelay);

            SpawnNextPulpit();
        }
    }

    void SpawnNextPulpit()
    {
        Vector3 direction = GetRandomDirection();

        Vector3 newPosition = currentPulpitPosition;

        if (direction == Vector3.forward)
        {
            newPosition += new Vector3(0.0f, 0.0f, 6.7f);
        }
        else if (direction == Vector3.back)
        {
            newPosition += new Vector3(0.0f, 0.0f, -6.7f);
        }
        else if (direction == Vector3.right)
        {
            newPosition += new Vector3(6.7f, 0.0f, 0.0f);
        }
        else if (direction == Vector3.left)
        {
            newPosition += new Vector3(-6.7f, 0.0f, 0.0f);
        }

        // The current Pulpit becomes the previous Pulpit
        previousPulpit = currentPulpit;

        // Spawn the new Pulpit
        currentPulpit = SpawnPulpit(newPosition);

        currentPulpitPosition = newPosition;

        // The old Pulpit disappears
        Destroy(previousPulpit);
    }

    Vector3 GetRandomDirection()
    {
        int randomDirection = Random.Range(0, 4);

        switch (randomDirection)
        {
            case 0:
                return Vector3.forward;

            case 1:
                return Vector3.back;

            case 2:
                return Vector3.right;

            default:
                return Vector3.left;
        }
    }

    GameObject SpawnPulpit(Vector3 position)
    {
        return Instantiate(
            pulpitPrefab,
            position,
            Quaternion.identity
        );
    }
}