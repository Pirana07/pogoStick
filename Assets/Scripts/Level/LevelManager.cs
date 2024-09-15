using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int currentLevelIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist through scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CompleteLevel()
    {
        currentLevelIndex++;
        SaveProgress();
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        if (currentLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentLevelIndex);
        }
        else
        {
            Debug.Log("All levels completed!");
        }
    }

    private void SaveProgress()
    {
        PlayerPrefs.SetInt("LevelIndex", currentLevelIndex);
        PlayerPrefs.Save();
    }

    private void LoadProgress()
    {
        currentLevelIndex = PlayerPrefs.GetInt("LevelIndex", 0);
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("LevelIndex");
        currentLevelIndex = 0;
        LoadNextLevel();
    }
}
