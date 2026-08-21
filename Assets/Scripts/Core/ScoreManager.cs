using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;

    private int score = 0;

    public int Score => score;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        UpdateScoreUI();
    }

    public void AddScore()
    {
        // Determine how many points this platform is worth
        int pointsToAdd = GetPointsPerPulpit();

        score += pointsToAdd;

        UpdateScoreUI();

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayScoreIncrease();
        }

        // Victory only exists in Normal Mode
        if (gameManager != null &&
            gameManager.IsNormalMode() &&
            score >= 50)
        {
            gameManager.Victory();
        }
    }

    int GetPointsPerPulpit()
    {
        if (score >= 25)
        {
            return 3;
        }

        if (score >= 10)
        {
            return 2;
        }

        return 1;
    }

    public bool IsJumpUnlocked()
    {
        return score >= 10;
    }

    public bool IsDoubleJumpUnlocked()
    {
        return score >= 25;
    }

    void UpdateScoreUI()
    {
        scoreText.text = "SCORE: " + score;
    }

    public void UpdateGameOverScore()
    {
        gameOverScoreText.text = "SCORE: " + score;
    }
}