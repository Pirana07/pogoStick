using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject finishMenuUI;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    [SerializeField] private TextMeshProUGUI respawnCountText;
    [SerializeField] private TextMeshProUGUI boxAwardedText;
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private GameObject player;
    // [SerializeField] private BoxManager boxManager;
     [SerializeField] private BoxManager boxManager;
    [SerializeField] private BoxDisplay boxDisplay;


    private int respawnCount = 0;
    // private BoxManager.BoxType awardedBox;
    private string currentLevelName;

    void Start()
    {
        finishMenuUI.SetActive(false);
        currentLevelName = SceneManager.GetActiveScene().name;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            CompleteLevel();
            LevelManager.instance?.CompleteLevel(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void IncrementRespawnCount()
    {
        respawnCount++;
    }

    void CompleteLevel()
    {
        gameTimer.StopTimer();
        float elapsedTime = gameTimer.GetElapsedTime();
        finalTimeText.text = FormatTime(elapsedTime);
        respawnCountText.text = "Respawns: " + respawnCount.ToString();
        SaveRecordTime(elapsedTime);
        // AwardKey();
        DisablePlayer();
        // UpdateUI();
        finishMenuUI.SetActive(true);
        boxManager.CollectKey();
                boxDisplay.UpdateAwardedKeysUI();

    }

    float currentRecordTimePB;

    void SaveRecordTime(float elapsedTime)
    {
        string recordKey = currentLevelName + "_RecordTime";
        float currentRecordTime = PlayerPrefs.GetFloat(recordKey, float.MaxValue);

        if (elapsedTime < currentRecordTime)
        {
            PlayerPrefs.SetFloat(recordKey, elapsedTime);
            PlayerPrefs.Save();
            Debug.Log("New record saved: " + FormatTime(elapsedTime));
        }
    }

    public void RestartRecord()
    {
        string recordKey = currentLevelName + "_RecordTime";
        PlayerPrefs.DeleteKey(recordKey);
        PlayerPrefs.Save();
        Debug.Log("Record time reset for: " + currentLevelName);
    }

    // void AwardKey()
    // {
    //     if (respawnCount >= 5)
    //     {
    //         awardedBox = BoxManager.BoxType.Rare; // Placeholder
    //         boxAwardedText.text = "You don't get a box because you had 5 respawns.";
    //         boxAwardedText.color = Color.red; // Indicate warning/error
    //     }
    //     else
    //     {
    //         awardedBox = boxManager.GetRandomBoxType(currentLevelName);
    //         boxManager.SaveAwardedBox(awardedBox);
    //     }
    // }

    // void UpdateUI()
    // {
    //     boxAwardedText.text = respawnCount >= 5 ? 
    //         "You don't get a box because you had 5 respawns." : 
    //         "Box Awarded: " + awardedBox.ToString();

    //     boxAwardedText.color = respawnCount >= 5 ? Color.red : GetBoxColor(awardedBox);
    // }

    // Color GetBoxColor(BoxManager.BoxType boxType)
    // {
    //     return boxType switch
    //     {
    //         BoxManager.BoxType.Rare => Color.green,
    //         BoxManager.BoxType.Legendary => Color.yellow,
    //         BoxManager.BoxType.Mythic => new Color(0.5f, 0f, 0.5f), // Purple
    //         BoxManager.BoxType.Youtuber => Color.red,
    //         _ => Color.white,
    //     };
    // }

    void DisablePlayer()
    {
        player.SetActive(false);
    }

    string FormatTime(float elapsedTime)
    {
        int minutes = (int)(elapsedTime / 60);
        int seconds = (int)(elapsedTime % 60);
        int milliseconds = (int)((elapsedTime * 1000) % 1000);
        return string.Format("Score: {0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
