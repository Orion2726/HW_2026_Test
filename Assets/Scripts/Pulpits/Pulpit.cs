using UnityEngine;

public class Pulpit : MonoBehaviour
{
    private float minLifetime;
    private float maxLifetime;
    private float spawnTimeBeforeDeath;

    private float lifetime;
    private float timer;

    private bool nextPulpitSpawned = false;

    void Start()
    {
        minLifetime =
            DoofusDiaryLoader.Config.pulpit_data.min_pulpit_destroy_time;

        maxLifetime =
            DoofusDiaryLoader.Config.pulpit_data.max_pulpit_destroy_time;

        spawnTimeBeforeDeath =
            DoofusDiaryLoader.Config.pulpit_data.pulpit_spawn_time;

        lifetime = Random.Range(minLifetime, maxLifetime);

        timer = lifetime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= spawnTimeBeforeDeath && !nextPulpitSpawned)
        {
            PulpitSpawner spawner =
                FindFirstObjectByType<PulpitSpawner>();

            if (spawner != null)
            {
                spawner.SpawnNextPulpit();
            }

            nextPulpitSpawned = true;
        }

        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}