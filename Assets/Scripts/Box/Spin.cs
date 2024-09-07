using System; 
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
        uiSpinButton.onClick.AddListener(() =>
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
                    boxManager.RemoveBox(boxType);

                    // Update the display to show the current boxes
                    boxDisplay.UpdateAwardedBoxesUI();

                    uiSpinButton.interactable = true;
                    uiSpinButtonText.text = "Spin";
                });

                pickerWheel.Spin();
            }
            else
            {
                Debug.Log("No rare boxes available. Cannot spin the wheel.");
            }
        });
    }

    private bool HasRareBox()
    {
        var boxCounts = boxManager.GetAwardedBoxCounts();
        return boxCounts.ContainsKey(BoxManager.BoxType.Rare) && boxCounts[BoxManager.BoxType.Rare] > 0;
    }
}
