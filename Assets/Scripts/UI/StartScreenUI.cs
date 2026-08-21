using UnityEngine;

public class StartScreenUI : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject score;

    private GameManager gameManager;

    void Start()
    {
        Time.timeScale = 0f;

        gameManager = FindFirstObjectByType<GameManager>();

        startPanel.SetActive(true);
        score.SetActive(false);
    }

    public void StartNormalMode()
    {
        if (gameManager != null)
        {
            gameManager.SetGameMode(
                GameManager.GameMode.Normal
            );
        }

        StartGame();
    }

    public void StartEndlessMode()
    {
        if (gameManager != null)
        {
            gameManager.SetGameMode(
                GameManager.GameMode.Endless
            );
        }

        StartGame();
    }

    void StartGame()
    {
        startPanel.SetActive(false);
        score.SetActive(true);

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayUIClick();
        }

        Time.timeScale = 1f;
    }
}