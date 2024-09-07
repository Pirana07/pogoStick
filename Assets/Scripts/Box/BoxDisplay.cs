using UnityEngine;
using TMPro;

public class BoxDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI awardedBoxesText;
    private BoxManager boxManager;

    private void Start()
    {
        boxManager = FindObjectOfType<BoxManager>();
        UpdateAwardedBoxesUI();
    }

    public void UpdateAwardedBoxesUI()
    {
        if (boxManager != null)
        {
            string awardedBoxes = boxManager.GetAwardedBoxes();
            awardedBoxesText.text = FormatAwardedBoxes(awardedBoxes);
            Debug.Log($"Updated Awarded Boxes UI: {awardedBoxesText.text}");
        }
        else
        {
            awardedBoxesText.text = "BoxManager not found.";
        }
    }

    private string FormatAwardedBoxes(string awardedBoxes)
    {
        string formattedBoxes = awardedBoxes
            .Replace("Legendary", "<color=yellow>Leg.</color>")
            .Replace("Mythic", "<color=purple>Myth.</color>")
            .Replace("Youtuber", "<color=red>Yter</color>")
            .Replace("Rare", "<color=green>Rare</color>");
        
        if (string.IsNullOrWhiteSpace(formattedBoxes))
        {
            formattedBoxes = "No boxes awarded.";
        }
        else
        {
            formattedBoxes = "Awarded Boxes:\n" + formattedBoxes;
        }

        return formattedBoxes;
    }
}
