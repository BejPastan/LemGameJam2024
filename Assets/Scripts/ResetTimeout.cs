using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResetTimeout : MonoBehaviour
{
    public TMP_Text timerText; // Reference to the UI Text object


    [Header("Timer Settings")]
    public float countdownTime = 60f; // Time in seconds

    public static float currentTime{get; private set;}
    private bool isTimerRunning = true;

    void Start()
    {
        // Initialize the timer
        currentTime = countdownTime;
        UpdateTimerText();
    }

    void Update()
    {
        UpdateTimerText();
        // Check if the timer is running
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;

            // Check if time has run out
            if (currentTime <= 0f)
            {
                TimerTimeout();
            }
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = currentTime.ToString("0.##");
        }
    }

// Function to handle timer timeout
    private void TimerTimeout()
    {
        isTimerRunning = false;
        // Reset the timer and simulate scene reload
        currentTime = 0.0f;
        Debug.Log("Time is up!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Function to interrupt the timer (e.g., upon meeting win conditions)
    public void InterruptTimer()
    {
        isTimerRunning = false;
        Debug.Log("Timer interrupted.");
    }

    public void ContinueTimer()
    {
        isTimerRunning = true;
        Debug.Log("Timer starts again.");
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

    private IEnumerator TimerCoroutine()
    {
        yield return null;
    }
}
