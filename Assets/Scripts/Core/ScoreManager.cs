using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;

    public UnlockNotification unlockNotification;

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
        // Remember score before adding points
        int previousScore = score;

        // Determine how many points this platform is worth
        int pointsToAdd = GetPointsPerPulpit();

        score += pointsToAdd;

        UpdateScoreUI();

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayScoreIncrease();
        }

        // =========================
        // JUMP UNLOCK
        // =========================

        if (previousScore < 10 && score >= 10)
        {
            if (unlockNotification != null)
            {
                unlockNotification.ShowJumpUnlocked();
            }
        }

        // =========================
        // DOUBLE JUMP UNLOCK
        // =========================

        if (previousScore < 25 && score >= 25)
        {
            if (unlockNotification != null)
            {
                unlockNotification.ShowDoubleJumpUnlocked();
            }
        }

        // =========================
        // NORMAL MODE VICTORY
        // =========================

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