using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    public Button resetButton; // Reference to the Reset Button

    void Start()
    {
        // Ensure the button is not null
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(OnResetButtonClicked);
        }
        else
        {
            Debug.LogWarning("Reset button not assigned.");
        }
    }

    void OnResetButtonClicked()
    {
        // Ensure LevelManager exists before calling it
        if (LevelManager.instance != null)
        {
            LevelManager.instance.ResetLevels();
            Debug.Log("Levels have been reset.");

            // Optional: Reload the current scene to reflect changes immediately
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Debug.LogWarning("LevelManager instance not found!");
        }
    }
}
