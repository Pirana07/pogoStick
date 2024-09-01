using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Stage1"); 
    }
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName); 
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene("OptionsScene"); 
    }

    public void QuitGame()
    {
        Debug.Log("Quit the game");
        Application.Quit(); 
    }
}


