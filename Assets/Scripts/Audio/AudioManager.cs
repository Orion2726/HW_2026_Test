using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    public AudioClip backgroundMusic;

    [Header("Sound Effects")]
    public AudioClip uiClickSound;
    public AudioClip scoreIncreaseSound;
    public AudioClip gameOverSound;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    void Awake()
    {
        // Prevent duplicate AudioManagers
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Music source
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.playOnAwake = false;

        // SFX source
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
    }

    void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
    }

    public void PlayUIClick()
    {
        if (uiClickSound != null)
        {
            sfxSource.PlayOneShot(uiClickSound);
        }
    }

    public void PlayScoreIncrease()
    {
        if (scoreIncreaseSound != null)
        {
            sfxSource.PlayOneShot(scoreIncreaseSound);
        }
    }

    public void PlayGameOver()
    {
        if (gameOverSound != null)
        {
            sfxSource.PlayOneShot(gameOverSound);
        }
    }
}