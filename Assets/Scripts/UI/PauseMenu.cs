using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]GameObject _UI;
    bool _isPaused = false;
    [SerializeField]  GameTimer gameTimer; // Reference to your timer script

 
    // Update is called once per frame
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

        SceneManager.LoadScene(0);
        
    }
    public void Resume(){
        _UI.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
        gameTimer.TransferTimerToGame();
    }
    public void Pause(){
        _UI.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
        gameTimer.TransferTimerToPauseMenu();

    }
}
