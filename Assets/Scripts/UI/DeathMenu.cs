using System.Collections;
using MaskTransitions;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathMenu : MonoBehaviour
{
    [SerializeField]
     GameObject deathMenuUI;

    [SerializeField] 
    GameTimer gameTimer; 

    [SerializeField] 
    GameObject player; // Reference to the player GameObject

    [SerializeField]
     TextMeshProUGUI timerText; // Timer text in the death menu

    [SerializeField] 
    ParticleSystem[] bloodParticles; // Blood particles array

    [SerializeField]
     PauseMenu pauseMenu; 

    [SerializeField]
    CircleCollider2D colider;

    [SerializeField]
    SpriteRenderer spriteRenderer;
    //Sounds
    [SerializeField]  AudioClip clickSound; 
    [SerializeField]  AudioClip deathSound; 

    [SerializeField]  AudioSource audioSource; 
     Vector3 lastPosition; // Last position to restore
     FinishLine finishLine; // Reference to the FinishLine script

    void Start()
    {
        deathMenuUI.SetActive(false); // Initially hide the death menu
        finishLine = FindObjectOfType<FinishLine>(); // Find the FinishLine script in the scene
    }

    public void TriggerDeath(Vector3 respawnPosition)
    {
        audioSource.PlayOneShot(deathSound);
        lastPosition = respawnPosition;
        Time.timeScale = 0f; // Pause the game
        gameTimer.StopTimer(); // Stop the timer
        UpdateTimerDisplay(); // Update the timer display
        deathMenuUI.SetActive(true); // Show the death menu 
        pauseMenu.enabled = false;
        finishLine.IncrementRespawnCount(); // Increment respawn count
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f; // Unpause the game
        gameTimer.TransferTimerToGame(); // Show the game HUD timer
        player.transform.position = lastPosition; // Restore the player’s position
        player.transform.rotation = Quaternion.Euler(0, 0, 0); // Reset the player's z-rotation
        deathMenuUI.SetActive(false); // Hide the death menu
        HideBloodParticles(); // Hide the blood particles
        
        // Re-enable the pogo stick controller and reset crash status
        PogoStickController controller = player.GetComponent<PogoStickController>();
        if (controller != null)
        {
            controller.enabled = true;
            CrashDetector crashDetector = player.GetComponent<CrashDetector>();
            if (crashDetector != null)
            {
                crashDetector.ResetCrashStatus(); // Reset the crash status
            }
        }

        gameTimer.ContinuteTimer(); // amis dedac vatire
        colider.enabled = false;
        pauseMenu.enabled = true;
        StartCoroutine(ImmortalSeconds());
         if (clickSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(clickSound);
            }
    }

    public void RestartLevel()
    {
         if (clickSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(clickSound);
            }
        Time.timeScale = 1f; // Unpause the game
        TransitionManager.Instance.LoadLevel(SceneManager.GetActiveScene().name);

    }

    public void ReturnToMainMenu()
    {
         if (clickSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(clickSound);
            }
        Time.timeScale = 1f; // Unpause the game
        TransitionManager.Instance.LoadLevel("MainMenu");
        
    }

    public void QuitGame()
    {
         if (clickSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(clickSound);
            }
        Application.Quit();
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            float elapsedTime = gameTimer.GetElapsedTime();
            int minutes = (int)(elapsedTime / 60);
            int seconds = (int)(elapsedTime % 60);
            int milliseconds = (int)((elapsedTime * 1000) % 1000);

            timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }
    }

    private void HideBloodParticles()
    {
        foreach (var particleSystem in bloodParticles)
        {
            if (particleSystem != null)
            {
                particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }
    }
    private IEnumerator ImmortalSeconds()
    {
        float flickerDuration = 3f; //inspectorshi unda gamomechina
        float flickerInterval = 0.2f; 
        float elapsedTime = 0f;

        while (elapsedTime < flickerDuration)
        {
            SetPlayerAlpha(spriteRenderer.color.a == 1f ? 0.5f : 1f);

            yield return new WaitForSeconds(flickerInterval);
            elapsedTime += flickerInterval;
        }

        SetPlayerAlpha(1f); 
        colider.enabled = true; 
    }
    private void SetPlayerAlpha(float alpha)
    { 
         Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
    }
}
