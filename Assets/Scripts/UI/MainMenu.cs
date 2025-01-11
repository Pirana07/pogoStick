using MaskTransitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]  AudioClip clickSound; 
    [SerializeField]  AudioSource audioSource; 
    public void StartGame()
    {
     if (clickSound != null && audioSource != null)
         {
             audioSource.PlayOneShot(clickSound);
         }
        TransitionManager.Instance.LoadLevel("Stage2");
    }
    public void LoadLevel(string levelName)
    {
    if (clickSound != null && audioSource != null)
        {
             audioSource.PlayOneShot(clickSound);
        }
        TransitionManager.Instance.LoadLevel(levelName);

    }
 
    public void OpenOptions()
    {
     if (clickSound != null && audioSource != null)
        {
             audioSource.PlayOneShot(clickSound);
        }
        TransitionManager.Instance.LoadLevel("OptionsScene");

    }

    public void QuitGame()
    {
     if (clickSound != null && audioSource != null)
        {
             audioSource.PlayOneShot(clickSound);
         }
        Debug.Log("Quit the game");
        Application.Quit(); 
    }
}


