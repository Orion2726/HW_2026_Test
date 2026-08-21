using UnityEngine;
using TMPro;
using System.Collections;

public class Pulpit : MonoBehaviour
{

    [Header("Jump Phase Difficulty")]
    public float jumpLifetimeMultiplier = 0.9f;

    [Header("Double Jump Phase Difficulty")]
    public float doubleJumpLifetimeMultiplier = 0.8f;

    [Header("Fade")]
    public float fadeInTime = 0.35f;
    public float fadeOutTime = 0.35f;

    [Header("Timer")]
    public TextMeshPro timerText;

    [Header("Special Pulpit Materials")]
    public Material normalMaterial;
    public Material sideToSideMaterial;
    public Material diagonalMaterial;
    public Material upDownMaterial;

    [Header("Side To Side Movement")]
    public float sideToSideDistance = 1.5f;
    public float sideToSideSpeed = 1.5f;

    [Header("Diagonal Movement")]
    public float diagonalDistance = 1.5f;
    public float diagonalSpeed = 1.2f;

    [Header("Up Down + Side Movement")]
    public float verticalDistance = 0.8f;
    public float upDownSideDistance = 1.5f;
    public float upDownSpeed = 1.3f;

    private float lifetime;
    private float timer;

    private bool nextPulpitSpawned = false;
    private bool fadeOutStarted = false;

    private Renderer[] renderers;
    private Material[] materials;

    private ScoreManager scoreManager;

    private Vector3 startingPosition;

    private PulpitType pulpitType;

    private enum PulpitType
    {
        Normal,
        SideToSide,
        Diagonal,
        UpDown
    }

    void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();

        startingPosition = transform.position;

        // Get all renderers in the pulpit
        renderers = GetComponentsInChildren<Renderer>();

        // Do NOT include the timer renderer
        renderers = System.Array.FindAll(
            renderers,
            r => timerText == null || r.gameObject != timerText.gameObject
        );

        // Choose pulpit type and material
        ChoosePulpitType();

        // Set lifetime using JSON + score progression
        SetLifetime();

        // Start invisible
        SetAlpha(0f);

        // Start timer text
        UpdateTimerText();

        // Fade in
        StartCoroutine(FadeIn());
    }

    void SetLifetime()
    {
        if (DoofusDiaryLoader.Config == null)
        {
            Debug.LogError("Doofus Diary Config has not been loaded!");
            return;
        }

        // Base lifetime comes directly from JSON
        float jsonMinLifetime =
            DoofusDiaryLoader.Config.pulpit_data.min_pulpit_destroy_time;

        float jsonMaxLifetime =
            DoofusDiaryLoader.Config.pulpit_data.max_pulpit_destroy_time;

        int currentScore = 0;

        if (scoreManager != null)
        {
            currentScore = scoreManager.Score;
        }

        if (currentScore < 10)
        {
            // Normal phase
            lifetime = Random.Range(
                jsonMinLifetime,
                jsonMaxLifetime
            );
        }
        else if (currentScore < 25)
        {
            // Single jump phase
            lifetime = Random.Range(
                jsonMinLifetime,
                jsonMaxLifetime
            );

            lifetime *= jumpLifetimeMultiplier;
        }
        else
        {
            // Double jump phase
            lifetime = Random.Range(
                jsonMinLifetime,
                jsonMaxLifetime
            );

            lifetime *= doubleJumpLifetimeMultiplier;
        }

        timer = lifetime;
    }

    void ChoosePulpitType()
    {
        int currentScore = 0;

        if (scoreManager != null)
        {
            currentScore = scoreManager.Score;
        }

        // First 10 points = normal stationary pulpits
        if (currentScore < 10)
        {
            pulpitType = PulpitType.Normal;

            ApplyMaterial(normalMaterial);

            return;
        }

        // Score 10+ = random special pulpit
        int randomType = Random.Range(0, 3);

        switch (randomType)
        {
            case 0:

                pulpitType = PulpitType.SideToSide;

                ApplyMaterial(sideToSideMaterial);

                break;

            case 1:

                pulpitType = PulpitType.Diagonal;

                ApplyMaterial(diagonalMaterial);

                break;

            case 2:

                pulpitType = PulpitType.UpDown;

                ApplyMaterial(upDownMaterial);

                break;
        }
    }

    void ApplyMaterial(Material selectedMaterial)
    {
        if (selectedMaterial == null)
            return;

        materials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            // Create individual material instance
            materials[i] = new Material(selectedMaterial);

            renderers[i].material = materials[i];
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;

        // Update countdown text
        UpdateTimerText();

        // Move special pulpit
        HandleMovement();

        // Spawn next pulpit
        if (timer <= GetSpawnTime() && !nextPulpitSpawned)
        {
            PulpitSpawner spawner =
                FindFirstObjectByType<PulpitSpawner>();

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

    float GetSpawnTime()
    {
        if (DoofusDiaryLoader.Config != null)
        {
            return DoofusDiaryLoader.Config
                .pulpit_data
                .pulpit_spawn_time;
        }

        return 2.5f;
    }

    void HandleMovement()
    {
        float time = Time.time;

        switch (pulpitType)
        {
            case PulpitType.Normal:

                transform.position =
                    startingPosition;

                break;

            case PulpitType.SideToSide:

                transform.position =
                    startingPosition +
                    Vector3.right *
                    Mathf.Sin(time * sideToSideSpeed) *
                    sideToSideDistance;

                break;

            case PulpitType.Diagonal:

                float diagonal =
                    Mathf.Sin(time * diagonalSpeed);

                transform.position =
                    startingPosition +
                    new Vector3(
                        diagonal * diagonalDistance,
                        0f,
                        diagonal * diagonalDistance
                    );

                break;

            case PulpitType.UpDown:

                float horizontal =
                    Mathf.Sin(time * upDownSpeed) *
                    upDownSideDistance;

                float vertical =
                    Mathf.Sin(time * upDownSpeed) *
                    verticalDistance;

                transform.position =
                    startingPosition +
                    new Vector3(
                        horizontal,
                        vertical,
                        0f
                    );

                break;
        }
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text =
                Mathf.Max(0f, timer).ToString("0");
        }
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;

        while (elapsed < fadeInTime)
        {
            elapsed += Time.deltaTime;

            float alpha =
                Mathf.Clamp01(
                    elapsed / fadeInTime
                );

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

            float alpha =
                1f -
                Mathf.Clamp01(
                    elapsed / fadeOutTime
                );

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
                Color color =
                    material.GetColor("_BaseColor");

                color.a = alpha;

                material.SetColor(
                    "_BaseColor",
                    color
                );
            }
            else if (material.HasProperty("_Color"))
            {
                Color color =
                    material.color;

                color.a = alpha;

                material.color = color;
            }
        }

        // Fade timer text
        if (timerText != null)
        {
            Color textColor =
                timerText.color;

            textColor.a = alpha;

            timerText.color = textColor;
        }
    }
}