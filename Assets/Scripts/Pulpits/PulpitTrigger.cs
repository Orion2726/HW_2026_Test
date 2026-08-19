using UnityEngine;

public class PulpitTrigger : MonoBehaviour
{
    private bool scored = false;

    private void OnTriggerEnter(Collider other)
    {
        if (scored)
            return;

        if (other.CompareTag("Player"))
        {
            ScoreManager scoreManager =
                FindFirstObjectByType<ScoreManager>();

            if (scoreManager != null)
            {
                scoreManager.AddScore();
        
            }

            scored = true;
        }
    }
}