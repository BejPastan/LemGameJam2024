using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResetTimeout : MonoBehaviour
{
    public TMP_Text timerText; // Reference to the UI Text object
    public static float currentTime{get; private set;}
    private Coroutine timerCoroutine;
    private bool isTimerRunning = false;

    [Header("Timer Settings")]
    public float countdownTime = 60f; // Time in seconds

    [Header("Blackout Effect Settings")]
    public CanvasGroup blackoutCanvasGroup; // Reference to a UI CanvasGroup for blackout effect
    public float blackoutDuration = 0.2f; // Blackout duration

    

    void Start()
    {
        // Initialize the timer
        currentTime = countdownTime;
        UpdateTimerText();
        // Ensure the blackout starts transparent
        if (blackoutCanvasGroup != null)
        {
            blackoutCanvasGroup.alpha = 0f;
        }

        // Start the timer coroutine when the scene loads
        Invoke(nameof(StartTimer), 0.1f);
    }

    private IEnumerator TimerCoroutine()
    {
        isTimerRunning = true;
        while (currentTime > 0.0f)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();
            yield return null; // Wait for the next frame
        }

        yield return TimerTimeout();
    }

    // Function to handle timer timeout
    private IEnumerator TimerTimeout()
    {
        isTimerRunning = false; // Ensure the timer is stopped
        currentTime = 0.0f;
        UpdateTimerText();

        // Trigger the blackout effect
        if (blackoutCanvasGroup != null)
        {
            Debug.Log("Timer timeout! Starting blackout...");
            yield return StartCoroutine(BlackoutEffect());
        }

        // Reset the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Coroutine for a smooth blackout effect
    private IEnumerator BlackoutEffect()
    {
        float elapsedTime = 0f;

        while (elapsedTime < blackoutDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / blackoutDuration);
            blackoutCanvasGroup.alpha = alpha;
            yield return null;
        }

        blackoutCanvasGroup.alpha = 1f; // Ensure fully black
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = currentTime.ToString("0.##");
        }
    }

    // Function to start the timer
    public void StartTimer()
    {
        Debug.Log("Starting timer.");
        if (!isTimerRunning)
        {
            timerCoroutine = StartCoroutine(TimerCoroutine());
        }
    }

    // Function to interrupt the timer (e.g., upon meeting win conditions)
    public void StopTimer()
    {
        if (isTimerRunning)
        {
            StopCoroutine(timerCoroutine);
            isTimerRunning = false;
            Debug.Log("Timer interrupted.");
        }
    }

    public void RestartTimer()
    {
        currentTime = countdownTime;
        Debug.Log("Timer restarted.");
    }

    public static void AddTime(float bonusTime = 10f)
    {
        currentTime += bonusTime;
    }
}
