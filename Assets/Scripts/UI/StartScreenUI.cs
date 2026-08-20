using UnityEngine;
public class StartScreenUI : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject score;
    void Start()
    {
        Time.timeScale = 0f;
    }
    public void StartGame()
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