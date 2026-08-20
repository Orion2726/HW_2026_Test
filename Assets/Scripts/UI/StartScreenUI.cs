using UnityEngine;

public class StartScreenUI : MonoBehaviour
{
    public GameObject startPanel;

    void Start()
    {
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        startPanel.SetActive(false);

        Time.timeScale = 1f;
    }
}