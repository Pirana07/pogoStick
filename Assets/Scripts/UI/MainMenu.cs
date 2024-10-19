using MaskTransitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        TransitionManager.Instance.LoadLevel("Levels");
    }
    public void LoadLevel(string levelName)
    {
        TransitionManager.Instance.LoadLevel(levelName);

    }

    public void OpenOptions()
    {
        TransitionManager.Instance.LoadLevel("OptionsScene");

    }

    public void QuitGame()
    {
        Debug.Log("Quit the game");
        Application.Quit(); 
    }
}


