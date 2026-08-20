using UnityEngine;
using System.Collections;

public class Pulpit : MonoBehaviour
{
    public float minLifetime = 6f;
    public float maxLifetime = 8f;
    public float spawnTimeBeforeDeath = 2.08f;

    public float fadeInTime = 0.35f;
    public float fadeOutTime = 0.35f;

    private float lifetime;
    private float timer;
    private bool nextPulpitSpawned = false;

    private Renderer[] renderers;
    private Material[] materials;

    void Start()
    {
        lifetime = Random.Range(minLifetime, maxLifetime);
        timer = lifetime;

        // Get all renderers in the pulpit
        renderers = GetComponentsInChildren<Renderer>();

        // Create material instances
        materials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            materials[i] = renderers[i].material;
        }

        // Start invisible
        SetAlpha(0f);

        // Fade in
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        timer -= Time.deltaTime;

        // Spawn next pulpit
        if (timer <= spawnTimeBeforeDeath && !nextPulpitSpawned)
        {
            PulpitSpawner spawner = FindFirstObjectByType<PulpitSpawner>();

            if (spawner != null)
            {
                spawner.SpawnNextPulpit();
            }

            nextPulpitSpawned = true;
        }

        // Start fading out
        if (timer <= fadeOutTime)
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;

        while (elapsed < fadeInTime)
        {
            elapsed += Time.deltaTime;

            float alpha = Mathf.Clamp01(elapsed / fadeInTime);

            SetAlpha(alpha);

            yield return null;
        }

        SetAlpha(1f);
    }

    IEnumerator FadeOut()
    {
        float elapsed = 0f;

        while (elapsed < fadeOutTime)
        {
            elapsed += Time.deltaTime;

            float alpha = 1f - Mathf.Clamp01(elapsed / fadeOutTime);

            SetAlpha(alpha);

            yield return null;
        }

        Destroy(gameObject);
    }

    void SetAlpha(float alpha)
    {
        foreach (Material material in materials)
        {
            if (material.HasProperty("_BaseColor"))
            {
                Color color = material.GetColor("_BaseColor");
                color.a = alpha;
                material.SetColor("_BaseColor", color);
            }
            else if (material.HasProperty("_Color"))
            {
                Color color = material.color;
                color.a = alpha;
                material.color = color;
            }
        }
    }
}