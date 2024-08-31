using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI gameTimerText; // Timer in the game HUD
    public TextMeshProUGUI pauseMenuTimerText; // Timer in the pause menu

    private float startTime;
    private bool isRunning = true;

    void Start()
    {
        startTime = Time.time;
        pauseMenuTimerText.gameObject.SetActive(false); // Initially hide the pause menu timer
    }

    void Update()
    {
        if (isRunning)
        {
            float elapsedTime = Time.time - startTime;
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
    }

    public void ResetTimer()
    {
        startTime = Time.time;
        isRunning = true;
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
        if (isRunning)
        {
            return Time.time - startTime;
        }
        else
        {
            return Time.time - startTime; // Return the last elapsed time when stopped
        }
    }
}
