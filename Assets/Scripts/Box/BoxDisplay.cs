using UnityEngine;
using TMPro;

public class BoxDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI awardedBoxesText; // Reference to the UI TextMeshPro element
    private BoxManager boxManager; // Reference to the BoxManager script

    private void Start()
    {
        // Get the BoxManager component from the scene
        boxManager = FindObjectOfType<BoxManager>();

        // Update the UI with the awarded boxes
        UpdateAwardedBoxesUI();
    }

    private void UpdateAwardedBoxesUI()
    {
        if (boxManager != null)
        {
            // Get the awarded boxes from the BoxManager
            string awardedBoxes = boxManager.GetAwardedBoxes();
            
            // Update the UI text element
            awardedBoxesText.text = FormatAwardedBoxes(awardedBoxes);
        }
        else
        {
            awardedBoxesText.text = "BoxManager not found.";
        }
    }

    private string FormatAwardedBoxes(string awardedBoxes)
    {
        // Replace full names with abbreviations and add color
        string formattedBoxes = awardedBoxes
            .Replace("Legendary", "<color=yellow>Leg.</color>")
            .Replace("Mythic", "<color=purple>Myth.</color>")
            .Replace("Youtuber", "<color=red>Yter</color>")
            .Replace("Rare", "<color=green>Rare</color>");

        return "Awarded Boxes:\n" + formattedBoxes;
    }
}
