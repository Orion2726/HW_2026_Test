using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameMode
    {
        Normal,
        Endless
    }

    [Header("Game Mode")]
    public GameMode currentMode = GameMode.Normal;

    [Header("Player")]
    public GameObject doofus;
    public float fallHeight = -5f;

    [Header("UI")]
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    public GameObject Score;

    [Header("Score")]
    public ScoreManager scoreManager;

    private bool gameOver = false;
    private bool victory = false;

    void Update()
    {
        if (gameOver || victory)
            return;

        if (doofus.transform.position.y < fallHeight)
        {
            GameOver();
        }
    }

    public void SetGameMode(GameMode mode)
    {
        currentMode = mode;

        Debug.Log("Game Mode: " + currentMode);
    }

    public bool IsNormalMode()
    {
        return currentMode == GameMode.Normal;
    }

    public bool IsEndlessMode()
    {
        return currentMode == GameMode.Endless;
    }

    void GameOver()
    {
        gameOver = true;

        scoreManager.UpdateGameOverScore();

        Score.SetActive(false);

        gameOverPanel.SetActive(true);

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayGameOver();
        }

        Time.timeScale = 0f;
    }

    public void Victory()
    {
        // Endless mode can NEVER have a victory
        if (currentMode != GameMode.Normal)
            return;

        if (victory || gameOver)
            return;

        victory = true;

        Score.SetActive(false);

        victoryPanel.SetActive(true);

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayUIClick();
        }

        Time.timeScale = 0f;

        Debug.Log("VICTORY!");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}