using MaskTransitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]GameObject _UI;
    bool _isPaused = false;
    [SerializeField]  GameTimer gameTimer; 
[SerializeField] private AudioClip clickSound; 
[SerializeField] private AudioClip pauseSound; 

    [SerializeField] private AudioSource audioSource; 
 
    void Update()
    {

     if (Input.GetKeyDown(KeyCode.Escape))
    {
        if (_isPaused)
        {
             Resume();
         }
         else
         {
              Pause();
         }
     }
    }
   
    public void menu(){
        Time.timeScale = 1f;
        TransitionManager.Instance.LoadLevel("MainMenu");
         if (clickSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(clickSound);
            }
    }
    public void Resume(){
        _UI.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
        gameTimer.TransferTimerToGame();
         if (clickSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(clickSound);
            }
    }
    public void Pause(){
        _UI.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
        gameTimer.TransferTimerToPauseMenu();
         if (pauseSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(pauseSound);
            }

    }
}
