// using UnityEngine;
// using TMPro;

// public class BoxDisplay : MonoBehaviour
// {
//     [SerializeField] private TextMeshProUGUI awardedBoxesText;
//     private BoxManager boxManager;

//     private void Start()
//     {
//         boxManager = FindObjectOfType<BoxManager>();
//         UpdateAwardedBoxesUI();
//     }

//     public void UpdateAwardedBoxesUI()
//     {
//         if (boxManager != null)
//         {
//             // Get the awarded boxes string
//             string awardedBoxes = boxManager.GetAwardedBoxes();
            
//             // Update the UI text with the awarded boxes information
//             awardedBoxesText.text = FormatAwardedBoxes(awardedBoxes);
//             Debug.Log($"Updated Awarded Boxes UI: {awardedBoxesText.text}");
//         }
//         else
//         {
//             awardedBoxesText.text = "BoxManager not found.";
//         }
//     }

//     private string FormatAwardedBoxes(string awardedBoxes)
//     {
//         // Directly return the awarded boxes string with formatting
//         string formattedBoxes = awardedBoxes
//             .Replace("Legendary", "<color=yellow>Leg.</color>")
//             .Replace("Mythic", "<color=purple>Myth.</color>")
//             .Replace("Youtuber", "<color=red>Yter</color>")
//             .Replace("Rare", "<color=green>Rare</color>");
        
//         // Ensure the text shows all box types, even if their count is zero
//         if (string.IsNullOrWhiteSpace(formattedBoxes))
//         {
//             formattedBoxes = "Rare - 0, Legendary - 0, Mythic - 0, Youtuber - 0";
//         }
//         else
//         {
//             formattedBoxes = "Awarded Boxes:\n" + formattedBoxes;
//         }

//         return formattedBoxes;
//     }
// }
