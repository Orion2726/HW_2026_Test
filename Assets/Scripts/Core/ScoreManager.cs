using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;

    private int score = 0;

    public int Score => score;

    void Start()
    {
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

        // Check for victory
        if (score >= 50)
        {
            GameManager gameManager = FindFirstObjectByType<GameManager>();

            if (gameManager != null)
            {
                gameManager.Victory();
            }
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
        return score >= 1;
    }

    public bool IsDoubleJumpUnlocked()
    {
        return score >= 2;
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