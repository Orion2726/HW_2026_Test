using UnityEngine;
public class Pulpit : MonoBehaviour
{
    public float minLifetime = 6f;
    public float maxLifetime = 8f;
    public float spawnTimeBeforeDeath = 2.08f; // tuned to your sweet spot
    private float lifetime;
    private float timer;
    private bool nextPulpitSpawned = false;

    void Start()
    {
        lifetime = Random.Range(minLifetime, maxLifetime);
        timer = lifetime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= spawnTimeBeforeDeath && !nextPulpitSpawned)
        {
            PulpitSpawner spawner = FindFirstObjectByType<PulpitSpawner>();
            if (spawner != null) spawner.SpawnNextPulpit();
            nextPulpitSpawned = true;
        }

        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}