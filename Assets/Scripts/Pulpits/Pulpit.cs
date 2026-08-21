using UnityEngine;
using TMPro;
using System.Collections;

public class Pulpit : MonoBehaviour
{
    [Header("Normal Lifetime")]
    public float minLifetime = 6f;
    public float maxLifetime = 8f;

    [Header("Jump Phase Lifetime")]
    public float jumpMinLifetime = 5f;
    public float jumpMaxLifetime = 7f;

    [Header("Double Jump Phase Lifetime")]
    public float doubleJumpMinLifetime = 4f;
    public float doubleJumpMaxLifetime = 6f;

    [Header("Spawning")]
    public float spawnTimeBeforeDeath = 2.08f;

    [Header("Fade")]
    public float fadeInTime = 0.35f;
    public float fadeOutTime = 0.35f;

    public TextMeshProUGUI timerText;

    private float lifetime;
    private float timer;

    private bool nextPulpitSpawned = false;
    private bool fadeOutStarted = false;

    private Renderer[] renderers;
    private Material[] materials;

    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();

        SetLifetime();

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

        // Start timer text
        UpdateTimerText();

        // Fade in
        StartCoroutine(FadeIn());
    }

    void SetLifetime()
    {
        if (scoreManager == null)
        {
            lifetime = Random.Range(minLifetime, maxLifetime);
        }
        else if (scoreManager.Score >= 25)
        {
            // DOUBLE JUMP PHASE
            lifetime = Random.Range(
                doubleJumpMinLifetime,
                doubleJumpMaxLifetime
            );
        }
        else if (scoreManager.Score >= 10)
        {
            // SINGLE JUMP PHASE
            lifetime = Random.Range(
                jumpMinLifetime,
                jumpMaxLifetime
            );
        }
        else
        {
            // NORMAL PHASE
            lifetime = Random.Range(
                minLifetime,
                maxLifetime
            );
        }

        timer = lifetime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        // Update countdown text
        UpdateTimerText();

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

        // Start fading out only once
        if (timer <= fadeOutTime && !fadeOutStarted)
        {
            fadeOutStarted = true;

            StartCoroutine(FadeOut());
        }
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.Max(0f, timer).ToString("0.00");
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

        SetAlpha(0f);

        Destroy(gameObject);
    }

    void SetAlpha(float alpha)
    {
        // Fade pulpit materials
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

        // Fade timer text
        if (timerText != null)
        {
            Color textColor = timerText.color;
            textColor.a = alpha;
            timerText.color = textColor;
        }
    }
}