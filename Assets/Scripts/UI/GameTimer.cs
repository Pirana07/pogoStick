using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameTimerText; // Timer in the game HUD
    [SerializeField] private TextMeshProUGUI pauseMenuTimerText; // Timer in the pause menu

    private float startTime;
    private float elapsedTime;
    private bool isRunning = true;

    void Start()
    {
        ResetTimer();
        pauseMenuTimerText.gameObject.SetActive(false); // Initially hide the pause menu timer
    }

    void Update()
    {
        if (isRunning)
        {
            // Calculate the elapsed time
            elapsedTime = Time.time - startTime;
            UpdateTimerDisplay(elapsedTime);
        }
    }

    void UpdateTimerDisplay(float elapsedTime)
    {
        int minutes = (int)(elapsedTime / 60);
        int seconds = (int)(elapsedTime % 60);
        int milliseconds = (int)((elapsedTime * 1000) % 1000);

        string timeFormatted = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        
        // Update both timer texts
        gameTimerText.text = timeFormatted;
        pauseMenuTimerText.text = timeFormatted;
    }

    public void StopTimer()
    {
        isRunning = false;
        // Store the elapsed time when stopping
        elapsedTime = Time.time - startTime;
    }

    public void ResetTimer()
    {
        startTime = Time.time; // Reset start time to now
        elapsedTime = 0f; // Reset elapsed time
        isRunning = true; // Ensure timer is running
    }

    public void TransferTimerToPauseMenu()
    {
        gameTimerText.gameObject.SetActive(false); // Hide the game HUD timer
        pauseMenuTimerText.gameObject.SetActive(true); // Show the pause menu timer
    }

    public void TransferTimerToGame()
    {
        gameTimerText.gameObject.SetActive(true); // Show the game HUD timer
        pauseMenuTimerText.gameObject.SetActive(false); // Hide the pause menu timer
    }

    public float GetElapsedTime()
    {
        // If the timer is running, return the current elapsed time
        return isRunning ? Time.time - startTime : elapsedTime;
    }
}
