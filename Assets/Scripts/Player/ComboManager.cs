using UnityEngine;
using TMPro;

public class ComboManager : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _comboText;

    public void ShowCombo(int comboCount)
    {
        _comboText.text = "Flip x" + comboCount;
        _comboText.gameObject.SetActive(true);
    }
}
