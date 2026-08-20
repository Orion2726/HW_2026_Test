using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;
    private int score = 0;
    void Start()
    {
        UpdateScoreUI();
    }
    public void AddScore()
    {
        score++;
        UpdateScoreUI();
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayScoreIncrease();
        }
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