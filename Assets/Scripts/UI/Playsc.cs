using MaskTransitions;
using UnityEngine;
using TMPro;

public class Playsc : MonoBehaviour
{
    [SerializeField] GameObject pogoStick;

    [SerializeField] GameObject playUI;

    [SerializeField] GameObject timer;

    [SerializeField] TextMeshProUGUI recordTimer;

    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] private AudioClip clickSound; 
    [SerializeField] private AudioSource audioSource; 

    private string currentLevelName;

    void Start()
    {
        currentLevelName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        LoadRecordTime();
        pauseMenu.enabled = false;
    }

    public void Play()
    {
        pogoStick.SetActive(true);
        timer.SetActive(true);
        playUI.SetActive(false);
        pauseMenu.enabled = true;
         if (clickSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(clickSound);
            }

    }

    void LoadRecordTime()
    {
        string recordKey = currentLevelName + "_RecordTime";

        // Check if a record exists for the current level
        if (PlayerPrefs.HasKey(recordKey))
        {
            float recordTime = PlayerPrefs.GetFloat(recordKey);
            recordTimer.text = FormatTime(recordTime);
        }
        else
        {
            recordTimer.text = "No Record Time";
        }
    }

    string FormatTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        int milliseconds = (int)((time * 1000) % 1000);
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
