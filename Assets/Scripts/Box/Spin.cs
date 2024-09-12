using UnityEngine;
using UnityEngine.UI;
using EasyUI.PickerWheelUI;
using TMPro;

public class Spin : MonoBehaviour
{
    [SerializeField] private Button uiSpinButton;
    [SerializeField] private TMP_Text uiSpinButtonText;
    [SerializeField] private PickerWheel pickerWheel;
    [SerializeField] private BoxManager boxManager;
    [SerializeField] private BoxDisplay boxDisplay; // Reference to BoxDisplay

    private void Start()
    {
        uiSpinButton.onClick.AddListener(OnSpinButtonClicked);

        // Initialize button state
        UpdateSpinButtonState();
    }

    private void OnSpinButtonClicked()
    {
        // Retrieve the awarded boxes string
        string awardedBoxes = boxManager.GetAwardedBoxes();

        // Check if "Rare - 0" is in the awarded boxes string
        if (awardedBoxes.Contains("Rare - 0"))
        {
            Debug.Log("No rare boxes available. Cannot spin the wheel.");
            uiSpinButton.interactable = false;
            uiSpinButtonText.text = "No Rare Boxes";
            return;
        }

        uiSpinButton.interactable = false;
        uiSpinButtonText.text = "Spinning";

        pickerWheel.OnSpinEnd(wheelPiece =>
        {
            Debug.Log(
                @" <b>Index:</b> " + wheelPiece.Index + "           <b>Label:</b> " + wheelPiece.Label
                + "\n <b>Amount:</b> " + wheelPiece.Amount + "      <b>Chance:</b> " + wheelPiece.Chance + "%"
            );

            // Example logic to determine BoxType from wheelPiece.Index
            BoxManager.BoxType boxType = (BoxManager.BoxType)wheelPiece.Index;

            // Update box counts and perform the spin
            boxManager.RemoveBox(BoxManager.BoxType.Rare);
            SaveAwardedBox(boxType);

            // Update the display to show the current boxes
            boxDisplay.UpdateAwardedBoxesUI();

            uiSpinButton.interactable = true;
            uiSpinButtonText.text = "Spin";

            // Update button state
            UpdateSpinButtonState();
        });

        pickerWheel.Spin();
    }

    private void SaveAwardedBox(BoxManager.BoxType boxType)
    {
        // Add logic for saving awarded box if needed
        // For example, you might want to keep track of which box was awarded in the game
    }

    private void UpdateSpinButtonState()
    {
        // Retrieve the awarded boxes string
        string awardedBoxes = boxManager.GetAwardedBoxes();

        // Check if "Rare - 0" is in the awarded boxes string
        if (awardedBoxes.Contains("Rare - 0"))
        {
            uiSpinButton.interactable = false;
            uiSpinButtonText.text = "No Rare Boxes";
        }
        else
        {
            uiSpinButton.interactable = true;
            uiSpinButtonText.text = "Spin";
        }
    }
}
