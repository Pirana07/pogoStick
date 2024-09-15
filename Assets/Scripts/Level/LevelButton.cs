using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int levelIndex; 
    public Button button; 
    public GameObject lockedIndicator; 

    void Start()
    {
        int unlockedLevelIndex = PlayerPrefs.GetInt("LevelIndex", 0);

        Debug.Log($"LevelIndex: {levelIndex}, UnlockedLevelIndex: {unlockedLevelIndex}");

        if (levelIndex <= unlockedLevelIndex)
        {
            button.interactable = true;
            if (lockedIndicator != null)
            {
                lockedIndicator.SetActive(false); 
            }
        }
        else
        {
            button.interactable = false;
            if (lockedIndicator != null)
            {
                lockedIndicator.SetActive(true); 
            }
        }
    }

    public void LoadLevel()
    {
        if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            Debug.LogError("Invalid level index: " + levelIndex);
        }
    }
}
