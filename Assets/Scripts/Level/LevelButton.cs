using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int levelIndex;
    public Button button;

    void Start()
    {
        if (LevelManager.instance.IsLevelUnlocked(levelIndex))
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
