using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject doofus;
    public GameObject gameOverPanel;
    public GameObject Score;
    public ScoreManager scoreManager;
    public float fallHeight = -5f;
    private bool gameOver = false;
    void Update()
    {
        if (gameOver)
            return;
        if (doofus.transform.position.y < fallHeight)
        {
            GameOver();
        }
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
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}