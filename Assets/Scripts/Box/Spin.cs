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
    [SerializeField] private SkinManager skinManager; // Reference to SkinManager

    private void Start()
    {
        uiSpinButton.onClick.AddListener(OnSpinButtonClicked);
        UpdateSpinButtonState(); // Initialize button state on start
    }

    private void OnSpinButtonClicked()
    {
        // Check if there are any Rare boxes left to allow spinning
        if (!CanSpin())
        {
            Debug.Log("No rare boxes available. Cannot spin the wheel.");
            UpdateSpinButton("No Rare Boxes", false);
            return;
        }

        // Disable the spin button while spinning
        UpdateSpinButton("Spinning", false);

        // Start spinning the wheel
        pickerWheel.OnSpinEnd(wheelPiece =>
        {
            Debug.Log(
                $"<b>Index:</b> {wheelPiece.Index}   <b>Label:</b> {wheelPiece.Label}\n" +
                $"<b>Amount:</b> {wheelPiece.Amount}  <b>Chance:</b> {wheelPiece.Chance}%"
            );

            // Determine the BoxType from the wheel's result
            BoxManager.BoxType awardedBox = (BoxManager.BoxType)wheelPiece.Index;

            // Remove one Rare box and save the awarded one
            boxManager.RemoveBox(BoxManager.BoxType.Rare);

            // Award a random skin based on the box type
            AwardRandomSkin(awardedBox);

            // Update the awarded boxes UI and spin button state
            boxDisplay.UpdateAwardedBoxesUI();
            UpdateSpinButtonState();
        });

        pickerWheel.Spin();
    }

    private bool CanSpin()
    {
        // Check if there are any Rare boxes available to spin
        string awardedBoxes = boxManager.GetAwardedBoxes();
        return !awardedBoxes.Contains("Rare - 0");
    }

    private void AwardRandomSkin(BoxManager.BoxType awardedBox)
    {
        // Check the SkinManager mapping for skins related to this box type
        foreach (var skin in skinManager.GetUnlockedSkins())
        {
            if (skinManager.skinBoxTypeMapping[skin] == awardedBox)
            {
                skinManager.UnlockSkin(skin);
                Debug.Log($"Unlocked new skin: {skin}");
                return;
            }
        }
    }

    private void SaveAwardedBox(BoxManager.BoxType boxType)
    {
        // Save the awarded box in BoxManager to track unlocked skins
        boxManager.SaveAwardedBox(boxType);
    }

    private void UpdateSpinButtonState()
    {
        // Update the button based on whether the player has Rare boxes
        if (CanSpin())
        {
            UpdateSpinButton("Spin", true);
        }
        else
        {
            UpdateSpinButton("No Rare Boxes", false);
        }
    }

    private void UpdateSpinButton(string text, bool interactable)
    {
        // Set button text and interactivity
        uiSpinButtonText.text = text;
        uiSpinButton.interactable = interactable;
    }
}
