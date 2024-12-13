using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorial : MonoBehaviour
{
    [Header("Canvas Settings")]
    public Canvas canvas; // Reference to the Canvas to manage
    public float disableDelay = 4f; // Time in seconds before disabling the canvas

    private CanvasGroup canvasGroup;

    void Start()
    {
        if (canvas == null)
        {
            Debug.LogError("Canvas is not assigned in the inspector.");
            return;
        }

        // Add or get a CanvasGroup component to manage visibility
        canvasGroup = canvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = canvas.gameObject.AddComponent<CanvasGroup>();
        }

        // Start with the canvas visible and enabled
        canvasGroup.alpha = 1f;
        canvas.gameObject.SetActive(true);

        // Trigger the disable sequence
        StartCoroutine(DisableCanvasAfterDelay());
    }

    private IEnumerator DisableCanvasAfterDelay()
    {
        // Wait for the delay duration
        yield return new WaitForSeconds(disableDelay);

        // Fade out the canvas for a smoother effect (optional)
        float fadeDuration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = 1f - (elapsedTime / fadeDuration);
            yield return null;
        }

        // Ensure canvas is fully invisible
        canvasGroup.alpha = 0f;

        // Disable the canvas
        canvas.gameObject.SetActive(false);
    }
}
