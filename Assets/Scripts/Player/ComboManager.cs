using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ComboManager : MonoBehaviour
{
    
 [SerializeField] 
 	TextMeshProUGUI _comboText;
    
 [SerializeField] 
	 Volume _globalVolume;
    
 	Bloom _bloom;
 	ChromaticAberration _chromaticAberration;
 	ColorAdjustments _colorAdjustments;

    void Start()
    {
        if (_globalVolume.profile.TryGet(out _bloom) &&
            _globalVolume.profile.TryGet(out _chromaticAberration) &&
            _globalVolume.profile.TryGet(out _colorAdjustments))
        {
            ResetEffects();
        }
    }

    public void ShowCombo(int comboCount)
    {
        if (comboCount < 2) return;

        _comboText.text = "Flip x" + comboCount + "!";
        _comboText.gameObject.SetActive(true);

        AdjustScreenEffects(comboCount);

        CancelInvoke(nameof(HideComboText));
        Invoke(nameof(HideComboText), 1.5f);
    }

    private void AdjustScreenEffects(int comboCount)
    {
        _bloom.intensity.value = Mathf.Clamp(1f + (comboCount * 0.3f), 1f, 5f);
        _chromaticAberration.intensity.value = Mathf.Clamp(comboCount * 0.1f, 0f, 1f);
        _colorAdjustments.contrast.value = Mathf.Clamp(comboCount * 5f, 0f, 50f);
    }

    public void ResetEffects()
    {
        _bloom.intensity.value = 1f;
        _chromaticAberration.intensity.value = 0f;
        _colorAdjustments.contrast.value = 0f;
    }

    private void HideComboText()
    {
        _comboText.gameObject.SetActive(false);
    }
}
