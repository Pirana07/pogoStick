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
        UpdateSpinButtonState();
    }

    private void OnSpinButtonClicked()
    {
        if (HasRareBox())
        {
            uiSpinButton.interactable = false;
            uiSpinButtonText.text = "Spinning";

            pickerWheel.OnSpinEnd(wheelPiece =>
            {
                Debug.Log(
                    @" <b>Index:</b> " + wheelPiece.Index + "           <b>Label:</b> " + wheelPiece.Label
                    + "\n <b>Amount:</b> " + wheelPiece.Amount + "      <b>Chance:</b> " + wheelPiece.Chance + "%"
                );

                // Assume wheelPiece.Chance corresponds to BoxType
                BoxManager.BoxType boxType = (BoxManager.BoxType)wheelPiece.Chance;

                // Update box counts and perform the spin
                boxManager.RemoveBox(BoxManager.BoxType.Rare);
                SaveAwardedBox(boxType);

                // Update the display to show the current boxes
                boxDisplay.UpdateAwardedBoxesUI();

                uiSpinButton.interactable = true;
                uiSpinButtonText.text = "Spin";

                // Update the spin button state after the spin
                UpdateSpinButtonState();
            });

            pickerWheel.Spin();
        }
        else
        {
            Debug.Log("No rare boxes available. Cannot spin the wheel.");
            uiSpinButton.interactable = false;
            uiSpinButtonText.text = "No Rare Boxes";
        }
    }

    private bool HasRareBox()
    {
        var boxCounts = boxManager.GetAwardedBoxCounts();
        return boxCounts.ContainsKey(BoxManager.BoxType.Rare) && boxCounts[BoxManager.BoxType.Rare] > 0;
    }

    private void SaveAwardedBox(BoxManager.BoxType boxType)
    {
        // Add logic for saving awarded box if needed
        // For example, you might want to keep track of which box was awarded in the game
    }

    private void UpdateSpinButtonState()
    {
        if (!HasRareBox())
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
