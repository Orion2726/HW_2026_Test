using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip music;
    public AudioClip uiClick;
    public AudioClip scoreIncrease;
    public AudioClip gameOver;
    [Header("SFX Volumes")]
    [Range(0f, 1f)] public float uiClickVolume = 1f;
    [Range(0f, 1f)] public float scoreVolume = 1f;
    [Range(0f, 1f)] public float gameOverVolume = 1f;
    [Header("Music")]
    [Range(0f, 1f)] public float musicVolume = 0.073f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (musicSource != null && music != null)
        {
            musicSource.clip = music;
            musicSource.loop = true;
            //musicSource.volume = musicVolume;
            musicSource.Play();
        }
    }

    public void PlayUIClick()
    {
        if (sfxSource != null && uiClick != null)
            sfxSource.PlayOneShot(uiClick, uiClickVolume);
    }

    public void PlayScoreIncrease()
    {
        if (sfxSource != null && scoreIncrease != null)
            sfxSource.PlayOneShot(scoreIncrease, scoreVolume);
    }

    public void PlayGameOver()
    {
        if (sfxSource != null && gameOver != null)
            sfxSource.PlayOneShot(gameOver, gameOverVolume);
    }
}