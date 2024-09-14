using UnityEngine;
using TMPro;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject finishMenuUI;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    [SerializeField] private TextMeshProUGUI respawnCountText;
    [SerializeField] private TextMeshProUGUI boxAwardedText;
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private GameObject player;
    [SerializeField] private BoxManager boxManager;

    private int respawnCount = 0;
    private BoxManager.BoxType awardedBox;
    private string currentLevelName;

    void Start()
    {
        finishMenuUI.SetActive(false);
        currentLevelName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            CompleteLevel();
        }
    }

    public void IncrementRespawnCount()
    {
        respawnCount++;
    }

    void CompleteLevel()
    {
        gameTimer.StopTimer();

        finalTimeText.text = FormatTime(gameTimer.GetElapsedTime());
        respawnCountText.text = "Respawns: " + respawnCount.ToString();

        AwardKey();
        DisablePlayer();

        UpdateUI();
        finishMenuUI.SetActive(true);
    }

    void AwardKey()
    {
        if (respawnCount >= 5)
        {
            // No box awarded due to high respawn count
            awardedBox = BoxManager.BoxType.Rare; // Placeholder
            boxAwardedText.text = "You don't get a box because you had 5 respawns.";
            boxAwardedText.color = Color.red; // Indicate warning/error
        }
        else
        {
            awardedBox = boxManager.GetRandomBoxType(currentLevelName);
            // Debug.Log($"Awarded a {awardedBox} box at level {currentLevelName}!");
            boxManager.SaveAwardedBox(awardedBox);
        }
    }

    void UpdateUI()
    {
        if (boxAwardedText != null)
        {
            if (respawnCount >= 5)
            {
                boxAwardedText.text = "You don't get a box because you had 5 respawns.";
                boxAwardedText.color = Color.red; // Indicate warning/error
            }
            else
            {
                boxAwardedText.text = "Box Awarded: " + awardedBox.ToString();

                switch (awardedBox)
                {
                    case BoxManager.BoxType.Rare:
                        boxAwardedText.color = Color.green;
                        break;
                    case BoxManager.BoxType.Legendary:
                        boxAwardedText.color = Color.yellow;
                        break;
                    case BoxManager.BoxType.Mythic:
                        boxAwardedText.color = new Color(0.5f, 0f, 0.5f); // Purple
                        break;
                    case BoxManager.BoxType.Youtuber:
                        boxAwardedText.color = Color.red;
                        break;
                }
            }
        }
    }

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
