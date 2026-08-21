using UnityEngine;
using TMPro;
using System.Collections;

public class UnlockNotification : MonoBehaviour
{
    public TextMeshProUGUI notificationText;

    [Header("Animation")]
    public float fadeInTime = 0.35f;
    public float displayTime = 1.5f;
    public float fadeOutTime = 0.5f;

    private CanvasGroup canvasGroup;
    private Coroutine currentAnimation;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f;
    }

    public void ShowJumpUnlocked()
    {
        ShowMessage("JUMP UNLOCKED!");
    }

    public void ShowDoubleJumpUnlocked()
    {
        ShowMessage("DOUBLE JUMP UNLOCKED!");
    }

    void ShowMessage(string message)
    {
        if (notificationText == null)
            return;

        // Enable notification
        gameObject.SetActive(true);

        // Reset alpha
        canvasGroup.alpha = 0f;

        // Set message
        notificationText.text = message;

        // Stop previous animation if there is one
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }

        currentAnimation = StartCoroutine(PlayNotification());
    }

    IEnumerator PlayNotification()
    {
        // =========================
        // FADE IN
        // =========================

        float elapsed = 0f;

        while (elapsed < fadeInTime)
        {
            elapsed += Time.deltaTime;

            canvasGroup.alpha =
                Mathf.Clamp01(elapsed / fadeInTime);

            yield return null;
        }

        canvasGroup.alpha = 1f;

        // =========================
        // DISPLAY
        // =========================

        yield return new WaitForSeconds(displayTime);

        // =========================
        // FADE OUT
        // =========================

        elapsed = 0f;

        while (elapsed < fadeOutTime)
        {
            elapsed += Time.deltaTime;

            canvasGroup.alpha =
                1f - Mathf.Clamp01(
                    elapsed / fadeOutTime
                );

            yield return null;
        }

        canvasGroup.alpha = 0f;

        currentAnimation = null;

        // Disable after animation
        gameObject.SetActive(false);
    }
}