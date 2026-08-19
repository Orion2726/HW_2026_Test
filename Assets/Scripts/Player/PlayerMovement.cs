using UnityEngine;

public class DoofusController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public PulpitSpawner pulpitSpawner;

    private bool spawnedNextPulpit = false;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement =
            new Vector3(horizontal, 0f, vertical).normalized;

        transform.Translate(
            movement * moveSpeed * Time.deltaTime,
            Space.World
        );

        CheckForEdge(horizontal, vertical);
    }

    void CheckForEdge(float horizontal, float vertical)
    {
        if (spawnedNextPulpit)
            return;

        // Forward
        if (vertical > 0 && transform.position.z >= 3.0f)
        {
            SpawnNext(Vector3.forward);
        }

        // Backward
        else if (vertical < 0 && transform.position.z <= -3.0f)
        {
            SpawnNext(Vector3.back);
        }

        // Right
        else if (horizontal > 0 && transform.position.x >= 3.0f)
        {
            SpawnNext(Vector3.right);
        }

        // Left
        else if (horizontal < 0 && transform.position.x <= -3.0f)
        {
            SpawnNext(Vector3.left);
        }
    }

    void SpawnNext(Vector3 direction)
    {
        pulpitSpawner.SpawnNextPulpit(direction);

        spawnedNextPulpit = true;
    }
}