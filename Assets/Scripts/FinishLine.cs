using UnityEngine;
using TMPro;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject finishMenuUI;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    [SerializeField] private TextMeshProUGUI respawnCountText;
    [SerializeField] private TextMeshProUGUI boxAwardedText; // New text field for showing awarded box
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private GameObject player;
    [SerializeField] private BoxManager boxManager; // Reference to BoxManager

    private int respawnCount = 0;
    private BoxManager.BoxType awardedBox;

    void Start()
    {
        finishMenuUI.SetActive(false); 
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

        AwardKey(); // Award a random box key
        DisablePlayer();

        UpdateUI(); // Update the UI to show the awarded box
        finishMenuUI.SetActive(true);
    }

    void AwardKey()
    {
        awardedBox = boxManager.GetRandomBoxType();
        Debug.Log($"Awarded a {awardedBox} box!");
        // Add logic to grant the player the box here
    }

    void UpdateUI()
    {
        if (boxAwardedText != null)
        {
            boxAwardedText.text = "Box Awarded: " + awardedBox.ToString();

            // Change text color based on box type
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
