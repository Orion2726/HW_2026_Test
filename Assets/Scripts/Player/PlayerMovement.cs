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

        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;

        transform.Translate(
            movement * moveSpeed * Time.deltaTime,
            Space.World
        );

        CheckForNextPulpit();
    }

    void CheckForNextPulpit()
    {
        // When Doofus gets close to the forward edge
        if (transform.position.z >= 4.0f && !spawnedNextPulpit)
        {
            pulpitSpawner.SpawnNextPulpit();

            spawnedNextPulpit = true;
        }
    }
}