using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CompleteLevel(int currentLevelIndex)
    {
        // Get the index of the next level to unlock
        int nextLevelIndex = currentLevelIndex + 1;

        // Check if the next level is already unlocked
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Only unlock the next level if it's the next one in sequence
        if (nextLevelIndex > unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", nextLevelIndex);
            PlayerPrefs.Save();
        }
    }

    public bool IsLevelUnlocked(int levelIndex)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        return levelIndex <= unlockedLevel;
    }

    public void ResetLevels()
    {
        // Reset the unlocked level to level 1
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        PlayerPrefs.Save();
    }
}
